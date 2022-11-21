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
   class GenerateSterilnoteViewModel: ObservableObject
   {
      //Commands, der er binded til forskellige knapper i GUI
      public RelayCommand SearchCommand { get; set; }
      public RelayCommand LoadImages { get; set; }
      public RelayCommand GeneratePDFCommand { get; set; }
      public RelayCommand FindDirectory { get; set; }

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

      //Dato for delivery to department of sterilization
      private DateTime _dateForDelivery;

      public DateTime DateForDelivery
      {
         get
         {
            return _dateForDelivery;
         }

         set
         {
            _dateForDelivery = value;
            OnPropertyChanged(nameof(DateForDelivery));
            InformationContainer.DateForDelivery = DateForDelivery;
         }
      }

      //Dato for delivery to OP
      private DateTime _dateForOPDelivery;

      public DateTime DateForOPDelivery
      {
         get
         {
            return _dateForOPDelivery;
         }

         set
         {
            _dateForOPDelivery = value;
            OnPropertyChanged(nameof(DateForOPDelivery));
            InformationContainer.DateForOPDelivery = DateForOPDelivery;
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

      private string _operator;

      public string Operator
      {
         get
         {
            return _operator;
         }

         set
         {
            _operator = value;
            OnPropertyChanged(nameof(Operator));
            InformationContainer.Operator = Operator;
         }
      }

      private string _intersectionPoint;

      public string IntersectionPoint
      {
         get
         {
            return _intersectionPoint;
         }

         set
         {
            _intersectionPoint = value;
            OnPropertyChanged(nameof(IntersectionPoint));
            InformationContainer.IntersectionPoint = IntersectionPoint;
         }
      }

      public List<string> ConfiguratedIntersectionPoints { get; set; }

      private string _hospitalRoom;

      public string HospitalRoom
      {
         get
         {
            return _hospitalRoom;
         }

         set
         {
            _hospitalRoom = value;
            OnPropertyChanged(nameof(HospitalRoom));
            InformationContainer.HospitalRoom = HospitalRoom;
         }
      }

      private string _numberOfPieces;

      public string NumberOfPieces
      {
         get
         {
            return _numberOfPieces;
         }

         set
         {
            _numberOfPieces = value;
            OnPropertyChanged(nameof(NumberOfPieces));
            InformationContainer.NumberOfPieces = NumberOfPieces; 
         }
      }

      private string _OPCoordinator;

      public string OPCoordinator
      {
         get
         {
            return _OPCoordinator;
         }

         set
         {
            _OPCoordinator = value;
            OnPropertyChanged(nameof(OPCoordinator));
            InformationContainer.OPCoordinator = OPCoordinator; 
         }
      }

      private string _numberOfPages;

      public string NumberOfPages
      {
         get
         {
            return _numberOfPages;
         }

         set
         {
            _numberOfPages = value;
            OnPropertyChanged(nameof(NumberOfPages));
            InformationContainer.NumberOfPages = NumberOfPages;
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

      public GenerateSterilnoteViewModel()
      {
         InformationContainer = new InformationContainer();
         InformationContainer.Protocol = "Sterilnote";
         ConfiguratedIntersectionPoints = new List<string>() { "OP-Nord 1 J309", "OP-Nord 1 J305", "OP-Øst 3 H309"};
         Loading = Visibility.Hidden;
         LoadingSearch = Visibility.Hidden;
         PatientNotFound = Visibility.Hidden;
         ButtonEnabled = true;
         SearchButtonEnabled = true;
         FileReader = new FileReader();
         PdfGenerator = new PDFGenerator();

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
               FilesForUpload = FileReader.LoadDeliveredInstrumentsPictures(SearchPath);
            }
            catch (Exception e)
            {
               MessageBox.Show("Error: No picturesfolder found...\r\n\r\nException: " + e.ToString());
            }
         });

         //Genererer wordfilen ud fra template
         GeneratePDFCommand = new RelayCommand(o =>
         {
            if (!string.IsNullOrEmpty(SearchPath) && !string.IsNullOrEmpty(CPRNumber))
            {
               //Starter loading
               Thread thread1 = new Thread(StartLoading);
               thread1.Start();

               //Selve genereringen
               Thread thread2 = new Thread(StartGenerating);
               thread2.Start();
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
      }
   }
}
