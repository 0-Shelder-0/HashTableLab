using System;
using System.Linq;
using System.Text;

namespace HashTableLab.Generator
{
    public class User
    {
        private readonly string _userId;
        private readonly DateTime _registrationDate;
        private readonly string[] _genres;
        
        public User(string id, DateTime registrationDate, string[] genres)
        {
            _userId = id;
            _registrationDate = registrationDate;
            _genres = genres;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append($"{_userId} \n");
            strBuilder.Append($"{_registrationDate} \n");
            
            foreach (var genre in _genres)
            {
                strBuilder.Append($"{genre} \t");
            }

            strBuilder.Append("\n");
            
            return strBuilder.ToString();
        }

        public override int GetHashCode()
        {
            const int coefficient = 13;
            var hashCode = _userId.Select((t, i) => t * Math.Pow(coefficient, i)).Sum();
            hashCode += 1;
            return Convert.ToInt32(Math.Ceiling(hashCode));
        }

        public override bool Equals(object obj)
        {
            if (obj is User user)
            {
                return string.Compare(_userId, user._userId, StringComparison.Ordinal) == 0;
            }
            return false;
        }
    }
}
