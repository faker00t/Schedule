using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Shedule.ViewModel;

namespace Shedule.View
{
    /// <summary>
    /// Interaction logic for ShedAuditoriumForm.xaml
    /// </summary>
    public partial class ShedAuditoriumForm : Window
    {
        public ShedAuditoriumForm()
        {
            InitializeComponent();
            this.DataContext = new ShedAuditoriumViewModel();
        }
    }
}
