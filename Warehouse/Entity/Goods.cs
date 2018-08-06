using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Entity
{
    public class Goods
    {
        public int GoodsId { get; set; }
        public string GoodsName { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
    }
}
