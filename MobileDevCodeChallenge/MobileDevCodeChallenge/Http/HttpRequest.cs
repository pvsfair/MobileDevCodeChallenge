using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace MobileDevCodeChallenge.Http
{
    public class HttpRequest:IHttpCall
    {
        private HttpClient Client { get; }
        private Dictionary<string, string> BodyContent { get; set; }
        private Dictionary<string, string> Headers { get; set; }
        private Dictionary<string, string> UrlQueries { get; set; }
        private Dictionary<string, string> UrlSegments { get; set; }

        protected string BaseUrl { get; set; }
        protected string MethodUrl { get; set; }
        protected HttpMethod Method { get; set; }
        protected object Body { get; set; }

        public HttpRequest()
        {
            Client = new HttpClient();

            BodyContent = new Dictionary<string, string>();
            Headers = new Dictionary<string, string>();
            UrlQueries = new Dictionary<string, string>();
            UrlSegments = new Dictionary<string, string>();
        }

        public IHttpCall baseUrl(string baseUrl)
        {
            BaseUrl = baseUrl;
            return this;
        }

        public IHttpCall asGet(string urlMethod)
        {
            Method = HttpMethod.Get;
            MethodUrl = urlMethod;
            return this;
        }

        public IHttpCall asPost(string urlMethod)
        {
            Method = HttpMethod.Post;
            MethodUrl = urlMethod;
            return this;
        }

        public IHttpCall addHeader(string key, string value)
        {
            Headers.Add(key, value);
            return this;
        }

        public IHttpCall addQueryString(string key, object value)
        {
            UrlQueries.Add(key, value.ToString());
            return this;
        }

        public IHttpCall addUrlSegment(string key, object value)
        {
            UrlSegments.Add(key, value.ToString());
            return this;
        }

        public IHttpCall addBody(string key, string value)
        {
            BodyContent.Add(key, value);
            return this;
        }

        public IHttpCall addBodyAsJson(object body)
        {
            throw new System.NotImplementedException();
//            addHeader("Accept", "application/json");
//            Body = JsonConvert.SerializeObject(body);
//            return this;
        }

        public async Task<T> requestAsync<T>()
        {
            try
            {
                var request = getRequet();

                var response = await Client.SendAsync(request).ConfigureAwait(false);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.OK)
                {
                    var r = JsonConvert.DeserializeObject<T>(responseContent);

                    return r;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return default(T);
        }

        public async Task requestAsync()
        {
            await requestAsync<object>();
        }

        private HttpRequestMessage getRequet()
        {
            var request = new HttpRequestMessage();

            if(Method == null)
                throw new Exception("Request Method was not set.");

            request.Method = Method;

            request.Content = new FormUrlEncodedContent(BodyContent);//TODO: add here the json body

            Headers.ForEach(h => request.Headers.Add(h.Key, h.Value));

            var urlMethodWithSegmentString = getUrlSegment(MethodUrl);
            var urlParametersString = getUrlParameters();

            var uri = Uri.EscapeUriString($"{BaseUrl}{urlMethodWithSegmentString}?{urlParametersString}");
            request.RequestUri = new Uri(uri);
            
            return request;
        }

        private string getUrlSegment(string urlMethod)
        {
            var urlMethodAltered = urlMethod;
            foreach (var segment in UrlSegments)
            {
                if (!urlMethodAltered.Contains(segment.Key))
                    throw new Exception("Segment not found in the URL.");

                urlMethodAltered = urlMethodAltered.Replace($"{{{segment.Key}}}", segment.Value);
            }

            if (Regex.IsMatch(urlMethodAltered, "[{}]"))
            {
                if (Regex.IsMatch(urlMethodAltered, "({.+})"))
                    throw new Exception($"Segment {Regex.Matches(urlMethodAltered, "({.+})")[0]} not set.");
                throw new Exception($"URL format error: {urlMethod}.");
            }

            return urlMethodAltered;
        }

        private string getUrlParameters()
        {
            var paramsString = new StringBuilder();
            var i = 0;
            foreach (var p in UrlQueries)
            {
                paramsString.Append(p.Key).Append("=").Append(p.Value);

                if (++i < UrlQueries.Count)
                    paramsString.Append("&");
            }

            return paramsString.ToString();
        }
    }
}