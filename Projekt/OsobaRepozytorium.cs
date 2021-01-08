using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class OsobaRepozytorium
    {
        private Dictionary<int, Osoba> _repo = new Dictionary<int, Osoba>();

        public Dictionary<int, Osoba> Repo { get => _repo; }

        public OsobaRepozytorium()
        {

        }

        public void Add(Osoba osoba)
        {
            
            foreach (var o in _repo.Values)
            {
                if (o.Pesel == osoba.Pesel)
                    throw new OsobaIstniejeException("Taki same pesel");
                if (o.Email == osoba.Email)
                    throw new OsobaIstniejeException("Taki same Email");
            }
            _repo.Add(osoba.Id, osoba);
        }

        public void Remove(int id)
        {
            _repo.Remove(id);
        }

        public void Remove(Osoba osoba)
        {
            _repo.Remove(osoba.Id);
        }
    }

    [Serializable]
    internal class OsobaIstniejeException : Exception
    {
        public OsobaIstniejeException()
        {
        }

        public OsobaIstniejeException(string message) : base(message)
        {
        }

        public OsobaIstniejeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OsobaIstniejeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
