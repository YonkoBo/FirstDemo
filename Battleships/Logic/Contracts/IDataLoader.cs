using Battleships.Models;
using System.Collections.Generic;

namespace Battleships.Logic.Contracts
{
    public interface IDataLoader
    {
        List<PlayerData> LoadData(IPlayerFactory playerFactory);
    }
}
