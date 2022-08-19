using System;
using System.Collections.Generic;
using System.Linq;

namespace Journalfoeringssystem.MVVM.Model
{
   public interface IDocument
   {
      void GeneratePDFDocument(string patientName, string patientCPR, Workers workers, DateTime dateForPlanning, DateTime dateForOperation, DateTime dateofScanning, string typeOfScanning, string serieOfScanning, string cuttingGuide, string remarks, List<IOrderedEnumerable<string>> filesPathSorted);
   }
}