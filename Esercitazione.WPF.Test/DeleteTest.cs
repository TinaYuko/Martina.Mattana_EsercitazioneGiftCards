using Esercitazione.Core.BL;
using Esercitazione.Core.Entities;
using Esercitazione.Core.Interfaces;
using Esercitazione.Core.Mock.Repos;
using Esercitazione.Core.Mock.Storage;
using System;
using Xunit;

namespace Esercitazione.WPF.Test
{
    public class DeleteTest
    {
        [Fact]
        public void ShouldDeleteGiftCard()
        {
            //ARRANGE
            //Recupero i dati che mi serviranno per gestire l'operazione
            MockStorage.Initialize();

            //Creazione del repository
            IGiftCardRepository Repository = new GiftCardRepositoryMock();

            //Creazione del business layer
            MainBusinessLayer layer = new MainBusinessLayer(Repository);

            GiftCard gf= new GiftCard();

            gf.Id = 1;
            gf.Destinatario = "Lino Banfi";
            gf.Mittente = "Pippo Franco";
            gf.Messaggio = "Buon compleanno Linuccio!";
            gf.Importo = 100.00;
            gf.DataDiScadenza = new DateTime(2022, 12, 25);
            

            //ACT - Eseguo l'operazione da testare
            var result = layer.DeleteGiftCard(gf);

            //ASSERT - Verifica del risultato ottenuto
            Assert.True(result.Success);

        }
    }
}