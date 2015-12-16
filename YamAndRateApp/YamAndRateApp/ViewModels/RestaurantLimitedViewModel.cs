﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamAndRateApp.Models;

namespace YamAndRateApp.ViewModels
{
    public class RestaurantLimitedViewModel
    {
        public RestaurantLimitedViewModel(string name, double rating, string photoId, Category cat)
        {
            this.Name = name;
            this.Rating = rating;
            this.PhotoUrl = photoId;
            this.Category = cat;
        }

        public string Name { get; set; }

        public double Rating { get; set; }

        public string PhotoUrl { get; set; }

        public Category Category { get; set; }
    }
}