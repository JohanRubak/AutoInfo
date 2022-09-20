using System.Collections.ObjectModel;

namespace Journalfoeringssystem.MVVM.Model
{
   public class Workers
   {
      public ObservableCollection<Worker> WorkersList { get; set; } = new ObservableCollection<Worker>();

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