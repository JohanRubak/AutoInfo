using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journalfoeringssystem.Domain;

namespace Journalfoeringssystem.MVVM.Model
{
   class MandibelTemplate: IDocument
   {
      public void GeneratePDFDocument(InformationContainer informationContainer, List<IOrderedEnumerable<string>> filesPathSorted)
      {
         throw new NotImplementedException();
      }
   }
}
