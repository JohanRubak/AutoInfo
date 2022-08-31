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
   public partial class GenerateKraniofacialView : UserControl
   {
      public GenerateKraniofacialView()
      {
         InitializeComponent();
         DateForPlanningDP.DisplayDate = DateTime.Today;
         DateForSurgeryDP.DisplayDate = DateTime.Today;
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

      private void SearchbarTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         SearchbarTextBox.SelectAll();
      }

      private void NameWorker_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         NameWorker.SelectAll();
      }

      private void WorkerTitel_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         WorkerTitel.SelectAll();
      }

      private void TypeOfScanningTextbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         TypeOfScanningTextbox.SelectAll();
      }

      private void SerieTextbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         SerieTextbox.SelectAll();
      }

      private void CuttingGuideTextbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         CuttingGuideTextbox.SelectAll();
      }

      private void RemarksTextbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         RemarksTextbox.SelectAll();
      }

      private void SearchbarTextBox_KeyDown(object sender, KeyEventArgs e)
      {
         if (SearchbarTextBox.Text == "Search for patient (XXXXXXXXX)")
         {
            SearchbarTextBox.Text = "";

            if (e.Key != Key.Back)
            {
               string text = this.SearchbarTextBox.Text;

               if (text.Replace("-", "").Length % 6 == 0 && text.Length != 0 && text.Substring(text.Length - 1) != "-")
               {
                  this.SearchbarTextBox.Text = this.SearchbarTextBox.Text + "-";

                  this.SearchbarTextBox.Select(this.SearchbarTextBox.Text.Length, 1);
               }
            }
         }

         else
         {
            if (e.Key != Key.Back)
            {
               string text = this.SearchbarTextBox.Text;

               if (text.Replace("-", "").Length % 6 == 0 && text.Length != 0 && text.Substring(text.Length - 1) != "-")
               {
                  this.SearchbarTextBox.Text = this.SearchbarTextBox.Text + "-";

                  this.SearchbarTextBox.Select(this.SearchbarTextBox.Text.Length, 1);
               }
            }
         }

         
      }
   }
}
