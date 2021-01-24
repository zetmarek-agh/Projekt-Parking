using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class MiejsceParkingowe
    {
        private static int _globalId = 0;
        private int _id;
        private Pojazd _obecnyPojazd;
        private DateTime? _dataRozpoczecia;
        private RodzajMiejsca _rodzajMiejsca;
        private bool _dlaNiepelnosprawnych;

        private Historia _historia = new Historia();

        public MiejsceParkingowe(RodzajMiejsca rodzajMiejsca, bool dlaNiepelnosprawnych) : this()
        {
            _rodzajMiejsca = rodzajMiejsca;
            _dlaNiepelnosprawnych = dlaNiepelnosprawnych;
        }

        private MiejsceParkingowe()
        {
            _id = _globalId;
            _globalId++;
        }

        public int Id { get => _id; }
        public Pojazd ObecnyPojazd { set => _obecnyPojazd = value; get => _obecnyPojazd; }
        public DateTime? DataRozpoczecia { set => _dataRozpoczecia = value;  get => _dataRozpoczecia; }
        public RodzajMiejsca RodzajMiejsca { get => _rodzajMiejsca; }
        public bool DlaNiepelnosprawnych { get => _dlaNiepelnosprawnych; }
        public Historia Historia { set => _historia = value; get => _historia; }
        public bool Zajete { get => _obecnyPojazd != null; }
        public string Repr { get => ToString(); }

        public void ZajmijMiejsce(Pojazd pojazd, Osoba wlasciciel, DateTime? dataRozpoczecia = null)
        {
            if(dataRozpoczecia == null)
            {
                dataRozpoczecia = DateTime.Now;
            }
            if(this.Zajete)
            {
                throw new MiejsceZajeteException();
            }
            if(pojazd.GetType() == typeof(DuzySamochod) && RodzajMiejsca != RodzajMiejsca.Duze)
            {
                throw new ZlyTypMiejscaException("Za małe miejsce parkingowe");
            }
            if (pojazd.GetType() == typeof(Samochod) && RodzajMiejsca == RodzajMiejsca.Male)
            {
                throw new ZlyTypMiejscaException("Za małe miejsce parkingowe");
            }
            if (!wlasciciel.Niepelnosprawna && DlaNiepelnosprawnych)
            {
                throw new ZlyTypMiejscaException("Osoba musi być niepełnosprawna żeby korzystać z miejsca dla niepełnosprawnych");
            }
            _obecnyPojazd = pojazd;
            _dataRozpoczecia = dataRozpoczecia;
        }

        public double OpuscMiejsce(DateTime? dataZakonczenia = null)
        {
            if (dataZakonczenia == null)
            {
                dataZakonczenia = DateTime.Now;
            }
            if(dataZakonczenia < _dataRozpoczecia)
            {
                throw new ArgumentException("dataZakonczenia musi być późniejsza niż dataRozpoczecia!");
            }
            if (!this.Zajete)
            {
                throw new MiejsceWolneException();
            }
            double czasWSekundach = (dataZakonczenia - _dataRozpoczecia).Value.TotalSeconds;
            WpisHistorii nowaHistoria = new WpisHistorii(_obecnyPojazd, _dataRozpoczecia, dataZakonczenia);
            _historia.Add(nowaHistoria);
            _obecnyPojazd = null;
            _dataRozpoczecia = null;
            return czasWSekundach;
        }

        public override string ToString()
        {
            return $"ID: {Id}, {RodzajMiejsca}, DlaNiepelnosprawnych: {DlaNiepelnosprawnych}, Zajęte? {Zajete}";
        }
    }

    [Serializable]
    public class ZlyTypMiejscaException : Exception
    {
        public ZlyTypMiejscaException()
        {
        }

        public ZlyTypMiejscaException(string message) : base(message)
        {
        }

        public ZlyTypMiejscaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ZlyTypMiejscaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class MiejsceWolneException : Exception
    {
        public MiejsceWolneException()
        {
        }

        public MiejsceWolneException(string message) : base(message)
        {
        }

        public MiejsceWolneException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MiejsceWolneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class MiejsceZajeteException : Exception
    {
        public MiejsceZajeteException()
        {
        }

        public MiejsceZajeteException(string message) : base(message)
        {
        }

        public MiejsceZajeteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MiejsceZajeteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public enum RodzajMiejsca
    {
        Male, Zwykle, Duze, Specjalne
    }
}
