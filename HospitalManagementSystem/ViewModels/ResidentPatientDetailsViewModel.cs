using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace HospitalManagementSystem.ViewModels
{
    public class ResidentPatientDetailsViewModel : BaseViewModel
    {
        // Main page Data
        public String PatientName { get; set; }
        public String PatientType { get; set; }
        public String PatientAddress { get; set; }
        public String PatientBirthDate { get; set; }
        public String PatientDiagnosis { get; set; }
        public String PatientRoomNumber { get; set; }
        public String PatientBill { get; set; }
        public String PatientDepartment { get; set; }
        public String textValidation { get; set; }
        // Edit Content
        public ICommand editResidentPatient { get; set; }
        public ICommand deleteResidentPatient { get; set; }
        public String EditPatientNameTextBox { get; set; }
        public ComboBoxPairs EditPatientDepartment { get; set; }
        public String EditPatientAddressTextBox { get; set; }
        public DateTime EditPatientBirthDatePicker { get; set; }
        public ComboBoxPairs EditRoomNumberComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> PatientRoomNumberComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> EditDepartmentComboBox { get; set; }
        // Main Page Lists
        public String DoctorsNumber { get; set; }
        public String NursesNumber { get; set; }
        public ObservableCollection<ComboBoxPairs> DoctorsList { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesList { get; set; }
        public ObservableCollection<ComboBoxPairs> MedicalHistoryList { get; set; }
        public ComboBoxPairs ListSelectedDoctor { get; set; }
        public ComboBoxPairs ListSelectedMedicine { get; set; }
        public ComboBoxPairs ListSelectedNurse { get; set; }
        // Items In Contents
        public ObservableCollection<ComboBoxPairs> DoctorsComboBox { get; set; }
        public ObservableCollection<ComboBoxPairs> NursesComboBox { get; set; }
        public ComboBoxPairs DoctorComboBox { get; set; }
        public ComboBoxPairs NurseComboBox { get; set; }
        public String MedicineName { get; set; }
        public DateTime MedicineStartDate { get; set; }
        public DateTime MedicineEndDate { get; set; }
        public ICommand assignDoctor { get; set; }
        public ICommand addMedicine { get; set; }
        public ICommand assignNurse { get; set; }

        public String PatientID;

     
        public ResidentPatientDetailsViewModel(String id)
        {
            PatientID = id;
            PatientDepartment = ((ResidentPatient)Hospital.Patients[id]).Department.Name;
            DoctorsList = new ObservableCollection<ComboBoxPairs>();
            NursesList = new ObservableCollection<ComboBoxPairs>();
            MedicalHistoryList = new ObservableCollection<ComboBoxPairs>();
            PatientRoomNumberComboBox = new ObservableCollection<ComboBoxPairs>();
            EditDepartmentComboBox = new ObservableCollection<ComboBoxPairs>();
            EditPatientBirthDatePicker = DateTime.Today;
            EditPatientAddressTextBox = Hospital.Patients[id].Address;
            EditPatientBirthDatePicker = Hospital.Patients[id].BirthDate;
            EditPatientNameTextBox = Hospital.Patients[id].Name;
           // EditPatientDepartment.Value = ((ResidentPatient)Hospital.Patients[id]).Department.Name;
            //EditRoomNumberComboBox.Value = ((ResidentPatient)Hospital.Patients[id]).Room.RoomNumber.ToString();
            foreach (Room room in Hospital.Rooms.Values)
            {   
                    if(room.hasAvailableBed())
                         PatientRoomNumberComboBox.Add(new ComboBoxPairs(room.ID, room.RoomNumber.ToString()));
            }
             // Lists
            foreach (Doctor doctor in Hospital.Patients[id].Doctors.Values)
            {
                DoctorsList.Add(new ComboBoxPairs(doctor.ID, doctor.Name));
            }
            DoctorsNumber = "Doctors: " + Hospital.Patients[id].Doctors.Count().ToString();

            foreach (Nurse nurse in ((ResidentPatient)Hospital.Patients[id]).Room.Nurses.Values)
            {
                NursesList.Add(new ComboBoxPairs(nurse.ID, nurse.Name));
            }
            NursesNumber = "Nurses:" + ((ResidentPatient)Hospital.Patients[id]).Room.Nurses.Count().ToString();

            foreach (Medicine medicine in ((ResidentPatient)Hospital.Patients[id]).History.Values)
            {
                MedicalHistoryList.Add(new ComboBoxPairs(medicine.ID, medicine.Name + " - Starting Date: " + medicine.StartingDate.ToShortDateString() + " | " + medicine.EndingDate.ToShortDateString()));
            }

            foreach (Department department in Hospital.Departments.Values)
            {
                EditDepartmentComboBox.Add(new ComboBoxPairs(department.ID, department.Name));
            }

            //TODO : DoctorsComboBoxItems 
            DoctorsComboBox = new ObservableCollection<ComboBoxPairs>();
            NursesComboBox = new ObservableCollection<ComboBoxPairs>();
            foreach (Employee employee in Hospital.Employees.Values)
            {
                Boolean valid = true;

                if (employee.GetType() == typeof(Doctor) && valid)
                {
                    foreach (Employee invalidEmployee in Hospital.Patients[id].Doctors.Values)
                    {
                        if (employee.ID == invalidEmployee.ID)
                        {
                            valid = false;
                        }
                    }
                    if(valid)
                    DoctorsComboBox.Add(new ComboBoxPairs(employee.ID, employee.Name));
                }
                else
                {
                    foreach (Employee invalidEmployee in ((ResidentPatient)Hospital.Patients[id]).Room.Nurses.Values)
                    {
                        if (employee.ID == invalidEmployee.ID)
                        {
                            valid = false;
                        }
                    }
                    if(valid)
                    NursesComboBox.Add(new ComboBoxPairs(employee.ID, employee.Name));
                }
            }
            editResidentPatient = new RelayCommand(EditResidentPatient);
            deleteResidentPatient = new RelayCommand(DeleteResidentPatient);
            DoctorComboBox = new ComboBoxPairs("Key", "Value");
            NurseComboBox = new ComboBoxPairs("Key", "Value");
            assignDoctor = new RelayCommand(AssignDoctor);
            assignNurse = new RelayCommand(AssignNurse);
            addMedicine = new RelayCommand(AddMedicine);
            ListSelectedDoctor = new ComboBoxPairs("Key", "Value");
            ListSelectedMedicine = new ComboBoxPairs("Key", "Value");
            ListSelectedNurse = new ComboBoxPairs("Key", "Value");
            MedicineStartDate = DateTime.Today;
            MedicineEndDate = DateTime.Today;
        }

        public void EditResidentPatient()
        {
            Hospital.Patients[PatientID].Name = PatientName = EditPatientNameTextBox;
            Hospital.Patients[PatientID].Address = PatientAddress = EditPatientAddressTextBox;
            PatientBirthDate = EditPatientBirthDatePicker.ToShortDateString();
            Hospital.Patients[PatientID].BirthDate = EditPatientBirthDatePicker;

            ((ResidentPatient)Hospital.Patients[PatientID]).Department.Patients.Remove(PatientID);              
            ((ResidentPatient)Hospital.Patients[PatientID]).Department = Hospital.Departments[EditPatientDepartment.Key];
            ((ResidentPatient)Hospital.Patients[PatientID]).Department.Patients.Add(PatientID, Hospital.Patients[PatientID]);

            ((ResidentPatient)Hospital.Patients[PatientID]).Room.Patients.Remove(PatientID);
            ((ResidentPatient)Hospital.Patients[PatientID]).Room = Hospital.Rooms[EditRoomNumberComboBox.Key];
            Hospital.Rooms[EditRoomNumberComboBox.Key].addPatient(Hospital.Patients[PatientID]);

            PatientRoomNumber = EditRoomNumberComboBox.Value;
            PatientDepartment = EditPatientDepartment.Value;
            HospitalDB.UpdatePatient(Hospital.Patients[PatientID]);
            Home.ViewModel.CloseRootDialog();
        }

        public void DeleteResidentPatient()
        {
            Hospital.DeletePatient(PatientID);
            HospitalDB.DeletePatient(PatientID);
            Home.ViewModel.CloseRootDialog();
            Home.ViewModel.Content = new PatientsViewModel();
        }
        public void AssignDoctor()
        {
            DoctorsList.Add(new ComboBoxPairs(DoctorComboBox.Key, DoctorComboBox.Value));
            foreach (Doctor doctor in Hospital.Employees.Values)
            {
                if(doctor.ID == DoctorComboBox.Key)
                {
                    Hospital.Patients[PatientID].Doctors.Add(doctor.ID, doctor);
                    HospitalDB.InsertDoctorPatient(doctor.ID, PatientID);
                    DoctorsNumber = "Doctors: " + Hospital.Patients[PatientID].Doctors.Count().ToString();
                    break;
                }
            }
            ((Doctor)Hospital.Employees[DoctorComboBox.Key]).Patients.Add(PatientID,Hospital.Patients[PatientID]);
            DoctorsComboBox.Remove(DoctorComboBox);

            Home.ViewModel.CloseRootDialog();
        }

        public void AssignNurse()
        {
            NursesList.Add(new ComboBoxPairs(NurseComboBox.Key, NurseComboBox.Value));
            foreach (Nurse nurse in Hospital.Employees.Values)
            {
                if(nurse.ID == NurseComboBox.Key)
                {
                    ((ResidentPatient)Hospital.Patients[PatientID]).Room.Nurses.Add(nurse.ID, nurse);
                    NursesNumber = "Nurses:" + ((ResidentPatient)Hospital.Patients[PatientID]).Room.Nurses.Count().ToString();
                    break;
                }
            }
            ((Nurse)Hospital.Employees[NurseComboBox.Key]).Patients.Add(PatientID, Hospital.Patients[PatientID]);
            NursesComboBox.Remove(NurseComboBox);
            Home.ViewModel.CloseRootDialog();
        }

        public void AddMedicine()
        {
            Medicine medicine = new Medicine();
            medicine.Name = MedicineName;
            medicine.StartingDate = MedicineStartDate;
            medicine.EndingDate = MedicineEndDate;
            ((ResidentPatient)Hospital.Patients[PatientID]).History.Add(medicine.ID, medicine);
            Medicine tempMedicine = ((ResidentPatient)Hospital.Patients[PatientID]).History[medicine.ID];
            MedicalHistoryList.Add(new ComboBoxPairs(medicine.ID, medicine.Name + " - Starting Date: " + medicine.StartingDate.ToShortDateString() + " | " + medicine.EndingDate.ToShortDateString()));
            HospitalDB.InsertMedicine(medicine, Hospital.Patients[PatientID]);
            Home.ViewModel.CloseRootDialog();

        }

        public void RemoveDr()
        {
            String text = "Do You Want To Remove " + ListSelectedDoctor.Value + " ?";
            DialogResult answer = System.Windows.Forms.MessageBox.Show(text, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                DoctorsComboBox.Add(new ComboBoxPairs(ListSelectedDoctor.Key, ListSelectedDoctor.Value));
                Hospital.Patients[PatientID].removeDoctor(ListSelectedDoctor.Key);
                ((Doctor)Hospital.Employees[ListSelectedDoctor.Key]).Patients.Remove(PatientID);
                HospitalDB.DeleteDoctorPatient(ListSelectedDoctor.Key, PatientID);
                DoctorsList.Remove(ListSelectedDoctor);
                DoctorsNumber = "Doctors: " + Hospital.Patients[PatientID].Doctors.Count().ToString();
            }
        }
        public void RemoveMedicine()
        {
            String text = "Do You Want To Remove " + ListSelectedDoctor.Value + " ?";
            DialogResult answer = System.Windows.Forms.MessageBox.Show(text, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                ((ResidentPatient)Hospital.Patients[PatientID]).History.Remove(ListSelectedMedicine.Key);
                HospitalDB.DeleteMedicine(ListSelectedMedicine.Key);
                MedicalHistoryList.Remove(ListSelectedMedicine);
            }
        }
    }
}
