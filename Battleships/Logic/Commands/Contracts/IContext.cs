namespace Battleships.Logic.Commands.Contracts
{
    public interface IContext
    {
        void ExecuteCommands(UserCommands command);
    }
}
