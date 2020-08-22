using System;


namespace VendingMachineSample.Domain.Model {
    public class 商品名 {
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
            return this == (商品名)obj;
        }

        public override int GetHashCode() {
            return this.Value.GetHashCode();
        }

        public static bool operator==(商品名 lhs, 商品名 rhs) {
            return lhs.Value == rhs.Value;
        }

        public static bool operator!=(商品名 lhs, 商品名 rhs) {
            return !(lhs == rhs);
        }
    }
}
