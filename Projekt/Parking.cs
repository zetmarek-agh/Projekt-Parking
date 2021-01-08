using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Parking
    {
        private Dictionary<int, MiejsceParkingowe> _repo = new Dictionary<int, MiejsceParkingowe>();

        public Dictionary<int, MiejsceParkingowe> Repo { get => _repo; }

        public Parking()
        {

        }
    }
}
