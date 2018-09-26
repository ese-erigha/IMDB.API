using System;
using System.Threading.Tasks;

namespace IMDB.Api.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
