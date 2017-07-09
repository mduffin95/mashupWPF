using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mashup
{
    class SegmentViewModel : ViewModelBase
    {
        private readonly string _original;
        private int _start;
        private int _length;
        private string _segment;
        public string Segment
        {
            get { return _segment; }
            set
            {
                _segment = value;
                RaisePropertyChangedEvent(nameof(Segment));
            }
        }

        public SegmentViewModel(int start, int length, string str)
        {
            _start = start;
            _length = length;
            _original = str;
            Segment = _original.Substring(_start, _length);
        }

        public ICommand OpenSegmentEditorCommand
        {
            get { return new DelegateCommand(OpenSegmentEditor); }
        }

        private void OpenSegmentEditor()
        {
            //Need to de-couple this from the view
            Window w = new SegmentEditorWindow();
            w.DataContext = this;
            w.Show();
        }

        public ICommand IncreaseLeftCommand
        {
            get { return new DelegateCommand(IncreaseLeft); }
        }

        public ICommand IncreaseRightCommand
        {
            get { return new DelegateCommand(IncreaseRight); }
        }

        public ICommand DecreaseLeftCommand
        {
            get { return new DelegateCommand(DecreaseLeft); }
        }

        public ICommand DecreaseRightCommand
        {
            get { return new DelegateCommand(DecreaseRight); }
        }

        private void IncreaseLeft()
        {
            if (_start > 0)
            {
                _start--;
                _length++;
            }
                
            Segment = _original.Substring(_start, _length);
        }

        private void IncreaseRight()
        {
            if (_start + _length < _original.Length)
                _length++;

            Segment = _original.Substring(_start, _length);
        }

        private void DecreaseLeft()
        {
            if (_length > 0)
            {
                _start++;
                _length--;
            }

            Segment = _original.Substring(_start, _length);
        }

        private void DecreaseRight()
        {
            if (_length > 0)
                _length--;
            Segment = _original.Substring(_start, _length);
        }
    }
}
