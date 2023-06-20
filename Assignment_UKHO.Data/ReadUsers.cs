using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data
{
    public class ReadUsers
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string User { get; set; }
        [JsonIgnore]
        public int AclId { get; set; }
    }
}
