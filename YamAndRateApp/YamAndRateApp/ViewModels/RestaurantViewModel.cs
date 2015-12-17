namespace YamAndRateApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Models;

    public class RestaurantViewModel : BaseViewModel
    {
        private ICommand saveRestaurant;
        private double yourVote;
        private double rating;

        public RestaurantViewModel()
        {
            this.Name = "Mrysnoto UI";
            this.Description = "Very cool and delicate cuisine";
            this.TestVotes = new ObservableCollection<double>()
            {
                2.0, 3.0, 4.0
            };

            this.Rating = this.TestVotes.Sum() / this.TestVotes.Count;
            this.YourVote = this.TestVotes[0];

            this.Specialties = new ObservableCollection<string>();
            this.Specialties.Add("Shkembe chorba");
            this.Specialties.Add("Kiselo zae s praz");
            this.Specialties.Add("Chibapchita");
            this.Specialties.Add("Bob");
            this.Specialties.Add("Lik s oriz");
            this.Specialties.Add("Krokodil na plocha");

            this.Votes = new ObservableCollection<Vote>();
            //this.Votes.Add(new Vote(3, "Pesho"));
            //this.Votes.Add(new Vote(2, "Evstati"));
            //this.Votes.Add(new Vote(4, "Az"));

            this.PhotoUrl = "http://www.gettyimages.ca/gi-resources/images/Homepage/Category-Creative/UK/UK_Creative_462809583.jpg";
            // this.Coords = new Coordinates(42.650999, 23.380356);
            this.Category = CategoryType.Bulgarian;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Rating
        {
            get
            {
                return this.rating;
            }

            set
            {
                if (!(this.rating == value))
                {
                    this.rating = value;
                    base.NotifyPropertyChanged("Rating");
                }
            }
        }

        public double YourVote
        {
            get
            {
                return this.yourVote;
            }

            set
            {
                if (!(this.yourVote == value))
                {
                    this.yourVote = value;
                    this.TestVotes[0] = value;
                    this.Rating = this.TestVotes.Sum() / this.TestVotes.Count;
                    base.NotifyPropertyChanged("YourVote");
                }
            }
        }


        public ObservableCollection<string> Specialties { get; set; }

        public ObservableCollection<Vote> Votes { get; set; }

        public ObservableCollection<double> TestVotes { get; set; }

        public string PhotoUrl { get; set; }

        // public Coordinates Coords { get; set; }

        public CategoryType Category { get; set; }

        public ICommand SaveRestaurant
        {
            get
            {
                if (this.saveRestaurant == null)
                {
                    this.saveRestaurant = new RelayCommand(this.OnSaveRestaurantExecute);
                }

                return this.saveRestaurant;
            }
        }

        // TODO: Test after AddRestaurantView is implemented
        private async void OnSaveRestaurantExecute()
        {
            var restaurant = new Restaurant
            {
                Name = this.Name,
                Description = this.Description,
                Category = new Category { CategoryType = this.Category },
                Specialties = this.Specialties,
                Votes = this.Votes,
                Rating = this.Rating,
                Location = new ParseGeoPoint(42.650999, 23.380356)
            };

            await restaurant.SaveAsync();
        }
    }
}
