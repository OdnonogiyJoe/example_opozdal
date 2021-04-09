using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace andrey_opozdal
{
    internal class VMStudents : INotifyPropertyChanged
    {
        Entities entities;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Special> Specials { get; set; }
        public CustomCommand AddStudent { get; set; }
        public CustomCommand SaveStudent { get; set; }
        private Student selectedStudent;

        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                SignalChanged();
            }
        }
        public VMStudents()
        {
            entities = DB.GetDB();
            LoadStudents();
            Specials = new ObservableCollection<Special>(entities.Specials);
            Students = new ObservableCollection<Student>(entities.Students);
            Groups = new ObservableCollection<Group>(entities.Groups);
            AddStudent = new CustomCommand(() =>
            {
                var student = new Student { FirstName ="Имя", LastName="Фамилия", Birthday=DateTime.Now };
                entities.Students.Add(student);
                SelectedStudent = student;
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
        void SignalChanged([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        private void LoadStudents()
        {
            Students = new ObservableCollection<Student>(entities.Students);
            SignalChanged("Student");
        }
    }
}