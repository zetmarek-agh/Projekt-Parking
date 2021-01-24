using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class WpisHistorii : IComparable<WpisHistorii>
    {
        private Pojazd _pojazd;
        //private MiejsceParkingowe _miejsceParkingowe;
        private DateTime? _dataRozpoczecia;
        private DateTime? _dataZakonczenia;

        public WpisHistorii(Pojazd pojazd, DateTime? dataRozpoczecia, DateTime? dataZakonczenia)
        {
            _pojazd = pojazd;
            _dataRozpoczecia = dataRozpoczecia;
            _dataZakonczenia = dataZakonczenia;
        }

        public Pojazd Pojazd { set => _pojazd = value;  get => _pojazd; }
        public DateTime? DataRozpoczecia { set => _dataRozpoczecia = value; get => _dataRozpoczecia; }
        public DateTime? DataZakonczenia { set => _dataZakonczenia = value; get => _dataZakonczenia; }

        public int CompareTo(WpisHistorii other)
        {
            return this._dataRozpoczecia.Value.CompareTo(other.DataRozpoczecia.Value);
        }
    }
}
