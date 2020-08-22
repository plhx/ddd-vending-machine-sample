using System;
using VendingMachineSample.Application.UseCase;
using VendingMachineSample.Domain.Model;
using VendingMachineSample.Presentation.Command;


namespace VendingMachineSample.Presentation.Controller {
    public interface I自動販売機Controller {
        public string Input();
        public I自動販売機CommandResult Execute(I自動販売機Command command);
        public I自動販売機Command Parse(string input);
    }

    public class 自動販売機ConsoleController : I自動販売機Controller {
        private readonly I自動販売機ユーザUseCase UseCase;

        public 自動販売機ConsoleController(I自動販売機ユーザUseCase useCase) {
            this.UseCase = useCase ?? throw new ArgumentNullException(nameof(useCase) + " cannot be null");
        }

        public string Input() {
            Console.Write(">> ");
            return Console.ReadLine();
        }

        public I自動販売機CommandResult Execute(I自動販売機Command command) {
            if(command is 自動販売機InvalidCommand) {
                var commandData = command as 自動販売機InvalidCommand;
                return new 自動販売機InvalidCommandResult(commandData.CommandString);
            }
            else if(command is 自動販売機商品一覧Command) {
                var itemList = this.UseCase.商品一覧();
                return new 自動販売機商品一覧CommandResult(itemList);
            }
            else if(command is 自動販売機投入金額合計Command) {
                var moneyEntered = this.UseCase.投入金額合計();
                return new 自動販売機投入金額合計CommandResult(moneyEntered);
            }
            else if(command is 自動販売機Insert貨幣Command) {
                var commandData = command as 自動販売機Insert貨幣Command;
                try {
                    var coin = new 硬貨(commandData.投入金額);
                    this.UseCase.Insert貨幣(coin);
                    return new 自動販売機Insert貨幣SuccessCommandResult(coin);
                }
                catch {
                }
                return new 自動販売機Insert貨幣FailureCommandResult(new 通貨(commandData.投入金額));
            }
            else if(command is 自動販売機Refund貨幣Command) {
                var refund = this.UseCase.Refund貨幣();
                return new 自動販売機Refund貨幣CommandResult(refund);
            }
            else if(command is 自動販売機決済Command) {
                var commandData = command as 自動販売機決済Command;
                try {
                    var result = this.UseCase.決済(commandData.販売コード);
                    return new 自動販売機決済SuccessCommandResult(result);
                }
                catch(自動販売機金額不足Exception) {
                    return new 自動販売機決済FailureCommandResult(自動販売機決済FailureReason.金額不足);
                }
                catch(自動販売機在庫切れException) {
                    return new 自動販売機決済FailureCommandResult(自動販売機決済FailureReason.在庫切れ);
                }
                catch(自動販売機該当商品なしException) {
                    return new 自動販売機決済FailureCommandResult(自動販売機決済FailureReason.該当商品なし);
                }
                throw new NotImplementedException();
            }
            throw new InvalidOperationException($"undefined command {command}");
        }

        public I自動販売機Command Parse(string input) {
            var args = input.Trim().Split(' ', 2);
            if(args.Length >= 1 && args[0] == "list")
                return new 自動販売機商品一覧Command();
            else if(args.Length >= 1 && args[0] == "show")
                return new 自動販売機投入金額合計Command();
            else if(args.Length >= 2 && args[0] == "insert")
                return new 自動販売機Insert貨幣Command(int.Parse(args[1]));
            else if(args.Length >= 1 && args[0] == "refund")
                return new 自動販売機Refund貨幣Command();
            else if(args.Length >= 2 && args[0] == "pay")
                return new 自動販売機決済Command(new 販売コード(args[1]));
            return new 自動販売機InvalidCommand(input);
        }
    }
}
