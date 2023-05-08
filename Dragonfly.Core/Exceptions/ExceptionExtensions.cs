using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dragonfly.Core
{
    public static class ExceptionExtensions
    {
        #region Public Methods
        public static string ToJson(this Exception ex)
        {
            var msg =
              new { ex.Message, InnerException = ex.InnerException?.Message, ex.StackTrace };
            return JsonConvert.SerializeObject(msg);
        }

        public static IList<string> Messages(this Exception ex)
        {
            var messages = new List<string>();

            var i = 0;
            var enumerator = ex;
            while (enumerator != null)
            {
                messages.Add($"{new string(' ', i) + ((i > 0) ? "+->" : string.Empty)}{enumerator.Message}");
                enumerator = enumerator.InnerException;
                i++;
            }

            return messages;
        }

        public static string AllMessages(this Exception ex)
        {
            var messages = new StringBuilder();
            var enumerator = ex;
            while (enumerator != null)
            {
                messages.AppendFormat("{0}{1}", messages.Length > 0 ? " / " : string.Empty, enumerator.Message);
                enumerator = enumerator.InnerException;
            }

            return messages.ToString();
        }
        #endregion
    }
}
