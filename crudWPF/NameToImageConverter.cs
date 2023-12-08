using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Imaging;

namespace crudWPF
{
    public class NameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            Uri uri;
         
            if (value.ToString() == "Male")
            {
                uri = new Uri("pack://application:,,,/crudWPF;component/Images/male.png");
            }
            else {
                uri = new Uri("pack://application:,,,/crudWPF;component/Images/female.png");
            }

            var bitmap = new BitmapImage(uri);
            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
