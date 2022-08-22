using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
                     localtext += $"{VARIABLE.WorkerName}({VARIABLE.WorkerJob}),\r\n";
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

               case "FrontImage":
                  newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightFrontImage / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "DeliveredInstruments1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[1].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightDI12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  height = newImage.Height;

                  break;

               case "DeliveredInstruments2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[1].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightDI12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  height = newImage.Height;

                  break;

               case "DeliveredInstruments3":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[1].ElementAt(2));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightDI23 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "DeliveredInstruments4":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[1].ElementAt(3));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightDI23 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "DeliveredInstruments5":

                  if (filesPathSorted[1].Count() == 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[1].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI23 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;

               case "PreOperativeSituation1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPOS15 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PreOperativeSituation2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[2].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPOS15 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PreOperativeSituation3":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[2].ElementAt(2));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPOS15 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PreOperativeSituation4":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[2].ElementAt(3));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPOS15 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PreOperativeSituation5":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[2].ElementAt(4));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPOS15 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy3":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(2));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy4":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(3));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy5":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(4));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy6":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(5));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPOS15 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome3":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(2));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome4":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(3));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome5":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(4));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome6":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(5));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO18 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "CuttingGuide1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[5].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightCG12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "CuttingGuide2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[5].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightCG12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "CuttingGuide3":
                  
                  if (filesPathSorted[5].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[5].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;

               case "CuttingGuide4":
                  
                  if (filesPathSorted[5].Count() == 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[5].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;

               case "RepositioningGuide1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[6].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightRG12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "RepositioningGuide2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[6].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightRG12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "RepositioningGuide3":

                  if (filesPathSorted[6].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[6].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightRG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;

               case "RepositioningGuide4":

                  if (filesPathSorted[6].Count() == 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[6].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightRG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;

               case "Spacers1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[7].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightSP12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Spacers2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[7].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightSP12 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;
            }
         }

         var newFilePath = @"C:\Patienter\Patient information - template3.docx";
         doc.SaveAs2(newFilePath);
      }
   }
}