using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 商品名Test {
        public class 商品名Sub : 商品名 {
            public 商品名Sub(string value) : base(value) {

            }
        }

        [TestMethod]
        public void 空文字列の商品名は作成できない() {
            try {
                new 商品名("");
            }
            catch(ArgumentException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 空白文字だけの商品名は作成できない() {
            try {
                new 商品名(" \t ");
            }
            catch(ArgumentException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当な商品名() {
            new 商品名("a");
            new 商品名("ナチュラルミネラルウォーター");
            new 商品名("アルティメット茶");
        }

        [TestMethod]
        public void ToStringは商品名を返す() {
            Assert.IsTrue(new 商品名("コーラ").ToString() == "コーラ");
        }

        [TestMethod]
        public void Nullとは等しくない() {
            Assert.IsFalse(new 商品名("コーラ").Equals(null as object));
            Assert.IsFalse(new 商品名("コーラ").Equals(null));
        }

        [TestMethod]
        public void サブクラスをobjectにアップキャストすると等しくない() {
            Assert.IsFalse(new 商品名("コーラ").Equals(new 商品名Sub("コーラ") as object));
        }

        [TestMethod]
        public void サブクラスとは等しい() {
            Assert.IsTrue(new 商品名("コーラ").Equals(new 商品名Sub("コーラ")));
        }

        [TestMethod]
        public void Thisとは等しい() {
            var x = new 商品名("コーラ");
            Assert.IsTrue(x.Equals(x as object));
            Assert.IsTrue(x.Equals(x));
        }

        [TestMethod]
        public void 違うオブジェクトでも名前が同じなら等しい() {
            var a = new 商品名("コーラ");
            var b = new 商品名("コーラ");
            Assert.IsTrue(a.Equals(b as object));
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void 同じ名前ならHashCodeも同じ() {
            var a = new 商品名("コーラ");
            var b = new 商品名("コーラ");
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());
        }
    }
}
