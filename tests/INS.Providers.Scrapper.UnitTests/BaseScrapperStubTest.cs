namespace INS.Provider.Scrapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;
using INS.Provider.Scrapper.Http;

using Microsoft.Extensions.Logging;

using Moq;

public class BaseScrapperStubTest
{

    //Mock<ILogger> _logger;
    //Mock<IDownloader> _downloader;

    //BaseScrapperStub _scrapper;

    //public BaseScrapperStubTest()
    //{
    //    _logger = new Mock<ILogger>();
    //    _downloader = new Mock<IDownloader>();

    //    _scrapper = new BaseScrapperStub(_logger.Object, _downloader.Object);
    //}

    //public void Dispose()
    //{
    //    _logger.Reset();
    //    _downloader.Reset();
    //}

    //[Fact]
    //public void WhenDownloadWithNullUrlAsInput_ThenException()
    //{

    //    _scrapper.Invoking(s => s.GetFromUrlAsync(null))
    //        .Should()
    //        .ThrowAsync<ArgumentException>()
    //        .WithMessage("Url has to be provided");
    //}

    //[Fact]
    //public async Task WhenValidUrlIsProvided_ThenDownloadContentsInFile()
    //{
    //    var url = new Uri("https://microsoft.com");
    //    //var handlerMock = MockHttpMessageHandler<string>.SetupBasicGetResourceList();
    //    //var httpClient = new HttpClient(handlerMock.Object);

    //    var scrapper = new BaseScrapperStub(_logger.Object, _downloader.Object);

    //    await scrapper.GetFromUrlAsync(url);
    //}
}
