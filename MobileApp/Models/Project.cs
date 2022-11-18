using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectOwner { get; set; }
        public string ProjectImage { get; set; }
        public string ProjectCategory { get; set; }
        public DateTime ProjectStartTime { get; set; }
        public DateTime ProjectEndTime { get; set; }
        public string ProjectStatus { get; set; }

        public int UserID { get; set; }
    }
}
