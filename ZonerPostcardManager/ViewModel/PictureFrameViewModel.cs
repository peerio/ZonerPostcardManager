using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZonerPostcardManager.Helpers;

namespace ZonerPostcardManager.ViewModel
{
    public class PictureFrameViewModel : ViewModelBase
    {
        XElement _xmlRectangle;

        private double _top;
        private double _left;
        private double _width;
        private double _height;

        #region Properties
        public double Top
        {
            get { return _top; }
            set { _top = value; RaisePropertyChanged(() => Top); }
        }

        public double Left
        {
            get { return _left; }
            set { _left = value; RaisePropertyChanged(() => Left); }
        }

        public double Width
        {
            get { return _width; }
            set { _width = value; RaisePropertyChanged(() => Width); }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value; RaisePropertyChanged(() => Height); }
        }
        #endregion

        public PictureFrameViewModel(XElement xmlRectangle)
        {
            _xmlRectangle = xmlRectangle;
        }

        public void SetDimensions(double width, double height)
        {
            var left = parseXmlPosition("left");
            var top = parseXmlPosition("top");
            var right = parseXmlPosition("right");
            var bottom = parseXmlPosition("bottom");

            Left = width * left;
            Top = height * top;
            Width = width * (right - left);
            Height = height * (bottom - top);
        }

        private double parseXmlPosition(string position)
        {
            return double.Parse(_xmlRectangle.Attribute(position).Value, CultureInfo.InvariantCulture);
        }
    }
}
