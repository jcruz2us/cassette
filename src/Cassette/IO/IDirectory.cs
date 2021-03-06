﻿using System.Collections.Generic;
using System.IO;

namespace Cassette.IO
{
    public interface IDirectory
    {
        FileAttributes Attributes { get; }
        string FullPath { get; }
        IFile GetFile(string filename);
        IDirectory GetDirectory(string path);
        bool DirectoryExists(string path);
        IEnumerable<IDirectory> GetDirectories();
        IEnumerable<IFile> GetFiles(string searchPattern, SearchOption searchOption);
    }
}