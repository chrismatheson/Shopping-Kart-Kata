using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_kart
{
    public delegate Decimal ChargeAdjustment(List<string> basket);

    public interface ICheckout
    {
        void Scan(string item);
        Decimal TotalPrice(Dictionary<string, Decimal> pricelist, List<ChargeAdjustment> offers );
    }

    public class ShoppingKart : ICheckout
    {
        private List<ChargeAdjustment> _noOffers = new List<ChargeAdjustment>();

        private List<string> _basket = new List<string>();

        public void Scan(string SKU) {
            _basket.Add(SKU);
        }

        public Decimal TotalPrice(Dictionary<string, Decimal> pricelist, List<ChargeAdjustment> offers) {
            var disscount = offers.Select(offer => offer(_basket)).Sum();
            var gross = _basket.Select(sku => pricelist[sku]).Sum();

            return gross + disscount;
        }
    }
}
