using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
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

    public class Jednoslad : Pojazd
    {
        protected Jednoslad(string nrRejstracyjny, Osoba wlasciciel, string marka, string kolor) : base(nrRejstracyjny, wlasciciel, marka, kolor)
        {
        }
    }

    public class Samochod : Pojazd
    {
        protected Samochod(string nrRejstracyjny, Osoba wlasciciel, string marka, string kolor) : base(nrRejstracyjny, wlasciciel, marka, kolor)
        {
        }
    }

    public class DuzySamochod : Samochod
    {
        protected DuzySamochod(string nrRejstracyjny, Osoba wlasciciel, string marka, string kolor) : base(nrRejstracyjny, wlasciciel, marka, kolor)
        {
        }
    }
}
