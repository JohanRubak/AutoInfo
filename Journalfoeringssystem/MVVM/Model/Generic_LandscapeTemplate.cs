using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Journalfoeringssystem.Domain;
using Microsoft.Office.Interop.Word;

namespace Journalfoeringssystem.MVVM.Model
{
   public class Generic_LandscapeTemplate : IDocument
   {
      //Starter word
      public Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

      float height = 0;

      //Højde på billeder i wordfil
      float constantHeightFrontImage = 250;
      float constantHeightDI12 = 200;
      float constantHeightDI36 = 100;
      float constantHeightPOS13 = 200;
      float constantHeightPOS45 = 180;
      float constantHeightO12 = 200;
      float constantHeightO34 = 180;
      float constantHeightO46 = 270;
      float constantHeightPO12 = 200;
      float constantHeightPO34 = 180;
      float constantHeightPO46 = 270;
      float constantHeightCG12 = 270;
      float scale = 0;
      InlineShape newImage = null;

      //Starter dokument
      Microsoft.Office.Interop.Word.Document doc = null;

      //Finder template
      string filePath = Path.Combine(Directory.GetCurrentDirectory() + @"\TemplateFiles\Templates\Generic\Informationstabel, Generic_Liggende - template.docx");

      public void GeneratePDFDocument(InformationContainer informationContainer, List<IOrderedEnumerable<string>> filesPathSorted)
      {
         //Tilføjer template til dokument
         doc = app.Documents.Add(filePath);
         doc.Activate();

         //Kæmpe foreach der løber alle områder, der kan ændres i igennem og indsætter korrekt information
         foreach (ContentControl contentControl in doc.ContentControls)
         {
            switch (contentControl.Title)
            {
               case "Headline":
                  contentControl.Range.Text = informationContainer.Headline;
                  break;

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
                        localtext += $"{VARIABLE.WorkerName} ({VARIABLE.WorkerJob}),\r\n";
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

               case "GuideText":
                  contentControl.Range.Text = informationContainer.Guide;
                  break;

               case "Scanning1":
                  contentControl.Range.Text = informationContainer.TypeOfScanning1;
                  break;

               case "Scanning1Date":
                  contentControl.Range.Text = informationContainer.DateForScanning1.ToShortDateString();
                  break;

               case "Scanning1Serie":
                  contentControl.Range.Text = informationContainer.SerieOfScanning1;
                  break;

               case "Scanning2":
                  contentControl.Range.Text = informationContainer.TypeOfScanning2;
                  break;

               case "Scanning2Date":
                  contentControl.Range.Text = informationContainer.DateForScanning2.ToShortDateString();
                  break;

               case "Scanning2Serie":
                  contentControl.Range.Text = informationContainer.SerieOfScanning2;
                  break;

               case "FrontImage":
                  if (filesPathSorted != null && filesPathSorted[0].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightFrontImage / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "DeliveredInstruments1":
                  if (filesPathSorted[1].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[1].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;

                     height = newImage.Height;
                  }

                  break;

               case "DeliveredInstruments2":
                  if (filesPathSorted[1].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[1].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;

                     height = newImage.Height;
                  }

                  break;

               case "DeliveredInstruments3":
                  if (filesPathSorted[1].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[1].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI36 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "DeliveredInstruments4":
                  if (filesPathSorted[1].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[1].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI36 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "DeliveredInstruments5":

                  if (filesPathSorted[1].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[1].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI36 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
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

                     scale = constantHeightDI36 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PreOperativeSituation1":
                  if (filesPathSorted[2].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPOS13 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PreOperativeSituation2":
                  if (filesPathSorted[2].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[2].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPOS13 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PreOperativeSituation3":
                  if (filesPathSorted[2].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[2].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPOS13 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PreOperativeSituation4":
                  if (filesPathSorted[2].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[2].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPOS45 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Osteotomy1":
                  if (filesPathSorted[3].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[3].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightO12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Osteotomy2":
                  if (filesPathSorted[3].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[3].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightO12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Osteotomy3":
                  if (filesPathSorted[3].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[3].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightO34 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Osteotomy4":
                  if (filesPathSorted[3].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[3].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightO34 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Osteotomy5":
                  if (filesPathSorted[3].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[3].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightO46 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

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

                  break;

               case "PlannedOutcome1":
                  if (filesPathSorted[4].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[4].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome2":
                  if (filesPathSorted[4].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[4].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome3":
                  if (filesPathSorted[4].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[4].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO34 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome4":
                  if (filesPathSorted[4].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[4].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO34 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

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

                  break;

               case "Guide1":
                  if (filesPathSorted[5].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[5].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Guide2":
                  if (filesPathSorted[5].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(
                        filesPathSorted[5].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Guide3":

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

                  break;

               case "Guide4":

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

                  break;
               
            }
         }

         try
         {
            //Gemmer
            doc.ReadOnlyRecommended = false;
            doc.Save();
            doc.Close();
            app.Quit();

         }

         catch (Exception e)
         {
            Console.WriteLine(e);

         }
      }
   }
}