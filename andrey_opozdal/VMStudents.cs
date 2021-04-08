using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace andrey_opozdal
{
    internal class VMStudents: INotifyPropertyChanged
    {
        Entities entities;
        private Student selectedStudent;

        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
       
        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                SignalChanged();
            }
        }

        public CustomCommand AddStudent { get; set; }
        public CustomCommand SaveStudent { get; set; }
        
        public VMStudents()
        {
            entities = DB.GetDB();
            LoadStudents();
            Groups = new ObservableCollection<Group>(entities.Groups);
            AddStudent = new CustomCommand(() =>
            {
                var student = new Student();
                entities.Students.Add(student);  
                SelectedStudent = student;
                /*SelectedStudent = new Student { FirstName = "Имя", LastName = "Фамилия", Birthday = DateTime.Now };
                entities.Students.Add(SelectedStudent);*/
                LoadStudents();
            });
            SaveStudent = new CustomCommand(() =>
            {
                try
                {
                    entities.SaveChanges();
                    LoadStudents();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
        }
        
        private void LoadStudents()
        {
            Students = new ObservableCollection<Student>(entities.Students);
            SignalChanged("Students");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}