using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Journalfoeringssystem.MVVM.Model;
using Journalfoeringssystem.MVVM.ViewModel;
using Microsoft.Win32;

namespace Journalfoeringssystem.MVVM.View
{
   /// <summary>
   /// Interaction logic for GeneratePDFView.xaml
   /// </summary>
   public partial class GeneratePDFView : UserControl
   {
      public GeneratePDFView()
      {
         InitializeComponent();
         DateForPlanningDP.DisplayDate = DateTime.Today;
         DateForOperationDP.DisplayDate = DateTime.Today;
         DateOfScanningDP.DisplayDate = DateTime.Today;
      }

      private void PersonsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         Worker workerInput = new Worker();
         workerInput = (Worker)PersonsListView.SelectedItem;

         if (workerInput != null)
         {
            NameWorker.Text = workerInput.WorkerName;
            WorkerTitel.Text = workerInput.WorkerJob;
         }
      }

      private void SearchbarTextBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         SearchbarTextBox.SelectAll();
      }

      private void NameWorker_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         NameWorker.SelectAll();
      }

      private void WorkerTitel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         WorkerTitel.SelectAll();
      }
   }
}
