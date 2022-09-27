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
      public GenerateKraniofacialViewModel GenerateKraniofacialVM { get; set; }
      public GenerateMandibelViewModel GenerateMandibelVM { get; set; }
      public GenerateGenericViewModel GenerateGenericVM { get; set; }
      public GenerateSterilnoteViewModel GenerateSterilnoteVM { get; set; }
      public HelpViewModel HelpVM { get; set; }
      public RelayCommand HomeViewCommand { get; set; }
      public RelayCommand GenerateKraniofacialViewCommand { get; set; }
      public RelayCommand GenerateMandibelViewCommand { get; set; }
      public RelayCommand GenerateGenericViewCommand { get; set; }
      public RelayCommand GenerateSterilnoteViewCommand { get; set; }
      public RelayCommand HelpViewCommand { get; set; }
      

      //Her ændres det der vises ved at ændre propertien, som sker i constructoren
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
         GenerateKraniofacialVM = new GenerateKraniofacialViewModel();
         GenerateMandibelVM = new GenerateMandibelViewModel();
         GenerateGenericVM = new GenerateGenericViewModel();
         GenerateSterilnoteVM = new GenerateSterilnoteViewModel();
         HelpVM = new HelpViewModel();
         CurrentView = HomeVM;

         HomeViewCommand = new RelayCommand(o =>
         {
            CurrentView = HomeVM;
         });

         GenerateKraniofacialViewCommand = new RelayCommand(o =>
         {
            CurrentView = GenerateKraniofacialVM;
         });

         GenerateMandibelViewCommand = new RelayCommand(o =>
         {
            CurrentView = GenerateMandibelVM;
         });

         GenerateGenericViewCommand = new RelayCommand(o =>
         {
            CurrentView = GenerateGenericVM;
         });

         GenerateSterilnoteViewCommand = new RelayCommand(o =>
         {
            CurrentView = GenerateSterilnoteVM;
         });

         HelpViewCommand = new RelayCommand(o =>
         {
            CurrentView = HelpVM;
         });
      }
   }
}
