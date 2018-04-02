using System;

namespace Battleships.Models
{
    public class PlayerData
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public double TimePlayed { get; set; }
        public Guid ID { get; set; }
    }
}
