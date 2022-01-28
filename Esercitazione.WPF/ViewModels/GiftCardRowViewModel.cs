using Esercitazione.Core.BL;
using Esercitazione.Core.Entities;
using Esercitazione.Core.Mock.Repos;
using Esercitazione.WPF.Messaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class GiftCardRowViewModel : ViewModelBase
    {

        private GiftCard item;
        private string destinatario;
        public string Destinatario
        {
            get { return destinatario; }
            set { destinatario = value; RaisePropertyChanged(); }

        }
        private double importo;
        public double Importo
        {
            get { return importo; }
            set { importo = value; RaisePropertyChanged(); }

        }
        private string mittente;
        public string Mittente
        {
            get { return mittente; }
            set { mittente = value; RaisePropertyChanged(); }

        }
        private string messaggio;
        public string Messaggio
        {
            get { return messaggio; }
            set { messaggio = value; RaisePropertyChanged(); }

        }
        private DateTime dataScadenza;
        public DateTime DataDiScadenza
        {
            get { return dataScadenza; }
            set { dataScadenza = value; RaisePropertyChanged(); }

        }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public GiftCardRowViewModel()
        {
            UpdateCommand = new RelayCommand(() => ExecuteUpdate());
            DeleteCommand = new RelayCommand(() => ExecuteDelete());
        }
        public GiftCardRowViewModel(GiftCard item) : this()
        {
            Destinatario = item.Destinatario;
            Importo = item.Importo;
            this.item = item;
        }

        private void ExecuteDelete()
        {
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Confirm Delete",
                Content = "Sei sicuro??",
                Icon = MessageBoxImage.Question,
                Buttons = MessageBoxButton.YesNo,
                Callback = OnMessageBoxResultReceived
            });
        }

        private void OnMessageBoxResultReceived(MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                var layer = new MainBusinessLayer(new GiftCardRepositoryMock());

                var response = layer.DeleteGiftCard(item);

                if (!response.Success)
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Errore",
                        Content = response.Message,
                        Icon = MessageBoxImage.Error,
                        Buttons = MessageBoxButton.OK,
                    });
                    return;
                }
                else
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Eliminazione confermata",
                        Content = response.Message,
                        Icon = MessageBoxImage.Information
                    });
                }
            }
        }

        private void ExecuteUpdate()
        {
            Messenger.Default.Send(new ShowUpdateGiftCardMessage { Entity = item });
        }

        //Checkbox
        private bool viewInfo = false;
        public bool ViewInfo
        {
            get { return viewInfo; }
            set { viewInfo = value; RaisePropertyChanged(); }
        }


    }
}
