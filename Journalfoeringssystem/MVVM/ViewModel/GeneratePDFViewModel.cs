using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Journalfoeringssystem.Core;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   public class GeneratePDFViewModel: ObservableObject
   {
      public RelayCommand SearchCommand { get; set; }
      public RelayCommand AddCommand { get; set; }

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

      private string _workerName;

      public string WorkerName
      {
         get
         {
            return _workerName;
         }

         set
         {
            _workerName = value;
            OnPropertyChanged(nameof(WorkerName));
         }
      }

      public GeneratePDFViewModel()
      {
         SearchCommand = new RelayCommand(o =>
         {
            Name = CPRNumber;
         });

         AddCommand = new RelayCommand(o =>
         {

         });
      }

      
   }

   
}
