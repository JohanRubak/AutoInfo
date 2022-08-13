using Journalfoeringssystem.MVVM.View;
using Journalfoeringssystem.MVVM.ViewModel;

namespace Journalfoeringssystem.Core
{
   public class UserDataContext
   {
      public GeneratePDFViewModel GeneratePdfViewModel { get; set; }
      public GeneratePDFView GeneratePdfView { get; set; }
   }
}