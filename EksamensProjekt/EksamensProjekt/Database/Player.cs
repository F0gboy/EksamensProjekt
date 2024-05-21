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
        public int Round { get; set; }
        public int Kills { get; set; }
        public int FishMoney { get; set; }
        public int totalMoney { get; set; }
        public string Name { get; set; }
        public int TilesSeed { get; set; }
    }
}
