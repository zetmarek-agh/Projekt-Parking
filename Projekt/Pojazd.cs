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
        private string _marka;
        private string _kolor;

        protected Pojazd(string nrRejstracyjny, string marka, string kolor)
        {
            _nrRejstracyjny = nrRejstracyjny;
            _marka = marka;
            _kolor = kolor;
        }
        public string NrRejstracyjny { get => _nrRejstracyjny; set => _nrRejstracyjny = value; }
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

        public override string ToString()
        {
            return $"{this.GetType().ToString()}: {NrRejstracyjny} {Kolor} {Marka}";
        }
    }

    [Serializable]
    public class Jednoslad : Pojazd
    {
        public Jednoslad(string nrRejstracyjny, string marka, string kolor) : base(nrRejstracyjny, marka, kolor)
        {
        }
    }

    [Serializable]
    public class Samochod : Pojazd
    {
        public Samochod(string nrRejstracyjny, string marka, string kolor) : base(nrRejstracyjny, marka, kolor)
        {
        }
    }

    [Serializable]
    public class DuzySamochod : Samochod
    {
        public DuzySamochod(string nrRejstracyjny, string marka, string kolor) : base(nrRejstracyjny, marka, kolor)
        {
        }
    }
}
