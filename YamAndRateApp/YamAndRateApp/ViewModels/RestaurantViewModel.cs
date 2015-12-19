namespace YamAndRateApp.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Models;
    using Windows.Devices.Geolocation;
    using System.Collections.Generic;

    public class RestaurantViewModel : BaseViewModel
    {
        private ICommand saveRestaurant;
        private string name;
        private string description;
        private string photoUrl;
        private string category;
        private int yourVote;
        private double rating;
        private double longitude;
        private double lattitude;

        public RestaurantViewModel()
        {
            this.Specialties = new ObservableCollection<string>();
            this.Votes = new ObservableCollection<int>();
            this.Categories = new ObservableCollection<string>()
            {
                "Unspecified",
                "Italian",
                "French",
                "Chinise",
                "OtherAsian",
                "Bulgarian"
            };
            this.Category = this.Categories[0];
        }

        public RestaurantViewModel(string selectedRestaurantName)
        {

            //this.Name = "Mrysnoto UI";
            //this.Description = "Very cool and delicate cuisine";

            // Remove this when we initialize the collection below after requesting data from Parse
            this.Votes = new ObservableCollection<int>()
            {
                2, 3, 4
            };

            /*
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
            */

            this.LoadRestaurantDetails(selectedRestaurantName);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                this.NotifyPropertyChanged("Description");
            }
        }

        public string PhotoUrl
        {
            get
            {
                return this.photoUrl;
            }
            set
            {
                this.photoUrl = value;
                this.NotifyPropertyChanged("PhotoUrl");
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
                this.NotifyPropertyChanged("Category");
            }
        }

        public double Rating
        {
            get
            {
                return this.rating;
            }

            set
            {
                if (this.rating != value)
                {
                    this.rating = value;
                    base.NotifyPropertyChanged("Rating");
                }
            }
        }

        public double Longitude
        {
            get
            {
                return this.longitude;
            }
            set
            {
                if (this.longitude != value)
                {
                    this.longitude = value;
                    this.NotifyPropertyChanged("Longitude");
                }
            }
        }

        public double Lattitude
        {
            get
            {
                return this.lattitude;
            }
            set
            {
                if (this.lattitude != value)
                {
                    this.lattitude = value;
                    this.NotifyPropertyChanged("Lattitude");
                }
            }
        }

        public int YourVote
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
                    this.Votes[0] = value;
                    this.Rating = this.Votes.Sum() / this.Votes.Count;
                    base.NotifyPropertyChanged("YourVote");
                }
            }
        }

        public ObservableCollection<string> Categories { get; set; }

        public ObservableCollection<string> Specialties { get; set; }

        public ObservableCollection<int> Votes { get; set; }

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

        private async void OnSaveRestaurantExecute(object parameters)
        {
            var restaurant = new Restaurant
            {
                Name = this.Name,
                Description = this.Description,
                Category = this.Category,
                Specialties = this.Specialties,
                Votes = new ObservableCollection<Vote>(),

                // We do not have to store the ratingas we can easily calculate it 
                // when we know all votes. However, we can keep it
                //Rating = this.Rating,

                // We should also attach photo here

                Location = new ParseGeoPoint(this.Lattitude, this.Longitude)
            };

            await restaurant.SaveAsync();
        }

        private async void LoadRestaurantDetails(string selectedRestaurantName)
        {
            var query = new ParseQuery<Restaurant>().Where(r => r.Name == selectedRestaurantName);
            var restaurant = await query.FirstOrDefaultAsync();

            this.Name = restaurant.Name;
            this.Description = restaurant.Description;
            this.Category = restaurant.Category;
            this.Specialties = (ObservableCollection<string>)restaurant.Specialties;
            this.PhotoUrl = restaurant.Photo.Url.ToString();

            // We do not need to store Rating in the model (Parse object)
            // However, we can keep it
            // this.Rating = restaurant.Rating;

            // The below is the proper implementation for the votes / ratings functionality.
            // By noy it throws exeption as there are no votes saved in Parse for the restaurants.
            // It needs the list to be different from null
            //List<int> votesValuesCollection = restaurant.Votes.Select(v => v.Value).ToList();
            //this.Votes = new ObservableCollection<int>(votesValuesCollection);
            //this.Rating = (votesValuesCollection.Sum()) / votesValuesCollection.Count;
            //this.YourVote = restaurant.Votes.FirstOrDefault().Value;

            this.Longitude = restaurant.Location.Longitude;
            this.Lattitude = restaurant.Location.Latitude;
        }
    }
}
