namespace YamAndRateApp.Models
{
    using System.Collections.Generic;

    using Parse;

    [ParseClassName("Restaurant")]
    public class Restaurant : ParseObject
    {
        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("category")]
        public string Category
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("id")]
        public int Id
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("specialties")]
        public IList<string> Specialties
        {
            get { return GetProperty<IList<string>>(); }
            set { SetProperty<IList<string>>(value); }
        }

        [ParseFieldName("votes")]
        public IEnumerable<Vote> Votes
        {
            get { return GetProperty<IEnumerable<Vote>>(); }
            set { SetProperty<IEnumerable<Vote>>(value); }
        }

        [ParseFieldName("rating")]
        public double Rating
        {
            get { return GetProperty<double>(); }
            set { SetProperty<double>(value); }
        }

        [ParseFieldName("photo")]
        public ParseFile Photo
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }

        [ParseFieldName("location")]
        public ParseGeoPoint Location
        {
            get { return GetProperty<ParseGeoPoint>(); }
            set { SetProperty<ParseGeoPoint>(value); }
        }        
    }
}
