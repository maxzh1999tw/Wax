using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Wax.MVC.Utilities.Attributes
{
    public class ElementRequiredAttribute : RequiredAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (!(value is IEnumerable))
            {
                throw new InvalidOperationException("屬性不是一個 IEnumerable 物件。");
            }

            var enumerator = ((IEnumerable)value).GetEnumerator();
            return enumerator.MoveNext();
        }
    }
}
