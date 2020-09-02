using System;
using System.Diagnostics.CodeAnalysis;


namespace VendingMachineSample.Core.Domain.Models {
    public class 通貨 : IEquatable<通貨>, IComparable<通貨> {
        public readonly int Value;

        public static readonly 通貨 Zero = new 通貨(0);

        public 通貨(int value) {
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
            return this.Equals(obj as 通貨);
        }

        public bool Equals(通貨 other) {
            if(Object.ReferenceEquals(other, null))
                return false;
            if(Object.ReferenceEquals(other, this))
                return true;
            return this == other;
        }

        public override int GetHashCode() {
            return HashCode.Combine(this.Value);
        }

        public int CompareTo([AllowNull] 通貨 other) {
            if(this < other)
                return -1;
            else if(this > other)
                return 1;
            return 0;
        }

        public static bool operator==(通貨 lhs, 通貨 rhs) {
            return lhs.Value == rhs.Value;
        }

        public static bool operator!=(通貨 lhs, 通貨 rhs) {
            return !(lhs == rhs);
        }

        public static bool operator<(通貨 lhs, 通貨 rhs) {
            return lhs.Value < rhs.Value;
        }

        public static bool operator<=(通貨 lhs, 通貨 rhs) {
            return !(lhs > rhs);
        }

        public static bool operator>(通貨 lhs, 通貨 rhs) {
            return lhs.Value > rhs.Value;
        }

        public static bool operator>=(通貨 lhs, 通貨 rhs) {
            return !(lhs < rhs);
        }

        public static 通貨 operator+(通貨 lhs, 通貨 rhs) {
            return new 通貨(lhs.Value + rhs.Value);
        }

        public static 通貨 operator-(通貨 lhs, 通貨 rhs) {
            return new 通貨(lhs.Value - rhs.Value);
        }

        public static 通貨 operator*(通貨 lhs, int rhs) {
            return new 通貨(lhs.Value * rhs);
        }

        public static 通貨 operator*(int lhs, 通貨 rhs) {
            return rhs * lhs;
        }
    }

    public class 貨幣 : 通貨 {
        public static readonly int[] AvailableValues = new int[] {1, 5, 10, 50, 100, 500, 1000, 5000, 10000};

        protected 貨幣(int value) : base(value) {
            if(!Array.Exists(AvailableValues, (x) => x == value))
                throw new ArgumentException($"cannot instantiate value {value}");
        }
    }

    public class 硬貨 : 貨幣 {
        public static new readonly int[] AvailableValues = new int[] {1, 5, 10, 50, 100, 500};

        public 硬貨(int value) : base(value) {
            if(!Array.Exists(AvailableValues, (x) => x == value))
                throw new ArgumentException($"cannot instantiate value {value}");
        }
    }
}
