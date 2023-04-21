using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Hosting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PointLightColorInversionRepro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        PointLight _PointLight;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TogglePointLight_Tapped()
        {

        }

        private void ToggleThemeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.RequestedTheme == ElementTheme.Light)
            {
                this.RequestedTheme = ElementTheme.Dark;
            }
            else
            {
                this.RequestedTheme = ElementTheme.Light;
            }
        }

        private void TogglePointLightButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (_PointLight == null)
            {
                EnablePointLight();
            }
            else
            {
                DisablePointLight();
            }
        }

        private void EnablePointLight()
        {
            var _TargetElement = LightingGrid;
            var elementVisual = GetVisual(_TargetElement);
            var _Compositor = elementVisual.Compositor;

            _PointLight = _Compositor.CreatePointLight();
            _PointLight.CoordinateSpace = elementVisual;
            _PointLight.Color = Colors.White;
            _PointLight.Intensity = 2;
            _PointLight.Targets.Add(elementVisual);
            _PointLight.Offset = new System.Numerics.Vector3(-1 * (float)(_TargetElement.ActualWidth / 2), (float)(_TargetElement.ActualHeight / 2), 100);
            PointLightStatusTB.Text = "PointLight ON- Line colours changed.";
        }

        private void DisablePointLight()
        {
            if (_PointLight != null)
            {
                _PointLight.Targets.RemoveAll();
                _PointLight.Dispose();
                _PointLight = null;
            }
            PointLightStatusTB.Text = "PointLight OFF- Line colours unchanged.";
        }

        private Visual GetVisual(UIElement element)
        {
            return ElementCompositionPreview.GetElementVisual(element);
        }
    }
}
