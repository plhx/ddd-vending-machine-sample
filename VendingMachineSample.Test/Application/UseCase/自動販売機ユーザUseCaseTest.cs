using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Application.Command;
using VendingMachineSample.Core.Application.UseCases;
using VendingMachineSample.Core.Domain.Models;
using VendingMachineSample.Infra.Repositories;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 自動販売機ユーザUseCaseTest {
        [TestMethod]
        public void Nullでは生成できない() {
            try {
                new 硬貨自動販売機ユーザUseCase(null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当な自動販売機で生成する() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            new 硬貨自動販売機ユーザUseCase(x);
        }

        [TestMethod]
        public void 商品一覧を取得する() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            r.Register商品(new 販売コード("COL-001"), new 販売商品(new 商品名("コーラ"), new 通貨(120)));
            r.Register商品(new 販売コード("WAT-002"), new 販売商品(new 商品名("おいしい水"), new 通貨(100)));
            u.商品一覧(new 自動販売機商品一覧Command());
        }

        [TestMethod]
        public void 硬貨100円と500円を投入すると600円になる() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            u.Insert貨幣(new 自動販売機Insert貨幣Command(100));
            u.Insert貨幣(new 自動販売機Insert貨幣Command(500));
            var y = u.投入金額合計(new 自動販売機投入金額合計Command());
            Assert.IsTrue(y is 自動販売機投入金額合計CommandResult);
            var z = y as 自動販売機投入金額合計CommandResult;
            Assert.IsTrue(z.投入金額合計 == new 通貨(600));
        }

        [TestMethod]
        public void 投入できない硬貨を投入すると失敗する() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            var y = u.Insert貨幣(new 自動販売機Insert貨幣Command(5));
            Assert.IsTrue(y is 自動販売機Insert貨幣FailureCommandResult);
        }

        [TestMethod]
        public void 硬貨100円と500円を投入してから返却すると100円と500円が返却される() {
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            u.Insert貨幣(new 自動販売機Insert貨幣Command(100));
            u.Insert貨幣(new 自動販売機Insert貨幣Command(500));
            var y = u.Refund貨幣(new 自動販売機Refund貨幣Command());
            Assert.IsTrue(y is 自動販売機Refund貨幣CommandResult);
            var z = y as 自動販売機Refund貨幣CommandResult;
            var t = z.返金.OrderBy(x => x.Value).ToList();
            Assert.IsTrue(t[0] == new 硬貨(100));
            Assert.IsTrue(t[1] == new 硬貨(500));
        }

        [TestMethod]
        public void 投入金額不足だと失敗する() {
            var c = new 販売コード("COLA-001");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(120)));
            r.Add数量(c, 1);
            u.Insert貨幣(new 自動販売機Insert貨幣Command(100));
            var y = u.決済(new 自動販売機決済Command(c));
            Assert.IsTrue(y is 自動販売機決済FailureCommandResult);
            var z = y as 自動販売機決済FailureCommandResult;
            Assert.IsTrue(z.Reason == 自動販売機決済FailureReason.金額不足);
        }

        [TestMethod]
        public void 在庫切れだと失敗する() {
            var c = new 販売コード("COLA-001");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(120)));
            u.Insert貨幣(new 自動販売機Insert貨幣Command(500));
            var y = u.決済(new 自動販売機決済Command(c));
            Assert.IsTrue(y is 自動販売機決済FailureCommandResult);
            var z = y as 自動販売機決済FailureCommandResult;
            Assert.IsTrue(z.Reason == 自動販売機決済FailureReason.在庫切れ);
        }

        [TestMethod]
        public void 該当商品なしだと失敗する() {
            var c = new 販売コード("COLA-001");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(120)));
            u.Insert貨幣(new 自動販売機Insert貨幣Command(500));
            var y = u.決済(new 自動販売機決済Command(new 販売コード("WATR-002")));
            Assert.IsTrue(y is 自動販売機決済FailureCommandResult);
            var z = y as 自動販売機決済FailureCommandResult;
            Assert.IsTrue(z.Reason == 自動販売機決済FailureReason.該当商品なし);
        }

        [TestMethod]
        public void 在庫があって十分な金額であれば成功する() {
            var c = new 販売コード("COLA-001");
            var r = new Memory商品在庫Repository();
            var x = new 硬貨自動販売機(r, new 硬貨支払機());
            var u = new 硬貨自動販売機ユーザUseCase(x);
            r.Register商品(c, new 販売商品(new 商品名("コーラ"), new 通貨(120)));
            r.Add数量(c, 1);
            u.Insert貨幣(new 自動販売機Insert貨幣Command(500));
            var y = u.決済(new 自動販売機決済Command(c));
            Assert.IsTrue(y is 自動販売機決済SuccessCommandResult);
        }
    }
}
