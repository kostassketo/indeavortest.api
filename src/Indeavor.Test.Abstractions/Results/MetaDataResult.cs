using System.Collections.Generic;
using Newtonsoft.Json;

namespace Indeavor.Test.Abstractions.Results
{
    public class MetaDataResult
    {
        [JsonIgnore]
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
