using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LAB6WPF
{
    enum Precipitation
    {
        солнечно = 0, //для проверки возможности использования русских букв  
        облачно = 1,
        дождь = 2,
        снег = 3
    };
    internal class WeatherControl:DependencyObject
    {
        private Precipitation precipitation;
        private string windDirection;
        private int windSpeed;
        public string WindDirection { get; set; }
        public int WindSpeed {
            get
            { 
                return windSpeed;
            }
            set
            {
                if (value >= 0)
                {
                    windSpeed = value;
                }
                else
                {
                    windSpeed = 0;//если отрицательная скорость ветра то ставим 0 
                }           
            } 
         }
        public int Temperature
        {
            get => (int)GetValue(temperatureProperty);
            set => SetValue(temperatureProperty, value);
        }
        public WeatherControl(string windDirection, int windSpeed, int temperature, Precipitation precipitation)
        {
            this.WindDirection = windDirection;
            this.WindSpeed = windSpeed;
            this.Temperature = temperature;
            this.precipitation = precipitation;
        }
        public static readonly DependencyProperty temperatureProperty;
        static WeatherControl()
        {
            temperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemperature)),
                    new ValidateValueCallback(ValidateTemperature));                  

        }

        private static bool ValidateTemperature(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50 && v <= 50)
            {
                return v;
            }
            else
            {
                if (v < -50)
                {
                    return -50;
                }
                else
                {
                    return 50;
                }
            }
        }
    }
}
