using Spring.AccioHelpers;
using Spring.ValueConverter;
using System;
using System.Drawing;
using System.Globalization;

namespace Spring.Pages.ValueConverter
{
    /// <summary>
    /// this converter check upcomming bool value if true gonna search for company logo, if not then don't
    /// return bitmap or icon or image.
    /// </summary>
    public class BoolToImgConverter : BaseValueConverter<BoolToImgConverter>
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (bool)value ? new Bitmap(AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring.exe").Length) + "init\\companylogo.png") : null;
            }
            catch (Exception excE)
            {
                return (bool)value ? new Bitmap(AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring for Server.exe").Length) + "init\\companylogo.png") : null;

            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
