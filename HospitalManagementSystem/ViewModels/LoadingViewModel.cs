using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        public String LoadingDataText { get; set; }

        public LoadingViewModel()
        {
            LoadingDataText = "Loading Data";
            Task.Run(async () =>
            {
                int i = 1, counter = 0;
                while(counter < 20)
                {
                    LoadingDataText = "Loading Data" + new String('.', i) + new string(' ', 3 - i);
                    i = (i % 3) + 1;
                    counter++;
                    await Task.Delay(1000);
                }
            });
        }
    }
}
