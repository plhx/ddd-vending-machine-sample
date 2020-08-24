using System;
using System.Collections.Generic;
using VendingMachineSample.Core.Application.Command;
using VendingMachineSample.Core.Domain.Models;


namespace VendingMachineSample.Core.Application.UseCases {
    public interface I自動販売機ユーザUseCase {
        public I自動販売機CommandResult 商品一覧(自動販売機商品一覧Command command);
        public I自動販売機CommandResult 投入金額合計(自動販売機投入金額合計Command command);
        public I自動販売機CommandResult Insert貨幣(自動販売機Insert貨幣Command command);
        public I自動販売機CommandResult Refund貨幣(自動販売機Refund貨幣Command command);
        public I自動販売機CommandResult 決済(自動販売機決済Command command);
    }

    public class 硬貨自動販売機ユーザUseCase : I自動販売機ユーザUseCase {
        private readonly I自動販売機<硬貨> 自動販売機;

        public 硬貨自動販売機ユーザUseCase(I自動販売機<硬貨> 自動販売機) {
            this.自動販売機 = 自動販売機;
        }

        public I自動販売機CommandResult 商品一覧(自動販売機商品一覧Command command) {
            var result = new Dictionary<販売コード, 販売可能商品>();
            foreach(var code in this.自動販売機.販売コードList()) {
                var item = this.自動販売機.Find商品(code);
                result[code] = new 販売可能商品(item.商品名, item.価格, item.数量 > 0);
            }
            return new 自動販売機商品一覧CommandResult(result);
        }

        public I自動販売機CommandResult 投入金額合計(自動販売機投入金額合計Command command) {
            var result = this.自動販売機.投入金額合計();
            return new 自動販売機投入金額合計CommandResult(result);
        }

        public I自動販売機CommandResult Insert貨幣(自動販売機Insert貨幣Command command) {
            var result = new 通貨(command.投入金額);
            try {
                this.自動販売機.Insert貨幣(new 硬貨(command.投入金額));
            }
            catch {
                return new 自動販売機Insert貨幣FailureCommandResult(result);
            }
            return new 自動販売機Insert貨幣SuccessCommandResult(result);
        }

        public I自動販売機CommandResult Refund貨幣(自動販売機Refund貨幣Command command) {
            var result = this.自動販売機.Refund貨幣();
            return new 自動販売機Refund貨幣CommandResult(result);
        }

        public I自動販売機CommandResult 決済(自動販売機決済Command command) {
            try {
                var result = this.自動販売機.決済(command.販売コード);
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
    }
}
