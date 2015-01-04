using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.customer.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            _pages = new ObservableCollection<IPage>();
            _pages.Add(new LoginVM());
            _currentPage = Pages[0];


        }

        private IPage _currentPage;
        public IPage CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }

        }

        private ObservableCollection<IPage> _pages;
        public ObservableCollection<IPage> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
                OnPropertyChanged("Pages");
            }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        public void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
