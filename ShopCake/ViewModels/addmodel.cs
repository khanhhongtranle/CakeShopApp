using ShopCake.Commands;
using ShopCake.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopCake.ViewModels
{
    class addmodel : ViewModelBase
    {
        private Cake cake;
        public Cake _Cake
        {
            get => cake;
            set => SetProperty(ref cake, value);
        }

        private readonly DelegateCommand _changeNameCommand;
        public ICommand ChangeNameCommand => _changeNameCommand;

        public addmodel()
        {
            _Cake = new Cake();
            _Cake.Name = "cakes";

            _changeNameCommand = new DelegateCommand(OnChangeName, CanChangeName);
        }

        private void OnChangeName(object commandParameter)
        {
            _Cake.Name = "Walter";
            _changeNameCommand.InvokeCanExecuteChanged();
        }

        private bool CanChangeName(object commandParameter)
        {
            return _Cake.Name != "Walter";
        }
    }
}
