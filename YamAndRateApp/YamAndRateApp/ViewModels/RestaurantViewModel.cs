namespace YamAndRateApp.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Models;

    using Windows.Devices.Geolocation;

    public class RestaurantViewModel : BaseViewModel
    {
        private ICommand saveRestaurant;
        private string name;
        private string description;
        private string photoUrl;
        private string category;
        private double yourVote;
        private double rating;
        private Geopoint coordinates;
        private int id;
        private int nextId = 1;

        public RestaurantViewModel()
            : this(1)
        {

        }

        public RestaurantViewModel(int selectedRestaurantId)
        {

            //this.Name = "Mrysnoto UI";
            //this.Description = "Very cool and delicate cuisine";

            this.TestVotes = new ObservableCollection<double>()
            {
                2.0, 3.0, 4.0
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

            this.LoadRestaurantDetails(selectedRestaurantId);
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

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
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
                if (!(this.rating == value))
                {
                    this.rating = value;
                    base.NotifyPropertyChanged("Rating");
                }
            }
        }

        public Geopoint Coordinates
        {
            get
            {
                return this.coordinates;
            }
            set
            {
                this.coordinates = value;
                this.NotifyPropertyChanged("Coordinates");
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
                Id = this.Id,
                Specialties = this.Specialties,
                Votes = this.Votes,
                Rating = this.Rating,
                Location = new ParseGeoPoint(42.650999, 23.380356)
            };

            await restaurant.SaveAsync();
            this.nextId++;
        }

        private async void LoadRestaurantDetails(int selectedRestaurantId)
        {
            var query = new ParseQuery<Restaurant>().Where(r => r.Id == selectedRestaurantId);
            var restaurant = await query.FirstOrDefaultAsync();

            this.Name = restaurant.Name;
            this.Description = restaurant.Description;
            this.Category = restaurant.Category;
            this.Id = restaurant.Id;
            this.Specialties = (ObservableCollection<string>)restaurant.Specialties;
            this.PhotoUrl = restaurant.Photo.Url.ToString();
            this.Rating = restaurant.Rating;
            this.Coordinates = new Geopoint(new BasicGeoposition()
            {
                Longitude = restaurant.Location.Longitude,
                Latitude = restaurant.Location.Latitude
            });
        }
    }
}
