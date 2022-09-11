namespace INS.Provider.Scrapper.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

public class LocalDiskFileHandlerTest : IDisposable
{

    LocalDiskFileHandler _fileHandler;

    public LocalDiskFileHandlerTest() 
    {
        _fileHandler = new LocalDiskFileHandler($"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}ins-providers{Path.DirectorySeparatorChar}"); 
    }
    public void Dispose()
    {
        if (Directory.Exists(_fileHandler.ParentFolder))
        {
            Directory.Delete(_fileHandler.ParentFolder, recursive: true);
        }
    }

    [Fact]
    public void WhenNullDownloadsFolder_ThenException()
    {
        Assert.Throws<ArgumentException>(() => new LocalDiskFileHandler(null));
    }

    [Fact]
    public void WhenEmptyDownloadsFolder_ThenException()
    {
        Assert.Throws<ArgumentException>(() => new LocalDiskFileHandler(""));
    }

    [Fact]
    public void WhenInvalidMinLenghtDownloadsFolder_ThenException()
    {
        Assert.Throws<ArgumentException>(() => new LocalDiskFileHandler("c:\a"));
    }

    [Fact]
    public async Task GivenFileAsStringStream_ThenCreateFile()
    {

        byte[] byteArray = Encoding.ASCII.GetBytes("<html><head></head><body></body>");
        Stream stream = new MemoryStream(byteArray);

        var actual = await _fileHandler.CreateFile(() => {
            return Task.FromResult(stream);
        });

        actual.Should().EndWith(".tmp");

        File.Exists(actual).Should().BeTrue();

    }

}
