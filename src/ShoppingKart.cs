using System;

namespace shopping_kart
{

    interface ICheckout
    {
        void Scan(string item);
        Decimal TotalPrice();
    }

    public class ShoppingKart : ICheckout
    {
        public void Scan(string SKU) {

        }

        public Decimal TotalPrice() {
            return 50;
        }
    }
}
