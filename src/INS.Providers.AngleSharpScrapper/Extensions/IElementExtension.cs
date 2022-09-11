namespace INS.Providers.AngleSharpScrapper.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AngleSharp.Dom;
using INS.Providers.Scrapper;

internal static class IElementExtension
{

    public static PageElement ToPageElement(this IElement element) 
    {
        return new PageElement() {
            ResourceUrl = new Uri(
                element
                .Attributes
                .Single(a => a.Name.Equals("href")).Value
            )
        };
    }

}
