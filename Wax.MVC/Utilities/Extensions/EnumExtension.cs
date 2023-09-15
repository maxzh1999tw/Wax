using System.ComponentModel.DataAnnotations;

namespace Wax.MVC.Utilities.Extensions
{
    internal static class EnumExtension
    {
        public static IEnumerable<KeyValuePair<string, TEnum>> ToListItems<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                       .Cast<TEnum>()
                       .Select(e => new KeyValuePair<string, TEnum>(e.GetDisplayName(), e))
                       .ToArray();
        }

        public static string GetDisplayName<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            var type = typeof(TEnum);
            var members = type.GetMember(enumValue.ToString());
            if (members.Length > 0)
            {
                var attrs = members[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DisplayAttribute)attrs[0]).Name ?? enumValue.ToString();
                }
            }
            return enumValue.ToString();
        }
    }
}
