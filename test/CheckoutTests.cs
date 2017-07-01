using System;
using Xunit;
using shopping_kart;

namespace checkout_tests
{
    public class CheckoutTests
    {
        [Fact]
        public void Should_be_capable_of_scanning_items()
        {
            var checkout = new ShoppingKart();
            checkout.Scan("Some SKU");
        }

        [Fact]
        public void Should_calculate_the_price_of_single_A_as_50()
        {
            var checkout = new ShoppingKart();
            checkout.Scan("A");
            Assert.Equal(50, checkout.TotalPrice());
        }
    }
}
