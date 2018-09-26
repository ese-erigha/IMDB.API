using System;
namespace IMDB.Api.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string AccessTokenExpireMinutes { get; set; }

        public string RefreshTokenExpireDays { get; set; }

        public string CacheExpirationInMinutes { get; set; }
    }
}
