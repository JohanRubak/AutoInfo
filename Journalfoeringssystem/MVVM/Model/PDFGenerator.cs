using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Journalfoeringssystem.MVVM.Model
{
   public class PDFGenerator
   {
      public string SearchPath { get; set; }
      public string PatientName { get; set; }
      public string PatientCPR { get; set; }
      public Workers Workers { get; set; }
      public DateTime DateForPlanning { get; set; }
      public DateTime DateForOperation { get; set; }
      public DateTime DateOfScanning { get; set; }
      public string TypeOfScanning { get; set; }
      public string SerieOfScanning { get; set; }
      public string CuttingGuide { get; set; }
      public string Remarks { get; set; }
      public string TypeOfProtocol { get; set; }
      public IDocument PdfDocument { get; set; }
      public List<IOrderedEnumerable<string>> FilesPathSorted { get; set; }

      public PDFGenerator()
      {

      }

      public void GeneratePDF(string searchPath, string patientName, string patientCPR, Workers workers, DateTime dateForPlanning, DateTime dateForOperation, DateTime dateofScanning, string typeOfScanning, string serieOfScanning, string cuttingGuide, string remarks, string typeOfProtocol)
      {
         SearchPath = searchPath;
         PatientName = patientName;
         PatientCPR = patientCPR;
         Workers = Workers;
         DateForPlanning = dateForPlanning;
         DateForOperation = dateForOperation;
         DateOfScanning = dateofScanning;
         TypeOfScanning = typeOfScanning;
         SerieOfScanning = serieOfScanning;
         CuttingGuide = cuttingGuide;
         Remarks = remarks;
         TypeOfProtocol = typeOfProtocol;

         FilesPathSorted = FindAndSortImages(searchPath);

         switch (TypeOfProtocol)
         {
            case "Kraniofacial":
               PdfDocument = new KranialTemplate();
               PdfDocument.GeneratePDFDocument(patientName, patientCPR, workers, dateForPlanning, dateForOperation, dateofScanning, typeOfScanning, serieOfScanning, cuttingGuide, remarks, FilesPathSorted);
               break;
         }

      }

      public List<IOrderedEnumerable<string>> FindAndSortImages(string searchPath)
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
   }
}