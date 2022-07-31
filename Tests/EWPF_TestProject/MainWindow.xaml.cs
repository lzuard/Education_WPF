using System;
using System.Threading;
using System.Windows;

namespace EWPF_TestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new Thread(ComputeValue).Start();
        }

        private void ComputeValue()
        {
            var value = LongProcess(DateTime.Now);
            if (ResultBlock.Dispatcher.CheckAccess())
                ResultBlock.Text = value;
            else
                //ResultBlock.Dispatcher.Invoke(() => ResultBlock.Text = value);
                ResultBlock.Dispatcher.BeginInvoke(new Action(() => ResultBlock.Text=value));
        }

        private static string LongProcess(DateTime time)
        {
            Thread.Sleep(3_000);
            return $"Value: {time}";
        }
    }
}
