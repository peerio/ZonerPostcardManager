using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZonerPostcardManager.Helpers;

namespace ZonerPostcardManager.ViewModel
{
    [DebuggerDisplay("{Name}")]
    public class PostcardViewModel : ViewModelBase
    {
        private bool _selected;

        public string Name { get; private set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        public List<PictureFrameViewModel> PictureFrames { get; private set; }

        public RelayCommand SelectToggleCommand
        {
            get;
            private set;
        }

        public PostcardViewModel(string name, IEnumerable<XElement> xmlRectangles)
        {
            Name = name;
            PictureFrames = xmlRectangles.Select(r => new PictureFrameViewModel(r)).ToList();

            SelectToggleCommand = new RelayCommand(() => Selected = !Selected);
        }

        public void SetDimensions(double width, double height)
        {
            PictureFrames.ForEach(f => f.SetDimensions(width, height));
        }

        public override void Cleanup()
        {
            if (PictureFrames != null)
            {
                PictureFrames.ForEach(f => f.Cleanup());
            }

            base.Cleanup();
        }
    }
}
