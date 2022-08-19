using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Office.Interop.Word;

namespace Journalfoeringssystem.MVVM.Model
{
   public class KranialTemplate : IDocument
   {
      public Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

      float height = 0;
      float constantHeightFrontImage = 280;
      float constantHeightDI12 = 270;
      float constantHeightDI23 = 110;
      float constantHeightPOS15 = 200;
      float constantHeightO18 = 200;
      float constantHeightPO18 = 200;
      float constantHeightCG12 = 280;
      float constantHeightRG12 = 280;
      float constantHeightSP12 = 300;
      float scale = 0;
      InlineShape newImage = null;

      Microsoft.Office.Interop.Word.Document doc = null;

      string filePath = @"C:\Patienter\Patient information - template.docx";

      public void GeneratePDFDocument(string patientName, string patientCPR, Workers workers, DateTime dateForPlanning, DateTime dateForOperation, DateTime dateofScanning, string typeOfScanning, string serieOfScanning, string cuttingGuide, string remarks, List<IOrderedEnumerable<string>> filesPathSorted)
      {
         doc = app.Documents.Add(filePath);
         doc.Activate();

         foreach (Microsoft.Office.Interop.Word.ContentControl contentControl in doc.ContentControls)
         {
            switch (contentControl.Title)
            {
               case "PatientName":
                  contentControl.Range.Text = patientName;
                  break;

               case "CPR":
                  contentControl.Range.Text = patientCPR;
                  break;

               case "VirtualPlanning":

                  string localtext = "";

                  foreach (var VARIABLE in workers.WorkersList)
                  {
                     localtext += $"{VARIABLE.WorkerName}({VARIABLE.WorkerJob}), ";
                  }
                  contentControl.Range.Text = localtext;

                  break;

               case "DateForPlanning":
                  contentControl.Range.Text = dateForPlanning.ToShortDateString();
                  break;

               case "DateForOperation":
                  contentControl.Range.Text = dateForOperation.ToShortDateString();
                  break;

               case "Remarks":
                  contentControl.Range.Text = remarks;
                  break;

               case "CuttingGuideText":
                  contentControl.Range.Text = cuttingGuide;
                  break;

               case "Scanning":
                  contentControl.Range.Text = typeOfScanning;
                  break;

               case "DateOfScanning":
                  contentControl.Range.Text = dateofScanning.ToShortDateString();
                  break;

               case "Serie":
                  contentControl.Range.Text = serieOfScanning;
                  break;
            }
         }

         var newFilePath = @"C:\Patienter\Patient information - template3.docx";
         doc.SaveAs2(newFilePath);
      }
   }
}