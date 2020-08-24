using System;
using System.Diagnostics.CodeAnalysis;


namespace VendingMachineSample.Core.Domain.Models {
    public class 販売コード : IEquatable<販売コード> {
        public readonly string Value;

        public 販売コード(string value) {
            if(String.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value) + " cannot be whitespaces or null");
            this.Value = value;
        }

        public override bool Equals(object obj) {
            if(Object.ReferenceEquals(obj, null))
                return false;
            if(Object.ReferenceEquals(obj, this))
                return true;
            if(obj.GetType() != this.GetType())
                return false;
            return this.Equals(obj as 販売コード);
        }

        public bool Equals([AllowNull] 販売コード other) {
            if(Object.ReferenceEquals(other, null))
                return false;
            if(Object.ReferenceEquals(other, this))
                return true;
            return Object.Equals(this.Value, other.Value);
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.Value);
        }

        public override string ToString() {
            return this.Value;
        }
    }
}
