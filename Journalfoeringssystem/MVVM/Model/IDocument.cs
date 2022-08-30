using System;
using System.Collections.Generic;
using System.Linq;
using Journalfoeringssystem.Domain;

namespace Journalfoeringssystem.MVVM.Model
{
   public interface IDocument
   {
      void GeneratePDFDocument(InformationContainer informationContainer, List<IOrderedEnumerable<string>> filesPathSorted);
   }
}