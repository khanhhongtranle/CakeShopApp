using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCake.Helpers
{
    class PagingHelper: INotifyPropertyChanged
    {
        public PagingHelper()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int TotalPages { get; set; }
        private int _currentPage;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public int Count { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
