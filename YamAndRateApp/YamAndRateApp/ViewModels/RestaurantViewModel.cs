using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamAndRateApp.Models;

namespace YamAndRateApp.ViewModels
{
    public class RestaurantViewModel : BaseViewModel
    {
        public RestaurantViewModel()
        {
            this.Name = "Mrysnoto";
            this.Description = "Very cool and delicate cuisine";
            this.Rating = 4.56;

            this.Specialties = new ObservableCollection<string>();
            this.Specialties.Add("Shkembe chorba");
            this.Specialties.Add("Kiselo zae s praz");
            this.Specialties.Add("Chibapchita");
            this.Specialties.Add("Bob");
            this.Specialties.Add("Lik s oriz");
            this.Specialties.Add("Krokodil na plocha");

            this.Votes = new ObservableCollection<Vote>();
            this.Votes.Add(new Vote(3, "Pesho"));
            this.Votes.Add(new Vote(2, "Evstati"));
            this.Votes.Add(new Vote(4, "Az"));

            this.PhotoUrl = "http://www.gettyimages.ca/gi-resources/images/Homepage/Category-Creative/UK/UK_Creative_462809583.jpg";
            this.Coords = new Coordinates(42.650999, 23.380356);
            this.Category = Category.Bulgarian;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public ObservableCollection<string> Specialties { get; set; }

        public ObservableCollection<Vote> Votes { get; set; }

        public string PhotoUrl { get; set; }

        public Coordinates Coords { get; set; }

        public Category Category { get; set; }
    }
}
