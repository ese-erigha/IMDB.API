using System;
namespace IMDB.Api.Services.Interfaces
{
    public interface ICacheService
    {
        string Get(string key);

        void Set<T>(string key, T data) where T : class;
    }
}
