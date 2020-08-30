using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 商品Test {
        [TestMethod]
        public void 商品名はNullで作成できない() {
            try {
                new 商品(null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当な商品名で商品を作成する() {
            new 商品(new 商品名("コーラ"));
        }
    }

    [TestClass]
    public class 販売商品Test {
        [TestMethod]
        public void 価格はNullで作成できない() {
            try {
                new 販売商品(new 商品名("コーラ"), null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当な商品名と価格で作成する() {
            new 販売商品(new 商品名("コーラ"), new 通貨(100));
        }
    }

    [TestClass]
    public class 販売商品在庫Test {
        [TestMethod]
        public void マイナスの数量で作成できない() {
            try {
                new 販売商品在庫(new 商品名("コーラ"), new 通貨(100), -1);
            }
            catch(ArgumentOutOfRangeException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当な商品名と価格と数量で作成する() {
            new 販売商品在庫(new 商品名("コーラ"), new 通貨(100), 10);
        }
    }

    [TestClass]
    public class 販売可能商品Test {
        [TestMethod]
        public void 適当な商品名と価格と販売可能で作成する() {
            new 販売可能商品(new 商品名("コーラ"), new 通貨(100), true);
        }
    }
}
