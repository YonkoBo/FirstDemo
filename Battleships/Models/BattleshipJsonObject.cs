using System;

namespace Battleships.Models
{
    public class BattleshipJsonObject
    {
        public string UserName { get; set; }
        public double PlayedTime { get; set; }
        public int Score { get; set; }
        public Guid Id { get; set; }
    }
}
