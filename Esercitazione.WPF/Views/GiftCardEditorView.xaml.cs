﻿using Esercitazione.WPF.Messaging;
using Esercitazione.WPF.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Esercitazione.WPF.Views
{
    /// <summary>
    /// Interaction logic for GiftCardEditorView.xaml
    /// </summary>
    public partial class GiftCardEditorView : Window
    {
        public GiftCardEditorView()
        {
            Messenger.Default.Register<ShowCreateGiftCardMessage>(this, OnShowCreateGiftCardExecuted);
            Messenger.Default.Register<ShowUpdateGiftCardMessage>(this, OnShowUpdateGiftCardMessageReceived);

        }

        private void OnShowUpdateGiftCardMessageReceived(ShowUpdateGiftCardMessage obj)
        {
            GiftCardUpdateView view = new GiftCardUpdateView();
            GiftCardUpdateViewModel vm = new GiftCardUpdateViewModel();
            view.DataContext = vm;
            view.ShowDialog();
        }

        private void OnShowCreateGiftCardExecuted(ShowCreateGiftCardMessage obj)
        {
            GiftCardCreateView view = new GiftCardCreateView();
            GiftCardCreateViewModel vm = new GiftCardCreateViewModel();
            view.DataContext = vm;
            view.ShowDialog();
        }
    }
}
