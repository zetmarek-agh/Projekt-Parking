using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public abstract class Pojazd : IEquatable<Pojazd>, IComparable<Pojazd>
    {
        private string _nrRejstracyjny;
        private Osoba _wlasciciel;
        private string _marka;
        private string _kolor;

        protected Pojazd(string nrRejstracyjny, Osoba wlasciciel, string marka, string kolor)
        {
            _nrRejstracyjny = nrRejstracyjny;
            _wlasciciel = wlasciciel;
            _marka = marka;
            _kolor = kolor;
        }

        public string NrRejstracyjny { get => _nrRejstracyjny; set => _nrRejstracyjny = value; }
        public Osoba Wlasciciel { get => _wlasciciel; set => _wlasciciel = value; }
        public string Marka { get => _marka; set => _marka = value; }
        public string Kolor { get => _kolor; set => _kolor = value; }

        public int CompareTo(Pojazd other)
        {
            return _nrRejstracyjny.CompareTo(other.NrRejstracyjny);
        }

        public bool Equals(Pojazd other)
        {
            return _nrRejstracyjny.Equals(other.NrRejstracyjny);
        }
    }

    [Serializable]
    public class Jednoslad : Pojazd
    {
        public Jednoslad(string nrRejstracyjny, Osoba wlasciciel, string marka, string kolor) : base(nrRejstracyjny, wlasciciel, marka, kolor)
        {
        }
    }

    [Serializable]
    public class Samochod : Pojazd
    {
        public Samochod(string nrRejstracyjny, Osoba wlasciciel, string marka, string kolor) : base(nrRejstracyjny, wlasciciel, marka, kolor)
        {
        }
    }

    [Serializable]
    public class DuzySamochod : Samochod
    {
        public DuzySamochod(string nrRejstracyjny, Osoba wlasciciel, string marka, string kolor) : base(nrRejstracyjny, wlasciciel, marka, kolor)
        {
        }
    }
}
