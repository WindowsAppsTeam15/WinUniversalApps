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

        [ParseFieldName("specialties")]
        public IEnumerable<string> Specialties
        {
            get { return GetProperty<IEnumerable<string>>(); }
            set { SetProperty<IEnumerable<string>>(value); }
        }

        [ParseFieldName("votes")]
        public IEnumerable<Vote> Votes
        {
            get { return GetProperty<IEnumerable<Vote>>(); }
            set { SetProperty<IEnumerable<Vote>>(value); }
        }

        /*
        public ParseFile Photo
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }
        */

        /*
        public ParseGeoPoint Location
        {
            get { return GetProperty<ParseGeoPoint>(); }
            set { SetProperty<ParseGeoPoint>(value); }
        }
        */

        [ParseFieldName("category")]
        public Category Category
        {
            get { return GetProperty<Category>(); }
            set { SetProperty<Category>(value); }
        }
    }
}
