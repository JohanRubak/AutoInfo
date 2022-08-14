using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Journalfoeringssystem.Core;
using Journalfoeringssystem.MVVM.Model;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   public class GeneratePDFViewModel: ObservableObject
   {
      public RelayCommand SearchCommand { get; set; }
      public RelayCommand AddCommand { get; set; }

      public RelayCommand RemoveCommand { get; set; }

      public RelayCommand EditCommand { get; set; }

      public Worker WorkerInput { get; set; } = new Worker();
      public Worker SelectedWorker { get; set; } = new Worker();
      public Workers WorkersInput { get; set; } = new Workers();

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

      private string _name;

      public string Name
      {
         get
         {
            return _name;
         }

         set
         {
            _name = value;
            OnPropertyChanged(nameof(Name));
         }
      }

      public GeneratePDFViewModel()
      {
         WorkerInput = new Worker();
         WorkersInput = new Workers();

         SearchCommand = new RelayCommand(o =>
         {
            Name = CPRNumber;
         });

         AddCommand = new RelayCommand(o =>
         {
            WorkersInput.AddWorker(new Worker(){WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob});
         });

         RemoveCommand = new RelayCommand(o =>
         {
            WorkersInput.RemoveWorker(SelectedWorker);
         });

         EditCommand = new RelayCommand(o =>
         {
            WorkersInput.EditWorker(SelectedWorker, new Worker(){ WorkerName = WorkerInput.WorkerName, WorkerJob = WorkerInput.WorkerJob });
         });
      }

      
   }

   
}
