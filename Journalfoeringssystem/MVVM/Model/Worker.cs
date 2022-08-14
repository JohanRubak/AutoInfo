using System.ComponentModel;
using System.Runtime.CompilerServices;
using Journalfoeringssystem.Annotations;

namespace Journalfoeringssystem.MVVM.Model
{
   public class Worker: INotifyPropertyChanged
   {
      public string _workerName;

      public string WorkerName
      {
         get
         {
            return _workerName;
         }

         set
         {
            _workerName = value;
            OnPropertyChanged(nameof(WorkerName));
         }
      }

      public string _workerJob;

      public string WorkerJob
      {
         get
         {
            return _workerJob;
         }

         set
         {
            _workerJob = value;
            OnPropertyChanged(nameof(WorkerJob));
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}