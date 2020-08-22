using System;
using System.Collections.Generic;


namespace VendingMachineSample.Domain.Model {
    public class 決済結果 {
        public readonly 商品 商品;
        public readonly IEnumerable<貨幣> 釣銭;

        public 決済結果(商品 商品, IEnumerable<貨幣> 釣銭) {
            this.商品 = 商品 ?? throw new ArgumentNullException(nameof(商品) + " cannot be null");
            this.釣銭 = 釣銭 ?? throw new ArgumentNullException(nameof(釣銭) + " cannot be null");
        }
    }
}
