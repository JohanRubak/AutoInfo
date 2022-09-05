using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Journalfoeringssystem.Core;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   class HelpViewModel: ObservableObject
   {
      public RelayCommand HelpKranioFacialCommand { get; set; }
      public RelayCommand HelpMandibelCommand { get; set; }

      public HelpViewModel()
      {
         HelpKranioFacialCommand = new RelayCommand(o =>
         {
            try
            {
               Application ap = new Application();
               var path = Path.Combine(Directory.GetCurrentDirectory() + @"\TemplateFiles\AutoInfo\AutoInfo - Kraniofacial.docx");
               for (int i = 0; i < 2; i++)
               {
                  Document document = ap.Documents.Open(path);
               }
            }
            catch (Exception e)
            {
               MessageBox.Show("Exception: " + e.ToString());
            }
         });

         HelpMandibelCommand = new RelayCommand(o =>
         {

            try
            {
               Application ap = new Application();
               var path = Path.Combine(Directory.GetCurrentDirectory() + @"\TemplateFiles\AutoInfo\AutoInfo - Kraniofacial.docx");
               for (int i = 0; i < 2; i++)
               {
                  Document document = ap.Documents.Open(path);
               }
            }
            catch (Exception e)
            {
               MessageBox.Show("Exception: " + e.ToString());
            }
         });
      }
   }
}
