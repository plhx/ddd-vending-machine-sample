using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 販売コードTest {
        public class 販売コードSub : 販売コード {
            public 販売コードSub(string value) : base(value) {

            }
        }

        [TestMethod]
        public void 空文字列の販売コードは作成できない() {
            try {
                new 販売コード("");
            }
            catch(ArgumentException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 空白文字だけの販売コードは作成できない() {
            try {
                new 販売コード(" \t ");
            }
            catch(ArgumentException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当な販売コード() {
            new 販売コード("ayb");
            new 販売コード("aybabtu");
            new 販売コード("aybabtu2101");
        }

        [TestMethod]
        public void ToStringは商品名を返す() {
            Assert.IsTrue(new 販売コード("asap").ToString() == "asap");
        }

        [TestMethod]
        public void Nullとは等しくない() {
            Assert.IsFalse(new 販売コード("ayb").Equals(null as object));
            Assert.IsFalse(new 販売コード("ayb").Equals(null));
        }

        [TestMethod]
        public void サブクラスをobjectにアップキャストすると等しくない() {
            Assert.IsFalse(new 販売コード("ayb").Equals(new 販売コードSub("ayb") as object));
        }

        [TestMethod]
        public void サブクラスとは等しい() {
            Assert.IsTrue(new 販売コード("ayb").Equals(new 販売コードSub("ayb")));
        }

        [TestMethod]
        public void Thisとは等しい() {
            var x = new 販売コード("ayb");
            Assert.IsTrue(x.Equals(x as object));
            Assert.IsTrue(x.Equals(x));
        }

        [TestMethod]
        public void 違うオブジェクトでも名前が同じなら等しい() {
            var a = new 販売コード("ayb");
            var b = new 販売コード("ayb");
            Assert.IsTrue(a.Equals(b as object));
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void 同じ名前ならHashCodeも同じ() {
            var a = new 販売コード("ayb");
            var b = new 販売コード("ayb");
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());
        }
    }
}
