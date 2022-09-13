using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AnimeMovie.Business
{
    [Serializable]
    public class ServiceResponse<T>
    {
        public bool HasExceptionError { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptionMessage { get; set; }

        public IList<T> List { get; set; }

        [JsonProperty]
        public T Entity { get; set; }

        public int Count { get; set; }

        public bool IsSuccessful { get; set; }

        public ServiceResponse()
        {
            List = new List<T>();
        }
    }
}

