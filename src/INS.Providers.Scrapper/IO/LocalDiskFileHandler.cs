namespace INS.Provider.Scrapper.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class LocalDiskFileHandler : IFileHandler
{

    private readonly string _parentFolder = Path.GetTempPath();

    public string ParentFolder { 
        get { return _parentFolder; } 
    }


    public LocalDiskFileHandler() : this(Path.GetTempPath())
    {
    
    }

    public LocalDiskFileHandler(string parentFolder) 
    {

        if (String.IsNullOrEmpty(parentFolder))
        {
            throw new ArgumentException("Download path has to be provided");
        }

        if (parentFolder.Length < Constants.minDownloadsPathLenght)
        {
            throw new ArgumentException($"Download path has to be greater than {Constants.minDownloadsPathLenght} characters");
        }

        if (parentFolder.Length > Constants.maxDownloadsPathLenght)
        {
            throw new ArgumentException($"Download path has to be less than {Constants.maxDownloadsPathLenght} characters");
        }

        _parentFolder = parentFolder;
    }

    public async Task<string> CreateFile(ReadFileAsStringDelegate readAsStringDelegate, string? filename = "")
    {
        // TODO: Extract this logic in separate value class
        var subFolder = YearMonthDayCurrentDateFormat();
        var filePath = String.IsNullOrEmpty(filename) ? 
            $"{_parentFolder}{subFolder}{Path.DirectorySeparatorChar}{Guid.NewGuid()}.tmp" 
            : 
            $"{_parentFolder}{subFolder}{Path.DirectorySeparatorChar}{filename}";

        Directory.CreateDirectory($"{_parentFolder}{subFolder}");

        var ms = await readAsStringDelegate();

        await using var newFile = File.Create(filePath);
        ms.Seek(0, SeekOrigin.Begin);
        ms.CopyTo(newFile);

        return filePath;
    }

    private string YearMonthDayCurrentDateFormat() => DateTime.Now.ToString("yyyy-MM-dd");
    
}
