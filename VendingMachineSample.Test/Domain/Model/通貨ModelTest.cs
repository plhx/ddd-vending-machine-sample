using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 通貨Test {
        [TestMethod]
        public void 通貨1円を作る() {
            new 通貨(1);
        }

        [TestMethod]
        public void 通貨0円を作る() {
            new 通貨(0);
        }

        [TestMethod]
        public void 通貨マイナス1円を作る() {
            new 通貨(-1);
        }

        [TestMethod]
        public void 文字列に変換したものは元の金額の文字列表現に等しい() {
            Assert.IsTrue(通貨.Zero.ToString() == 0.ToString());
        }

        [TestMethod]
        public void Nullとは等しくない() {
            Assert.IsFalse(new 通貨(100).Equals(null as object));
            Assert.IsFalse(new 通貨(100).Equals(null));
        }

        [TestMethod]
        public void Thisとは等しい() {
            var x = new 通貨(100);
            Assert.IsTrue(x.Equals(x as object));
            Assert.IsTrue(x.Equals(x));
        }

        [TestMethod]
        public void サブクラスをobjectにアップキャストすると等しくない() {
            Assert.IsFalse(new 通貨(100).Equals(new 硬貨(100) as object));
        }

        [TestMethod]
        public void サブクラスとは等しい() {
            Assert.IsTrue(new 通貨(100).Equals(new 硬貨(100)));
        }

        [TestMethod]
        public void 違うオブジェクトでも同じ金額なら等しい() {
            Assert.IsTrue(new 通貨(100).Equals(new 通貨(100) as object));
        }

        [TestMethod]
        public void 同じ金額ならHashCodeも同じ() {
            var a = new 通貨(100);
            var b = new 通貨(100);
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());
        }

        [TestMethod]
        public void CompareToの確認() {
            Assert.IsTrue(new 通貨(100).CompareTo(new 通貨(200)) == -1);
            Assert.IsTrue(new 通貨(200).CompareTo(new 通貨(100)) == 1);
            Assert.IsTrue(new 通貨(100).CompareTo(new 通貨(100)) == 0);
        }

        [TestMethod]
        public void 通貨100円と通貨100円は等しい() {
            Assert.IsTrue(new 通貨(100) == new 通貨(100));
        }

        [TestMethod]
        public void 通貨100円と通貨200円は異なる() {
            Assert.IsTrue(new 通貨(100) != new 通貨(200));
        }

        [TestMethod]
        public void 通貨100円は通貨200円未満() {
            Assert.IsTrue(new 通貨(100) < new 通貨(200));
        }

        [TestMethod]
        public void 通貨200円は通貨100円より高い() {
            Assert.IsTrue(new 通貨(200) > new 通貨(100));
        }

        [TestMethod]
        public void 通貨100円は通貨200円以下() {
            Assert.IsTrue(new 通貨(100) <= new 通貨(200));
        }

        [TestMethod]
        public void 通貨200円は通貨100円以上() {
            Assert.IsTrue(new 通貨(200) >= new 通貨(100));
        }

        [TestMethod]
        public void 通貨100円と通貨200円を足すと通貨300円になる() {
            var a = new 通貨(100);
            var b = new 通貨(200);
            var c = new 通貨(300);
            Assert.IsTrue(a + b == c);
        }

        [TestMethod]
        public void 通貨100円から通貨200円を引くと通貨マイナス100円になる() {
            var a = new 通貨(100);
            var b = new 通貨(200);
            var c = new 通貨(-100);
            Assert.IsTrue(a - b == c);
        }

        [TestMethod]
        public void 通貨100円を3倍すると300円になる() {
            Assert.IsTrue(new 通貨(100) * 3 == new 通貨(300));
            Assert.IsTrue(3 * new 通貨(100) == new 通貨(300));
        }

        [TestMethod]
        public void 硬貨99円は作れない() {
            try {
                new 硬貨(99);
            }
            catch(ArgumentException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 硬貨1000円は作れない() {
            try {
                new 硬貨(1000);
            }
            catch(ArgumentException) {
                return;
            }
            Assert.Fail();
        }
    }
}
