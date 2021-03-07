namespace CurrencyComApi.Queries
{
    public class ApiKeyRequiredQuery : ApiQueryBase
    {
        public string X_MBX_APIKEY { get; set; }

        public override string ToString()
        {
            Headers.Add("X-MBX-APIKEY", X_MBX_APIKEY);
            return base.ToString();
        }
    }
}
