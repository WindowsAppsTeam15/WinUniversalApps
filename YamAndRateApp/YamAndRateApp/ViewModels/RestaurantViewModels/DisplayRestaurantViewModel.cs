

namespace YamAndRateApp.ViewModels.RestaurantViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Parse;

    using YamAndRateApp.Models;
    using YamAndRateApp.Utils;

    public class DisplayRestaurantViewModel : DetailsBaseRestaurantViewModel
    {
        private int yourVote;
        private double rating;
        private string[] restaurantIds;
        private string nextRestaurant;
        private string prevRestaurantId;
        private Restaurant restaurant;

        public DisplayRestaurantViewModel(string selectedRestaurantId)
        {
            this.LoadRestaurantDetails(selectedRestaurantId);
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
        }
    }
}
