using System;


namespace VendingMachineSample.Core.Domain.Models {
    public class 商品 {
        public readonly 商品名 商品名;

        public 商品(商品名 商品名) {
            this.商品名 = 商品名 ?? throw new ArgumentNullException(nameof(商品名) + " cannot be null");
        }
    }

    public class 販売商品 : 商品 {
        public readonly 通貨 価格;

        public 販売商品(商品名 商品名, 通貨 価格) : base(商品名) {
            this.価格 = 価格 ?? throw new ArgumentNullException(nameof(価格) + " cannot be null");
        }
    }

    public class 販売商品在庫 : 販売商品 {
        public int 数量 { get; private set; }

        public 販売商品在庫(商品名 商品名, 通貨 価格, int 数量) : base(商品名, 価格) {
            this.Add数量(数量);
        }

        public void Add数量(int 数量) {
            if(this.数量 + 数量 < 0)
                throw new ArgumentOutOfRangeException(nameof(数量) + " cannot be less than 0");
            this.数量 += 数量;
        }
    }

    public class 販売可能商品 : 販売商品 {
        public readonly bool 販売可能;

        public 販売可能商品(商品名 商品名, 通貨 価格, bool 販売可能) : base(商品名, 価格) {
            this.販売可能 = 販売可能;
        }
    }
}
