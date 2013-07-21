using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ZonerPostcardManager.Helpers
{
    public static class DispatcherExtensions
    {
        public static void OnUIThread(this System.Windows.Controls.Control control, Action a)
        {
            if (control.Dispatcher.CheckAccess())
            {
                a();
            }
            else
            {
                control.Dispatcher.BeginInvoke(a, null);
            }
        }
    }
}
