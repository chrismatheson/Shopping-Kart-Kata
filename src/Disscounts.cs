using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_kart {
  public static class Disscounts {
    public static Func<List<string>, Decimal> fiftty_percent_of_second(string sku, Decimal price) {
      return list => {
        var countOfMatching = list.Count(i => i.Equals(sku));
        var numberOfDiscounts = Math.Floor((Decimal)(countOfMatching / 2));
        var totalDisscount = numberOfDiscounts * (price / 2);
        var ammountToApply = totalDisscount * -1;
        return ammountToApply;
      };
    }
  }
}