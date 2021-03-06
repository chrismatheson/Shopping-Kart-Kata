using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_kart {
    public static class Adjustments {
        public static ChargeAdjustment fiftty_percent_of_second(string sku, Decimal price) {
            return list => {
                var countOfMatching = list.Count(i => i.Equals(sku));
                var numberOfDiscounts = Math.Floor((Decimal)(countOfMatching / 2));
                var totalDisscount = numberOfDiscounts * (price / 2);
                var ammountToApply = totalDisscount * -1;
                return ammountToApply;
            };
        }

        public static ChargeAdjustment fixed_disscount_on_third_item(string sku, Decimal ammount) {
            return list => {
                var countOfMatching = list.Count(i => i.Equals(sku));
                var numberOfDiscounts = Math.Floor((Decimal)(countOfMatching / 3));
                var totalDisscount = numberOfDiscounts * ammount;
                var ammountToApply = totalDisscount * -1;
                return ammountToApply;
            };
        }

        public static ChargeAdjustment standard_unit_price(Dictionary<string, Decimal> pricelist) {
            return list => {
                return list.Select(sku => pricelist[sku]).Sum();
            };
        }

    }
}