namespace YamAndRateApp.ViewModels.RestaurantViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Models;
    using YamAndRateApp.Utils;
    using YamAndRateApp.Helpers;

    public class SaveRestaurantViewModel : DetailsBaseRestaurantViewModel
    {
        private ICommand saveRestaurant;
        private string specialty; // Check about this
        private string errorMessage;
        private double longitude;
        private double lattitude;

        public SaveRestaurantViewModel()
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

        // It is not good to stay like that!!!!
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

        // Replace the one in the base with these
        
        public byte[] PhotoData { get; set; }

        public ObservableCollection<string> Categories { get; set; }

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

            //this.Votes.Add(0);

            try
            {
                var restaurant = new Restaurant
                {
                    Name = this.Name,
                    Description = this.Description,
                    Category = this.Category,
                    // ObjectId = this.Id,
                    Specialties = new List<string>(this.Specialties),
                    Votes = new List<int>(this.Votes),
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
    }
}
