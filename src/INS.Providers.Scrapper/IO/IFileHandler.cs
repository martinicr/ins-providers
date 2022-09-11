namespace INS.Provider.Scrapper.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public delegate Task<Stream> ReadFileAsStringDelegate();

public interface IFileHandler
{
    Task<string> CreateFile(ReadFileAsStringDelegate del, string? filename = "");
}
