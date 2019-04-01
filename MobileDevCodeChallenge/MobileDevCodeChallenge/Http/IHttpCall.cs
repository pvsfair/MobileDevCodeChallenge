using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevCodeChallenge.Models;

namespace MobileDevCodeChallenge.Http
{
    public interface IHttpCall
    {
        IHttpCall baseUrl(string baseUrl);
        IHttpCall asGet(string urlMethod);
        IHttpCall asPost(string urlMethod);
        IHttpCall addHeader(string key, string value);
        IHttpCall addQueryString(string key, object value);
        IHttpCall addUrlSegment(string key, object value);
        IHttpCall addBody(string key, string value);
        IHttpCall addBodyAsJson(object body);
        Task<T> requestAsync<T>();
        Task requestAsync();
        IHttpCall addApiKey();
    }
}