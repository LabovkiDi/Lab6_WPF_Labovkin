using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Lab6_WPF_Labovkin
{
    //перечисление
    enum Precipitation
    {
        sunny,
        cloudly,
        rain,
        snow
    }
    //наследуем свой класс WeatherControl от абстрактного класса DependencyObject
    class WeatherControl : DependencyObject
    {
        private Precipitation precipitation;
        private string wind_direction;
        private int wind_speed;
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public WeatherControl(string winddir, int windsp, Precipitation precipitation)
        {
            this.WindDirection = winddir;
            this.WindSpeed = windsp;
            this.precipitation = precipitation;
        }
        //создаем свойство зависимостей
        public static readonly DependencyProperty TempProperty;
        public int Temp
        {
            //пишем 2 метода GetValue и SetValue для получения значений свойств
            get => (int)GetValue(TempProperty);
            set => SetValue(TempProperty, value);
        }
        static WeatherControl()
        {
            //регистрируем свойства в статическом конструкторе нашего класса с помощью метода Register
            TempProperty = DependencyProperty.Register(
                nameof(Temp),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    //создаем 2 делегата для валидации значения свойства
        //делегат ValidateValueCallback возвращает true, если значение проходит валидацию
        //делегат CoerceValueCallback, может модифицировать это значение, если оно вдруг является некорректным.
                    new CoerceValueCallback(CoerceTemp)),
                new ValidateValueCallback(ValidateTemp));
        }
        //пишем метод ValidateTemp, который в качестве параметра принимает новое значение свойства
        private static bool ValidateTemp(object value)
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
        //пишем метод CoerceTemp, который должен принимать два параметра: DependencyObject - валидируемый объект
        //и object - новое значение для свойства Temp
        private static object CoerceTemp(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50 && v <= 50)
            {
                return v;
            }
            else
            {
                return null;
            }
        }

    }
}
