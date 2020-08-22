using System;
using System.Collections.Generic;
using VendingMachineSample.Domain.Model;


namespace VendingMachineSample.Domain.Repository {
    public interface I商品在庫Repository {
        public IEnumerable<販売コード> 販売コード一覧();
        public 販売商品在庫 Find商品(販売コード 販売コード);
        public void Register商品(販売コード 販売コード, 販売商品 販売商品);
        public void Add数量(販売コード 販売コード, int 数量);
        public int Count数量(販売コード　販売コード);
        public 販売コード New販売コード();
    }
}
