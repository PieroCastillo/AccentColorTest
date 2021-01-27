using System;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Media;
using Microsoft.Win32;
using Avalonia.Utilities;

namespace AccentColorTest
{
 #nullable enable  
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{Color.Parse($"{GetAccentColor()}")}"); //Imprime en Consola
        }


        static object GetAccentColor()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows); //Checa si es windows
            var keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\DWM\\"; //Link de acceso
            var valueName = "AccentColor"; //Key de la dirección
            var defaultIntValue = 30935;//Valor default del color en Int
            var defaultValue = "#FF0078D7";// default Avalonia Accent Color

            object value = new object();

            switch (isWindows)
            {
                case true:
                    var c = (int)Registry.GetValue(keyName, valueName, defaultValue);
                    value = ("#" + c.ToString("X2")) ??( "#" + defaultIntValue.ToString("X2")); //retorna el color de Acento
                    break;
                case false:
                    value = defaultValue; //retorna el color default
                    break;
            }
            return value;
        }
    }
}
