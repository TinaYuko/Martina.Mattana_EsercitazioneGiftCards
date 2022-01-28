using Esercitazione.Core.Entities;
using Esercitazione.Core.Interfaces;
using Esercitazione.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.BL
{
    public class MainBusinessLayer
    {
        private IGiftCardRepository Repo;

        public MainBusinessLayer(IGiftCardRepository giftCardRepository)
        {
            Repo = giftCardRepository;
        }

        public IList<GiftCard> GetAllGiftCards()
        {
            return Repo.GetAll();
        }

        public Response CreateGiftCard(GiftCard giftCard)
        {
            if (giftCard==null)
            {
                return new Response { Success = false, Message = "GiftCard inesistente" };
            }
            if (giftCard.Importo<=0.0)
            {
                return new Response { Success = false, Message = "Non puoi creare una carta con Importo negativo!" };
            }
            Repo.Create(giftCard);
            return new Response
            {
                Success = true,
                Message = $"La giftCard per {giftCard.Destinatario} è stata emessa"
            };
        }

        public Response DeleteGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
                return new Response { Success = false, Message = "GiftCard inesistente" };
            if (giftCard.Id < 0)
                return new Response { Success = false, Message = "ID non valido" };
            var giftCardToDelete = GetAllGiftCards().FirstOrDefault(x => x.Id == giftCard.Id);
            if (giftCardToDelete == null)
                return new Response
                {
                    Success = false,
                    Message = $"Non è stato possibile trovare nessuna giftCard con Id: {giftCard.Id}"
                };
            Repo.Delete(giftCardToDelete);

            return new Response { Success = true, Message = "GiftCard cancellata correttamente" };
        }

        public Response UpdateGiftCard(GiftCard giftCard)
        {
                if (giftCard == null)
                return new Response() { Success = false, Message = "GiftCard inesistente" };

            var giftCardToUpdate = GetAllGiftCards().FirstOrDefault(x => x.Id == giftCard.Id);
            Repo.Update(giftCardToUpdate, giftCard);
            return new Response() { 
                Success = true, 
                Message = "GiftCard aggiornata correttamente" };
        }

    }
}
