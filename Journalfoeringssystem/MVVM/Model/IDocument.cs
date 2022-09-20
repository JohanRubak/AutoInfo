using System;
using System.Collections.Generic;
using System.Linq;
using Journalfoeringssystem.Domain;

namespace Journalfoeringssystem.MVVM.Model
{
   //Interface der gør det muligt at oprette forskellige typer af dokumenter med samme klasse
   public interface IDocument
   {
      void GeneratePDFDocument(InformationContainer informationContainer, List<IOrderedEnumerable<string>> filesPathSorted);
   }
}