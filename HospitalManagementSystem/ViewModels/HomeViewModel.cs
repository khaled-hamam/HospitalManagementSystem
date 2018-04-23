using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private BaseViewModel previousContent;
        public BaseViewModel PreviousContent { get { return previousContent;  } set { previousContent = value; Console.WriteLine("&& " + value.ToString()); }  }
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
                    PreviousContent = Content;
                    content = new LoadingViewModel();
                }
                else
                {
                    Content = PreviousContent;
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
            Hospital.InitializeData();
        }

    }
}
