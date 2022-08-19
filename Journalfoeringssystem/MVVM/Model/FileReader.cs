using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Documents;
using Journalfoeringssystem.MVVM.View;

namespace Journalfoeringssystem.MVVM.Model
{
   public class FileReader
   {
      public string RootPath { get; set; } = @"C:\Patienter";

      public string[] Information { get; set; } = new string[2];

      public string[] SearchForFiles(string patientCPR)
      {
         var filePath = Directory.GetDirectories(RootPath, "*" + patientCPR + "*", SearchOption.AllDirectories);

         string patientName = string.Empty;

         string patient = new DirectoryInfo(filePath[0]).Name;

         for (int i = 0; i < patient.Length; i++)
         {
            if (Char.IsDigit(patient[i]))
            {
               break;
            }

            patientName += patient[i];
         }


         Information[0] = filePath[0];
         Information[1] = patientName;

         return Information;
      }

      public List<FileUpload> LoadPictures(string path)
      {
         List<FileUpload> filesForUpload = new List<FileUpload>();

         if (!string.IsNullOrEmpty(path))
         {
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
               string filename = Path.GetFileName(files[i]);
               FileInfo fileInfo = new FileInfo(files[i]);
               filesForUpload.Add(new FileUpload()
               {
                  FileName = filename,

                  FileSize = string.Format("{0} {1}", (fileInfo.Length / 1.049e+6).ToString("0.0"), "Mb"),
                  UploadProgress = 100
               });
            }

            return filesForUpload;
         }

         else
         {
            return filesForUpload;
         }
      }
   }
}