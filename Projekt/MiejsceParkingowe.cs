using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
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
        public Pojazd ObecnyPojazd { get => _obecnyPojazd; }
        public DateTime? DataRozpoczecia { get => _dataRozpoczecia; }
        public RodzajMiejsca RodzajMiejsca { get => _rodzajMiejsca; }
        public bool DlaNiepelnosprawnych { get => _dlaNiepelnosprawnych; }
        public Historia Historia { get => _historia; }

        public void ZajmijMiejsce(Pojazd pojazd, DateTime? dataRozpoczecia = null)
        {
            if(dataRozpoczecia == null)
            {
                dataRozpoczecia = DateTime.Now;
            }
            if(_obecnyPojazd != null)
            {
                throw new MiejsceZajeteException();
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
            if (_obecnyPojazd == null)
            {
                throw new MiejsceWolneException();
            }
            double czasWSekundach = (dataZakonczenia - _dataRozpoczecia).Value.TotalSeconds;
            WpisHistorii nowaHistoria = new WpisHistorii(_obecnyPojazd, this, _dataRozpoczecia, dataZakonczenia);
            _historia.Add(nowaHistoria);
            _obecnyPojazd = null;
            _dataRozpoczecia = null;
            return czasWSekundach;
        }
    }

    [Serializable]
    internal class MiejsceWolneException : Exception
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
    internal class MiejsceZajeteException : Exception
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
