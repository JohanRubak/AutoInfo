using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Journalfoeringssystem.MVVM.Model
{
   public class Workers
   {
      public ObservableCollection<Worker> WorkersList { get; set; } = new ObservableCollection<Worker>();
      public List<string> ConfiguratedWorkerNames { get; set; }
      public List<string> ConfiguratedWorkerJobs { get; set; }

      public Workers()
      {
         ConfiguratedWorkerNames = new List<string>(){ "Joakim Lundtoft Lindhardt" , "Karen Eich Hammer", "Anders Mølgaard Jakobseb", "Mads Emil Nielsen", "Johan Andreas Balle Rubak", "Johan Blomlöf", "Sven Erik Nørholt", "Otto Thorsson ", "Emir Hasanbegovic", "Christian Bang", "Birgitte Jul Kiil"};
         ConfiguratedWorkerJobs = new List<string>() { "Ingeniør", "Kirurg", "Læge", "Sygeplejerske" };
      }

      //Tilføjelse af worker til liste
      public void AddWorker(Worker worker)
      {
         WorkersList.Add(worker);
      }

      //Fjerne worker fra liste
      public void RemoveWorker(Worker worker)
      {
         WorkersList.Remove(worker);
      }

      //Ændre i worker i liste
      public void EditWorker(Worker worker, Worker workerNew)
      {
         foreach (var VARIABLE in WorkersList)
         {
            if (VARIABLE == worker)
            {
               VARIABLE.WorkerName = workerNew.WorkerName;
               VARIABLE.WorkerJob = workerNew.WorkerJob;
            }
         }
      }
   }
}