using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AccentColorTest
{
    public class AccentColors : Styles
    {
        public AccentColors()
        {
            AvaloniaXamlLoader.Load(this);

            var accentcolor = Color.Parse($"{GetAccentColor()}");
            var light1 = ChangeLuminosity(accentcolor, 0.3d);
            var light2 = ChangeLuminosity(accentcolor, 0.6d);
            var light3 = ChangeLuminosity(accentcolor, 0.8d);
            var dark1 = ChangeLuminosity(accentcolor, -0.3);
            var dark2 = ChangeLuminosity(accentcolor, -0.6);
            var dark3 = ChangeLuminosity(accentcolor, -0.8);


            this.Resources.Add("SystemAccentColor", accentcolor);
            this.Resources.Add("SystemAccentColorLight1", light1);
            this.Resources.Add("SystemAccentColorLight2", light2);
            this.Resources.Add("SystemAccentColorLight3", light3);
            this.Resources.Add("SystemAccentColorDark1", dark1);
            this.Resources.Add("SystemAccentColorDark2", dark2);
            this.Resources.Add("SystemAccentColorDark3", dark3);


            Debug.WriteLine("SystemAccentColor added");
        }

        private object GetAccentColor()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows); //check if the OS is Windows
            var keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\DWM\\"; //Key link
            var valueName = "AccentColor"; //"AccentColor"; //Value name
            var defaultIntValue = 30935;//default Avalonia Accent Color as Int
            var defaultValue = "#FF0078D7";// default Avalonia Accent Color

            object value = new object();

            switch (isWindows)
            {
                case true:
                    var c = (int)Registry.GetValue(keyName, valueName, defaultValue);//gets the value
                    var prev_color = Color.Parse(("#" + c.ToString("X2")));//parse a previous color, windows stores the accent color backwards

                    value = new Color(prev_color.A, prev_color.B, prev_color.G, prev_color.R).ToString() ??
                          ("#" + defaultIntValue.ToString("X2")); //return the System accent color
                    break;
                case false:
                    value = defaultValue; //return default color
                    break;
            }
            return value;
        }

        private Color ChangeLuminosity(Color color, double correctionFactor)
        {
            var red = (double)color.R; 
            var green = (double)color.G; 
            var blue = (double)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return new Color(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
