using AKT.DVDCentral.BL.Models;
using AKT.DVDCentral.UI.Extensions;

namespace AKT.DVDCentral.UI.Models
{
    public static class Authenticate
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
