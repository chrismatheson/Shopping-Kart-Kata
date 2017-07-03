using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_kart
{
    public delegate Decimal ChargeAdjustment(List<string> basket);

    public interface ICheckout
    {
        void Scan(string item);
        Decimal TotalPrice(List<ChargeAdjustment> adjustors);
    }

    public class ShoppingKart : ICheckout
    {
        private List<ChargeAdjustment> _noOffers = new List<ChargeAdjustment>();

        private List<string> _basket = new List<string>();

        public void Scan(string SKU) {
            _basket.Add(SKU);
        }

        public Decimal TotalPrice(List<ChargeAdjustment> adjustors) {
            var price = adjustors.Select(offer => offer(_basket)).Sum();

            if(price <= 0) {
                throw new Exception("Probably dot want to be giving away money");
            }

            return price;
        }
    }
}
