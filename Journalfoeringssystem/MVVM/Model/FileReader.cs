using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Documents;
using System.Windows.Forms.Design;
using Journalfoeringssystem.Domain;
using Journalfoeringssystem.MVVM.View;

namespace Journalfoeringssystem.MVVM.Model
{
   public class FileReader
   {
      public string[] Information { get; set; } = new string[4];

      public string[] SearchForFiles(string patientCPR, string rootPath)
      {
         if (patientCPR.Length <= 11)
         {
            var filePath = Directory.GetDirectories(rootPath, "*" + patientCPR + "*", SearchOption.AllDirectories);

            try
            {
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


               Information[0] = filePath[0] + @"\Billeder";
               Information[1] = patientName;
               Information[3] = filePath[0];

               return Information;

            }

            catch (Exception e)
            {
               Console.WriteLine(e);
               return null;
            }
         }

         else
         {
            return null;
         }

      }

      public string[] SearchForFiles(string searchPath)
      {

         var filePath = new DirectoryInfo(searchPath);

         try
         {
            string patientName = string.Empty;

            string patientCPR = string.Empty;

            string patient = filePath.Name;

            for (int i = 0; i < patient.Length; i++)
            {
               if (Char.IsLetter(patient[i]) || Char.IsWhiteSpace(patient[i]))
               {
                  patientName += patient[i];
               }

               else if (Char.IsDigit(patient[i]) || patient[i] == '-')
               {
                  patientCPR += patient[i];
               }
            }


            Information[0] = filePath + @"\Billeder";
            Information[1] = patientName;
            Information[2] = patientCPR;

            return Information;

         }

         catch (Exception e)
         {
            Console.WriteLine(e);
            return null;
         }
      }

      public List<ScanningInformationContainer> SearchForScanning(string searchPath)
      {
         List<ScanningInformationContainer> scanningList = new List<ScanningInformationContainer>();

         try
         {
            var filePath = Directory.GetDirectories(searchPath, "*" + "Scan" + "*", SearchOption.AllDirectories);

            for (int i = 0; i < filePath.Length; i++)
            {
               string scanFolder = new DirectoryInfo(filePath[i]).Name;

               string[] scanInfo = scanFolder.Split('_');

               ScanningInformationContainer ScanningInfo = new ScanningInformationContainer();

               ScanningInfo.TypeOfScanning += scanInfo[1] + ", ";

               for (int j = 0; j < scanInfo[2].Length; j++)
               {
                  if (Char.IsLetter(scanInfo[2][j]))
                  {
                  
                  }

                  else
                  {
                     ScanningInfo.SerieOfScanning += scanInfo[2][j];
                  }
               }

               scanningList.Add(ScanningInfo);
            }

            return scanningList;
         }

         catch (Exception e)
         {
            Console.WriteLine(e);
            return scanningList;
         }
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

      public int[] LoadNumbers(string rootPath)
      {
         int[] Numbers = new int[2];

         string departmentpath = @"C:\Patienter\";

         int numberOfDepartment = 0;

         int numberOfPatients = 0;

         numberOfDepartment = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly).Length;

         //var searchForPatients = Directory.GetDirectories(departmentpath, "*", SearchOption.TopDirectoryOnly);

         //for (int i = 0; i < searchForPatients.Length; i++)
         //{
         //   numberOfPatients += Directory.GetDirectories(searchForPatients[i] + @"\2021", "*", SearchOption.TopDirectoryOnly).Length;
         //   numberOfPatients += Directory.GetDirectories(searchForPatients[i] + @"\2022", "*", SearchOption.TopDirectoryOnly).Length;
         //}

         Numbers[0] = numberOfDepartment;
         Numbers[1] = numberOfPatients;

         return Numbers;
      }
   }
}