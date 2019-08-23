using System;

namespace Git_Pack
{
    public class CommandResult : ICommandResult
    {
        public Exception Error
        {
            get;
            set;
        }

        public bool HasError
        {
            get { return this.Error != null; }
        }

        public CommandResult(Exception error = null)
        {
            this.Error = error;
        }
    }
}
