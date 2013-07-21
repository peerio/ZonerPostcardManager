using System.Windows;
using System.Windows.Input;
using ZonerPostcardManager.ViewModel;

namespace ZonerPostcardManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PreviewKeyDown += mainWindowPreviewKeyDown;
        }

        private void mainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && Keyboard.Modifiers == ModifierKeys.Control)
            {
                var vm = DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.SelectAll();
                }
            }
            else if (e.Key == Key.Escape)
            {
                var vm = DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.SelectNone();
                }
            }
            else if (e.Key == Key.Delete)
            {
                var vm = DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.Delete();
                }
            }
            else if (e.Key == Key.F5)
            {
                var vm = DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.Refresh();
                }
            }
        }
    }
}
