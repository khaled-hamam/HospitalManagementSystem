using System;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Properties
        /// </summary>
        public String StandardPrice { get; set; }
        public String SemiPrice { get; set; }
        public String PrivatePrice { get; set; }
        public String AppointmentPrice { get; set; }
        public String StandardCapacity { get; set; }
        public String SemiCapacity { get; set; }
        public String PrivateCapacity { get; set; }

        /// <summary>
        /// Command Property
        /// </summary>
        public ICommand SaveSettingsAction { get; set; }

        public SettingsViewModel()
        {
            SaveSettingsAction = new RelayCommand(saveSettings);
        }

        public void saveSettings()
        {
            Console.WriteLine("Saving Settings..");
        }
    }
}
