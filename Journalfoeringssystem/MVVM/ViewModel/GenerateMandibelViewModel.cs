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
            FilesForUpload = FileReader.LoadPictures(SearchPath);
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
