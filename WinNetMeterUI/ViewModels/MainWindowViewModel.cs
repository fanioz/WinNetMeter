﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using WinNetMeterUI.Helpers;

namespace WinNetMeterUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "WinNetMeter Preview";
        private string _pagetitle = "General";

        private readonly IRegionManager _regionManager;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string PageTitle
        {
            get => _pagetitle;
            set => SetProperty(ref _pagetitle, value);
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(DoNavigate);

            if (!Env.IsProd())
            {
                Title += " (Internal)";
            }
        }

        private void DoNavigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                _regionManager.RequestNavigate("MainRegion", navigatePath, NavigationComplete);
            }
        }

        private void NavigationComplete(NavigationResult result)
        {
            PageTitle = result.Context.Uri.ToString().Replace("Page", "");
        }
    }
}