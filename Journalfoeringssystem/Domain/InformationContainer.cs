using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journalfoeringssystem.MVVM.Model;

namespace Journalfoeringssystem.Domain
{
   //DTO-klasse, der indeholder alle informationer, der skal bruges til at udfylde template
   public class InformationContainer
   {
      //Kraniofacial
      public string SearchPath { get; set; }
      public string PatientName { get; set; }
      public string CPRNumber { get; set; }
      public Workers WorkersInput { get; set; }
      public DateTime DateForPlanning { get; set; }
      public DateTime DateForSurgery { get; set; }
      public DateTime DateForScanning1 { get; set; }
      public string TypeOfScanning1 { get; set; }
      public string SerieOfScanning1 { get; set; }
      public string CuttingGuide { get; set; }
      public string Remarks { get; set; }
      public string Protocol { get; set; }

      //Mandibel Ekstra
      public DateTime DateForScanning2 { get; set; }
      public string TypeOfScanning2 { get; set; }
      public string SerieOfScanning2 { get; set; }
      public string ResectionFrom { get; set; } 
      public string ResectionTo { get; set; }
      public string WhichFibula { get; set; }
      public string DistanceToMalleol { get; set; }
      public string Piece1Length { get; set; }
      public string Piece1PlacingOfFibula { get; set; }
      public string Piece1PlacingOfMandibel { get; set; }
      public string Piece2Length { get; set; }
      public string Piece2PlacingOfFibula { get; set; }
      public string Piece2PlacingOfMandibel { get; set; }
      public string Piece3Length { get; set; }
      public string Piece3PlacingOfFibula { get; set; }
      public string Piece3PlacingOfMandibel { get; set; }
      public string TotalLength { get; set; }
      public string CuttingThickness { get; set; }
      public string ScrewDiameter { get; set; }
      public string Direction { get; set; }
      public string Comments { get; set; }

      public InformationContainer()
      {

      }
   }
}
