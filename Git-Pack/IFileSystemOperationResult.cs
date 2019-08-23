using System;

namespace Git_Pack
{
    public interface IFileSystemOperationResult
    {
        Exception Error
        {
            get;
        }

        string ToString();
    }
}
