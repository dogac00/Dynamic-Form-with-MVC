using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicFormsApplication.Models
{
    public class Field
    {
        public int Id { get; set; }
        public bool Required { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}
