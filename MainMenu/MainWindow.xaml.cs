using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // a list to store yoga poses with their categories
        List<YogaPoseWithCategory> yogaPosesWithCategories = new List<YogaPoseWithCategory>();
        // a list to store all the routine objects
        List<Routine> routines = new List<Routine>();

        private Routine _selectedRoutine;
        private int _currentPoseIndex;

        public MainWindow()
        {
            InitializeComponent();
            FetchYogaPoses();

            //populate combobox
            string[] yogaPoseCategories = { "Core Yoga","Seated Yoga","Strengthening Yoga","Chest Opening Yoga","Backbend Yoga",
                "Forward Bend Yoga","Hip opening Yoga","Standing Yoga","Restorative Yoga","Arm Balance Yoga","Balancing Yoga",
                "Inversion Yoga", "All Poses"};
            cbx_Category.ItemsSource = yogaPoseCategories;
        }
        private void FetchYogaPoses()
        {
            string link = "Poses.json";
            try
            {
                // Read the JSON data from the file
                string jsonData = File.ReadAllText(link);

                // Deserialize the JSON response
                List<Root> roots = JsonConvert.DeserializeObject<List<Root>>(jsonData);


                //Process the deserialized data
                foreach (Root root in roots)
                {
                    foreach (Pose p in root.poses)
                    {
                        // Create a new YogaPoseWithCategory object for each yoga pose
                        YogaPoseWithCategory yogaPoseWithCategory = new YogaPoseWithCategory
                        {
                            Name = p.english_name,
                            Category = p.category_name
                        };

                        // Add the yoga pose to the list
                        yogaPosesWithCategories.Add(yogaPoseWithCategory);
                    }
                }

                // Set the ListBox's ItemsSource to the list of YogaPoseWithCategory objects
                lbx_yogaPoses.ItemsSource = yogaPosesWithCategories;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void lbx_yogaPoses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if an item is selected
            if (lbx_yogaPoses.SelectedItem != null)
            {
                // Retrieve the selected YogaPoseWithCategory object
                YogaPoseWithCategory selectedYogaPose = (YogaPoseWithCategory)lbx_yogaPoses.SelectedItem;

                // Find the corresponding Pose object from the original data
                Pose selectedPose = FindPose(selectedYogaPose.Name, selectedYogaPose.Category);

                // Display the details in the lbx_YogaPoseSummary ListBox
                if (selectedPose != null)
                {
                    //clear function for each element - refreshPoseDisplay()  -
                    tblk_PoseName.Text = $"{selectedPose.english_name}";
                    tblk_PoseName_Sanskrit.Text = $"{selectedPose.sanskrit_name_adapted}";
                    tblk_PoseBenefits.Text = $"{selectedPose.pose_benefits}";
                    tblk_PoseDescription.Text = $"{selectedPose.pose_description}";
                }
                // Display the image
                if (!string.IsNullOrEmpty(selectedPose.url_png))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedPose.url_png);
                    bitmap.EndInit();

                    // Create an Image control and set its Source to the bitmap
                    Image image = new Image();
                    image.Source = bitmap;
                    image.Width = 250; // Set the desired width
                    image.Height = 400; // Set the desired height

                    img_PoseIcon.Source = bitmap;
                }
            }
        }

        // Method to find the corresponding Pose object based on name and category
        private Pose FindPose(string name, string category)
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText("Poses.json");

            // Deserialize the JSON response
            List<Root> roots = JsonConvert.DeserializeObject<List<Root>>(jsonData);

            // Search for the corresponding Pose object
            foreach (Root root in roots)
            {
                foreach (Pose p in root.poses)
                {
                    if (p.english_name == name && p.category_name == category)
                    {
                        return p;
                    }
                }
            }

            return null; // Return null if not found
        }

        private void cbx_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbx_yogaPoses.ItemsSource = yogaPosesWithCategories;
            RefreshFilteredPoses();
        }
        private void RefreshFilteredPoses()
        {
            if (cbx_Category.SelectedItem != null)
            {
                string selectedCategory = cbx_Category.SelectedItem.ToString();
                List<YogaPoseWithCategory> filteredPoses = new List<YogaPoseWithCategory>();

                foreach (var pose in yogaPosesWithCategories)
                {
                    if (selectedCategory == "All Poses" || pose.Category == selectedCategory)
                    {
                        // Check if the pose is already in the filtered list
                        if (!filteredPoses.Any(p => p.Name == pose.Name && p.Category == pose.Category))
                        {
                            filteredPoses.Add(pose);
                        }
                    }
                }

                lbx_yogaPoses.ItemsSource = filteredPoses;
            }
        }


        private void DisplayPoseDetails(YogaPoseWithCategory pose)
        {
            var selectedPose = FindPose(pose.Name, pose.Category);
            if (selectedPose != null)
            {
                tblk_PoseName.Text = $"{selectedPose.english_name}";
                tblk_PoseName_Sanskrit.Text = $"{selectedPose.sanskrit_name_adapted}";
                tblk_PoseBenefits.Text = $"{selectedPose.pose_benefits}";
                tblk_PoseDescription.Text = $"{selectedPose.pose_description}";

                if (!string.IsNullOrEmpty(selectedPose.url_png))
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(selectedPose.url_png));
                    img_PoseIcon.Source = bitmap;
                }
            }
        }

        private void btn_RandomYogaPose_Click(object sender, RoutedEventArgs e)
        {
            //create Random Obj
            Random random = new Random();
            if (yogaPosesWithCategories.Any())
            {
                var randomPose = yogaPosesWithCategories[random.Next(yogaPosesWithCategories.Count)];
                DisplayPoseDetails(randomPose);
            }
        }

        private void btn_AddRoutine_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_RoutineName.Text))
            {
                Routine newRoutine = new Routine { Name = txt_RoutineName.Text };
                routines.Add(newRoutine);
                lbx_Routines.Items.Add(newRoutine);
                txt_RoutineName.Clear(); // Clear the textbox after adding
            }
            else
            {
                MessageBox.Show("Please enter a name for the routine.");
            }
        }


        private void btn_AddPose_ToRoutine_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_yogaPoses.SelectedItem != null)
            {
                YogaPoseWithCategory selectedYogaPose = (YogaPoseWithCategory)lbx_yogaPoses.SelectedItem;
                Pose pose = FindPose(selectedYogaPose.Name, selectedYogaPose.Category);  
                if (pose != null)
                {
                    SelectRoutineWindow selectRoutineWindow = new SelectRoutineWindow(routines);
                    selectRoutineWindow.PoseToAdd = pose;
                    selectRoutineWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Pose details not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a pose to add.");
            }
        }
        private void lbx_Routines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbx_Routines.SelectedItem is Routine selectedRoutine && selectedRoutine.Poses.Count > 0)
            {
                _selectedRoutine = selectedRoutine;
                _currentPoseIndex = 0; // Reset index to show the first pose
                UpdatePoseDetails(_selectedRoutine.Poses[_currentPoseIndex]);
            }
            else
            {
                // Optionally clear previous details or show a default message
                ClearPoseDetails();
            }
        }
        private void ClearPoseDetails()
        {
            tbk_PoseName.Text = "Select a pose";
            tbk_PoseDescription.Text = "";
            img_SelectedPose.Source = null;
        }
        private void UpdatePoseDetails(Pose pose)
        {
            tbk_PoseName.Text = pose.english_name;
            tbk_PoseDescription.Text = pose.pose_description;
            img_SelectedPose.Source = new BitmapImage(new Uri(pose.url_png, UriKind.RelativeOrAbsolute));
        }

        private void btn_PreviousPose_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRoutine != null && _currentPoseIndex > 0)
            {
                _currentPoseIndex--;
                UpdatePoseDetails(_selectedRoutine.Poses[_currentPoseIndex]);
            }
        }

        private void btn_NextPose_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRoutine != null && _currentPoseIndex < _selectedRoutine.Poses.Count - 1)
            {
                _currentPoseIndex++;
                UpdatePoseDetails(_selectedRoutine.Poses[_currentPoseIndex]);
            }
        }

    }
}
