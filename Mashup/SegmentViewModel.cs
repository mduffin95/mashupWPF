using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mashup
{
    class SegmentViewModel : ViewModelBase
    {
        private readonly string _original;
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
            _original = str;
            Segment = _original.Substring(start, length);
        }

        public ICommand ChangeSegmentCommand
        {
            get { return new DelegateCommand(ChangeSegment); }
        }

        private void ChangeSegment()
        {
            Segment = _original.Substring(20, 5);
        }
    }
}
