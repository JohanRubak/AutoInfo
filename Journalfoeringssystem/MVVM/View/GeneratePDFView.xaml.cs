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

      private void LoadPicturesButton_Click(object sender, RoutedEventArgs e)
      {
         string path = "C:\\Patienter\\Johan Rubak, 0208990179";

         var files = Directory.GetFiles(path,"*.*", SearchOption.AllDirectories);

         for (int i = 0; i < files.Length; i++)
         {
            string filename = System.IO.Path.GetFileName(files[i]);
            FileInfo fileInfo = new FileInfo(files[i]);
            UploadingFilesList.Items.Add(new FileUpload()
            {
               FileName = filename,

               FileSize = string.Format("{0} {1}", (fileInfo.Length/1.049e+6).ToString("0.0"), "Mb"),
               UploadProgress = 100
            });
         }
      }
   }
}
