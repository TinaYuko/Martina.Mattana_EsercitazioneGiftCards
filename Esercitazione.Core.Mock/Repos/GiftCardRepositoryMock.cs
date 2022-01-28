using Esercitazione.Core.Entities;
using Esercitazione.Core.Interfaces;
using Esercitazione.Core.Mock.Storage;

namespace Esercitazione.Core.Mock.Repos
{
    public class GiftCardRepositoryMock : IGiftCardRepository
    {
        public void Create(GiftCard giftcard)
        {
            var newId = MockStorage.GiftCards.Max(x => x.Id) + 1;
            giftcard.Id = newId;
            MockStorage.GiftCards.Add(giftcard);
        }

        public void Delete(GiftCard giftCard)
        {
            var existingGiftCard = MockStorage.GiftCards.FirstOrDefault(x => x.Id == giftCard.Id);
            MockStorage.GiftCards.Remove(existingGiftCard);
        }

        public IList<GiftCard> GetAll()
        {
            return MockStorage.GiftCards;
        }

        public void Update(GiftCard oldGiftCard, GiftCard newGiftCard)
        {
            var existingGiftCard = MockStorage.GiftCards.FirstOrDefault(x => x.Id == newGiftCard.Id);
            MockStorage.GiftCards.Remove(oldGiftCard);

            MockStorage.GiftCards.Add(newGiftCard);
        }
    }
}