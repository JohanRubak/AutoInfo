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
   class GenerateMandibelViewModel: ObservableObject
   {
      public RelayCommand SearchCommand { get; set; }
      public RelayCommand AddCommand { get; set; }
      public RelayCommand RemoveCommand { get; set; }
      public RelayCommand EditCommand { get; set; }
      public RelayCommand LoadImages { get; set; }
      public RelayCommand GeneratePDFCommand { get; set; }
      public RelayCommand FindDirectory { get; set; }
      public RelayCommand SelectedRadioButton { get; set; }
      public Worker WorkerInput { get; set; }
      public Worker SelectedWorker { get; set; }
      public Workers WorkersInput { get; set; }
      public FileReader FileReader { get; set; }
      public PDFGenerator PdfGenerator { get; set; }
      public string DriveForSearch { get; set; }
      public InformationContainer InformationContainer { get; set; }

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

      private string _piece1Length;

      public string Piece1Length
      {
         get
         {
            return _piece1Length;
         }

         set
         {
            _piece1Length = value;
            OnPropertyChanged(nameof(Piece1Length));
            InformationContainer.Piece1Length = Piece1Length;
         }
      }

      private string _piece1PlacingOfFibula;

      public string Piece1PlacingOfFibula
      {
         get
         {
            return _piece1PlacingOfFibula;
         }

         set
         {
            _piece1PlacingOfFibula = value;
            OnPropertyChanged(nameof(Piece1PlacingOfFibula));
            InformationContainer.Piece1PlacingOfFibula = Piece1PlacingOfFibula;
         }
      }

      private string _piece1PlacingOfMandibel;

      public string Piece1PlacingOfMandibel
      {
         get
         {
            return _piece1PlacingOfMandibel;
         }

         set
         {
            _piece1PlacingOfMandibel = value;
            OnPropertyChanged(nameof(Piece1PlacingOfMandibel));
            InformationContainer.Piece1PlacingOfMandibel = Piece1PlacingOfMandibel;
         }
      }

      private string _piece2Length;

      public string Piece2Length
      {
         get
         {
            return _piece2Length;
         }

         set
         {
            _piece2Length = value;
            OnPropertyChanged(nameof(Piece2Length));
            InformationContainer.Piece2Length = Piece2Length;
         }
      }

      private string _piece2PlacingOfFibula;

      public string Piece2PlacingOfFibula
      {
         get
         {
            return _piece2PlacingOfFibula;
         }

         set
         {
            _piece2PlacingOfFibula = value;
            OnPropertyChanged(nameof(Piece2PlacingOfFibula));
            InformationContainer.Piece2PlacingOfFibula = Piece2PlacingOfFibula;
         }
      }

      private string _piece2PlacingOfMandibel;

      public string Piece2PlacingOfMandibel
      {
         get
         {
            return _piece2PlacingOfMandibel;
         }

         set
         {
            _piece2PlacingOfMandibel = value;
            OnPropertyChanged(nameof(Piece2PlacingOfMandibel));
            InformationContainer.Piece2PlacingOfMandibel = Piece2PlacingOfMandibel;
         }
      }

      private string _piece3Length;

      public string Piece3Length
      {
         get
         {
            return _piece3Length;
         }

         set
         {
            _piece3Length = value;
            OnPropertyChanged(nameof(Piece3Length));
            InformationContainer.Piece3Length = Piece3Length;
         }
      }

      private string _piece3PlacingOfFibula;

      public string Piece3PlacingOfFibula
      {
         get
         {
            return _piece3PlacingOfFibula;
         }

         set
         {
            _piece3PlacingOfFibula = value;
            OnPropertyChanged(nameof(Piece3PlacingOfFibula));
            InformationContainer.Piece3PlacingOfFibula = Piece3PlacingOfFibula;
         }
      }

      private string _piece3PlacingOfMandibel;

      public string Piece3PlacingOfMandibel
      {
         get
         {
            return _piece3PlacingOfMandibel;
         }

         set
         {
            _piece3PlacingOfMandibel = value;
            OnPropertyChanged(nameof(Piece3PlacingOfMandibel));
            InformationContainer.Piece3PlacingOfMandibel = Piece3PlacingOfMandibel;
         }
      }

      private string _comments;

      public string Comments
      {
         get
         {
            return _comments;
         }

         set
         {
            _comments = value;
            OnPropertyChanged(nameof(Comments));
            InformationContainer.Comments = Comments;
         }
      }

      private string _resectionFrom;

      public string ResectionFrom
      {
         get
         {
            return _resectionFrom;
         }

         set
         {
            _resectionFrom = value;
            OnPropertyChanged(nameof(ResectionFrom));
            InformationContainer.ResectionFrom = ResectionFrom;
         }
      }

      private string _resectionTo;

      public string ResectionTo
      {
         get
         {
            return _resectionTo;
         }

         set
         {
            _resectionTo = value;
            OnPropertyChanged(nameof(ResectionTo));
            InformationContainer.ResectionTo = ResectionTo;
         }
      }

      private string _distanceToMalleol;

      public string DistanceToMalleol
      {
         get
         {
            return _distanceToMalleol;
         }

         set
         {
            _distanceToMalleol = value;
            OnPropertyChanged(nameof(DistanceToMalleol));
            InformationContainer.DistanceToMalleol = DistanceToMalleol;
         }
      }

      private string _totalLength;

      public string TotalLength
      {
         get
         {
            return _totalLength;
         }

         set
         {
            _totalLength = value;
            OnPropertyChanged(nameof(TotalLength));
            InformationContainer.TotalLength = TotalLength;
         }
      }

      private string _cuttingThickness;

      public string CuttingThickness
      {
         get
         {
            return _cuttingThickness;
         }

         set
         {
            _cuttingThickness = value;
            OnPropertyChanged(nameof(CuttingThickness));
            InformationContainer.CuttingThickness = CuttingThickness;
         }
      }

      private string _screwDiameter;

      public string ScrewDiameter
      {
         get
         {
            return _screwDiameter;
         }

         set
         {
            _screwDiameter = value;
            OnPropertyChanged(nameof(ScrewDiameter));
            InformationContainer.ScrewDiameter = ScrewDiameter;
         }
      }

      private string _direction;

      public string Direction
      {
         get
         {
            return _direction;
         }

         set
         {
            _direction = value;
            OnPropertyChanged(nameof(Direction));
            InformationContainer.Direction = Direction;
         }
      }

      private string _whichFibula;

      public string WhichFibula
      {
         get
         {
            return _whichFibula;
         }

         set
         {
            _whichFibula = value;
            OnPropertyChanged(nameof(WhichFibula));

            if (WhichFibula == "Right")
            {
               InformationContainer.WhichFibula = "Højre";
            }

            else
            {
               InformationContainer.WhichFibula = "Venstre";
            }
         }
      }

      public GenerateMandibelViewModel()
      {
         InformationContainer = new InformationContainer();
         InformationContainer.Protocol = "Mandibel";
         Loading = Visibility.Hidden;
         PatientNotFound = Visibility.Hidden;
         ButtonEnabled = true;
         WorkerInput = new Worker();
         WorkersInput = new Workers();
         FileReader = new FileReader();
         PdfGenerator = new PDFGenerator();

         AddCommand = new RelayCommand(o =>
         {
            if (WorkerInput.WorkerName != null && WorkerInput.WorkerJob != null && WorkerInput.WorkerName != "" && WorkerInput.WorkerJob != "")
            {
               WorkersInput.AddWorker(new Worker() { WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });
            }

            InformationContainer.WorkersInput = WorkersInput;

         });

         RemoveCommand = new RelayCommand(o =>
         {
            WorkersInput.RemoveWorker(SelectedWorker);

            InformationContainer.WorkersInput = WorkersInput;

         });

         EditCommand = new RelayCommand(o =>
         {
            WorkersInput.EditWorker(SelectedWorker, new Worker() { WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });

            InformationContainer.WorkersInput = WorkersInput;

         });

         SearchCommand = new RelayCommand(o =>
         {
            if (!string.IsNullOrEmpty(SearchNumber) && !string.IsNullOrEmpty(DriveForSearch))
            {
               string[] path = FileReader.SearchForFiles(SearchNumber, DriveForSearch);

               if (path != null)
               {
                  SearchPath = path[0];
                  PatientName = path[1];
                  CPRNumber = SearchNumber;
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

         });

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

         SelectedRadioButton = new RelayCommand(o =>
         {
            WhichFibula = (string)o;
         });

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

         FindDirectory = new RelayCommand(o =>
         {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            DriveForSearch = dialog.SelectedPath;
            SearchPath = DriveForSearch;
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
   }
}
