using System;
using System.Linq;
using System.Collections.Generic;


namespace VendingMachineSample.Core.Domain.Models {
    public interface I支払機<T> where T : 貨幣 {
        public 通貨 投入金額合計();
        public void Insert貨幣(T 投入貨幣);
        public IEnumerable<T> Refund貨幣();
        public IEnumerable<T> 決済(通貨 請求金額);
    }

    public class 硬貨支払機 : I支払機<硬貨> {
        private List<硬貨> 投入硬貨 = new List<硬貨>();

        public 通貨 投入金額合計() {
            return new 通貨(this.投入硬貨.Select(x => x.Value).Sum());
        }

        public void Insert貨幣(硬貨 投入貨幣) {
            this.投入硬貨.Add(投入貨幣);
        }

        public IEnumerable<硬貨> Refund貨幣() {
            var result = this.投入硬貨.ToList();
            this.投入硬貨.Clear();
            return result;
        }

        public IEnumerable<硬貨> 決済(通貨 請求金額) {
            if(this.投入金額合計() < 請求金額)
                throw new InvalidOperationException("cannot payment");
            var result = this.Factorize硬貨(this.投入金額合計() - 請求金額);
            this.投入硬貨.Clear();
            return result;
        }

        private IEnumerable<硬貨> Factorize硬貨(通貨 金額) {
            List<硬貨> result = new List<硬貨>();
            foreach(var x in 硬貨.AvailableValues.OrderByDescending(x => x)) {
                int n = 金額.Value / x;
                for(int i = 0; i < n; i++)
                    result.Add(new 硬貨(x));
                金額 -= new 硬貨(x) * n;
            }
            return result;
        }
    }
}
