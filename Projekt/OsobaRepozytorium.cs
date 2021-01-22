using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class OsobaRepozytorium
    {
        private Dictionary<int, Osoba> _repo = new Dictionary<int, Osoba>();

        public Dictionary<int, Osoba> Repo { get => _repo; }

        public List<Osoba> List { get => _repo.Values.ToList(); }

        public OsobaRepozytorium()
        {

        }

        public void Add(Osoba osoba)
        {
            
            foreach (var o in _repo.Values)
            {
                if (o.Pesel == osoba.Pesel)
                    throw new OsobaIstniejeException("Taki sam pesel");
                if (o.Email == osoba.Email)
                    throw new OsobaIstniejeException("Taki sam Email");
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

        public void ZapiszJson(string nazwa)
        {
            TextWriter tw = new StreamWriter($"{nazwa}");
            JsonSerializer xs = new JsonSerializer();
            xs.TypeNameHandling = TypeNameHandling.Auto; //zapisz informację o typach żeby klasy abstrakcyjne działały
            xs.Serialize(tw, this);
            tw.Close();
        }

        public static OsobaRepozytorium OdczytajJson(string nazwa)
        {
            TextReader tr = new StreamReader($"{nazwa}");
            string content = tr.ReadToEnd();
            tr.Close();
            return JsonConvert.DeserializeObject<OsobaRepozytorium>(content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
    }

    [Serializable]
    public class OsobaIstniejeException : Exception
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
