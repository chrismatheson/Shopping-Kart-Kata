using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_kart
{
    using SpecialOffer = Func<List<string>, Decimal>;

    public interface ICheckout
    {
        void Scan(string item);
        Decimal TotalPrice(Dictionary<string, Decimal> pricelist, List<SpecialOffer> offers );
    }

    public class ShoppingKart : ICheckout
    {
        private List<SpecialOffer> _noOffers = new List<SpecialOffer>();

        private List<string> _basket = new List<string>();

        public void Scan(string SKU) {
            _basket.Add(SKU);
        }

        public Decimal TotalPrice(Dictionary<string, Decimal> pricelist, List<SpecialOffer> offers) {
            var disscount = offers.Select(offer => offer(_basket)).Sum();
            var gross = _basket.Select(sku => pricelist[sku]).Sum();

            return gross + disscount;
        }
    }
}
