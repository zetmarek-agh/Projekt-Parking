using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Osoba : IEquatable<Osoba>, IComparable<Osoba>
    {
        private static int _globalId = 0;
        private int _id;
        private string _imie;
        private string _nazwisko;
        private string _pesel;
        private string _email;
        private string _telefon;

        private Osoba()
        {
            _id = _globalId;
            _globalId++;
        }

        public Osoba(string imie, string nazwisko, string pesel, string email, string telefon) : this()
        {
            _imie = imie;
            _nazwisko = nazwisko;
            _pesel = pesel;
            _email = email;
            _telefon = telefon;
        }

        public int Id { get => _id; set => _id = value; }
        public string Imie { get => _imie; set => _imie = value; }
        public string Nazwisko { get => _nazwisko; set => _nazwisko = value; }
        public string Pesel { get => _pesel; set => _pesel = value; }
        public string Email { get => _email; set => _email = value; }
        public string Telefon { get => _telefon; set => _telefon = value; }

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
