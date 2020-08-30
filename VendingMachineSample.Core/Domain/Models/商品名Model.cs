using System;


namespace VendingMachineSample.Core.Domain.Models {
    public class 商品名 : IEquatable<商品名> {
        public readonly string Value;

        public 商品名(string value) {
            if(String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value) + "cannot be null or whitespaces");
            this.Value = value;
        }

        public override string ToString() {
            return this.Value.ToString();
        }

        public override bool Equals(object obj) {
            if(Object.ReferenceEquals(obj, null))
                return false;
            if(Object.ReferenceEquals(obj, this))
                return true;
            if(obj.GetType() != this.GetType())
                return false;
            return this.Equals(obj as 商品名);
        }

        public bool Equals(商品名 other) {
            if(Object.ReferenceEquals(other, null))
                return false;
            if(Object.ReferenceEquals(other, this))
                return true;
            return this.Value == other.Value;
        }

        public override int GetHashCode() {
            return this.Value.GetHashCode();
        }
    }
}
