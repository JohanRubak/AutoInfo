using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

      public Worker WorkerInput { get; set; }
      public Worker SelectedWorker { get; set; }
      public Workers WorkersInput { get; set; }
      public FileReader FileReader { get; set; }

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

      public GeneratePDFViewModel()
      {
         WorkerInput = new Worker();
         WorkersInput = new Workers();
         FileReader = new FileReader();

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
            string[] path = FileReader.SearchForFiles(SearchNumber);

            SearchPath = path[0];
            PatientName = path[1];
            CPRNumber = SearchNumber;

         });

         LoadImages = new RelayCommand(o =>
         {
            FilesForUpload = FileReader.LoadPictures(SearchPath);
         });

      }

      
   }

   
}
