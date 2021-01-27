using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AccentColorTest.App
{
    public class ColorUnit : UserControl
    {
        public ColorUnit()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public string ColorName
        {
            get => GetValue(ColorNameProperty);
            set => SetValue(ColorNameProperty, value);
        }

        public readonly static StyledProperty<string> ColorNameProperty =
            AvaloniaProperty.Register<ColorUnit, string>(nameof(ColorName));
    }
}
