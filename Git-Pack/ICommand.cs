using System;

namespace Git_Pack
{
    public interface ICommand
    {
        ICommandResult Execute(Action<string> reporter = null);
    }
}
