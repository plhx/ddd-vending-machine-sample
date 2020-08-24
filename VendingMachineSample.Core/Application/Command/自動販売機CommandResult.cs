using System;
using System.Collections.Generic;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Core.Application.Command {
    public interface I自動販売機CommandResult {

    }

    public class 自動販売機InvalidCommandResult : I自動販売機CommandResult {
        public readonly string CommandString;

        public 自動販売機InvalidCommandResult(string commandString) {
            this.CommandString = commandString ?? throw new ArgumentNullException(nameof(commandString) + " cannot be null");
        }
    }

    public class 自動販売機商品一覧CommandResult : I自動販売機CommandResult {
        public readonly Dictionary<販売コード, 販売可能商品> 商品一覧;

        public 自動販売機商品一覧CommandResult(Dictionary<販売コード, 販売可能商品> 商品一覧) {
            this.商品一覧 = 商品一覧;
        }
    }

    public class 自動販売機投入金額合計CommandResult : I自動販売機CommandResult {
        public readonly 通貨 投入金額合計;

        public 自動販売機投入金額合計CommandResult(通貨 投入金額合計) {
            this.投入金額合計 = 投入金額合計;
        }
    }

    public class 自動販売機Insert貨幣SuccessCommandResult : I自動販売機CommandResult {
        public readonly 通貨 投入金額;

        public 自動販売機Insert貨幣SuccessCommandResult(通貨 投入金額) {
            this.投入金額 = 投入金額 ?? throw new ArgumentNullException(nameof(投入金額) + " cannot be null");
        }
    }

    public class 自動販売機Insert貨幣FailureCommandResult : I自動販売機CommandResult {
        public readonly 通貨 投入金額;

        public 自動販売機Insert貨幣FailureCommandResult(通貨 投入金額) {
            this.投入金額 = 投入金額 ?? throw new ArgumentNullException(nameof(投入金額) + " cannot be null");
        }
    }

    public class 自動販売機Refund貨幣CommandResult : I自動販売機CommandResult {
        public readonly IEnumerable<貨幣> 返金;

        public 自動販売機Refund貨幣CommandResult(IEnumerable<貨幣> 返金) {
            this.返金 = 返金;
        }
    }

    public class 自動販売機決済SuccessCommandResult : I自動販売機CommandResult {
        public readonly 決済結果 決済結果;

        public 自動販売機決済SuccessCommandResult(決済結果 決済結果) {
            this.決済結果 = 決済結果 ?? throw new ArgumentNullException(nameof(決済結果) + " cannot be null");
        }
    }

    public enum 自動販売機決済FailureReason {
        在庫切れ,
        金額不足,
        該当商品なし
    }

    public class 自動販売機決済FailureCommandResult : I自動販売機CommandResult {
        public readonly 自動販売機決済FailureReason Reason;

        public 自動販売機決済FailureCommandResult(自動販売機決済FailureReason reason) {
            this.Reason = reason;
        }
    }
}
