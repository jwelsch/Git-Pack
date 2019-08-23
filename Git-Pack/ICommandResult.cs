using System;

namespace Git_Pack
{
    public interface ICommandResult
    {
        Exception Error
        {
            get;
        }

        bool HasError
        {
            get;
        }
    }
}
