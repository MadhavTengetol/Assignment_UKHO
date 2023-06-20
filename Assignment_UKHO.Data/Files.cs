using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment_UKHO.Data
{
    public class Files
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string MimeType { get; set; }
        public string Hash { get; set; }
        public List<Attributes> Attributes { get; set; }

        [JsonIgnore]
        public Guid BatchId { get; set; }
    }
}
