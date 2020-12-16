using System;
using System.Text;

namespace HashTableLab
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
    }
}
