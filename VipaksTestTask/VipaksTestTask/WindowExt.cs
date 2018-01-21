using System.ComponentModel;
using System.Windows;

namespace VipaksTestTask
{
    public class WindowExt : Window
    {
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            var viewModel = DataContext as ViewModel;
            viewModel?.Dispose();
        }
    }
}
