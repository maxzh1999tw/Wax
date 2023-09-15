using Microsoft.AspNetCore.Mvc;

namespace Wax.MVC
{
    internal static class IUrlHelperExtension
    {
        public static string? Api(this IUrlHelper Url, string? name)
        {
            return Url.Action(name, "Api");
        } 
    }
}
