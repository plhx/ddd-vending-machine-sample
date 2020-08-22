using System;
using System.Linq;
using VendingMachineSample.Presentation.Command;


namespace VendingMachineSample.Presentation.View {
    public interface I自動販売機Presentation {
        public void Print(I自動販売機CommandResult commandResult);
    }

    public class 自動販売機ConsolePresentation : I自動販売機Presentation {
        public void Print(I自動販売機CommandResult commandResult) {
            if(commandResult is 自動販売機InvalidCommandResult) {
                var result = commandResult as 自動販売機InvalidCommandResult;
                Console.WriteLine($"invalid command - {result.CommandString}");
                return;
            }
            else if(commandResult is 自動販売機商品一覧CommandResult) {
                var result = commandResult as 自動販売機商品一覧CommandResult;
                foreach(var (key, value) in result.商品一覧)
                    Console.WriteLine($"{value.商品名}({key}) - {value.価格}円 - {(value.販売可能 ? "販売中" : "売切")}");
                return;
            }
            else if(commandResult is 自動販売機投入金額合計CommandResult) {
                var result = commandResult as 自動販売機投入金額合計CommandResult;
                Console.WriteLine($"{result.投入金額合計}円");
                return;
            }
            else if(commandResult is 自動販売機Insert貨幣SuccessCommandResult) {
                var result = commandResult as 自動販売機Insert貨幣SuccessCommandResult;
                Console.WriteLine($"{result.投入金額}円投入しました");
                return;
            }
            else if(commandResult is 自動販売機Insert貨幣FailureCommandResult) {
                var result = commandResult as 自動販売機Insert貨幣FailureCommandResult;
                Console.WriteLine($"{result.投入金額}円は受け付けられません");
                return;
            }
            else if(commandResult is 自動販売機Refund貨幣CommandResult) {
                var result = commandResult as 自動販売機Refund貨幣CommandResult;
                foreach(var value in result.返金)
                    Console.WriteLine($"{value}円");
                return;
            }
            else if(commandResult is 自動販売機決済SuccessCommandResult) {
                var result = commandResult as 自動販売機決済SuccessCommandResult;
                Console.WriteLine($"{result.決済結果.商品.商品名}を購入しました");
                Console.WriteLine($"お釣りは {String.Join(", ", result.決済結果.釣銭.Select(x => x.ToString() + "円"))} です");
                return;
            }
            else if(commandResult is 自動販売機決済FailureCommandResult) {
                var result = commandResult as 自動販売機決済FailureCommandResult;
                if(result.Reason == 自動販売機決済FailureReason.在庫切れ)
                    Console.WriteLine("売り切れです");
                else if(result.Reason == 自動販売機決済FailureReason.金額不足)
                    Console.WriteLine("お金が足りません");
                else if(result.Reason == 自動販売機決済FailureReason.該当商品なし)
                    Console.WriteLine("商品が見つかりません");
                return;
            }
            throw new InvalidOperationException($"undefined {nameof(commandResult)} {commandResult}");
        }
    }
}
