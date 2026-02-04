using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Asistant_FrameWork.ValidationAttributes
{
    public class RequireUppercaseAttribute: ValidationAttribute
    {
        public RequireUppercaseAttribute()
        {
            ErrorMessage = "مقدار ورودی باید حداقل یک کاراکتر بزرگ (Uppercase) داشته باشد.";
        }
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var input = value.ToString();
            return Regex.IsMatch(input, "[A-Z]");
        }
    }
}
