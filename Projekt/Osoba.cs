using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class Osoba : IEquatable<Osoba>, IComparable<Osoba>
    {
        private static int _globalId = 0;
        private int _id;
        private string _imie;
        private string _nazwisko;
        private string _pesel;
        private string _email;
        private string _telefon;
        private bool _niepelnosprawna;
        private Cennik cennik = new DomyslnyCennik();
        private List<Pojazd> pojazdy = new List<Pojazd>();

        private Osoba()
        {
            _id = _globalId;
            _globalId++;
        }

        public Osoba(string imie, string nazwisko, string pesel, string email, string telefon, bool niepelnosprawna = false) : this()
        {
            _imie = imie;
            _nazwisko = nazwisko;
            Pesel = pesel;
            Email = email;
            _telefon = telefon;
            _niepelnosprawna = niepelnosprawna;
        }

        public int Id { get => _id; set => _id = value; }
        public string Imie { get => _imie; set => _imie = value; }
        public string Nazwisko { get => _nazwisko; set => _nazwisko = value; }

        public bool Niepelnosprawna { get => _niepelnosprawna; set => _niepelnosprawna = value; }
        public string Pesel
        {
            get { return _pesel; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Pesel nie może być null lub pusty");
                if (value.Length != 11)
                    throw new ArgumentException("Pesel ma złą długość");
                int sum = 0;
                try
                {
                    sum = 1 * int.Parse(value[0].ToString()) + 3 * int.Parse(value[1].ToString()) + 7 * int.Parse(value[2].ToString()) + 9 * int.Parse(value[3].ToString()) + 1 * int.Parse(value[4].ToString()) + 3 * int.Parse(value[5].ToString()) + 7 * int.Parse(value[6].ToString()) + 9 * int.Parse(value[7].ToString()) + 1 * int.Parse(value[8].ToString()) + 3 * int.Parse(value[9].ToString()) + 1 * int.Parse(value[10].ToString());
                }
                catch
                {
                    throw new ArgumentException("Pesel nie ma tylko cyfr");
                }
                if (sum % 10 != 0)
                    throw new ArgumentException("Pesel ma złą sumę kontrolną");
                _pesel = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Email nie może być null lub pusty");
                try
                {
                    new System.Net.Mail.MailAddress(value);
                }
                catch
                {
                    throw new ArgumentException("Email jest zły");
                }
                _email = value;
            }
        }
        public string Telefon { get => _telefon; set => _telefon = value; }
        public Cennik Cennik { get => cennik; set => cennik = value; }

        public List<Pojazd> Pojazdy { get => pojazdy; }

        public bool Equals(Osoba other)
        {
            return _pesel.Equals(other.Pesel);
        }

        public int CompareTo(Osoba other)
        {
            int result = _nazwisko.CompareTo(other.Nazwisko);
            if (result == 0)
                return _imie.CompareTo(other.Imie);
            return result;
        }
    }
}
