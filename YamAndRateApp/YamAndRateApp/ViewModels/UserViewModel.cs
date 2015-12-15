using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamAndRateApp.Helpers;

namespace YamAndRateApp.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private string repeatedPassword;
        private bool isInRegisterMode;

        public UserViewModel()
        {
            this.LogInUser = new RelayCommand(this.OnLogInUserExecute, this.OnLogInUserCanExecute);
            this.RegisterUser = new RelayCommand(this.OnRegisterUserExecute, this.OnRegisterUserCanExecute);
            this.isInRegisterMode = false;
        }

        public RelayCommand RegisterUser { get; private set; }
        public RelayCommand LogInUser { get; private set; }

        public bool IsInRegisterMode
        {
            get { return this.isInRegisterMode; }

            set
            {
                if (value != this.isInRegisterMode)
                {
                    this.isInRegisterMode = value;
                    base.NotifyPropertyChanged("IsInRegisterMode");
                }
            }
        }

        public string Email
        {
            get { return this.email; }

            set
            {
                if (value != this.email)
                {
                    this.email = value;
                    base.NotifyPropertyChanged("Email");
                }
            }
        }

        public string Pasword
        {
            get { return this.password; }

            set
            {
                if (value != this.password)
                {
                    this.password = value;
                    NotifyPropertyChanged("Pasword");
                }
            }
        }

        public string RepeatedPassword
        {
            get { return this.repeatedPassword; }

            set
            {
                if (value != this.repeatedPassword)
                {
                    this.repeatedPassword = value;
                    NotifyPropertyChanged("RepeatedPassword");
                }
            }
        }

        private bool OnRegisterUserCanExecute(object obj)
        {
            return true;
        }

        private void OnRegisterUserExecute(object obj)
        {
            if (!isInRegisterMode)
            {
                this.IsInRegisterMode = true;
                return;
            }

            // We should implement vaidations for email and pass length
            if (string.IsNullOrEmpty(this.Pasword)
                || string.IsNullOrEmpty(this.RepeatedPassword)
                || string.IsNullOrEmpty(this.Email))
            {
                // Retern error
                return;
            }

            // Register user
            return;
        }

        private bool OnLogInUserCanExecute(object obj)
        {
            return true;
        }

        private void OnLogInUserExecute(object obj)
        {
            // We should implement vaidations for email and pass length
            if (string.IsNullOrEmpty(this.Pasword)
               || string.IsNullOrEmpty(this.Email))
            {
                // Return error message
                return;
            }

            // Login user
            return;
        }
    }
}
