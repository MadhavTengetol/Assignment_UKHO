using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data
{
    public class BusinessUnit
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Unit { get; set; }

        [JsonIgnore]
        public Guid BatchId { get; set; }
    }
}
