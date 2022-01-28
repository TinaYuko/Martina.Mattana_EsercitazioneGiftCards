using Esercitazione.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Mock.Storage
{
    public class MockStorage
    {
        public static IList<GiftCard> GiftCards { get; set; }

        public static void Initialize()
        {
            GiftCards = new List<GiftCard>();

            GiftCards.Add(new GiftCard
            {
                Id = 1,
                Destinatario = "Lino Banfi",
                Mittente = "Pippo Franco",
                Messaggio = "Buon compleanno Linuccio!",
                Importo = 100.00,
                DataDiScadenza = new DateTime(2022, 12, 25)
            });

            GiftCards.Add(new GiftCard
            {
                Id = 2,
                Destinatario = "Cher",
                Mittente = "Orietta Berti",
                Messaggio = "Così arrivi a fine mese, amica",
                Importo = 250.00,
                DataDiScadenza = new DateTime(2022, 03, 31)
            });
            GiftCards.Add(new GiftCard
            {
                Id = 3,
                Destinatario = "Renato Zero",
                Mittente = "Giorgio Panariello",
                Messaggio = "Scusami se ti imitavo nel mio show",
                Importo = 50.00,
                DataDiScadenza = new DateTime(2022, 08, 05)
            });
        }

    }
}
