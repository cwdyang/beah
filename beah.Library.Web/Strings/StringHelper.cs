using System;
using System.Collections.Generic;
using System.Linq;

namespace beah.Library.Web.Strings
{
    public static class StringHelper
    {
        public static string FormatWithNamedParameters(this string input, dynamic parameters, bool blankIfNull = false, List<string> excludeTheseFields = null)
        {
            Type type = parameters.GetType();
            string tag;

            excludeTheseFields = ((excludeTheseFields) ?? new List<string>());

            type.GetProperties().Select(x => x).ToList().ForEach(y =>
            {
                tag = "{" + y.Name + "}";

                try
                {
                    input = input.Replace(tag,
                    (
                    (excludeTheseFields.Any(x => x == y.Name)) ?
                    tag :

                    (((y.GetValue(parameters)) ?? ((blankIfNull) ? string.Empty : tag)).ToString())
                    )
                    );

                }
                catch (Exception e)
                {

                    throw;
                }

            });

            return input;
        }

        public static string FormatWithNamedParameters(this string input, List<dynamic> parameters, bool blankIfNull = false, List<string> excludeTheseFields = null, bool reverseOrder = true)
        {
            if (reverseOrder)
                parameters.Reverse();

            parameters.ForEach(parameter =>
            {
                input = StringHelper.FormatWithNamedParameters(input, parameter, blankIfNull, excludeTheseFields);
            });

            return input;
        }


    }
}
