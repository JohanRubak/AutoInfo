using System.Collections.ObjectModel;

namespace Journalfoeringssystem.MVVM.Model
{
   public class Workers
   {
      public ObservableCollection<Worker> WorkersList { get; set; } = new ObservableCollection<Worker>();

      public void AddWorker(Worker worker)
      {
         WorkersList.Add(worker);
      }

      public void RemoveWorker(Worker worker)
      {
         WorkersList.Remove(worker);
      }

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