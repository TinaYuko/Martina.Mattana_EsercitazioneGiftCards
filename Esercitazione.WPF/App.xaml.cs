using Esercitazione.Core.Mock.Storage;
using Esercitazione.WPF.Messaging;
using Esercitazione.WPF.ViewModels;
using Esercitazione.WPF.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Esercitazione.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MockStorage.Initialize();

            //Collego vm a view
            HomeView view = new HomeView();
            HomeViewModel viewModel = new HomeViewModel();
            view.DataContext = viewModel;
            view.Show();
            base.OnStartup(e);
        }
    }
}
