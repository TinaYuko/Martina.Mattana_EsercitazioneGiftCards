using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercitazione.Core.Entities
{
    public class GiftCard
    {
        /*
         * Ciascun biglietto sarà caratterizzato da:
         * •Mittente(string)
         * •Destinatario(string)
         * •Messaggio(string)
         * •Importo(double)
         * •Datadiscadenza(DateTime)
         */

        public int Id { get; set; }
        public string Mittente { get; set; }
        public string Destinatario { get; set; }
        public string Messaggio { get; set; }
        public double Importo { get; set; }
        public DateTime DataDiScadenza { get; set; }
    }
}
