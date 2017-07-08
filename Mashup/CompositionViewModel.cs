using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mashup
{
    class CompositionViewModel : ViewModelBase
    {
        private readonly ObservableCollection<SegmentViewModel> _segments
            = new ObservableCollection<SegmentViewModel>();
        public ObservableCollection<SegmentViewModel> Segments
        {
            get { return _segments; }
        }

        private readonly ObservableCollection<SegmentViewModel> _searchSegments
            = new ObservableCollection<SegmentViewModel>();
        public ObservableCollection<SegmentViewModel> SearchSegments
        {
            get { return _searchSegments; }
        }

        private string _fileText;

        public CompositionViewModel()
        {
            _fileText = "abcdefghijklmnopqrstuvwxyz";
            Segments.Add(new SegmentViewModel(0, 5, _fileText));
            Segments.Add(new SegmentViewModel(5, 5, _fileText));
            Segments.Add(new SegmentViewModel(10, 5,_fileText));
        }

        public ICommand OpenFileCommand
        {
            get { return new DelegateCommand(OpenFile); }
        }
                
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                _fileText = File.ReadAllText(openFileDialog.FileName);
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChangedEvent(nameof(SearchText));
            }
        }

        public ICommand SearchCommand
        {
            get { return new DelegateCommand(Search); }
        }

        private void Search()
        {
            string pattern = SearchText;
            string sentence = _fileText;

            SearchSegments.Clear();
            foreach (Match match in Regex.Matches(sentence, pattern))
            {
                SearchSegments.Add(new SegmentViewModel(match.Index, match.Length, _fileText));
            }
        }

    }
}
