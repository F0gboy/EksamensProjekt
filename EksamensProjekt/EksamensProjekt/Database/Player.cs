using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Database
{
    internal class Player
    {
        public int PlayerId { get; set; }
        public int TotalRound { get; set; }
        public int TotalKills { get; set; }        
        public int TotalMoney { get; set; }
        public int LoginId { get; set; }
    }
}
