using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;
using beah.Library.Web.Logging;
using log4net;

namespace beah.Library.Web.REST
{
    public class RestHelper : IRestHelper
    {
        private static readonly ILog _logger = Logger.GetLogger(typeof(RestHelper));
        private static ILog Log { get { return _logger; } }

        public static string HTTPPOST = "POST";
        public static string HTTPGET = "GET";

        public const string CONTENT_TYPE_FORM_URLENCODED = "application/x-www-form-urlencoded";
        public const string CONTENT_TYPE_FORM_JSON = "application/json";

        /// <summary>
        /// Method Creates an HttpWebRequest used for making rest calls
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod">GET, POST, DELETE, PUT</param>
        /// <param name="requestBody"></param>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        /// application/x-www-form-urlencoded
        public HttpWebRequest CreateRequest(string uri, string httpMethod, string requestBody = "", string ContentType = CONTENT_TYPE_FORM_JSON, WebProxy webProxy = null)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (webProxy != null)
                request.Proxy = webProxy;

            request.Method = httpMethod.ToUpper();
            request.ContentType = ContentType;

            switch (request.Method)
            {
                case "GET":
                // purposely let this drop to the DELETE block
                case "DELETE":
                    request.ContentLength = 0;
                    return InjectIntoRequest(request);
                case "POST":
                // purposely let this drop to the PUT block
                case "PUT":
                    return AddBodyToRequest(request, requestBody);
                default:
                    Log.Warn(String.Format("Unexpected Http Method. Expected: GET, POST, PUT, DELETE. Actual: {0}", httpMethod));
                    throw new ArgumentException(String.Format("Unexpected Http Method. Expected: GET, POST, PUT, DELETE. Actual: {0}", httpMethod));
            }
        }

        public HttpWebRequest CreateRequest(string uri, string httpMethod, string requestBody = "")
        {
            return CreateRequest(uri, httpMethod, requestBody, null);
        }


        /// <summary>
        /// Method Creates an HttpWebRequest used for making rest calls
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod">GET, POST, DELETE, PUT</param>
        /// <param name="timeout"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public HttpWebRequest CreateRequest(string uri, string httpMethod, int timeout, string requestBody = "", WebProxy webProxy = null)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (webProxy != null)
                request.Proxy = webProxy;

            request.Timeout = timeout;
            request.Method = httpMethod.ToUpper();
            request.ContentType = CONTENT_TYPE_FORM_JSON;

            switch (request.Method)
            {
                case "GET":
                // purposely let this drop to the DELETE block
                case "DELETE":
                    request.ContentLength = 0;
                    return InjectIntoRequest(request);
                case "POST":
                // purposely let this drop to the PUT block
                case "PUT":
                    return AddBodyToRequest(request, requestBody);
                default:
                    Log.Warn(String.Format("Unexpected Http Method. Expected: GET, POST, PUT, DELETE. Actual: {0}", httpMethod));
                    throw new ArgumentException(String.Format("Unexpected Http Method. Expected: GET, POST, PUT, DELETE. Actual: {0}", httpMethod));
            }
        }

        public HttpWebRequest CreateRequest(string uri, string httpMethod, int timeout, string requestBody = "")
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Method Creates an HttpWebRequest used for making rest calls
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod">GET, POST, DELETE, PUT</param>
        /// <param name="contentType"></param>
        /// <param name="requestBody"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public HttpWebRequest CreateRequest(string uri, string httpMethod, string userName, string password, string contentType, string requestBody, WebProxy webProxy = null)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;

            if (webProxy != null)
                request.Proxy = webProxy;

            request.Method = httpMethod.ToUpper();
            request.ContentType = contentType;
            request.Credentials = new NetworkCredential(userName, password);


            switch (request.Method)
            {
                case "GET":
                // purposely let this drop to the DELETE block
                case "DELETE":
                    request.ContentLength = 0;
                    return InjectIntoRequest(request);
                case "POST":
                // purposely let this drop to the PUT block
                case "PUT":
                    return AddBodyToRequest(request, requestBody);
                default:
                    Log.Warn(String.Format("Unexpected Http Method. Expected: GET, POST, PUT, DELETE. Actual: {0}", httpMethod));
                    throw new ArgumentException(String.Format("Unexpected Http Method. Expected: GET, POST, PUT, DELETE. Actual: {0}", httpMethod));
            }
        }

        public HttpWebRequest CreateRequest(string uri, string httpMethod, string userName, string password, string contentType, string requestBody)
        {
            return CreateRequest(uri, httpMethod, userName, password, contentType, requestBody, null);
        }

        /// <summary>
        /// Method Adds body to a request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        internal HttpWebRequest AddBodyToRequest(HttpWebRequest request, string requestBody, WebProxy webProxy = null)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] body = encoding.GetBytes(requestBody);
            request.ContentLength = body.Length;
            request = InjectIntoRequest(request);
            Stream stream = request.GetRequestStream();
            stream.Write(body, 0, body.Length);

            return request;
        }

        /// <summary>
        /// This method is used to inject any properties to the header.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal virtual HttpWebRequest InjectIntoRequest(HttpWebRequest request, WebProxy webProxy = null)
        {
            return request;
        }

        /// <summary>
        /// Execite a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public string ExecuteRequest(string uri, string httpMethod, string requestBody = "", WebProxy webProxy = null)
        {
            return ExecuteRequest(CreateRequest(uri, httpMethod, requestBody,"", webProxy));
        }

        public string ExecuteRequest(string uri, string httpMethod, string requestBody = "")
        {
            return ExecuteRequest(uri, httpMethod, requestBody, null);
        }

        /// <summary>
        /// Execite a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="timeout"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public string ExecuteRequest(string uri, string httpMethod, int timeout, string requestBody = "", WebProxy webProxy = null)
        {
            return ExecuteRequest(CreateRequest(uri, httpMethod, timeout, requestBody, webProxy));
        }

        public string ExecuteRequest(string uri, string httpMethod, int timeout, string requestBody = "")
        {
            return ExecuteRequest(uri, httpMethod, timeout, requestBody, null);
        }

        /// <summary>
        /// Execite a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="contenType"></param>
        /// <param name="requestBody"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string ExecuteRequest(string uri, string httpMethod, string userName, string password, string contenType, string requestBody, WebProxy webProxy = null)
        {
            return ExecuteRequest(CreateRequest(uri, httpMethod, userName, password, contenType, requestBody, webProxy));
        }

        public string ExecuteRequest(string uri, string httpMethod, string userName, string password, string contenType, string requestBody)
        {
            return ExecuteRequest(uri, httpMethod, userName, password, contenType, requestBody, null);
        }

        /// <summary>
        /// Executes a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers, WebProxy webProxy = null)
        {
            HttpWebRequest request = CreateRequest(uri, httpMethod, "","", webProxy);
            request.Headers.Add(headers);
            return ExecuteRequest(request);
        }

        public string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers)
        {
            return ExecuteRequest(uri, httpMethod, headers, "", null);
        }

        /// <summary>
        /// Executes a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="headers"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers, string requestBody, WebProxy webProxy = null)
        {
            HttpWebRequest request = CreateRequest(uri, httpMethod, requestBody,"", webProxy);


            request.Headers.Add(headers);
            return ExecuteRequest(request);
        }

        public string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers, string requestBody)
        {
            return ExecuteRequest(uri, httpMethod, headers, requestBody, null);
        }

        /// <summary>
        /// Executes the HttpWebRequest and return the json response
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal string ExecuteRequest(HttpWebRequest request, WebProxy webProxy = null)
        {
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (webProxy != null)
                    request.Proxy = webProxy;

                StreamReader reader = new StreamReader(response.GetResponseStream());
                var jsonResponse = reader.ReadToEnd();

                return jsonResponse;
            }
        }
    }
}
