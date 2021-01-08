using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Historia 
    {
        private List<WpisHistorii> _zapisHistorii = new List<WpisHistorii>();

        public Historia()
        {

        }

        public void Add(WpisHistorii wh)
        {
            _zapisHistorii.Add(wh);
        }

        public List<WpisHistorii> ZapisHistorii { get => _zapisHistorii; }
    }
}
