using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZonerPostcardManager.ViewModel;

namespace ZonerPostcardManager.Controls
{
    public partial class Postcard : UserControl
    {
        private double _width;
        private double _height;

        public Postcard()
        {
            InitializeComponent();
            DataContextChanged += postcardDataContextChanged;
            Loaded += postcardLoaded;
            Unloaded += postcardUnloaded;
        }

        private void postcardLoaded(object sender, RoutedEventArgs e)
        {
            setDimensionsOnViewModel();
        }

        private void postcardDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            setDimensionsOnViewModel();
        }

        private void setDimensionsOnViewModel()
        {
            if (ActualHeight > 0 && ActualWidth > 0)
            {
                var viewmodel = DataContext as PostcardViewModel;

                if (viewmodel != null && _width != canvas.ActualWidth)
                {
                    _width = canvas.ActualWidth;
                    _height = canvas.ActualHeight;
                    viewmodel.SetDimensions(_width, _height);
                }
            }
        }

        /// <summary>
        /// Helps prevent memory leaks when removing from view.
        /// </summary>
        private void postcardUnloaded(object sender, RoutedEventArgs e)
        {
            DataContextChanged -= postcardDataContextChanged;
            Loaded -= postcardLoaded;
            Unloaded -= postcardUnloaded;
        }
    }
}
