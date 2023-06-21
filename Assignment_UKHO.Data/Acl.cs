using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data
{
    public class Acl
    {
        [JsonIgnore]
        public int Id { get; set; }
    
        public List<ReadUsers>? ReadUsers { get; set; }
    
        public List<ReadGroups>? ReadGroups { get; set; }

        [JsonIgnore]
        public Guid BatchId { get; set; }
    }
}
