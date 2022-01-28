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
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class GiftCardCreateViewModel: ViewModelBase
    {
        public ICommand CreateCommand { get; set; }

        private string destinatario;
        public string Destinatario
        {
            get { return destinatario; }
            set
            {
                destinatario = value; RaisePropertyChanged();
            }
        }

        private string mittente;
        public string Mittente
        {
            get { return mittente; }
            set
            {
                mittente = value; RaisePropertyChanged();
            }
        }

        private double importo;
        public double Importo
        {
            get { return importo; }
            set
            {
                importo = value; RaisePropertyChanged();
            }
        }

        private string messaggio;
        public string Messaggio
        {
            get { return messaggio; }
            set
            {
                messaggio = value; RaisePropertyChanged();
            }
        }

        private DateTime dataDiScadenza;
        public DateTime DataDiScadenza
        {
            get { return dataDiScadenza; }
            set
            {
                dataDiScadenza = value; RaisePropertyChanged();
            }
        }

        public GiftCardCreateViewModel()
        {
            CreateCommand = new RelayCommand(() => ExecuteCreate(), () => CanExecuteCreate());

            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (CreateCommand as RelayCommand).RaiseCanExecuteChanged();
                };
            }
        }

        private bool CanExecuteCreate()
        {
            return !string.IsNullOrEmpty(Destinatario) &&
                !string.IsNullOrEmpty(Mittente) &&
                !string.IsNullOrEmpty(Messaggio) &&
                !string.IsNullOrEmpty(Importo.ToString()) &&
                !string.IsNullOrEmpty(DataDiScadenza.ToString());
        }

        private void ExecuteCreate()
        {
            var entity = new GiftCard
            {
                Destinatario = Destinatario,
                Mittente = Mittente,
                Messaggio = Messaggio,
                DataDiScadenza = DataDiScadenza,
                Importo = Importo
            };

            //prendo il bl
            var layer = new MainBusinessLayer(new GiftCardRepositoryMock());
            //e richiamo l'operazione
            var response = layer.CreateGiftCard(entity);

            if (!response.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Qualcosa è andato storto",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Warning
                });
                return;
            }
            else
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Creazione completata",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Information
                });
            }
            Messenger.Default.Send(new CloseCreateGiftCardMessage());
        }
    }
}
