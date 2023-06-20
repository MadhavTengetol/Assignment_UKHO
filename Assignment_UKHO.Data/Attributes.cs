using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data
{
    public class Attributes
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

        [JsonIgnore]
        public int FilesId { get; set; }
    }
}
