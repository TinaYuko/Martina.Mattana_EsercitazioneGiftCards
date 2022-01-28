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
    public class GiftCardUpdateViewModel: ViewModelBase
    {
        public ICommand UpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; } //per chiudere la finestra dopo che clicco aggiorna


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

        public GiftCardUpdateViewModel()
        {
            //() => ExecuteUpdate(), 
            UpdateCommand = new RelayCommand(() => ExecuteUpdate(), ()=> CanExecuteUpdate());
            CancelCommand = new RelayCommand(() => ExecuteCancel());
            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (UpdateCommand as RelayCommand).RaiseCanExecuteChanged();
                };

            }
        }
        private GiftCard giftCard;
        public GiftCardUpdateViewModel(GiftCard entity) : this()
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            giftCard = entity;
            Mittente = entity.Mittente;
            Destinatario = entity.Destinatario;
            Importo = entity.Importo;
            Messaggio = entity.Messaggio;
            DataDiScadenza = entity.DataDiScadenza;
        }

        private void ExecuteCancel()
        {
            Messenger.Default.Send(new CloseUpdateGiftCardMessage());
        }

        private bool CanExecuteUpdate()
        {
            //Posso fare l'update solo se i campi principali ci sono e se la data di scadenza non è "scaduta"
            return
                !string.IsNullOrEmpty(Mittente) &&
                !string.IsNullOrEmpty(Destinatario) && !string.IsNullOrEmpty(Importo.ToString()) 
                && DataDiScadenza > DateTime.Today ;
        }

        private void ExecuteUpdate()
        {

            var entity = new GiftCard
            {
                Mittente = Mittente,
                Destinatario = Destinatario,
                Importo = Importo,
                Messaggio = Messaggio,
                DataDiScadenza = DataDiScadenza
        };


            var layer = new MainBusinessLayer(new GiftCardRepositoryMock());

            var result = layer.UpdateGiftCard(entity);

            if (!result.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Attenzione! Alcuni dati non sono validi!",
                    Content = result.Message,
                    Icon = MessageBoxImage.Warning
                });
                return;
            }

            //Mostro un messaggio per segnalare che il salvataggio è andato bene
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Conferma",
                Content = $"La GiftCard per {Mittente} è stata aggiornata!",
                Icon = MessageBoxImage.Information
            });

            //Chiudo la finestra corrente
            CancelCommand.Execute(null);
        }

    }
}
