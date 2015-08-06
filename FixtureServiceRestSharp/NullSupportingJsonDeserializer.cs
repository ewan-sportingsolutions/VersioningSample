using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace FixtureServiceRestSharp
{
    public class NullSupportingJsonDeserializer : IDeserializer
    {
        public T Deserialize<T>(IRestResponse response)
        {
            if (response.Content == "null")
            {
                return default(T);
            }
            var deserializer = new JsonDeserializer
            {
                RootElement = this.RootElement,
                Namespace = this.Namespace,
                DateFormat = this.DateFormat
            };
            return deserializer.Deserialize<T>(response);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    } 
}
