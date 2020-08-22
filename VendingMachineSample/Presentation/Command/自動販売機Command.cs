using System;
using VendingMachineSample.Domain.Model;


namespace VendingMachineSample.Presentation.Command {
    public interface I自動販売機Command {

    }

    public class 自動販売機InvalidCommand : I自動販売機Command {
        public readonly string CommandString;

        public 自動販売機InvalidCommand(string commandString) {
            this.CommandString = commandString ?? throw new ArgumentNullException(nameof(commandString) + " cannot be null");
        }
    }

    public class 自動販売機商品一覧Command : I自動販売機Command {

    }

    public class 自動販売機投入金額合計Command : I自動販売機Command {

    }

    public class 自動販売機Insert貨幣Command : I自動販売機Command {
        public readonly int 投入金額;

        public 自動販売機Insert貨幣Command(int 投入金額) {
            this.投入金額 = 投入金額;
        }
    }

    public class 自動販売機Refund貨幣Command : I自動販売機Command {

    }

    public class 自動販売機決済Command : I自動販売機Command {
        public readonly 販売コード 販売コード;

        public 自動販売機決済Command(販売コード 販売コード) {
            this.販売コード = 販売コード ?? throw new ArgumentNullException(nameof(販売コード) + " cannot be null");
        }
    }
}
