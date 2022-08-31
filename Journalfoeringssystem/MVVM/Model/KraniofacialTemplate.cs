using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using Journalfoeringssystem.Domain;
using Microsoft.Office.Interop.Word;

namespace Journalfoeringssystem.MVVM.Model
{
   public class KranioFacialTemplate : IDocument
   {
      public Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

      float height = 0;
      float constantHeightFrontImage = 250;
      float constantHeightDI12 = 250;
      float constantHeightDI23 = 100;
      float constantHeightPOS15 = 180;
      float constantHeightO14 = 180;
      float constantHeightO46 = 270;
      float constantHeightPO14 = 180;
      float constantHeightPO46 = 270;
      float constantHeightCG12 = 270;
      float constantHeightRG12 = 270;
      float constantHeightSP12 = 270;
      float scale = 0;
      InlineShape newImage = null;

      Microsoft.Office.Interop.Word.Document doc = null;

      string filePath = @"C:\Patienter\Templates\Kraniofacial\Informationstabel, Kraniofacial - template.docx";

      public void GeneratePDFDocument(InformationContainer informationContainer, List<IOrderedEnumerable<string>> filesPathSorted)
      {
         doc = app.Documents.Add(filePath);
         doc.Activate();

         foreach (Microsoft.Office.Interop.Word.ContentControl contentControl in doc.ContentControls)
         {
            switch (contentControl.Title)
            {
               case "PatientName":
                  contentControl.Range.Text = informationContainer.PatientName;
                  break;

               case "CPR":
                  contentControl.Range.Text = informationContainer.CPRNumber;
                  break;

               case "VirtualPlanning":

                  string localtext = "";

                  if (informationContainer.WorkersInput != null)
                  {
                     foreach (var VARIABLE in informationContainer.WorkersInput.WorkersList)
                     {
                        localtext += $"{VARIABLE.WorkerName}({VARIABLE.WorkerJob}),\r\n";
                     }
                  }

                  contentControl.Range.Text = localtext;

                  break;

               case "DateForPlanning":
                  contentControl.Range.Text = informationContainer.DateForPlanning.ToShortDateString();
                  break;

               case "DateForSurgery":
                  contentControl.Range.Text = informationContainer.DateForSurgery.ToShortDateString();
                  break;

               case "Remarks":
                  contentControl.Range.Text = informationContainer.Remarks;
                  break;

               case "CuttingGuideText":
                  contentControl.Range.Text = informationContainer.CuttingGuide;
                  break;

               case "Scanning":
                  contentControl.Range.Text = informationContainer.TypeOfScanning1;
                  break;

               case "DateOfScanning":
                  contentControl.Range.Text = informationContainer.DateForScanning1.ToShortDateString();
                  break;

               case "Serie":
                  contentControl.Range.Text = informationContainer.SerieOfScanning1;
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

                  if (filesPathSorted[1].Count() >= 5)
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

               case "DeliveredInstruments6":

                  if (filesPathSorted[1].Count() >= 6)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[1].ElementAt(5));

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

                  if (filesPathSorted[2].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[2].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPOS15 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }


                  break;

               case "Osteotomy1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy3":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(2));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy4":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(3));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy5":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[3].ElementAt(4));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightO46 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "Osteotomy6":

                  if (filesPathSorted[3].Count() >= 6)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[3].ElementAt(5));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightO46 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;

               case "PlannedOutcome1":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(0));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome2":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(1));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome3":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(2));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome4":
                  newImage = contentControl.Range.InlineShapes.AddPicture(
                     filesPathSorted[4].ElementAt(3));

                  newImage.ScaleWidth = 100;
                  newImage.ScaleHeight = 100;

                  height = newImage.Height;

                  scale = constantHeightPO14 / height * 100;

                  newImage.ScaleHeight = scale;
                  newImage.ScaleWidth = scale;

                  break;

               case "PlannedOutcome5":

                  if (filesPathSorted[4].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[4].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO46 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;

               case "PlannedOutcome6":

                  if (filesPathSorted[4].Count() >= 6)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[4].ElementAt(5));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO46 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

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

                  if (filesPathSorted[5].Count() >= 4)
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

                  if (filesPathSorted[6].Count() >= 4)
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

               case "RepositioningGuide5":

                  if (filesPathSorted[6].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[6].ElementAt(4));

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

               case "Spacers3":

                  if (filesPathSorted[7].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[7].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightSP12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  else
                  {
                     contentControl.Delete();
                  }

                  break;
            }
         }

         try
         {
            doc.Save();
         }

         catch (Exception e)
         {
            Console.WriteLine(e);
         }
      }
   }
}