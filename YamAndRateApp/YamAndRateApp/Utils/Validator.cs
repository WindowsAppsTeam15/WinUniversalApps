namespace YamAndRateApp.Utils
{
    using System.Linq;

    public static class Validator
    {
        private const string InvalidUsernameMessage = "Username must be atleast 5 characters and contain only letters and digits!";
        private const string InvalidEmailMessage = "Email cannot be empty!";
        private const string InvalidPasswordMessage = "Password must be atleast 5 characters!";
        private const string InvalidConfirmedPasswordMessage = "Password and Confirmed password do not match!";
        private const string InvalidRestaurantNameMessage = "Restaurant name cannot be empty!";
        private const string InvalidRestaurantDescriptionMessage = "Restaurant description must be atleast 5 characters!";        

        public static string ValidateUserRegistration(string username, string email, string password, string confirmedPassword)
        {
            if (!IsValidUsername(username))
            {
                return InvalidUsernameMessage;
            }

            if (!IsValidPassword(password))
            {
                return InvalidPasswordMessage;
            }

            if (!IsValidEmail(email))
            {
                return InvalidEmailMessage;
            }

            if (password != confirmedPassword)
            {
                return InvalidConfirmedPasswordMessage;
            }

            return string.Empty;           
        }        

        public static string ValidateRestaurantDetails(string name, string description)
        {
            if (!IsValidRestaurantName(name))
            {
                return InvalidRestaurantNameMessage;
            }

            if (!IsValidRestaurantDescription(description))
            {
                return InvalidRestaurantDescriptionMessage;
            }

            return string.Empty;
        }

        private static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                !ValidateStringMinLength(username, 5) ||
                !username.All(char.IsLetterOrDigit))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || !ValidateStringMinLength(password, 5))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidRestaurantName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidRestaurantDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || !ValidateStringMinLength(description, 5))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateStringMinLength(string str, int minLength)
        {
            if (str.Length < minLength)
            {
                return false;
            }

            return true;
        }        
    }
}
