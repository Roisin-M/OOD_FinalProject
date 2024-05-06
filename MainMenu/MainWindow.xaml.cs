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
        // Create a list to store yoga poses with their categories
        List<YogaPoseWithCategory> yogaPosesWithCategories = new List<YogaPoseWithCategory>();

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
            string link = @"C:\Users\35389\OneDrive - Atlantic TU\4sem\ood\Project\draft3\YogaFlow_OOPCA-master\YogaPoses.json";
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
                Console.WriteLine("Error: " + ex.Message);
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

                    img_PoseIcon.Source=bitmap;
                }
            }
        }

        // Method to find the corresponding Pose object based on name and category
        private Pose FindPose(string name, string category)
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(@"C:\Users\35389\OneDrive - Atlantic TU\4sem\ood\Project\draft3\YogaFlow_OOPCA-master\YogaPoses.json");

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
            // Check if a category is selected
            if (cbx_Category.SelectedItem != null )
            {
                // Retrieve the selected category
                string selectedCategory = cbx_Category.SelectedItem.ToString();
                
                if (selectedCategory =="All Poses")
                {
                    lbx_yogaPoses.ItemsSource= yogaPosesWithCategories;
                }
                else
                {
                    // Filter the yoga poses based on the selected category
                    List<YogaPoseWithCategory> filteredPoses = new List<YogaPoseWithCategory>();

                    foreach (YogaPoseWithCategory pose in lbx_yogaPoses.Items)
                    {
                        if (pose.Category == selectedCategory)
                        {
                            filteredPoses.Add(pose);
                        }
                    }

                    // Update the ListBox with the filtered items
                    lbx_yogaPoses.ItemsSource = filteredPoses;
                }
            }
        }
    }
}
