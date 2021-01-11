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
        private MiejsceParkingowe _miejsceParkingowe;
        private DateTime? _dataRozpoczecia;
        private DateTime? _dataZakonczenia;

        public WpisHistorii(Pojazd pojazd, MiejsceParkingowe miejsceParkingowe, DateTime? dataRozpoczecia, DateTime? dataZakonczenia)
        {
            _pojazd = pojazd;
            _miejsceParkingowe = miejsceParkingowe;
            _dataRozpoczecia = dataRozpoczecia;
            _dataZakonczenia = dataZakonczenia;
        }

        public Pojazd Pojazd { get => _pojazd; }
        public MiejsceParkingowe MiejsceParkingowe { get => _miejsceParkingowe; }
        public DateTime? DataRozpoczecia { get => _dataRozpoczecia; }
        public DateTime? DataZakonczenia { get => _dataZakonczenia; }

        public int CompareTo(WpisHistorii other)
        {
            return this._dataRozpoczecia.Value.CompareTo(other.DataRozpoczecia.Value);
        }
    }
}
