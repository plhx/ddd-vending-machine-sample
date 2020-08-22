using System;
using System.Collections.Generic;
using System.Linq;

using VendingMachineSample.Domain.Repository;


namespace VendingMachineSample.Domain.Model {
    public class 自動販売機該当商品なしException : Exception {

    }

    public class 自動販売機在庫切れException : Exception {

    }

    public class 自動販売機金額不足Exception : Exception {

    }

    public interface I自動販売機<T> where T: 貨幣 {
        public IEnumerable<販売コード> 販売コードList();
        public 販売商品在庫 Find商品(販売コード 販売コード);
        public 通貨 投入金額合計();
        public void Insert貨幣(T 投入金額);
        public IEnumerable<T> Refund貨幣();
        public 決済結果 決済(販売コード 商品コード);
    }

    public class 硬貨自動販売機 : I自動販売機<硬貨> {
        private readonly I商品在庫Repository 商品在庫Repository;
        private readonly I支払機<硬貨> 支払機;

        private readonly static 硬貨[] AcceptableCoins = new 硬貨[] {
            new 硬貨(10), new 硬貨(50), new 硬貨(100), new 硬貨(500)
        };

        public 硬貨自動販売機(I商品在庫Repository 商品在庫Repository, I支払機<硬貨> 支払機) {
            this.商品在庫Repository = 商品在庫Repository ?? throw new ArgumentNullException(nameof(商品在庫Repository) + " cannot be null");
            this.支払機 = 支払機 ?? throw new ArgumentNullException(nameof(支払機) + " cannot be null");
        }

        public IEnumerable<販売コード> 販売コードList() {
            return this.商品在庫Repository.販売コード一覧();
        }

        public 販売商品在庫 Find商品(販売コード 販売コード) {
            return this.商品在庫Repository.Find商品(販売コード);
        }

        public void Insert貨幣(硬貨 投入金額) {
            if(!AcceptableCoins.Contains(投入金額))
                throw new InvalidOperationException(nameof(投入金額) + $"({投入金額}) cannot be acceptable");
            this.支払機.Insert貨幣(投入金額);
        }

        public IEnumerable<硬貨> Refund貨幣() {
            return this.支払機.Refund貨幣();
        }

        public 通貨 投入金額合計() {
            return this.支払機.投入金額合計();
        }

        public 決済結果 決済(販売コード 商品コード) {
            var item = this.Find商品(商品コード) ?? throw new 自動販売機該当商品なしException();
            if(item.数量 <= 0)
                throw new 自動販売機在庫切れException();
            if(this.支払機.投入金額合計() < item.価格)
                throw new 自動販売機金額不足Exception();
            var change = this.支払機.決済(item.価格);
            item.Add数量(-1);
            return new 決済結果(item, change);
        }
    }
}
