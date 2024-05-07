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

namespace MainMenu
{
    /// <summary>
    /// Interaction logic for SelectRoutineWindow.xaml
    /// </summary>
    public partial class SelectRoutineWindow : Window
    {
        public Pose PoseToAdd { get; set; }

        public SelectRoutineWindow(List<Routine> routines)
        {
            InitializeComponent();
            lbx_Routines.ItemsSource = routines;
        }

        private void btn_AddPose_Click(object sender, RoutedEventArgs e)
        {
            Routine selectedRoutine = lbx_Routines.SelectedItem as Routine;
            if (selectedRoutine != null && PoseToAdd != null)
            {
                selectedRoutine.AddPose(PoseToAdd);
                Close();
            }
            else
            {
                MessageBox.Show("Please select a routine.");
            }
        }
    }


}
