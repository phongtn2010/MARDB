using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class CCongThuc
    {
        public static string LayTruongTrongCongThuc(String sCongThuc)
        {
            String vR = "";
            int cs1 = sCongThuc.IndexOf("[");
            int cs2 = sCongThuc.IndexOf("]");
            if (cs1 >= 0 && cs2 > cs1)
            {
                vR = sCongThuc.Substring(cs1 + 1, cs2 - cs1 - 1);
            }
            return vR;
        }
    }
}
