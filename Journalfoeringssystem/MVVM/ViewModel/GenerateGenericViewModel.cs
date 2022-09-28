using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Journalfoeringssystem.Core;
using Journalfoeringssystem.Domain;
using Journalfoeringssystem.MVVM.Model;
using Journalfoeringssystem.MVVM.View;
using Microsoft.Office.Interop.Word;
using MessageBox = System.Windows.MessageBox;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   class GenerateGenericViewModel: ObservableObject
   {
      //Commands, der er binded til forskellige knapper i GUI
      public RelayCommand SearchCommand { get; set; }
      public RelayCommand AddCommand { get; set; }
      public RelayCommand RemoveCommand { get; set; }
      public RelayCommand EditCommand { get; set; }
      public RelayCommand LoadImages { get; set; }
      public RelayCommand GeneratePDFCommand { get; set; }
      public RelayCommand FindDirectory { get; set; }
      public RelayCommand SelectedRadioButton { get; set; }

      //Anvendes til at tilføje personer, der har arbejdet på casen
      public Worker WorkerInput { get; set; }
      public Worker SelectedWorker { get; set; }
      public Workers WorkersInput { get; set; }

      //Anvendes til at indlæse korrekte informationer vedrørende stien med patienten, navn, cpr, scanninger osv.
      public FileReader FileReader { get; set; }

      //Anvendes til at generere ny wordfil med indlæste billeder og information
      public PDFGenerator PdfGenerator { get; set; }

      //Property for det valgte drev at søge efter billeder i
      public string DriveForSearch { get; set; }

      //DTO, der indeholder alle informationer, der er nødvendige for at kunne udfylde template
      public InformationContainer InformationContainer { get; set; }

      //Liste med billeder, der uploades
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

      //CPR nummeret, der søges på
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

      //Indlæste CPR nummer
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

      //Søgepath
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

      //Dato for operation 1
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

      //Dato for scanning 1
      private DateTime _dateForScanning1;

      public DateTime DateForScanning1
      {
         get
         {
            return _dateForScanning1;
         }

         set
         {
            _dateForScanning1 = value;
            OnPropertyChanged(nameof(DateForScanning1));
            InformationContainer.DateForScanning1 = DateForScanning1;
         }
      }
      
      //Type af scanning 1
      private string _typeOfScanning1;

      public string TypeOfScanning1
      {
         get
         {
            return _typeOfScanning1;
         }

         set
         {
            _typeOfScanning1 = value;
            OnPropertyChanged(nameof(TypeOfScanning1));
            InformationContainer.TypeOfScanning1 = TypeOfScanning1;
         }
      }

      //Serie af scanning 1
      private string _serieOfScanning1;

      public string SerieOfScanning1
      {
         get
         {
            return _serieOfScanning1;
         }

         set
         {
            _serieOfScanning1 = value;
            OnPropertyChanged(nameof(SerieOfScanning1));
            InformationContainer.SerieOfScanning1 = SerieOfScanning1;
         }
      }

      //Dato for scanning 2
      private DateTime _dateForScanning2;

      public DateTime DateForScanning2
      {
         get
         {
            return _dateForScanning2;
         }

         set
         {
            _dateForScanning2 = value;
            OnPropertyChanged(nameof(DateForScanning2));
            InformationContainer.DateForScanning2 = DateForScanning2;
         }
      }

      //Type af scanning 2
      private string _typeOfScanning2;

      public string TypeOfScanning2
      {
         get
         {
            return _typeOfScanning2;
         }

         set
         {
            _typeOfScanning2 = value;
            OnPropertyChanged(nameof(TypeOfScanning2));
            InformationContainer.TypeOfScanning2 = TypeOfScanning2;
         }
      }

      //Serie af scanning 2
      private string _serieOfScanning2;

      public string SerieOfScanning2
      {
         get
         {
            return _serieOfScanning2;
         }

         set
         {
            _serieOfScanning2 = value;
            OnPropertyChanged(nameof(SerieOfScanning2));
            InformationContainer.SerieOfScanning2 = SerieOfScanning2;
         }
      }

      //Bruges til at ændre udseende for knap, når der trykkes på
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

      //Bruges til at styre synlighed af loading ikon
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

      //Bruges til at styre, hvorvidt knapper kan trykkes på
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

      //Bruges til at informere om, hvorvidt patienten er fundet eller ikke
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

      private string _headline;

      public string Headline
      {
         get
         {
            return _headline;
         }

         set
         {
            _headline = value;
            OnPropertyChanged(nameof(Headline));
            InformationContainer.Headline = Headline;
         }
      }

      private string _guide;

      public string Guide
      {
         get
         {
            return _guide;
         }

         set
         {
            _guide = value;
            OnPropertyChanged(nameof(Guide));
            InformationContainer.Guide = Guide;
         }
      }

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

      private string _protocol;

      public string Protocol
      {
         get
         {
            return _protocol;
         }

         set
         {
            _protocol = value;
            OnPropertyChanged(nameof(Protocol));

            if (Protocol == "Portrait")
            {
               InformationContainer.Protocol = "Generic_portrait";
            }

            else
            {
               InformationContainer.Protocol = "Generic_landscape";
            }
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

      //Styre hvorvidt der kan trykkes på knap
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

      public GenerateGenericViewModel()
      {
         InformationContainer = new InformationContainer();
         Loading = Visibility.Hidden;
         LoadingSearch = Visibility.Hidden;
         PatientNotFound = Visibility.Hidden;
         ButtonEnabled = true;
         SearchButtonEnabled = true;
         WorkerInput = new Worker();
         WorkersInput = new Workers();
         FileReader = new FileReader();
         PdfGenerator = new PDFGenerator();

         //Tilføjer personer der har arbejdet på casen
         AddCommand = new RelayCommand(o =>
         {
            if (WorkerInput.WorkerName != null && WorkerInput.WorkerJob != null && WorkerInput.WorkerName != "" && WorkerInput.WorkerJob != "")
            {
               WorkersInput.AddWorker(new Worker() { WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });
            }

            InformationContainer.WorkersInput = WorkersInput;

         });


         //Fjerner personer, der har arbejdet på casen
         RemoveCommand = new RelayCommand(o =>
         {
            WorkersInput.RemoveWorker(SelectedWorker);

            InformationContainer.WorkersInput = WorkersInput;

         });

         //Ændrer i personer, der har arbejdet på casen
         EditCommand = new RelayCommand(o =>
         {
            WorkersInput.EditWorker(SelectedWorker, new Worker() { WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });

            InformationContainer.WorkersInput = WorkersInput;

         });

         //Anvendes til at søge efter patient
         SearchCommand = new RelayCommand(o =>
         {
            //Styre synlighed af loading
            Thread thread1 = new Thread(StartSearchLoading);
            thread1.Start();

            //Selve søgningen
            Thread thread2 = new Thread(StartSearching);
            thread2.Start();

         });

         //Bruges til at loade billeder
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

         //Anvendes til at vælge mellem højre eller venstre fibula
         SelectedRadioButton = new RelayCommand(o =>
         {
            Protocol = (string)o;
         });

         //Genererer wordfilen ud fra template
         GeneratePDFCommand = new RelayCommand(o =>
         {
            if (!string.IsNullOrEmpty(SearchPath) && !string.IsNullOrEmpty(CPRNumber) && Protocol != null)
            {
               //Starter loading
               Thread thread1 = new Thread(StartLoading);
               thread1.Start();

               //Selve genereringen
               Thread thread2 = new Thread(StartGenerating);
               thread2.Start();
            }

            else
            {
               MessageBox.Show("No document orientation selected...");
            }

         });

         //Søger efter patient ved at vælge mappe
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

      //Søger efter patient og finder korrekt sti for mappe og scanninger
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
                  TypeOfScanning1 = scannings[0].TypeOfScanning;
                  SerieOfScanning1 = scannings[0].SerieOfScanning;
                  TypeOfScanning2 = scannings[1].TypeOfScanning;
                  SerieOfScanning2 = scannings[1].SerieOfScanning;
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

      //Finder korrekt patient, billeder sti og scanninger ud fra valg af mappe
      public void StartSearchingFromFolderChoice()
      {
         string[] path = FileReader.SearchForFiles(DriveForSearch);
         List<ScanningInformationContainer> scannings = FileReader.SearchForScanning(DriveForSearch);

         SearchPath = path[0];
         PatientName = path[1];
         CPRNumber = path[2];
         
         try
         {
            TypeOfScanning1 = scannings[0].TypeOfScanning;
            SerieOfScanning1 = scannings[0].SerieOfScanning;
            TypeOfScanning2 = scannings[1].TypeOfScanning;
            SerieOfScanning2 = scannings[1].SerieOfScanning;
         }
         catch (Exception e)
         {
            MessageBox.Show("No scannings were found\r\n" + e);
         }

      }
   }
}
