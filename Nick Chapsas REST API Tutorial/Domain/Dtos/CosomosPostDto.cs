using Cosmonaut.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Domain.Dtos
{
    [CosmosCollection("posts")]
    public class CosomosPostDto
    {
        [CosmosPartitionKey]
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
