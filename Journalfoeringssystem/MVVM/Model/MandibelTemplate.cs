using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journalfoeringssystem.Domain;
using Microsoft.Office.Interop.Word;

namespace Journalfoeringssystem.MVVM.Model
{
   public class MandibelTemplate : IDocument
   {
      public Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

      float height = 0;
      float constantHeightDI12 = 300;
      float constantHeightDI23 = 170;
      float constantHeightROF12 = 450;
      float constantHeightPO12 = 350;
      float constantHeightPO37 = 280;
      float constantHeightO1 = 280;
      float constantHeightCG12 = 450;
      float constantHeightCG35 = 280;
      float constantHeightG12 = 270;
      float scale = 0;
      InlineShape newImage = null;

      Microsoft.Office.Interop.Word.Document doc = null;

      string filePath = Path.Combine(Directory.GetCurrentDirectory() + @"\TemplateFiles\Templates\Mandibel\Informationstabel, Mandibel - template.docx");

      public void GeneratePDFDocument(InformationContainer informationContainer,
         List<IOrderedEnumerable<string>> filesPathSorted)
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

               case "ResectionFrom":
                  contentControl.Range.Text = informationContainer.ResectionFrom;
                  break;

               case "ResectionTo":
                  contentControl.Range.Text = informationContainer.ResectionTo;
                  break;

               case "Fibula":
                  contentControl.Range.Text = informationContainer.WhichFibula;
                  break;

               case "DistanceToMalleol":
                  contentControl.Range.Text = informationContainer.DistanceToMalleol;
                  break;

               case "LengthPiece1":
                  contentControl.Range.Text = informationContainer.Piece1Length;
                  break;

               case "Piece1PlacingOfFibula":
                  contentControl.Range.Text = informationContainer.Piece1PlacingOfFibula;
                  break;

               case "Piece1PlacingOfMandibel":
                  contentControl.Range.Text = informationContainer.Piece1PlacingOfMandibel;
                  break;

               case "LengthPiece2":
                  contentControl.Range.Text = informationContainer.Piece2Length;
                  break;

               case "Piece2PlacingOfFibula":
                  contentControl.Range.Text = informationContainer.Piece2PlacingOfFibula;
                  break;

               case "Piece2PlacingOfMandibel":
                  contentControl.Range.Text = informationContainer.Piece2PlacingOfMandibel;
                  break;

               case "LengthPiece3":
                  contentControl.Range.Text = informationContainer.Piece3Length;
                  break;

               case "Piece3PlacingOfFibula":
                  contentControl.Range.Text = informationContainer.Piece3PlacingOfFibula;
                  break;

               case "Piece3PlacingOfMandibel":
                  contentControl.Range.Text = informationContainer.Piece3PlacingOfMandibel;
                  break;

               case "TotalLength":
                  contentControl.Range.Text = informationContainer.TotalLength;
                  break;

               case "CuttingThickness":
                  contentControl.Range.Text = informationContainer.CuttingThickness;
                  break;

               case "ScrewDiameter":
                  contentControl.Range.Text = informationContainer.ScrewDiameter;
                  break;

               case "Direction":
                  contentControl.Range.Text = informationContainer.Direction;
                  break;

               case "Comments":
                  contentControl.Range.Text = informationContainer.Comments;
                  break;

               case "DeliveredInstruments1":
                  if (filesPathSorted[0].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "DeliveredInstruments2":
                  if (filesPathSorted[0].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "DeliveredInstruments3":
                  if (filesPathSorted[0].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI23 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "DeliveredInstruments4":
                  if (filesPathSorted[0].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI23 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "ResectionOfFibula1":
                  if (filesPathSorted[1].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[1].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightROF12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "ResectionOfFibula2":
                  if (filesPathSorted[1].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[1].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightROF12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome1":
                  if (filesPathSorted[2].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome2":
                  if (filesPathSorted[2].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome3":
                  if (filesPathSorted[2].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO37 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome4":
                  if (filesPathSorted[2].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO37 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome5":
                  if (filesPathSorted[2].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO37 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome6":
                  if (filesPathSorted[2].Count() >= 6)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(5));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO37 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "PlannedOutcome7":
                  if (filesPathSorted[2].Count() >= 7)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[2].ElementAt(6));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightPO37 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Osteotomy1":
                  if (filesPathSorted[3].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[3].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightO1 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "CuttingGuide1":
                  if (filesPathSorted[4].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[4].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "CuttingGuide2":
                  if (filesPathSorted[4].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[4].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "CuttingGuide3":
                  if (filesPathSorted[4].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[4].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG35 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "CuttingGuide4":
                  if (filesPathSorted[4].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[4].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG35 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "CuttingGuide5":
                  if (filesPathSorted[4].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[4].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightCG35 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Gutter1":
                  if (filesPathSorted[5].Any())
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[5].ElementAt(0));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Gutter2":
                  if (filesPathSorted[5].Count() >= 2)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[5].ElementAt(1));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightG12 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
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
