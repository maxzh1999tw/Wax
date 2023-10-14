using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Wax.MVC.Models.Shared;

namespace Wax.MVC.Utilities.Extensions
{
    internal static class EnumExtension
    {
        public static IEnumerable<VuetifySelectItem> ToSelectItems<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                       .Cast<TEnum>()
                       .Select(e => new VuetifySelectItem(e.GetDisplayName(), e))
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
