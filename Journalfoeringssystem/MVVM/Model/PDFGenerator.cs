using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Journalfoeringssystem.Domain;

namespace Journalfoeringssystem.MVVM.Model
{
   public class PDFGenerator
   {
      public IDocument PdfDocument { get; set; }
      public List<IOrderedEnumerable<string>> FilesPathSorted { get; set; }

      public PDFGenerator()
      {

      }

      public void GeneratePDF(InformationContainer informationContainer)
      {
         switch (informationContainer.Protocol)
         {
            case "Kraniofacial":
               try
               {
                  FilesPathSorted = FindAndSortImagesForKraniofacial(informationContainer.SearchPath);
                  PdfDocument = new KranioFacialTemplate();
                  PdfDocument.GeneratePDFDocument(informationContainer, FilesPathSorted);
               }
               catch (Exception e)
               {
                  MessageBox.Show("Error: Wrong folderstructure for pictures or not correct amount of pictures!" + "\r\n\r\nException: " + e.ToString());
               }

               break;

            case "Mandibel":
               try
               {
                  FilesPathSorted = FindAndSortImagesForMandibel(informationContainer.SearchPath);
                  PdfDocument = new MandibelTemplate();
                  PdfDocument.GeneratePDFDocument(informationContainer, FilesPathSorted);
               }
               catch (Exception e)
               {
                  MessageBox.Show("Error: Wrong folderstructure for pictures or not correct amount of pictures!" + "\r\n\r\nException: " + e.ToString());
               }

               break;
         }
      }

      public List<IOrderedEnumerable<string>> FindAndSortImagesForKraniofacial(string searchPath)
      {
         List<IOrderedEnumerable<string>> filesPathSorted = new List<IOrderedEnumerable<string>>();

         var frontImageFiles = Directory.GetFiles(searchPath + @"\Front Image", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var deliveredInstrumentsFiles = Directory.GetFiles(searchPath + @"\Delivered Instruments", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var preOperativeSituationFiles = Directory.GetFiles(searchPath + @"\Preoperative Situation", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var osteotomiesFiles = Directory.GetFiles(searchPath + @"\Osteotomies", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var plannedOutcomeFiles = Directory.GetFiles(searchPath + @"\Planned Outcome", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var cuttingGuideFiles = Directory.GetFiles(searchPath + @"\Cutting Guide", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var repositioningGuideFiles = Directory.GetFiles(searchPath + @"\Repositioning Guide", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var spacersFiles = Directory.GetFiles(searchPath + @"\Spacers", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);

         filesPathSorted.Add(frontImageFiles);
         filesPathSorted.Add(deliveredInstrumentsFiles);
         filesPathSorted.Add(preOperativeSituationFiles);
         filesPathSorted.Add(osteotomiesFiles);
         filesPathSorted.Add(plannedOutcomeFiles);
         filesPathSorted.Add(cuttingGuideFiles);
         filesPathSorted.Add(repositioningGuideFiles);
         filesPathSorted.Add(spacersFiles);

         return filesPathSorted;
      }

      public List<IOrderedEnumerable<string>> FindAndSortImagesForMandibel(string searchPath)
      {
         List<IOrderedEnumerable<string>> filesPathSorted = new List<IOrderedEnumerable<string>>();

         var deliveredInstrumentsFiles = Directory.GetFiles(searchPath + @"\Delivered Instruments", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var resectionOfFibulaSituationFiles = Directory.GetFiles(searchPath + @"\Resection of Fibula", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var plannedOutcomeFiles = Directory.GetFiles(searchPath + @"\Planned Outcome", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var cuttingGuideFiles = Directory.GetFiles(searchPath + @"\Cutting Guide", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);
         var gutterFiles = Directory.GetFiles(searchPath + @"\Gutter", "*.*", SearchOption.AllDirectories).OrderBy(t => new FileInfo(t).LastWriteTime);

         filesPathSorted.Add(deliveredInstrumentsFiles);
         filesPathSorted.Add(resectionOfFibulaSituationFiles);
         filesPathSorted.Add(plannedOutcomeFiles);
         filesPathSorted.Add(cuttingGuideFiles);
         filesPathSorted.Add(gutterFiles);

         return filesPathSorted;
      }
   }
}