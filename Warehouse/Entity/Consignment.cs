using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Entity
{
    public class Consignment
    {
        public int ConsignmentId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}
