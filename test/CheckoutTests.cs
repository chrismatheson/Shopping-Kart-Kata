using System;
using System.Collections.Generic;
using Xunit;
using shopping_kart;
using static shopping_kart.Adjustments;

namespace checkout_tests
{
    public class CheckoutTests
    {

        private ICheckout _kart;

        private Dictionary<string, Decimal> _pricelist = new Dictionary<string, decimal> {
            {"A", 50},
            {"B", 30},
            {"C", 20},
            {"D", 15}
        };

        private List<ChargeAdjustment> _standardListOfAdjustments = new List<ChargeAdjustment> {

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
            Assert.Equal(50, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist))
            }));
        }

        [Fact]
        public void Should_calculate_the_price_of_single_B_as_30()
        {
            _kart.Scan("B");
            Assert.Equal(30, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist))
            }));
        }

        [Fact]
        public void Should_calculate_the_price_of_three_B_as_90()
        {
            _kart.Scan("B");
            _kart.Scan("B");
            _kart.Scan("B");
            Assert.Equal(90, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist))
            }));
        }

        [Fact]
        public void Should_calculate_the_price_of_mixed_multiple_items()
        {
            _kart.Scan("B");
            _kart.Scan("A");
            _kart.Scan("D");
            Assert.Equal(95, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist))
            }));
        }

        [Fact]
        public void Should_apply_disscount_on_B()
        {
            _kart.Scan("B");
            _kart.Scan("B");
            Assert.Equal(45, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist)),
                new ChargeAdjustment(fiftty_percent_of_second("B", 30))
            }));
        }

        [Fact]
        public void Should_apply_disscount_on_B_only_twice()
        {
            _kart.Scan("B");
            _kart.Scan("B");
            _kart.Scan("B");
            _kart.Scan("B");
            _kart.Scan("B");
            Assert.Equal(120, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist)),
                new ChargeAdjustment(fiftty_percent_of_second("B", 30))
            }));
        }

        [Fact]
        public void Should_apply_disscount_on_A()
        {
            _kart.Scan("A");
            _kart.Scan("A");
            _kart.Scan("A");
            Assert.Equal(130, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist)),
                new ChargeAdjustment(fixed_disscount_on_third_item("A", 20))
            }));
        }

        [Fact]
        public void Should_calculate_complex_basket_correctly()
        {
            new List<string> {
                "A", "A", "A", //130
                "B", "B", "B", //75
                "C", "C", "C", //60
                "D", "D", "D"  //45
            }.ForEach(_kart.Scan);

            Assert.Equal(310, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist)),
                new ChargeAdjustment(fixed_disscount_on_third_item("A", 20)),
                new ChargeAdjustment(fiftty_percent_of_second("B", 30))
            }));
        }

        [Fact]
        public void Should_be_agnostic_to_order()
        {
            _kart.Scan("B");
            _kart.Scan("A");
            _kart.Scan("B");
            Assert.Equal(95, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist)),
                new ChargeAdjustment(fixed_disscount_on_third_item("A", 20)),
                new ChargeAdjustment(fiftty_percent_of_second("B", 30))
            }));

            //reset
            _kart = new ShoppingKart();

            _kart.Scan("A");
            _kart.Scan("B");
            _kart.Scan("B");
            Assert.Equal(95, _kart.TotalPrice(new List<ChargeAdjustment> {
                new ChargeAdjustment(standard_unit_price(_pricelist)),
                new ChargeAdjustment(fixed_disscount_on_third_item("A", 20)),
                new ChargeAdjustment(fiftty_percent_of_second("B", 30))
            }));
        }

        [Fact]
        public void Should_crash_on_negative_total_just_in_case()
        {
            _kart.Scan("B");
            _kart.Scan("B");
            _kart.Scan("B");
            Assert.Throws<Exception>(() => {
                _kart.TotalPrice(new List<ChargeAdjustment> {
                    new ChargeAdjustment(standard_unit_price(_pricelist)),
                    new ChargeAdjustment(fixed_disscount_on_third_item("B", 200)),
                    new ChargeAdjustment(fiftty_percent_of_second("B", 30))
                });
            });
        }
    }
}
