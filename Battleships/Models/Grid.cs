using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public class Grid
    {
        public bool IsHit { get; set; }
        public bool IsShip { get; set; }
        public override string ToString()
        {
            if (IsHit)
            {
                return IsShip ? "S" : "M";
            }
            return "X";
        }
    }
}
