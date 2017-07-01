using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_kart
{

    public interface ICheckout
    {
        void Scan(string item);
        Decimal TotalPrice(Dictionary<string, Decimal> pricelist);
    }

    public class ShoppingKart : ICheckout
    {
        private List<string> _basket = new List<string>();

        public void Scan(string SKU) {
            _basket.Add(SKU);
        }

        public Decimal TotalPrice(Dictionary<string, Decimal> pricelist) {
            return _basket.Select(sku => pricelist[sku]).Sum();
        }
    }
}
