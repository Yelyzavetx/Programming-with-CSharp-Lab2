using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Lab2
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException()
            : base("Invalid age: The person does not exist. Birth date in the future") { }
    }

    public class TooOldPersonException : Exception
    {
        public TooOldPersonException()
            : base("Invalid age: The person is older than 135 years") { }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string email)
            : base($"Invalid email address: {email}") { }
    }

    class Person
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;

        //Public properties for accessing private fields
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string Email => _email;
        public DateTime BirthDate => _birthDate;


        public bool IsAdult => CalculateIsAdult();
        public bool IsBirthday => CalculateIsBirthday();
        public string SunSign => GetWesternZodiacSign();
        public string ChineseSign => GetChineseZodiacSign();


        // Constructor with 4 parameters
        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = birthDate;
        }

        // Constructor without email
        public Person(string firstName, string lastName, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = string.Empty;
            _birthDate = birthDate;
        }


        // Constructor without birthDate
        public Person(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthDate = DateTime.MinValue;
        }

        public static void ValidateBirthDate(DateTime birthDate) //Birth date check
        {
            if (birthDate > DateTime.Today)
            {
                throw new FutureBirthDateException();
            }

            int age = DateTime.Today.Year - birthDate.Year - (DateTime.Today.DayOfYear < birthDate.DayOfYear ? 1 : 0);
            if (age > 135)
            {
                throw new TooOldPersonException();
            }
        }


        public static void ValidateEmail(string email) //email check
        {
            var emailAddress = new EmailAddressAttribute();

            if (!emailAddress.IsValid(email))
            {
                throw new InvalidEmailException(email);
            }
        }


        private bool CalculateIsAdult()
        {

            int age = DateTime.Today.Year - _birthDate.Year - (DateTime.Today.DayOfYear < _birthDate.DayOfYear ? 1 : 0);
            return age >= 18;

        }


        private bool CalculateIsBirthday()
        {
            return _birthDate.Day == DateTime.Today.Day && _birthDate.Month == DateTime.Today.Month;
        }


        private string GetWesternZodiacSign() //zodiak sign
        {
            int month = _birthDate.Month;
            int day = _birthDate.Day;
            if ((month == 3 && day >= 21) || (month == 4 && day <= 19)) return "Aries";
            if ((month == 4 && day >= 20) || (month == 5 && day <= 20)) return "Taurus";
            if ((month == 5 && day >= 21) || (month == 6 && day <= 20)) return "Gemini";
            if ((month == 6 && day >= 21) || (month == 7 && day <= 22)) return "Cancer";
            if ((month == 7 && day >= 23) || (month == 8 && day <= 22)) return "Leo";
            if ((month == 8 && day >= 23) || (month == 9 && day <= 22)) return "Virgo";
            if ((month == 9 && day >= 23) || (month == 10 && day <= 22)) return "Libra";
            if ((month == 10 && day >= 23) || (month == 11 && day <= 21)) return "Scorpio";
            if ((month == 11 && day >= 22) || (month == 12 && day <= 21)) return "Sagittarius";
            if ((month == 12 && day >= 22) || (month == 1 && day <= 19)) return "Capricorn";
            if ((month == 1 && day >= 20) || (month == 2 && day <= 18)) return "Aquarius";
            return "Pisces";
        }

        private string GetChineseZodiacSign() //chinese zodiak sign
        {
            int year = _birthDate.Year;
            string[] chineseZodiacs = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            return chineseZodiacs[year % 12];
        }
    }
}
