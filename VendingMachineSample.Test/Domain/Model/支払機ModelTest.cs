using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 支払機Test {
        [TestMethod]
        public void 硬貨100円と500円を投入したら合計300円になる() {
            var x = new 硬貨支払機();
            x.Insert貨幣(new 硬貨(100));
            x.Insert貨幣(new 硬貨(500));
            Assert.IsTrue(x.投入金額合計() == new 通貨(600));
        }

        [TestMethod]
        public void 硬貨100円と500円を投入して返却すると100円と500円が返却される() {
            var x = new 硬貨支払機();
            x.Insert貨幣(new 硬貨(100));
            x.Insert貨幣(new 硬貨(500));
            var y = x.Refund貨幣().OrderBy(x => x.Value).ToList();
            Assert.IsTrue(y[0] == new 硬貨(100));
            Assert.IsTrue(y[1] == new 硬貨(500));
        }

        [TestMethod]
        public void 硬貨100円と500円を投入して返却すると投入金額は0円になる() {
            var x = new 硬貨支払機();
            x.Insert貨幣(new 硬貨(100));
            x.Insert貨幣(new 硬貨(500));
            x.Refund貨幣();
            Assert.IsTrue(x.投入金額合計() == new 通貨(0));
        }

        [TestMethod]
        public void 足りない金額で決済すると失敗する() {
            var x = new 硬貨支払機();
            x.Insert貨幣(new 硬貨(10));
            try {
                x.決済(new 通貨(100));
            }
            catch(InvalidOperationException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ちょうどの金額で決済するとお釣りが出ない() {
            var x = new 硬貨支払機();
            x.Insert貨幣(new 硬貨(100));
            var y = x.決済(new 通貨(100)).ToList();
            Assert.IsTrue(y.Count == 0);
        }

        [TestMethod]
        public void お釣りが出る金額で決済するとお釣りが出る() {
            var x = new 硬貨支払機();
            x.Insert貨幣(new 硬貨(500));
            var y = x.決済(new 通貨(140)).OrderBy(x => x.Value).ToList();
            Assert.IsTrue(y[0] == new 硬貨(10));
            Assert.IsTrue(y[1] == new 硬貨(50));
            Assert.IsTrue(y[2] == new 硬貨(100));
            Assert.IsTrue(y[3] == new 硬貨(100));
            Assert.IsTrue(y[4] == new 硬貨(100));
        }
    }
}
