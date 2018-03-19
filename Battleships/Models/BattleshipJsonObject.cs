using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Models
{
    public class BattleshipJsonObject
    {
        public string UserName { get; set; }
        public int PlayedTime { get; set; }
        public int Score { get; set; }
        public Guid Id { get; set; }
    }
}
