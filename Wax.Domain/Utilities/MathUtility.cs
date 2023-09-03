namespace Wax.Domain.Utilities
{
    internal static class MathUtility
    {
        internal static bool IsBetterThen(double? a, double? b, bool isBiggerBetter)
        {
            if (a == b)
            {
                return false;
            }
            else if (a == null || b == null)
            {
                return isBiggerBetter ? b == null : a == null;
            }
            else
            {
                return isBiggerBetter ? a.Value > b.Value : a.Value < b.Value;
            }
        }

        internal static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }
    }
}
