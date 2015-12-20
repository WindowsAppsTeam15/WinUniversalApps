namespace YamAndRateApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Models;
    using Utils;
    using System.Net;
    public class RestaurantViewModel : BaseViewModel
    {
        private ICommand saveRestaurant;
        private string name;
        private string description;
        private string photoUrl;
        private string category;
        private string specialty;
        private string errorMessage;
        private int id;
        private int yourVote;
        private double rating;
        private double longitude;
        private double lattitude;
        private ObservableCollection<string> specialties;

        public RestaurantViewModel()
        {
            this.Specialties = new ObservableCollection<string>();
            this.Votes = new ObservableCollection<int>();
            this.Categories = new ObservableCollection<string>()
            {
                "Unspecified",
                "Italian",
                "French",
                "Chinеse",
                "Other Asian",
                "Bulgarian"
            };
            this.Category = this.Categories[0];
        }

        public RestaurantViewModel(int selectedRestaurantId)
        {
            // Remove this when we initialize the collection below after requesting data from Parse
            /*
            this.Votes = new ObservableCollection<int>()
            {
                2, 3, 4
            };

            /*
            this.Rating = this.TestVotes.Sum() / this.TestVotes.Count;
            this.YourVote = this.TestVotes[0];            

            this.Votes = new ObservableCollection<Vote>();
            //this.Votes.Add(new Vote(3, "Pesho"));
            //this.Votes.Add(new Vote(2, "Evstati"));
            //this.Votes.Add(new Vote(4, "Az"));
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

        public string FirstSpecialty
        {
            get
            {
                return this.specialty;
            }
            set
            {
                this.specialty = value;
                this.NotifyPropertyChanged("Specialty");
                this.Specialties.Add(this.FirstSpecialty);
            }
        }

        public string SecondSpecialty
        {
            get
            {
                return this.specialty;
            }
            set
            {
                this.specialty = value;
                this.NotifyPropertyChanged("Specialty");
                this.Specialties.Add(this.SecondSpecialty);
            }
        }

        public string ThirdSpecialty
        {
            get
            {
                return this.specialty;
            }
            set
            {
                this.specialty = value;
                this.NotifyPropertyChanged("Specialty");
                this.Specialties.Add(this.ThirdSpecialty);
            }
        }

        public string FourthSpecialty
        {
            get
            {
                return this.specialty;
            }
            set
            {
                this.specialty = value;
                this.NotifyPropertyChanged("Specialty");
                this.Specialties.Add(this.FourthSpecialty);
            }
        }

        public string FifthSpecialty
        {
            get
            {
                return this.specialty;
            }
            set
            {
                this.specialty = value;
                this.NotifyPropertyChanged("Specialty");
                this.Specialties.Add(this.FifthSpecialty);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
                this.NotifyPropertyChanged("ErrorMessage");
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

        public byte[] PhotoData { get; set; }

        public ObservableCollection<string> Categories { get; set; }

        public ObservableCollection<string> Specialties
        {
            get
            {
                return this.specialties;
            }
            set
            {
                this.specialties = value;
                this.NotifyPropertyChanged("Specialties");
            }
        }

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
            var restaurantsCount = await new ParseQuery<Restaurant>().CountAsync();
            this.Id = ++restaurantsCount;

            if (Validator.ValidateRestaurantDetails(this.Name, this.Description) != string.Empty)
            {
                this.ErrorMessage = Validator.ValidateRestaurantDetails(this.Name, this.Description);
                return;
            }

            ParseFile photo;
            try
            {
                photo = new ParseFile(this.Name + ".jpg", this.PhotoData);
                await photo.SaveAsync();
            }
            catch (ArgumentNullException ex)
            {
                this.ErrorMessage = "Add a photo!";
                return;
            }
            catch (WebException ex)
            {
                this.ErrorMessage = "No internet connection!";
                return;
            }

            this.Votes.Add(0);

            try
            {
                var restaurant = new Restaurant
                {
                    Name = this.Name,
                    Description = this.Description,
                    Category = this.Category,
                    Id = this.Id,
                    Specialties = new List<string>(this.Specialties),
                    Votes = new List<int>(this.Votes),
                    Rating = this.Rating,
                    Photo = photo,
                    Location = new ParseGeoPoint(this.Lattitude, this.Longitude)
                };

                await restaurant.SaveAsync();
            }
            catch (WebException)
            {
                this.ErrorMessage = "No internet connection!";
                return;
            }

            this.ErrorMessage = string.Empty;  
        }

        private async void LoadRestaurantDetails(int selectedRestaurantId)
        {
            var query = new ParseQuery<Restaurant>().Where(r => r.Id == selectedRestaurantId);
            var restaurant = await query.FirstOrDefaultAsync();

            this.Name = restaurant.Name;
            this.Description = restaurant.Description;
            this.Category = restaurant.Category;
            this.Id = restaurant.Id;
            this.PhotoUrl = restaurant.Photo.Url.ToString();
            this.Rating = restaurant.Rating;
            this.Votes = new ObservableCollection<int>(restaurant.Votes);
            this.Rating += this.Votes.Sum();
            this.Rating /= this.Votes.Count;
            this.YourVote = restaurant.Votes.FirstOrDefault();

            if (restaurant.Specialties == null)
            {
                restaurant.Specialties = new ObservableCollection<string>();
            }
            this.Specialties = new ObservableCollection<string>(restaurant.Specialties);

            this.Longitude = restaurant.Location.Longitude;
            this.Lattitude = restaurant.Location.Latitude;
        }
    }
}
