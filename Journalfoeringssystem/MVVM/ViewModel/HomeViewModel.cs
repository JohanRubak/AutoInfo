using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journalfoeringssystem.Core;
using Journalfoeringssystem.MVVM.Model;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   class HomeViewModel: ObservableObject
   {
      private int _numberOfDepartments;

      public int NumberOfDepartments
      {
         get
         {
            return _numberOfDepartments;
         }
         set
         {
            _numberOfDepartments = value;
            OnPropertyChanged(nameof(NumberOfDepartments));
         }
      }

      private int _numberOfPatients;

      public int NumberOfPatients
      {
         get
         {
            return _numberOfPatients;
         }
         set
         {
            _numberOfPatients = value;
            OnPropertyChanged(nameof(NumberOfPatients));
         }
      }

      public HomeViewModel()
      {
         FileReader fileReader = new FileReader();
      }
   }
}
