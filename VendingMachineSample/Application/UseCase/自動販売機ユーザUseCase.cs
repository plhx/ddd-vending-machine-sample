using System;
using System.Collections.Generic;
using VendingMachineSample.Domain.Model;


namespace VendingMachineSample.Application.UseCase {
    public interface I自動販売機ユーザUseCase {
        public Dictionary<販売コード, 販売可能商品> 商品一覧();
        public 通貨 投入金額合計();
        public void Insert貨幣(貨幣 投入金額);
        public IEnumerable<貨幣> Refund貨幣();
        public 決済結果 決済(販売コード 販売コード);
    }

    public class 硬貨自動販売機ユーザUseCase : I自動販売機ユーザUseCase {
        private readonly I自動販売機<硬貨> 自動販売機;

        public 硬貨自動販売機ユーザUseCase(I自動販売機<硬貨> 自動販売機) {
            this.自動販売機 = 自動販売機;
        }

        public Dictionary<販売コード, 販売可能商品> 商品一覧() {
            var result = new Dictionary<販売コード, 販売可能商品>();
            foreach(var code in this.自動販売機.販売コードList()) {
                var item = this.自動販売機.Find商品(code);
                result[code] = new 販売可能商品(item.商品名, item.価格, item.数量 > 0);
            }
            return result;
        }

        public 通貨 投入金額合計() {
            return this.自動販売機.投入金額合計();
        }

        public void Insert貨幣(貨幣 投入金額) {
            this.自動販売機.Insert貨幣(new 硬貨(投入金額.Value));
        }

        public IEnumerable<貨幣> Refund貨幣() {
            return this.自動販売機.Refund貨幣();
        }

        public 決済結果 決済(販売コード 販売コード) {
            return this.自動販売機.決済(販売コード);
        }
    }
}
