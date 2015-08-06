using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;
using RestSharp;

namespace FixtureServiceRestSharp
{
    public class FixtureServiceAsync_RestSharp : IFixtureServiceAsync
    {
        private static readonly Version AssemblyVersion = Assembly.GetCallingAssembly().GetName().Version;

        private const string XAuthTokenHeaderName = "X-Auth-Token";
        private const string VersionHeaderName = "Version";
        private RestClient client;
        private string authKey;

        public FixtureServiceAsync_RestSharp(
            Uri endpoint,
            string authKey,
            int timeout = 10000)
        {
            this.authKey = authKey;

            this.client = new RestClient(endpoint);
            client.AddHandler("application/json", new NullSupportingJsonDeserializer());
            client.AddHandler("text/json", new NullSupportingJsonDeserializer());
            client.AddHandler("text/x-json", new NullSupportingJsonDeserializer());
        }
        #region restsharpmethods
        /// <summary>
        /// build a rest request ensureing that the auth token and version headers are added
        /// </summary>
        private RestRequest getRequest(
            Uri resource,
            Method method,
            object data,
            Dictionary<string, object> queryParameters,
            Dictionary<string, string> extraHeaders) 

        {
            RestRequest req = new RestRequest(resource,method);
            req.RequestFormat = DataFormat.Json;

            //add the auth header
            req.AddHeader(
                XAuthTokenHeaderName,
                authKey);

            //add the version header
            req.AddHeader(
                VersionHeaderName,
                AssemblyVersion.ToString());


            if (extraHeaders != null)
            {
                foreach (var p in extraHeaders)
                {
                    req.AddHeader(
                        p.Key,
                        p.Value);
                }
            }

            if (queryParameters != null)
            {
                foreach (var p in queryParameters)
                {
                    req.AddParameter(
                        p.Key,
                        p.Value, 
                        ParameterType.QueryString);
                }
            }

            if (data != null)
            {
                req.AddBody(data);
            }

            return req;
        }

        private async Task<IRestResponse<T>> GetResponseAsync<T>(
            Uri resource,
            Method method,
            object data,
            Dictionary<string, object> queryParameters,
            Dictionary<string, string> extraHeaders = null) 
            where T : new()
        {
            RestRequest req = this.getRequest(
                resource,
                method, 
                data,
                queryParameters,
                extraHeaders);


            IRestResponse<T> resp;
            try
            {
                var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

                client.ExecuteAsync<T>(req, (response) => taskCompletionSource.SetResult(response));
                resp = await taskCompletionSource.Task;

                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    if (resp.ResponseStatus == ResponseStatus.Completed)
                    {
                        return resp;
                    }
                    else
                    {
                        throw new Exception("problem with response :" + resp.ErrorMessage + " : " + resp.Content);
                    }
                }
                else
                {
                    throw new Exception("http error : " + resp.StatusCode + " - " + resp.Content + " - " + resp.ErrorMessage);
                }
            }
            catch (System.ArgumentException ex) {
                //unable to deserialize?
                throw new Exception("unable to cope with response " + ex.InnerException.Message);

            }

        }

        private async Task<IRestResponse> GetResponseAsync(
            Uri resource,
            Method method,
            object data,
            Dictionary<string, object> queryParameters,
            Dictionary<string,string> extraHeaders = null)
        {
            RestRequest req = this.getRequest(
                resource,
                method,
                data,
                queryParameters,
                extraHeaders);

            IRestResponse resp;
            try
            {
                var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

                client.ExecuteAsync(req, (response) => taskCompletionSource.SetResult(response));
                resp = await taskCompletionSource.Task;

                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    if (resp.ResponseStatus == ResponseStatus.Completed)
                    {
                        return resp;
                    }
                    else
                    {
                        throw new Exception("problem with response :" + resp.ErrorMessage + " : " + resp.Content);
                    }
                }
                else
                {
                    throw new Exception("http error : " + resp.StatusCode + " - " + resp.Content + " - " + resp.ErrorMessage);
                }
            }
            catch (System.ArgumentException ex)
            {
                //unable to deserialize?
                throw new Exception("unable to cope with response " + ex.InnerException.Message);

            }
        }

        #endregion
        public async Task<Model.Fixture> GetFixtureAsync(string id)
        {
            Uri resource = new Uri("api/FixtureService/GetFixture", UriKind.Relative);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", id);

            var result = await this.GetResponseAsync<Model.Fixture>(
                resource,
                Method.GET,
                null,
                parameters);

            return result.Data;
        }

        public async Task<Model.Fixture> AddFixtureAsync(Model.Fixture fixture)
        {
            Uri resource = new Uri("api/FixtureService/AddFixture", UriKind.Relative);
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            var result = await this.GetResponseAsync<Model.Fixture>(
                resource,
                Method.POST,
                fixture,
                parameters);

            return result.Data;
        }


        public async Task<List<Model.Fixture>> GetFixtureByDateAsync(DateTime startDate, DateTime endDate)
        {
            Uri resource = new Uri("api/FixtureService/TODOFILLTHISIN!", UriKind.Relative);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);

            var result = await this.GetResponseAsync<List<Model.Fixture>>(
                resource,
                Method.GET,
                null,
                parameters);

            return result.Data;
        }
    }
}
