using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace andrey_opozdal
{
    internal class VMSpecials : INotifyPropertyChanged
    {
        Entities entities;
        private Special selectedSpecial;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Special> Specials { get; set; }
        public Special SelectedSpecial { get => selectedSpecial; set { selectedSpecial = value; SignalChanged(); } }
        public VMSpecials()
        {
            entities = DB.GetDB();
            LoadSpecials();
            AddSpecial = new CustomCommand(() =>
            {
                SelectedSpecial = new Special { Title = "Название" };
                entities.Specials.Add(SelectedSpecial);
                LoadSpecials();
            });
            SaveSpecials = new CustomCommand(() =>
            {
                try
                {
                    entities.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
        }

        private void LoadSpecials()
        {
            Specials = new ObservableCollection<Special>(entities.Specials);
            SignalChanged("Specials");
        }
        void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public CustomCommand AddSpecial { get; set; }
        public CustomCommand SaveSpecials { get; set; }

    }
}