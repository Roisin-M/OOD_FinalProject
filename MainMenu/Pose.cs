using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMenu
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Pose
    {
        public int id { get; set; }
        public string category_name { get; set; }
        public string english_name { get; set; }
        public string sanskrit_name_adapted { get; set; }
        public string sanskrit_name { get; set; }
        public string translation_name { get; set; }
        public string pose_description { get; set; }
        public string pose_benefits { get; set; }
        public string url_svg { get; set; }
        public string url_png { get; set; }
        public string url_svg_alt { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public string category_name { get; set; }
        public string category_description { get; set; }
        public List<Pose> poses { get; set; }
    }
    public class YogaPoseWithCategory
    {
        public string Name { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return $"{Name}   -   {Category}";
        }
    }
    // routine class to hold name of routine and the list of poses
    public class Routine
    {
        public string Name { get; set; }
        public List<Pose> Poses { get; set; } 

        public Routine()
        {
            Poses = new List<Pose>();
        }

        // Method to add a pose to the routine
        public void AddPose(Pose pose)
        {
            if (!Poses.Contains(pose))
            {
                Poses.Add(pose);
            }
        }

        // Method to remove a pose from the routine
        public void RemovePose(Pose pose)
        {
            Poses.Remove(pose);
        }
        public override string ToString()
        {
            return Name;
        }
    }



}
