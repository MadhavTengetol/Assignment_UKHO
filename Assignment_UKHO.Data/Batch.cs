using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Status { get; set; } = "Incomplete";

        public string BusinessUnit { get; set; }
     
        public Acl Acl { get; set; }
        public List<Attributes> Attributes { get; set; }
        public DateTime BatchPublicationDate { get; set; } = DateTime.Now;

        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(1);

        public List<Files> Files { get; set; }
    }
}
