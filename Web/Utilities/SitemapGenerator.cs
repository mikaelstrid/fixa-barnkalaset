using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Web.Utilities
{
    // https://rehansaeed.com/dynamically-generating-sitemap-xml-for-asp-net-mvc/
    public class SitemapGenerator : ISitemapGenerator
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionAccessor;

        public SitemapGenerator(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionAccessor = actionAccessor;
        }

        public async Task<string> GetAsString(ICityRepository cityRepository)
        {
            var sitemapNodes = GetSitemapNodes(cityRepository);
            var xml = GetSitemapDocument(await sitemapNodes);
            return xml;
        }

        private async Task<IEnumerable<SitemapNode>> GetSitemapNodes(ICityRepository cityRepository)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionAccessor.ActionContext);

            var nodes = new List<SitemapNode>
            {
                new SitemapNode {Url = urlHelper.AbsoluteAction("Index", "Home"), Priority = 1},
                new SitemapNode {Url = urlHelper.AbsoluteAction("Index", "InvitationCards"), Priority = 1},
                new SitemapNode {Url = urlHelper.AbsoluteAction("Index", "Rsvp"), Priority = 1}
            };
            foreach (var city in await cityRepository.GetAll())
            {
                nodes.Add(new SitemapNode
                {
                    Url = urlHelper.AbsoluteAction("Index", "Arrangements", new { citySlug = city.Slug }),
                    Priority = 0.9,
                    Frequency = SitemapFrequency.Daily,
                    LastModified = city.Arrangements.Max(c => c.LastUpdatedUtc)
                });

                foreach (var arrangement in city.Arrangements)
                {
                    nodes.Add(new SitemapNode
                    {
                        Url = urlHelper.AbsoluteAction("Details", "Arrangements", new { citySlug = city.Slug, arrangementSlug = arrangement.Slug }),
                        Priority = 0.8,
                        Frequency = SitemapFrequency.Weekly,
                        LastModified = arrangement.LastUpdatedUtc
                    });
                }
            }
            
            return nodes;
        }

        private static string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var root = new XElement(xmlns + "urlset");
            foreach (var sitemapNode in sitemapNodes)
            {
                var urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }
            var document = new XDocument(root);
            return document.ToString();
        }

        private class SitemapNode
        {
            public SitemapFrequency? Frequency { get; set; }
            public DateTime? LastModified { get; set; }
            public double? Priority { get; set; }
            public string Url { get; set; }
        }

        private enum SitemapFrequency
        {
            Never,
            Yearly,
            Monthly,
            Weekly,
            Daily,
            Hourly,
            Always
        }
    }
}
