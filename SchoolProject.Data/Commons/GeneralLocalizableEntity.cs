using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Commons
{
    public class GeneralLocalizableEntity
    {
        public string localize(string NameAr, string NameEn)
        {

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            if (cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return NameAr;
            }
            return NameEn;
        }
    }
}
