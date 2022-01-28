using Esercitazione.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Interfaces
{
    public interface IGiftCardRepository
    {
        IList<GiftCard> GetAll();

        void Create(GiftCard giftcard);

        void Update(GiftCard oldGiftCard, GiftCard newGiftCard);

        void Delete(GiftCard giftCard);
    }
}
