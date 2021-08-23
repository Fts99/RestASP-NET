using RestASPNET.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestASPNET.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
