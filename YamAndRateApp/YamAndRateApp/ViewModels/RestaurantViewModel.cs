namespace YamAndRateApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Models;
    using YamAndRateApp.Utils;

    public class RestaurantViewModel : ViewModelBase
    {
        private ICommand saveRestaurant;
        private string name;
        private string description;
        private string photoUrl;
        private string category;
        private string specialty;
        private string errorMessage;
        private string id;
        private int yourVote;
        private double rating;
        private double longitude;
        private double lattitude;
        private ObservableCollection<string> specialties;
        private string[] restaurantIds;
        private string nextRestaurant;
        private string prevRestaurantId;
        private Restaurant restaurant;

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

        public RestaurantViewModel(string selectedRestaurantId)
        {
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

        public string Id
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
                    this.Votes.Add(value);
                    
                    this.Rating = this.Votes.Sum() / this.Votes.Count;
                    base.NotifyPropertyChanged("YourVote");
                    UpdateDbVotes(value);
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

        public string NextRestaurantId
        {
            get
            {
                return this.nextRestaurant;
            }

            set
            {
                if (this.nextRestaurant != value)
                {
                    this.nextRestaurant = value;
                    base.NotifyPropertyChanged("NextRestaurantId");
                }
            }
        }

        public string PrevRestaurantId
        {
            get
            {
                return this.prevRestaurantId;
            }

            set
            {
                if (this.prevRestaurantId != value)
                {
                    this.prevRestaurantId = value;
                    base.NotifyPropertyChanged("PrevRestaurantId");
                }
            }
        }

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

        private async void UpdateDbVotes(int value)
        {
            try
            {
                this.restaurant.Votes.Add(value);
                await this.restaurant.SaveAsync();
            }
            catch (Exception e)
            {
            }
        }

        private async void OnSaveRestaurantExecute(object parameters)
        {
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
            catch (ArgumentNullException)
            {
                this.ErrorMessage = "Add a photo!";
                return;
            }
            catch (WebException)
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
                    ObjectId = this.Id,
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
            ToastManager toastManager = new ToastManager();
            var heading = "Successfully added new restaurant!";
            var content = string.Format("{0} - {1}", this.Name, this.Description);
            var image = photo.Url.ToString();
            // TODO: Maybe navigate to restaurant details is better
            var navigateTo = "allRestaurants";
            toastManager.CreateToast(heading, content, image, navigateTo);
        }

        private async void LoadRestaurantDetails(string selectedRestaurantId)
        {
            int currentIdIndex;
            try
            {
                var query = new ParseQuery<Restaurant>().Where(r => r.ObjectId == selectedRestaurantId);
                this.restaurant = await query.FirstOrDefaultAsync();

                this.restaurantIds = (await new ParseQuery<Restaurant>().FindAsync()).AsQueryable().Select(r => r.ObjectId).OrderBy(i => i).ToArray();
                currentIdIndex = Array.IndexOf(restaurantIds, selectedRestaurantId);
            }
            catch
            {
                ToastManager toastManager = new ToastManager();
                var heading = "There is no internet connection!";
                var image = "/Assets/LockScreenLogo.scale-200.png";
                var navigateTo = "main";
                toastManager.CreateToast(heading, String.Empty, image, navigateTo);
                return;
            }

            this.Name = restaurant.Name;
            this.Description = restaurant.Description;
            this.Category = restaurant.Category;
            this.Id = restaurant.ObjectId;
            this.PhotoUrl = restaurant.Photo.Url.ToString();
            this.Votes = new ObservableCollection<int>(restaurant.Votes);
            this.Rating += (double)this.Votes.Sum();
            this.Rating /= this.Votes.Count;
            this.YourVote = restaurant.Votes.FirstOrDefault();

            if (currentIdIndex == 0)
            {
                this.PrevRestaurantId = this.restaurantIds[this.restaurantIds.Length - 1];
                this.NextRestaurantId = this.restaurantIds[currentIdIndex + 1];
            }
            else if (currentIdIndex == this.restaurantIds.Length - 1)
            {
                this.PrevRestaurantId = this.restaurantIds[currentIdIndex - 1];
                this.NextRestaurantId = this.restaurantIds[0];
            }
            else
            {
                this.PrevRestaurantId = this.restaurantIds[currentIdIndex - 1];
                this.NextRestaurantId = this.restaurantIds[currentIdIndex + 1];
            }

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
