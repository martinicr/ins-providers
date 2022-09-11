namespace INS.Provider.Scrapper.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Moq;
using FluentAssertions;
using INS.Provider.Scrapper.IO;

public class FileDownloaderTest : IDisposable
{
    Mock<IFileHandler> _fileHandler;

    FileDownloader downloader;

    public FileDownloaderTest()
    {
        _fileHandler = new Mock<IFileHandler>();

        downloader = new FileDownloader(_fileHandler.Object);
    }

    public void Dispose()
    {
        _fileHandler.Reset();
    }


    [Fact]
    public void WhenDownloadWithNullUrlAsInput_ThenException()
    {

        downloader.Invoking(d => d.DownloadAsync(null))
            .Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage("A valid Url has to be provided");

    }

    [Fact]
    public async Task WhenValidUrlIsProvided_ThenDownloadContentsInFile()
    {
        var filePathExpected = "c:\\ins-downloads\\file.html";
        var url = new Uri("https://microsoft.com");
        
        var handlerMock = MockHttpMessageHandler<string>.SetupBasicGetResourceList();
        var httpClient = new HttpClient(handlerMock.Object);

        var downloader = new FileDownloader(httpClient, _fileHandler.Object);

        _fileHandler.Setup(f => f.CreateFile(It.IsAny<ReadFileAsStringDelegate>(), "").Result).Returns(filePathExpected);

        var actual = await downloader.DownloadAsync(url);

        _fileHandler.Verify(f => f.CreateFile(It.IsAny<ReadFileAsStringDelegate>(), ""), Times.Once);

        actual.FilePath.Should().Be(filePathExpected); 
        actual.DownloadedDate.Should().BeBefore(DateTime.Now);
    }
}

