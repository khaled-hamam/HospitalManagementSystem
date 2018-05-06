using HospitalManagementSystem.Models;
using System;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Properties
        /// </summary>
        public double StandardPrice { get; set; }
        public double SemiPrice { get; set; }
        public double PrivatePrice { get; set; }
        public double AppointmentPrice { get; set; }
        public double StandardCapacity { get; set; }
        public double SemiCapacity { get; set; }
        public double PrivateCapacity { get; set; }

        /// <summary>
        /// Command Property
        /// </summary>
        public ICommand SaveSettingsAction { get; set; }

        public SettingsViewModel()
        {
            SaveSettingsAction = new RelayCommand(saveSettings);

            StandardPrice = Hospital.Config.StandardWardPrice;
            StandardCapacity = Hospital.Config.StandardWardCapacity;
            SemiPrice = Hospital.Config.SemiPrivateRoomPrice;
            SemiCapacity = Hospital.Config.SemiPrivateRoomCapacity;
            PrivatePrice = Hospital.Config.PrivateRoomPrice;
            PrivateCapacity = Hospital.Config.PrivateRoomCapacity;
            AppointmentPrice = Hospital.Config.AppointmentHourPrice;
        }

        public void saveSettings()
        {
            Console.WriteLine("Saving Settings..");
        }
    }
}
