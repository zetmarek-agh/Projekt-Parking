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
            TextWriter tw = new StreamWriter($"{nazwa}");
            JsonSerializer xs = new JsonSerializer();
            xs.TypeNameHandling = TypeNameHandling.Auto; //zapisz informację o typach żeby klasy abstrakcyjne działały
            xs.Serialize(tw, this);
            tw.Close();
        }

        public static Parking OdczytajJson(string nazwa)
        {
            JsonSerializer xs = new JsonSerializer();
            xs.TypeNameHandling = TypeNameHandling.Auto;
            using (StreamReader file = File.OpenText($"{nazwa}"))
            {
                return (Parking)xs.Deserialize(file, typeof(Parking));
            }
        }
    }
}
