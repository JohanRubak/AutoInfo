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
   public partial class GenerateSterilnoteView : UserControl
   {
      public GenerateSterilnoteView()
      {
         InitializeComponent();

         //Indstiller dato til dagsdato
         DateForSurgeryDP.DisplayDate = DateTime.Today;
         DateForDelivery.DisplayDate = DateTime.Today;
         DateForOPDelivery.DisplayDate = DateTime.Today;
      }


      private void SearchbarTextBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         SearchbarTextBox.SelectAll();
      }

      private void SearchbarTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
          SearchbarTextBox.SelectAll();
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

      private void Operator_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         Operator.SelectAll();
      }

      private void HospitalRoom_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         HospitalRoom.SelectAll();
      }

      private void NumberOfPieces_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         NumberOfPieces.SelectAll();
      }

      private void OPCoordinator_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         OPCoordinator.SelectAll();
      }
   }
}
