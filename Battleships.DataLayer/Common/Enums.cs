using System.ComponentModel;

namespace Battleships.DataLayer.Common
{
    public enum OccupationType
    {
        [Description(".")]
        Empty,

        [Description("B")]
        Battleship,

        [Description("C")]
        Cruiser,

        [Description("D")]
        Destroyer,

        [Description("S")]
        Submarine,

        [Description("A")]
        Carrier,

        [Description("X")]
        Hit,

        [Description("M")]
        Miss
    }
    public enum GameStatus
    {
        Play,
        Miss,
        Hit,
        Sunk,
        Error,
        Show,
        End,
        Sorting
    }
}
