using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace andrey_opozdal
{
    /// <summary>
    /// Логика взаимодействия для WinSpecials.xaml
    /// </summary>
    public partial class WinSpecials : Window
    {
        public WinSpecials()
        {
            InitializeComponent();
            DataContext = new VMSpecials();
        }
    }
}
