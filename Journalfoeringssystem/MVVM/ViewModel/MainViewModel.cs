using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journalfoeringssystem.Core;

namespace Journalfoeringssystem.MVVM.ViewModel
{
   class MainViewModel: ObservableObject
   {
      public HomeViewModel HomeVM { get; set; }
      public GeneratePDFViewModel GeneratePDFVM { get; set; }
      public HelpViewModel HelpVM { get; set; }
      public RelayCommand HomeViewCommand { get; set; }
      public RelayCommand GeneratePDFViewCommand { get; set; }
      public RelayCommand HelpViewCommand { get; set; }
      
      private object _currentView;

      public object CurrentView
      {
         get { return _currentView; }
         set
         {
            _currentView = value; 
            OnPropertyChanged();
         }
      }

      public MainViewModel()
      {
         HomeVM = new HomeViewModel();
         GeneratePDFVM = new GeneratePDFViewModel();
         HelpVM = new HelpViewModel();
         CurrentView = HomeVM;

         HomeViewCommand = new RelayCommand(o =>
         {
            CurrentView = HomeVM;
         });

         GeneratePDFViewCommand = new RelayCommand(o =>
         {
            CurrentView = GeneratePDFVM;
         });

         HelpViewCommand = new RelayCommand(o =>
         {
            CurrentView = HelpVM;
         });
      }
   }
}
