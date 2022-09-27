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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Journalfoeringssystem.MVVM.Model;
using Journalfoeringssystem.MVVM.ViewModel;
using Microsoft.Win32;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace Journalfoeringssystem.MVVM.View
{
   /// <summary>
   /// Interaction logic for GeneratePDFView.xaml
   /// </summary>
   public partial class GenerateGenericView : UserControl
   {
      public GenerateGenericView()
      {
         InitializeComponent();

         //Indstiller dato til dagsdato
         DateForPlanningDP.DisplayDate = DateTime.Today;
         DateForSurgeryDP.DisplayDate = DateTime.Today;
         DateOfScanningDP.DisplayDate = DateTime.Today;
         DateOfScanning2DP.DisplayDate = DateTime.Today;
      }

      private void PersonsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         //Indlæser valgte person i redigerbare felter
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

      private void TypeOfScanning2Textbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         TypeOfScanning2Textbox.SelectAll();
      }

      private void Serie2Textbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         Serie2Textbox.SelectAll();
      }

      private void P1LTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P1LTB.SelectAll();
      }

      private void P1FTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P1FTB.SelectAll();
      }

      private void P1MTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P1MTB.SelectAll();
      }

      private void P2LTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P2LTB.SelectAll();
      }

      private void P2FTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P2FTB.SelectAll();
      }

      private void P2MTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P2MTB.SelectAll();
      }

      private void P3L_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P3L.SelectAll();
      }

      private void P3L_GotKeyboardFocus_1(object sender, KeyboardFocusChangedEventArgs e)
      {
         P3L.SelectAll();
      }

      private void P3FTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P3FTB.SelectAll();
      }

      private void P3MTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         P3MTB.SelectAll();
      }

      private void CommentTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         CommentTB.SelectAll();
      }

      private void FromTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         FromTB.SelectAll();
      }

      private void ToTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         ToTB.SelectAll();
      }

      private void DisMalTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         DisMalTB.SelectAll();
      }

      private void TlTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         TlTB.SelectAll();
      }

      private void CtTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         CtTB.SelectAll();
      }

      private void SdTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         SdTB.SelectAll();
      }

      private void DTB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         DTB.SelectAll();
      }

      //Sørger for, at der kun kan indtastes CPR-nummer, og at det automatisk sepereres med -
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
