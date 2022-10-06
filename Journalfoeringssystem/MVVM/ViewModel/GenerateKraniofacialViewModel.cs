using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Journalfoeringssystem.Annotations;
using Journalfoeringssystem.Core;
using Journalfoeringssystem.Domain;
using Journalfoeringssystem.MVVM.Model;
using Journalfoeringssystem.MVVM.View;
using MessageBox = System.Windows.MessageBox;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   public class GenerateKraniofacialViewModel: ObservableObject
   {
      //Commands, der anvendes ved tryk på knapper i GUI
      public RelayCommand SearchCommand { get; set; }
      public RelayCommand AddCommand { get; set; }
      public RelayCommand RemoveCommand { get; set; }
      public RelayCommand EditCommand { get; set; }
      public RelayCommand LoadImages { get; set; }
      public RelayCommand GeneratePDFCommand { get; set; }
      public RelayCommand FindDirectory { get; set; }

      //Klasser der anvendes til at holde styr på ansatte, der har arbejdet på case
      public Worker WorkerInput { get; set; }
      public Worker SelectedWorker { get; set; }
      public Workers WorkersInput { get; set; }

      //Anvendes til at indlæse korrekte informationer omkring stier og filer
      public FileReader FileReader { get; set; }

      //Anvendes til at generere wordfil ud fra template med korrekte informationer
      public PDFGenerator PdfGenerator { get; set; }

      //Det valgte drev, der søges på
      public string DriveForSearch { get; set; }

      //DTO, der indeholder alle nødvendige informationer for at kunne udfylde template
      public InformationContainer InformationContainer { get; set; }

      //Liste med filer for billeder, der indlæses
      private List<FileUpload> _filesForUpload;

      public List<FileUpload> FilesForUpload
      {
         get
         {
            return _filesForUpload;
         }

         set
         {
            _filesForUpload = value;
            OnPropertyChanged(nameof(FilesForUpload));
         }
      }

      //Søgte CPR-nummer
      private string _searchNumber;

      public string SearchNumber
      {
         get
         {
            return _searchNumber;
         }
         set
         {
            _searchNumber = value;
            OnPropertyChanged(nameof(SearchNumber));
         }
      }

      //Valgte CPR-nummer
      private string _CPRnumber;

      public string CPRNumber
      {
         get
         {
            return _CPRnumber;
         }
         set
         {
            _CPRnumber = value;
            OnPropertyChanged(nameof(CPRNumber));
            InformationContainer.CPRNumber = CPRNumber;
         }
      }

      //Patient navn
      private string _patientName;

      public string PatientName
      {
         get
         {
            return _patientName;
         }
         set
         {
            _patientName = value;
            OnPropertyChanged(nameof(PatientName));
            InformationContainer.PatientName = PatientName;
         }
      }

      //Søgestreng
      private string _searchPath;

      public string SearchPath
      {
         get
         {
            return _searchPath;
         }

         set
         {
            _searchPath = value;
            OnPropertyChanged(nameof(SearchPath));
            InformationContainer.SearchPath = SearchPath;
         }
      }

      //Dato for planlægning
      private DateTime _dateForPlanning;

      public DateTime DateForPlanning
      {
         get
         {
            return _dateForPlanning;
         }

         set
         {
            _dateForPlanning = value;
            OnPropertyChanged(nameof(DateForPlanning));
            InformationContainer.DateForPlanning = DateForPlanning;
         }
      }

      //Dato for operation
      private DateTime _dateForSurgery;

      public DateTime DateForSurgery
      {
         get
         {
            return _dateForSurgery;
         }

         set
         {
            _dateForSurgery = value;
            OnPropertyChanged(nameof(DateForSurgery));
            InformationContainer.DateForSurgery = DateForSurgery;
         }
      }

      //Dato for scanning
      private DateTime _dateForScanning;

      public DateTime DateForScanning
      {
         get
         {
            return _dateForScanning;
         }

         set
         {
            _dateForScanning = value;
            OnPropertyChanged(nameof(DateForScanning));
            InformationContainer.DateForScanning1 = DateForScanning;
         }
      }

      //Type af scanning
      private string _typeOfScanning;

      public string TypeOfScanning
      {
         get
         {
            return _typeOfScanning;
         }

         set
         {
            _typeOfScanning = value;
            OnPropertyChanged(nameof(TypeOfScanning));
            InformationContainer.TypeOfScanning1 = TypeOfScanning;
         }
      }

      //Serie af scanning
      private string _serieOfScanning;

      public string SerieOfScanning
      {
         get
         {
            return _serieOfScanning;
         }

         set
         {
            _serieOfScanning = value;
            OnPropertyChanged(nameof(SerieOfScanning));
            InformationContainer.SerieOfScanning1 = SerieOfScanning;
         }
      }

      //Beskrivelse for skæreguide
      private string _cuttingGuide;

      public string CuttingGuide
      {
         get
         {
            return _cuttingGuide;
         }

         set
         {
            _cuttingGuide = value;
            OnPropertyChanged(nameof(CuttingGuide));
            InformationContainer.CuttingGuide = CuttingGuide;
         }
      }

      //Kommentarer
      private string _remarks;

      public string Remarks
      {
         get
         {
            return _remarks;
         }

         set
         {
            _remarks = value;
            OnPropertyChanged(nameof(Remarks));
            InformationContainer.Remarks = Remarks;
         }
      }

      //Bruges til at styre tekst inde i knap
      private string _buttonText;

      public string ButtonText
      {
         get
         {
            return _buttonText;
         }

         set
         {
            _buttonText = value;
            OnPropertyChanged(nameof(ButtonText));
         }
      }

      //Bruges til at styre synlighed af loading ikon i GUI
      private Visibility _loading;

      public Visibility Loading
      {
         get
         {
            return _loading;
         }

         set
         {
            _loading = value;
            OnPropertyChanged(nameof(Loading));
         }
      }

      //Bruges til at styre om knap kan trykkes på
      private bool _buttonEnabled;

      public bool ButtonEnabled
      {
         get
         {
            return _buttonEnabled;
         }

         set
         {
            _buttonEnabled = value;
            OnPropertyChanged(nameof(ButtonEnabled));
         }
      }

      //Bruges til at illustrere om patienten ikke kunne fremsøges
      private Visibility _patientNotFound;

      public Visibility PatientNotFound
      {
         get
         {
            return _patientNotFound;
         }

         set
         {
            _patientNotFound = value;
            OnPropertyChanged(nameof(PatientNotFound));
         }
      }

      //Styre synlighed af loading ved søgning
      private Visibility _loadingSearch;

      public Visibility LoadingSearch
      {
         get
         {
            return _loadingSearch;
         }

         set
         {
            _loadingSearch = value;
            OnPropertyChanged(nameof(LoadingSearch));
         }
      }

      //Styre om knappen kan trykkes på ved søgning
      private bool _searchButtonEnabled;

      public bool SearchButtonEnabled
      {
         get
         {
            return _searchButtonEnabled;
         }

         set
         {
            _searchButtonEnabled = value;
            OnPropertyChanged(nameof(SearchButtonEnabled));
         }
      }

      //Søgeknap tekst
      private string _searchButtonText;

      public string SearchButtonText
      {
         get
         {
            return _searchButtonText;
         }

         set
         {
            _searchButtonText = value;
            OnPropertyChanged(nameof(SearchButtonText));
         }
      }

      public GenerateKraniofacialViewModel()
      {
         InformationContainer = new InformationContainer();
         InformationContainer.Protocol = "Kraniofacial";
         Loading = Visibility.Hidden;
         LoadingSearch = Visibility.Hidden;
         PatientNotFound = Visibility.Hidden;
         ButtonEnabled = true;
         SearchButtonEnabled = true;
         WorkerInput = new Worker();
         WorkersInput = new Workers();
         FileReader = new FileReader();
         PdfGenerator = new PDFGenerator();

         //Tilføjelse af personer på case
         AddCommand = new RelayCommand(o =>
         {
            if (WorkerInput.WorkerName != null && WorkerInput.WorkerJob != null && WorkerInput.WorkerName != "" && WorkerInput.WorkerJob != "")
            {
               WorkersInput.AddWorker(new Worker() { WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });
            }

            InformationContainer.WorkersInput = WorkersInput;

         });

         //Fjerne personer på case
         RemoveCommand = new RelayCommand(o =>
         {
            WorkersInput.RemoveWorker(SelectedWorker);

            InformationContainer.WorkersInput = WorkersInput;

         });

         //Ændre personer på case
         EditCommand = new RelayCommand(o =>
         {
            WorkersInput.EditWorker(SelectedWorker, new Worker() { WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });

            InformationContainer.WorkersInput = WorkersInput;

         });

         //Søge
         SearchCommand = new RelayCommand(o =>
         {
            //Visuel loading
            Thread thread1 = new Thread(StartSearchLoading);
            thread1.Start();

            //Faktiske søgning
            Thread thread2 = new Thread(StartSearching);
            thread2.Start();

         });

         //Loading af billeder
         LoadImages = new RelayCommand(o =>
         {
            try
            {
               FilesForUpload = FileReader.LoadPictures(SearchPath);
            }
            catch (Exception e)
            {
               MessageBox.Show("Error: No picturesfolder found...\r\n\r\nException: " + e.ToString());
            }
         });

         //Genererign af wordfil
         GeneratePDFCommand = new RelayCommand(o =>
         {
            if (!string.IsNullOrEmpty(SearchPath) && !string.IsNullOrEmpty(CPRNumber))
            {
               Thread thread1 = new Thread(StartLoading);
               thread1.Start();

               Thread thread2 = new Thread(StartGenerating);
               thread2.Start();
            }
         });

         //Fremsøgning af drev, der skal søges i eller den faktiske patient
         FindDirectory = new RelayCommand(o =>
         {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            DriveForSearch = dialog.SelectedPath;
            SearchPath = DriveForSearch;
            StartSearchingFromFolderChoice();
         });
      }

      public void StartLoading()
      {
         Loading = Visibility.Visible;
         ButtonText = "";
         ButtonEnabled = false;

      }

      public void StartGenerating()
      {
         PdfGenerator.GeneratePDF(InformationContainer);
         Loading = Visibility.Hidden;
         ButtonText = "Generate PDF";
         ButtonEnabled = true;
      }

      public void StartSearchLoading()
      {
         LoadingSearch = Visibility.Visible;
         SearchButtonText = "";
         SearchButtonEnabled = false;
      }

      //Søgefunktion ud fra indskrevet CPR-nummer
      public void StartSearching()
      {
         if (!string.IsNullOrEmpty(SearchNumber) && !string.IsNullOrEmpty(DriveForSearch))
         {
            string[] path = FileReader.SearchForFiles(SearchNumber, DriveForSearch);

            if (path != null)
            {
               SearchPath = path[0];
               PatientName = path[1];
               CPRNumber = SearchNumber;
               List<ScanningInformationContainer> scannings = FileReader.SearchForScanning(path[3]);
               
               try
               {
                  TypeOfScanning = scannings[0].TypeOfScanning;
                  SerieOfScanning = scannings[0].SerieOfScanning;
               }
               catch (Exception e)
               {
                  MessageBox.Show("No scannings were found\r\n" + e);
               }

               PatientNotFound = Visibility.Hidden;
            }

            else
            {
               PatientNotFound = Visibility.Visible;
            }
         }

         else
         {

         }

         LoadingSearch = Visibility.Hidden;
         SearchButtonText = "Search";
         SearchButtonEnabled = true;
      }

      //Søgefunktion ud fra valg af patientmappe
      public void StartSearchingFromFolderChoice()
      {
         string[] path = FileReader.SearchForFiles(DriveForSearch);
         List<ScanningInformationContainer> scannings = FileReader.SearchForScanning(DriveForSearch);

         SearchPath = path[0];
         PatientName = path[1];
         CPRNumber = path[2];
         
         try
         {
            TypeOfScanning = scannings[0].TypeOfScanning;
            SerieOfScanning = scannings[0].SerieOfScanning;
            DateForScanning = scannings[0].DateOfScanning;
         }
         catch (Exception e)
         {
            MessageBox.Show("No scannings were found\r\n" + e);
         }

      }
   }
}
