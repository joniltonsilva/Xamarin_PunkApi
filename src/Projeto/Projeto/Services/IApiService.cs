using System;
using System.Threading.Tasks;

namespace Projeto.Services
{
    public interface IApiService : IDisposable
    {
        Task<T> GetAsync<T>(string url, Action<T> callback = null);
    }
}
