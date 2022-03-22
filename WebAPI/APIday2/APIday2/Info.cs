using Microsoft.OpenApi.Models;

namespace APIday2
{
    internal class Info : OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public object Contact { get; set; }
        public object License { get; set; }
    }
}