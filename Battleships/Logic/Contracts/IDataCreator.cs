namespace Battleships.Logic.Contracts
{
    public interface IDataCreator
    {
        void CreateNewPlayerFile(string playerName, double timePlayed, int score, IPlayerFactory playerFactory);
    }
}
