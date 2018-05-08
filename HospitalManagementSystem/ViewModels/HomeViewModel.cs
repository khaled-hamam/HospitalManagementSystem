using HospitalManagementSystem.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ICommand reloadDataCommand;
        public ICommand ReloadDataCommand
        {
            get
            {
                if (reloadDataCommand == null)
                    reloadDataCommand = new RelayCommand(InitializeHospital);

                return reloadDataCommand;
            }

            set { reloadDataCommand = value; }
        }

        private BaseViewModel previousContent;
        private BaseViewModel content;
        public BaseViewModel Content
        {
            get { return content; }
            set
            {
                if (IsLoading == true)
                {
                    previousContent = value;
                }
                else
                {
                    previousContent = content;
                    content = value;
                }
            }
        }

        public Boolean IsLoading { get; set; }

        public HomeViewModel()
        {
            content = new EmployeesViewModel();
            IsLoading = false;
        }

        public void InitializeHospital()
        {
           Hospital.InitializeData();
        }

        public void GoBack()
        {
            Content = previousContent;
        }

        public void CloseRootDialog()
        {
            DialogHost.CloseDialogCommand.Execute("RootDialog", null);
        }
    }
}
