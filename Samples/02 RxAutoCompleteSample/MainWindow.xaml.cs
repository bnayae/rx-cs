#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reactive.Linq;
using System.Reactive;
using System.Diagnostics;

#endregion // Using

namespace WpfRxAutoComplete
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IObservable<EventPattern<TextChangedEventArgs>> eventStream =
                Observable.FromEventPattern<TextChangedEventHandler, TextChangedEventArgs>(
                    h => _txt.TextChanged += h,
                    h => _txt.TextChanged -= h);

            IObservable<string> textStream = from e in eventStream
                                             select (e.Sender as TextBox).Text;

            //textStream = textStream.Do(s => Trace.WriteLine(s));
            var viewModel = new ViewModel(textStream);

            DataContext = viewModel;
        }
    }
}
