using System.Net;

namespace beah.Library.Web.REST
{
    public interface IRestHelper
    {
        /// <summary>
        /// Method Creates an HttpWebRequest used for making rest calls
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod">GET, POST, DELETE, PUT</param>
        /// <param name="requestBody"></param>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        /// application/x-www-form-urlencoded
        HttpWebRequest CreateRequest(string uri, string httpMethod, string requestBody = "", string ContentType ="application/json"  , WebProxy webProxy = null);

        HttpWebRequest CreateRequest(string uri, string httpMethod, string requestBody = "");

        /// <summary>
        /// Method Creates an HttpWebRequest used for making rest calls
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod">GET, POST, DELETE, PUT</param>
        /// <param name="timeout"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        HttpWebRequest CreateRequest(string uri, string httpMethod, int timeout, string requestBody = "", WebProxy webProxy = null);

        HttpWebRequest CreateRequest(string uri, string httpMethod, int timeout, string requestBody = "");

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
        HttpWebRequest CreateRequest(string uri, string httpMethod, string userName, string password, string contentType, string requestBody, WebProxy webProxy = null);

        HttpWebRequest CreateRequest(string uri, string httpMethod, string userName, string password, string contentType, string requestBody);

        /// <summary>
        /// Execite a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        string ExecuteRequest(string uri, string httpMethod, string requestBody = "", WebProxy webProxy = null);

        string ExecuteRequest(string uri, string httpMethod, string requestBody = "");

        /// <summary>
        /// Execite a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="timeout"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        string ExecuteRequest(string uri, string httpMethod, int timeout, string requestBody = "", WebProxy webProxy = null);

        string ExecuteRequest(string uri, string httpMethod, int timeout, string requestBody = "");

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
        string ExecuteRequest(string uri, string httpMethod, string userName, string password, string contenType, string requestBody, WebProxy webProxy = null);

        string ExecuteRequest(string uri, string httpMethod, string userName, string password, string contenType, string requestBody);

        /// <summary>
        /// Executes a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers, WebProxy webProxy = null);

        string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers);

        /// <summary>
        /// Executes a REST request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="headers"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers, string requestBody, WebProxy webProxy = null);

        string ExecuteRequest(string uri, string httpMethod, WebHeaderCollection headers, string requestBody);
    }
}