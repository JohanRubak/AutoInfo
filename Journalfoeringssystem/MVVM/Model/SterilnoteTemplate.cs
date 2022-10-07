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
   public class SterilnoteTemplate : IDocument
   {
      //Starer word
      public Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();

      float height = 0;

      //Højder for billeder i wordfil
      float constantHeightDI12 = 300;
      float constantHeightDI36 = 170;

      float scale = 0;
      InlineShape newImage = null;

      //Starter dokument
      Microsoft.Office.Interop.Word.Document doc = null;

      //Finder template
      string filePath = Path.Combine(Directory.GetCurrentDirectory() + @"\TemplateFiles\Templates\Sterilnote\Informationstabel, Sterilnote - template.docx");

      public void GeneratePDFDocument(InformationContainer informationContainer,
         List<IOrderedEnumerable<string>> filesPathSorted)
      {
         //Tilføjer template til dokument
         doc = app.Documents.Add(filePath);
         doc.Activate();

         //Kæmpe foreach der løber alle områder, der kan ændres i igennem og indsætter korrekt information
         foreach (ContentControl contentControl in doc.ContentControls)
         {
            switch (contentControl.Title)
            {
               case "DateForSurgery":
                  contentControl.Range.Text = informationContainer.DateForSurgery.ToShortDateString();
                  break;

               case "Operator":
                  contentControl.Range.Text = informationContainer.Operator;
                  break;

               case "IntersectionPoint":
                  contentControl.Range.Text = informationContainer.IntersectionPoint;
                  break;

               case "HospitalRoom":
                  contentControl.Range.Text = informationContainer.HospitalRoom;
                  break;

               case "NumberOfPieces":
                  contentControl.Range.Text = informationContainer.NumberOfPieces;
                  break;

               case "OPCoordinator":
                  contentControl.Range.Text = informationContainer.OPCoordinator;
                  break;

               case "Piece1":
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

               case "Piece2":
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

               case "Piece3":
                  if (filesPathSorted[0].Count() >= 3)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(2));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI36 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Piece4":
                  if (filesPathSorted[0].Count() >= 4)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(3));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI36 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Piece5":
                  if (filesPathSorted[0].Count() >= 5)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(4));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI36 / height * 100;

                     newImage.ScaleHeight = scale;
                     newImage.ScaleWidth = scale;
                  }

                  break;

               case "Piece6":
                  if (filesPathSorted[0].Count() >= 6)
                  {
                     newImage = contentControl.Range.InlineShapes.AddPicture(filesPathSorted[0].ElementAt(5));

                     newImage.ScaleWidth = 100;
                     newImage.ScaleHeight = 100;

                     height = newImage.Height;

                     scale = constantHeightDI36 / height * 100;

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
