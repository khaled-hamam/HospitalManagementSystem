using HospitalManagementSystem.Models;
using MaterialDesignThemes.Wpf;
using System;

namespace HospitalManagementSystem.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public DialogHost RootDialog { set; get; }
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

        private Boolean isLoading;
        public Boolean IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;

                if (value == true)
                {
                    previousContent = Content;
                    content = new LoadingViewModel();
                }
                else
                {
                    Content = previousContent;
                }
            }
        }

        public HomeViewModel()
        {
            content = new EmployeesViewModel();
            isLoading = false;
        }

        public void InitializeHospital()
        {
            // Hospital.InitializeData();
        }

        public void GoBack()
        {
            Content = previousContent;
        }

        public void CloseRootDialog()
        {
            DialogHost.CloseDialogCommand.Execute(RootDialog, null);

        }
    }
}
