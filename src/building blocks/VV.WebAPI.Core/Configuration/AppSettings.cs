namespace VV.WebAPI.Core.Configuration
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }

        public string AuthUrl { get; set; }
        public string CatalogUrl { get; set; }
    }
}
