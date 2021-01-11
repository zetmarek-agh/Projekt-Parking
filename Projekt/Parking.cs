using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class Parking
    {
        private Dictionary<int, MiejsceParkingowe> _repo = new Dictionary<int, MiejsceParkingowe>();

        public Dictionary<int, MiejsceParkingowe> Repo { get => _repo; }

        public List<MiejsceParkingowe> ListRepo { get => _repo.Values.ToList(); }

        public Parking()
        {

        }

        public void Add(MiejsceParkingowe mp)
        {

            _repo.Add(mp.Id, mp);
        }
        public void ZapiszJson(string nazwa)
        {
            TextWriter tw = new StreamWriter($"{nazwa}.json");
            JsonSerializer xs = new JsonSerializer();
            xs.Serialize(tw, this);
            tw.Close();
        }

        public static Parking OdczytajJson(string nazwa)
        {
            TextReader tr = new StreamReader($"{nazwa}.json");
            string content = tr.ReadToEnd();
            tr.Close();
            return JsonConvert.DeserializeObject<Parking>(content);
        }
    }
}
