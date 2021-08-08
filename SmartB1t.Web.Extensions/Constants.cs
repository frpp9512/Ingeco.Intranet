using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Web.Extensions
{
    public static class TempDataAlertModelState
    {
        public static string Created = nameof(Created);
        public static string Updated = nameof(Updated);
        public static string Removed = nameof(Removed);
    }

    public static class TempDataAlertConstants
    {
        public static string UserDeactivated = nameof(UserDeactivated);
        public static string UserDeactivatedEmail = nameof(UserDeactivatedEmail);
        public static string UserLoggedOut = nameof(UserLoggedOut);
        public static string EmailChanged = nameof(EmailChanged);
        public static string PasswordChanged = nameof(PasswordChanged);
    }
}
