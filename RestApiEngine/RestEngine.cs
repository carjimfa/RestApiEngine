using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestApiEngine
{
    public class RestEngine : HttpClient
    {
        private string BaseUrl { get; set; }
        private string Accept { get; set; }
        public List<string> UriParams { get; private set; } = new List<string>();
        public Dictionary<string, string> QueryParams { get; private set; } = new Dictionary<string, string>();
        public StringContent StringBodyContent { get; private set; }

        public RestEngine()
        {

        }

        public RestEngine(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public RestEngine(string baseUrl, string accept)
        {
            BaseUrl = baseUrl;
            AddAccept(accept);
        }

        public RestEngine AddAccept(string accept)
        {
            Accept = accept;
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
            return this;
        }

        public RestEngine AddHeader(string key, string value)
        {
            DefaultRequestHeaders.Add(key, value);
            return this;
        }

        public RestEngine AddUriParam(string param)
        {
            UriParams.Add(param);
            return this;
        }

        public RestEngine AddQueryParam(string key, string value)
        {
            if(!QueryParams.ContainsKey(key))
            {
                QueryParams.Add(key, value);
            }
            return this;
        }

        public RestEngine AddBearerAuthentication(string token)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        public RestEngine AddCustomAuthentication(string scheme, string token)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
            return this;
        }

        public RestEngine AddBodyString(string bodyContent, Encoding encoding, string contentType)
        {
            StringBodyContent = new StringContent(bodyContent, encoding, contentType);
            return this;
        }

        public RestEngine ClearAccept()
        {
            Accept = null;
            DefaultRequestHeaders.Accept.Clear();
            return this;
        }

        public RestEngine ClearHeader(string key)
        {
            if (DefaultRequestHeaders.Contains(key))
            {
                DefaultRequestHeaders.Remove(key);
            }
            return this;
        }

        public RestEngine ClearHeaders()
        {
            Accept = null;
            DefaultRequestHeaders.Clear();
            return this;
        }

        public RestEngine ClearBaseUrl()
        {
            BaseUrl = null;
            return this;
        }

        public RestEngine ClearUriParam(string key)
        {
            UriParams.Remove(key);
            return this;
        }

        public RestEngine ClearQueryParam(string key)
        {
            QueryParams.Remove(key);
            return this;
        }

        public RestEngine ClearUriParams()
        {
            UriParams = new List<string>();
            return this;
        }

        public RestEngine ClearQueryParams()
        {
            QueryParams = new Dictionary<string, string>();
            return this;
        }

        public RestEngine ClearEverything()
        {
            ClearAccept();
            ClearHeaders();
            ClearQueryParams();
            ClearUriParams();
            return this;
        }

        public async Task<HttpResponseMessage> ProcessGetAsync(string path)
        {
            ValidateCall();

            var result = await GetAsync(GetCompleteUrl(path));

            return result;
        }

        public HttpResponseMessage ProcessGetSync(string path)
        {
            ValidateCall();

            return GetAsync(GetCompleteUrl(path)).Result;
        }

        public async Task<HttpResponseMessage> ProcessGetAsync()
        {
            ValidateCall();

            var result = await GetAsync(GetCompleteUrl());

            return result;
        }

        public HttpResponseMessage ProcessGetSync()
        {
            ValidateCall();

            return GetAsync(GetCompleteUrl()).Result;
        }

        public async Task<HttpResponseMessage> ProcessPostStringAsync()
        {
            var result = await PostAsync(GetCompleteUrl(), StringBodyContent);
            return result;
        }

        public HttpResponseMessage ProcessPostStringSync()
        {
            return PostAsync(GetCompleteUrl(), StringBodyContent).Result;
        }

        public async Task<HttpResponseMessage> ProcessPostStringAsync(string path)
        {
            var result = await PostAsync(GetCompleteUrl(path), StringBodyContent);
            return result;
        }

        public HttpResponseMessage ProcessPostStringSync(string path)
        {
            return PostAsync(GetCompleteUrl(path), StringBodyContent).Result;
        }

        private string GetCompleteUrl()
        {
            var completeUrl = GetCompleteUrl(UriParams);
            if (QueryParams.Count > 0)
            {
                AddQueryParamsToUrl(QueryParams, ref completeUrl);
            }
            return completeUrl;
        }

        private string GetCompleteUrl(string path)
        {
            string[] pathParts = path.Split('/');
            return GetCompleteUrl(pathParts);
        }

        private string GetCompleteUrl(IEnumerable<string> uriParams)
        {
            var completeUrl = BaseUrl;
            foreach (var part in uriParams)
            {
                if (!completeUrl.EndsWith('/'))
                {
                    completeUrl = completeUrl + "/";
                }
                if (!UriParams.Contains(part))
                {
                    UriParams.Add(part);
                }
                completeUrl = completeUrl + part;
            }

            return completeUrl;
        }

        private void AddQueryParamsToUrl(IDictionary<string, string> queryParams, ref string url)
        {
            foreach(var param in queryParams)
            {
                if (!url.Contains("?"))
                {
                    url = url + "?";
                }
                else
                {
                    url = url + "&";
                }
                url = url + param.Key + "=" + param.Value;
            }
        }

        private bool ValidateBaseUrl() => !string.IsNullOrEmpty(BaseUrl);

        private void ValidateCall()
        {
            if (!ValidateBaseUrl())
            {
                throw new ArgumentNullException("BaseUrl cannot be null");
            }
        }
    }
}
