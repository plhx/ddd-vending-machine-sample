using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 決済結果Test {
        [TestMethod]
        public void 商品名がNullでは作成できない() {
            try {
                new 決済結果(null, new 貨幣[] { new 硬貨(100) });
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 釣銭がNullでは作成できない() {
            try {
                new 決済結果(new 商品(new 商品名("コーラ")), null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当な商品名と釣銭で作成する() {
            new 決済結果(
                new 商品(new 商品名("コーラ")),
                new 貨幣[] { new 硬貨(100) }
            );
        }
    }
}
