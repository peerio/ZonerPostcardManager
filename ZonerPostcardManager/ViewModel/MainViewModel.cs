using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZonerPostcardManager.Helpers;
using ZonerPostcardManager.Model;
using System.Linq;

namespace ZonerPostcardManager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private XmlFile _xml;

        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged(() => Path);

                Refresh();
            }
        }

        public List<PostcardViewModel> Postcards { get; set; }

        #region Commands
        public RelayCommand SelectAllCommand { get; private set; }

        public RelayCommand SelectNoneCommand { get; private set; }

        public RelayCommand DeleteCommand { get; private set; }

        public RelayCommand RefreshCommand { get; private set; }
        #endregion

        /// <summary>
        /// One window app, manage the postcard formats 
        /// </summary>
        /// <remarks>Later on add a popup to add new formats</remarks>
        public MainViewModel()
        {
            Path = @"D:\Pictures\Zoner\postcards - Copy.xml";//todo: set from interface

            SelectAllCommand = new RelayCommand(SelectAll);
            SelectNoneCommand = new RelayCommand(SelectNone);
            DeleteCommand = new RelayCommand(Delete);
            RefreshCommand = new RelayCommand(Refresh);
        }

        public void Delete()
        {
            var selectedCardNames = Postcards.Where(c => c.Selected).Select(c => c.Name);
            _xml.DeleteCards(selectedCardNames);
            Refresh();
        }

        public void ChangeOrder(IEnumerable<string> names)
        {
        }

        public void SelectAll()
        {
            Postcards.Where(c => !c.Selected).ToList().ForEach(c => c.Selected = true);
        }

        public void SelectNone()
        {
            Postcards.Where(c => c.Selected).ToList().ForEach(c => c.Selected = false);
        }

        public void Refresh()
        {
            if (Postcards != null)
            {
                Postcards.ForEach(c => c.Cleanup());
            }

            _xml = new XmlFile(_path);
            Postcards = _xml.Postcards;

            RaisePropertyChanged(() => Postcards);
        }
    }
}