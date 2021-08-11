using RestASPNET.Hypermedia;
using RestASPNET.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestASPNET.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public long Id { get; set; }

        public string Author { get; set; }

        public System.DateTime LaunchDate { get; set; }

        public decimal Price { get; set; }

        public string Title { get; set; }

        public List<HyperMediaLink> Links { get ; set ; } = new List<HyperMediaLink>();
    }
}
