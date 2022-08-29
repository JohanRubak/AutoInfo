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
using Journalfoeringssystem.Core;
using Journalfoeringssystem.MVVM.Model;
using Journalfoeringssystem.MVVM.View;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   public class GeneratePDFViewModel: ObservableObject
   {
      public RelayCommand SearchCommand { get; set; }
      public RelayCommand AddCommand { get; set; }
      public RelayCommand RemoveCommand { get; set; }
      public RelayCommand EditCommand { get; set; }
      public RelayCommand LoadImages { get; set; }
      public RelayCommand SelectedRadioButton { get; set; }
      public RelayCommand GeneratePDFCommand { get; set; }
      public RelayCommand FindDirectory { get; set; }
      public Worker WorkerInput { get; set; }
      public Worker SelectedWorker { get; set; }
      public Workers WorkersInput { get; set; }
      public FileReader FileReader { get; set; }
      public PDFGenerator PdfGenerator { get; set; }
      public string DriveForSearch { get; set; }

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
         }
      }

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
         }
      }

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
         }
      }

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
         }
      }

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

      public GeneratePDFViewModel()
      {
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

         });

         RemoveCommand = new RelayCommand(o =>
         {
            WorkersInput.RemoveWorker(SelectedWorker);
         });

         EditCommand = new RelayCommand(o =>
         {
            WorkersInput.EditWorker(SelectedWorker, new Worker() { WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });

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

         SelectedRadioButton = new RelayCommand(o =>
         {
            Protocol = (string)o;
         });

         GeneratePDFCommand = new RelayCommand(o =>
         {
            if (Protocol != null && !string.IsNullOrEmpty(SearchPath))
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
         PdfGenerator.GeneratePDF(SearchPath, PatientName, CPRNumber, WorkersInput, DateForPlanning, DateForSurgery,
            DateForScanning, TypeOfScanning, SerieOfScanning, CuttingGuide, Remarks, Protocol);
         Loading = Visibility.Hidden;
         ButtonText = "Generate PDF";
         ButtonEnabled = true;
      }
   }
}
