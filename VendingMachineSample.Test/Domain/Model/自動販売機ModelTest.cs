using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Domain.Models;
using VendingMachineSample.Infra.Repositories;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 自動販売機Test {
        [TestMethod]
        public void 商品在庫はNullにできない() {
            try {
                new 硬貨自動販売機(null, new 硬貨支払機());
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 支払機はNullにできない() {
            try {
                new 硬貨自動販売機(new Memory商品在庫Repository(), null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当なインスタンスを生成する() {
            new 硬貨自動販売機(new Memory商品在庫Repository(), new 硬貨支払機());
        }

        [TestMethod]
        public void 商品リストを取得する() {
            var x = new 硬貨自動販売機(new Memory商品在庫Repository(), new 硬貨支払機());
            x.販売コードList();
        }

        [TestMethod]
        public void 商品コードから商品を取得する() {
            var c = new 販売コード("CO-1234");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(100)));
            r.Add数量(c, 100);
            var y = x.Find商品(c);
            Assert.IsTrue(y.商品名.Equals(new 商品名("コーラ")));
            Assert.IsTrue(y.価格.Equals(new 通貨(100)));
            Assert.IsTrue(y.数量.Equals(100));
        }

        [TestMethod]
        public void 存在しない商品コードを取得するとNull() {
            var c = new 販売コード("CO-1234");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var y = x.Find商品(c);
            Assert.IsTrue(y == null);
        }

        [TestMethod]
        public void 硬貨1円は投入できない() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            try {
                x.Insert貨幣(new 硬貨(1));
            }
            catch(InvalidOperationException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 硬貨5円は投入できない() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            try {
                x.Insert貨幣(new 硬貨(5));
            }
            catch(InvalidOperationException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 硬貨10円は投入できる() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            x.Insert貨幣(new 硬貨(10));
            Assert.IsTrue(x.投入金額合計() == new 通貨(10));
        }

        [TestMethod]
        public void 硬貨50円は投入できる() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            x.Insert貨幣(new 硬貨(50));
            Assert.IsTrue(x.投入金額合計() == new 通貨(50));
        }

        [TestMethod]
        public void 硬貨100円は投入できる() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            x.Insert貨幣(new 硬貨(100));
            Assert.IsTrue(x.投入金額合計() == new 通貨(100));
        }

        [TestMethod]
        public void 硬貨500円は投入できる() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            x.Insert貨幣(new 硬貨(500));
            Assert.IsTrue(x.投入金額合計() == new 通貨(500));
        }

        [TestMethod]
        public void 硬貨100円と500円を投入して返却すると100円と500円が返却される() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            x.Insert貨幣(new 硬貨(100));
            x.Insert貨幣(new 硬貨(500));
            var y = x.Refund貨幣().OrderBy(x => x.Value).ToList();
            Assert.IsTrue(y[0] == new 硬貨(100));
            Assert.IsTrue(y[1] == new 硬貨(500));
        }

        [TestMethod]
        public void 販売コードがNullでは決済できない() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            try {
                x.決済(null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 存在しない販売コードでは決済できない() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            try {
                x.決済(new 販売コード("AYBABTU"));
            }
            catch(自動販売機該当商品なしException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 在庫切れの商品では決済できない() {
            var c = new 販売コード("COLA-001");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(100)));
            try {
                x.決済(c);
            }
            catch(自動販売機在庫切れException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 投入金額不足では決済できない() {
            var c = new 販売コード("COLA-001");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(110)));
            r.Add数量(c, 1);
            x.Insert貨幣(new 硬貨(100));
            try {
                x.決済(c);
            }
            catch(自動販売機金額不足Exception) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 投入金額が十分であれば決済できる() {
            var c = new 販売コード("COLA-001");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(140)));
            r.Add数量(c, 1);
            x.Insert貨幣(new 硬貨(500));
            var y = x.決済(c);
            Assert.IsTrue(y.商品.商品名.Equals(new 商品名("コーラ")));
            var t = y.釣銭.OrderBy(x => x.Value).ToList();
            Assert.IsTrue(t[0] == new 硬貨(10));
            Assert.IsTrue(t[1] == new 硬貨(50));
            Assert.IsTrue(t[2] == new 硬貨(100));
            Assert.IsTrue(t[3] == new 硬貨(100));
            Assert.IsTrue(t[4] == new 硬貨(100));
        }
    }
}
