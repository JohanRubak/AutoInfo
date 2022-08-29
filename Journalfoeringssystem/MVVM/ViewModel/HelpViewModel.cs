using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journalfoeringssystem.Core;
using Microsoft.Office.Interop.Word;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   class HelpViewModel: ObservableObject
   {
      public RelayCommand HelpKranioFacialCommand { get; set; }

      public HelpViewModel()
      {
         HelpKranioFacialCommand = new RelayCommand(o =>
         {
            Application ap = new Application();

            try
            {
               Document document = ap.Documents.Open(@"C:\Patienter\AutoInfo\AutoInfo - Generelt.docx");
            }
            catch (Exception e)
            {
               Console.WriteLine(e);
            }
         });
      }
   }
}
