using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public abstract class Cennik
    {
        public virtual Decimal ObliczKoszt(DateTime? dataRozpoczecia, DateTime? dataZakonczenia, MiejsceParkingowe mp, Osoba o)
        {
            return 0;
        }
    }

    public class DomyslnyCennik : Cennik
    {
        public override decimal ObliczKoszt(DateTime? dataRozpoczecia, DateTime? dataZakonczenia, MiejsceParkingowe mp, Osoba o)
        {
            Decimal baseCost = 1;
            double sec = (dataZakonczenia.Value - dataRozpoczecia.Value).TotalSeconds;
            int hour = (int)Math.Ceiling((sec / 3600) - 180);
            if (hour < 1)
                hour = 1;
            hour--;
            baseCost = baseCost + (hour * 1.5m);

            if (mp.DlaNiepelnosprawnych)
                baseCost = baseCost * 0.2m;

            if (mp.RodzajMiejsca == RodzajMiejsca.Duze)
                baseCost = Decimal.Multiply(baseCost, 1.2m);

            if (mp.RodzajMiejsca == RodzajMiejsca.Male)
                baseCost = Decimal.Multiply(baseCost, 0.7m);

            return baseCost;
        }
    }

    public class AbonamentCennik : Cennik
    {
        public override decimal ObliczKoszt(DateTime? dataRozpoczecia, DateTime? dataZakonczenia, MiejsceParkingowe mp, Osoba o)
        {
            return 0;
        }
    }
}
