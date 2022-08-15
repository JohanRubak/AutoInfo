using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace Journalfoeringssystem.MVVM.Model
{
   public class FileReader
   {
      public string RootPath { get; set; } = @"C:\Patienter";

      public string[] Information { get; set; } = new string[2];

      public string[] SearchForFiles(string patientCPR)
      {
         var filePath = Directory.GetDirectories(RootPath, "*" + patientCPR + "*", SearchOption.TopDirectoryOnly);

         string folder = new DirectoryInfo(filePath[0]).Name;

         Information[0] = filePath[0];
         Information[1] = folder;

         return Information;
      }
   }
}