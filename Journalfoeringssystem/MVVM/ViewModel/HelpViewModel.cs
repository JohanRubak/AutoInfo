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
            Application ap = new Application();

            try
            {
               var path = Path.Combine(Directory.GetCurrentDirectory() + @"\TemplateFiles\AutoInfo\AutoInfo - Kraniofacial.docx");

               Document document = ap.Documents.Open(path);
            }
            catch (Exception e)
            {
               MessageBox.Show("Exception: " + e.ToString());
            }
         });

         HelpMandibelCommand = new RelayCommand(o =>
         {
            Application ap = new Application();

            try
            {
               var path = Path.Combine(Directory.GetCurrentDirectory() + @"\TemplateFiles\AutoInfo\AutoInfo - Mandibel.docx");
               
               Document document = ap.Documents.Open(path);
            }
            catch (Exception e)
            {
               MessageBox.Show("Exception: " + e.ToString());
            }
         });
      }
   }
}
