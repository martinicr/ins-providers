namespace INS.Provider.Scrapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using INS.Provider.Scrapper.Parser;

using Microsoft.Extensions.Logging;

using Moq;
using FluentAssertions;
using System.Xml;
using INS.Providers.Scrapper;

public class INSProvidersPageScrapperTest : IDisposable
{

	Mock<ILogger<INSProvidersPageScrapper>> _logger;
	//Mock<IDownloader> _downloader;
	Mock<IParserStrategy> _parser;

	INSProvidersPageScrapper scrapper;
	
	public INSProvidersPageScrapperTest()
	{
		_logger = new Mock<ILogger<INSProvidersPageScrapper>>();
		//_downloader = new Mock<IDownloader>();
		_parser = new Mock<IParserStrategy>();

		scrapper = new INSProvidersPageScrapper(_logger.Object, _parser.Object);
	}

	public void Dispose()
	{
		_logger.Reset();
		//_downloader.Reset();
		_parser.Reset();
	}

	//[Fact]
	//public void WhenDownloadWithNullUrlAsInput_ThenException()
	//{
	//	scrapper.Invoking(s => s.GetFromUrlAsync(null))
	//		.Should()
	//		.ThrowAsync<ArgumentException>()
	//		.WithMessage("Url has to be provided");
	//}

	[Fact]
	public async Task ProcessScrapperCommand()
	{
		var url = new Uri("https://microsoft.com");
		var query = "*";

		XmlDocument doc = new XmlDocument();

		doc.CreateElement("elem1");

		_parser.Setup(p => p.Parse(It.IsAny<Uri>(), It.IsAny<string>()).Result).Returns(
			new List<PageElement>()
			{
				new PageElement()
				{
					ResourceUrl = new Uri("https://wikipedia.com")
				},
                new PageElement()
                {
                    ResourceUrl = new Uri("https://bbc.com")
                }
            }
		); ; ; 

        await scrapper.Parse(url, query);

		_parser.Verify(p => p.Parse(url, query), Times.Once);
	}
}
