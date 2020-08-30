using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineSample.Core.Application.Command;


namespace VendingMachineSample.Test {
    [TestClass]
    public class 自動販売機InvalidCommandTest {
        [TestMethod]
        public void Nullでは生成できない() {
            try {
                new 自動販売機InvalidCommand(null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当なコマンドで生成する() {
            var x = new 自動販売機InvalidCommand("all your base are belong to us");
            Assert.IsTrue(x.CommandString == "all your base are belong to us");
        }
    }

    [TestClass]
    public class 自動販売機InvalidCommandResultTest {
        [TestMethod]
        public void Nullでは生成できない() {
            try {
                new 自動販売機InvalidCommandResult(null);
            }
            catch(ArgumentNullException) {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void 適当なコマンドで生成する() {
            var x = new 自動販売機InvalidCommandResult("all your base are belong to us");
            Assert.IsTrue(x.CommandString == "all your base are belong to us");
        }
    }
}
