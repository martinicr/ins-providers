namespace INS.Provider.AngleSharpScrapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using INS.Provider.AngleSharpScrapper.Parser;

using Moq;
using FluentAssertions;
using WireMock.Server;
using WireMock.ResponseBuilders;
using WireMock.RequestBuilders;

public class AngleSharpScrapperTest : IDisposable
{
    WireMockServer _server;

    public AngleSharpScrapperTest()
    {
        _server = WireMockServer.Start();
    }

    public void Dispose()
    {
        _server.Stop();
    }

    [Fact]
    public async Task WhenUrlAndQueryIsProvided_ThenParseResource()
    {

        _server
          .Given(Request.Create().WithPath("/valid-page").UsingGet())
          .RespondWith(
            Response.Create()
              .WithStatusCode(200)
              .WithHeader("Content-Type", "text/html")
              .WithBodyFromFile("ins-providers-page.html")
          );


        var scrapperStrategy = new INSProvidersPageAngleSharpStrategy(new HttpClient());

        var url = new Uri($"{_server.Urls[0]}/valid-page");
        var query = "body a[href$=\".xlsx\"]";
       

        var actual = await scrapperStrategy.Parse(url, query);

        actual
            .Should()
            .Satisfy(
                e => e.ResourceUrl.ToString() == "https://www.ins-cr.com/media/8453/atencion-médica-primaria-asegurados-setiembre_andrea-mora-acuña.xlsx",
                e => e.ResourceUrl.ToString() == "https://www.ins-cr.com/media/8455/red-preferente-clientes-setiembre-2021_andrea-mora-acuña.xlsx",
                e => e.ResourceUrl.ToString() == "https://www.ins-cr.com/media/8456/registro-de-proveedores-clientes-setiembre-20_andrea-mora-acuña.xlsx",
                e => e.ResourceUrl.ToString() == "https://www.ins-cr.com/media/8454/plan-16-setiembre-2021_andrea-mora-acuña.xlsx"
            );      

    }
}
