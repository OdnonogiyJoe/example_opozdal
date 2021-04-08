using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace andrey_opozdal
{
    internal class VMGroups: INotifyPropertyChanged
    {
        Entities entities;
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<Special> Specials { get; set; }
        private Group selectedGroup;

        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                SignalChanged();
            }
        }
        public CustomCommand AddGroup { get; set; }
        public CustomCommand SaveGroups { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public VMGroups()
        {
            entities = DB.GetDB();
            LoadGroups();
            Specials = new ObservableCollection<Special>(entities.Specials);
            AddGroup = new CustomCommand(() =>
            {
                var group = new Group { Number = "Номер группы" };
                entities.Groups.Add(group);
                SelectedGroup = group;
            });
            SaveGroups = new CustomCommand(() =>
            {
                try
                {
                    entities.SaveChanges();
                    LoadGroups();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
        }

        private void LoadGroups()
        {
            Groups = new ObservableCollection<Group>(entities.Groups);
            SignalChanged("Groups");
        }

        void SignalChanged([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}