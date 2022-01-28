using Esercitazione.WPF.Messaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class HomeViewModel: ViewModelBase 
    {
        public ICommand ShowGiftCardCommand { get; set; }

        public HomeViewModel()
        {
            ShowGiftCardCommand= new RelayCommand(() => ExecuteShowGiftCard());
        }

        private void ExecuteShowGiftCard()
        {
            Messenger.Default.Send(new ShowGiftCardMessage());
        }
    }
}
