using System;
using System.Collections.Generic;
using VendingMachineSample.Domain.Model;
using VendingMachineSample.Domain.Repository;


namespace VendingMachineSample.Infrastructure.Repository {
    public class Memory商品在庫Repository : I商品在庫Repository {
        private Dictionary<販売コード, 販売商品在庫> 販売商品 = new Dictionary<販売コード, 販売商品在庫>();

        public IEnumerable<販売コード> 販売コード一覧() {
            return this.販売商品.Keys;
        }

        public 販売商品在庫 Find商品(販売コード 販売コード) {
            if(this.販売商品.ContainsKey(販売コード))
                return this.販売商品[販売コード];
            return null;
        }

        public void Add数量(販売コード 販売コード, int 数量) {
            this.Find商品(販売コード).Add数量(数量);
        }

        public int Count数量(販売コード 販売コード) {
            return this.Find商品(販売コード).数量;
        }

        public void Register商品(販売コード 販売コード, 販売商品 販売商品) {
            this.販売商品[販売コード] = new 販売商品在庫(販売商品.商品名, 販売商品.価格, 0);
        }

        public 販売コード New販売コード() {
            return new 販売コード(Guid.NewGuid().ToString());
        }
    }
}
