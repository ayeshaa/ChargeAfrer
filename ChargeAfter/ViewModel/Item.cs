using ChargeAfter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargeAfter.ViewModel
{
    public class Item
    {
        private Product pro = new Product();

        public Product Pro
        {
            get { return pro; }
            set { pro = value; }
        }


        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public Item()
        {

        }
        public Item(Product product, int quantity)
        {
            this.pro = product;
            this.quantity = quantity;
        }
    }
}