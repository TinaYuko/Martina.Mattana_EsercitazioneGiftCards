using Esercitazione.Core.BL;
using Esercitazione.Core.Entities;
using Esercitazione.Core.Interfaces;
using Esercitazione.Core.Mock.Repos;
using Esercitazione.WPF.Messaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Esercitazione.WPF.ViewModels
{
    public class GiftCardEditorViewModel: ViewModelBase
    {
        public ICommand CreateGiftCard { get; set; }


        public ObservableCollection<GiftCardRowViewModel> GiftCardSource;
        private ICollectionView giftCards;
        public ICollectionView GiftCards
        {
            get { return giftCards; }
            set { giftCards = value; RaisePropertyChanged(); }
        }

        

        public ICommand LoadGiftCardsCommand { get; set; }

        public GiftCardEditorViewModel()
        {
            CreateGiftCard = new RelayCommand(() => ExecuteShowCreateGiftCard());
            LoadGiftCardsCommand = new RelayCommand(() => ExecuteLoadGiftCards());

            GiftCardSource = new ObservableCollection<GiftCardRowViewModel>();
            giftCards = new CollectionView(GiftCardSource);

            LoadGiftCardsCommand.Execute(null);
        }

        private void ExecuteLoadGiftCards()
        {
            IGiftCardRepository repo = new GiftCardRepositoryMock();
            MainBusinessLayer layer = new MainBusinessLayer(repo);

            //Dipendenti provienienti dal repo
            var employees = layer.GetAllGiftCards();

            //Pulizia della lista sorgente
            GiftCardSource.Clear();

            foreach (GiftCard item in giftCards)
            {
                var vmGCard = new GiftCardRowViewModel(item);
                GiftCardSource.Add(vmGCard);
            }
        }

        private void ExecuteShowCreateGiftCard()
        {
            Messenger.Default.Send(new ShowCreateGiftCardMessage());
        }
    }
}
