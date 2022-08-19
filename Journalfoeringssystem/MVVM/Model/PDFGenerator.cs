using System;
using System.Collections.Generic;

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

      public PDFGenerator()
      {

      }

      public void GeneratePDF(string searchPath, string patientName, string patientCPR, Workers workers, DateTime dateForPlanning, DateTime dateForOperation, DateTime dateofScanning, string typeOfScanning, string serieOfScanning, string cuttingGuide, string remarks)
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


      }
   }
}