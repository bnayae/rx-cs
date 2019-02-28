#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Concurrency;
using System.Diagnostics;
using System.Threading;

#endregion // Using

namespace WpfRxAutoComplete
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        #region Ctor

        public ViewModel(IObservable<string> inputStream)
        {
            // Throttle the input to forward values only if no other value supply within 1 second
            inputStream = inputStream.Throttle(TimeSpan.FromSeconds(1));

            IObservable<string[]> rxAutoCompleteStream =
                inputStream.Select(value => (
                    // the actual filter
                    from word in Model.WORDS
                    where word.StartsWith(value, StringComparison.InvariantCultureIgnoreCase)
                    select word)
                    #region Remarked

                    //inputStream.SelectMany(value => Observable.FromAsync(
                    //    () => Service.AutoComplete(value)));

                    #endregion // Remarked
                    .ToArray());

            rxAutoCompleteStream.Subscribe(arr =>
                {
                    Results = arr;
                });
        }

        #endregion // Ctor

        #region Results

        private string[] _results;
        public string[] Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Results"));
            }
        }

        #endregion // Results
    }
}
