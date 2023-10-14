using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application
{
    public class EsBulkRequest
    {
        public EsBulkType type { get; set; }
        [Required]
        public string index { get; set; }
        public string id { get; set; }
        [Required]
        public object[] data { get; set; }
    }
    public enum EsBulkType
    {
        index,
        create,
        delete,
        update
    }
}
