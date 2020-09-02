using System;
using VendingMachineSample.Core.Application.Command;
using VendingMachineSample.Core.Application.UseCases;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.UserInterface.CLI.Presentation.Controller {
    public interface I自動販売機Controller {
        public I自動販売機CommandResult Execute(I自動販売機Command command);
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
                var commandEntity = command as 自動販売機InvalidCommand;
                return new 自動販売機InvalidCommandResult(commandEntity.CommandString);
            }
            else if(command is 自動販売機商品一覧Command) {
                var commandEntity = command as 自動販売機商品一覧Command;
                return this.UseCase.商品一覧(commandEntity);
            }
            else if(command is 自動販売機投入金額合計Command) {
                var commandEntity = command as 自動販売機投入金額合計Command;
                return this.UseCase.投入金額合計(commandEntity);
            }
            else if(command is 自動販売機Insert貨幣Command) {
                var commandEntity = command as 自動販売機Insert貨幣Command;
                return this.UseCase.Insert貨幣(commandEntity);
            }
            else if(command is 自動販売機Refund貨幣Command) {
                var commandEntity = command as 自動販売機Refund貨幣Command;
                return this.UseCase.Refund貨幣(commandEntity);
            }
            else if(command is 自動販売機決済Command) {
                var commandEntity = command as 自動販売機決済Command;
                return this.UseCase.決済(commandEntity);
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
