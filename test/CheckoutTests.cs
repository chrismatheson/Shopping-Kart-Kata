using System;
using System.Collections.Generic;
using Xunit;
using shopping_kart;

namespace checkout_tests
{
    public class CheckoutTests
    {

        private ICheckout _kart;

        private Dictionary<string, Decimal> _pricelist = new Dictionary<string, decimal> {
            {"A", 50},
            {"B", 30},
        };

        public CheckoutTests() {
            _kart = new ShoppingKart();
        }

        [Fact]
        public void Should_be_capable_of_scanning_items()
        {
            _kart.Scan("Some SKU");
        }

        [Fact]
        public void Should_calculate_the_price_of_single_A_as_50()
        {
            _kart.Scan("A");
            Assert.Equal(50, _kart.TotalPrice(_pricelist));
        }

        [Fact]
        public void Should_calculate_the_price_of_single_B_as_30()
        {
            _kart.Scan("B");
            Assert.Equal(30, _kart.TotalPrice(_pricelist));
        }
    }
}
