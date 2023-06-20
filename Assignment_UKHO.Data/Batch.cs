using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data
{
    public class Batch
    {
        [JsonIgnore]
        public Guid BatchId { get; set; }

        public string BusinessUnit { get; set; }
        public Acl Acl { get; set; }
        public List<Attributes> Attributes { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.Today;

        public List<Files> Files { get; set; }
    }
}
