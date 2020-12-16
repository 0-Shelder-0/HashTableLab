using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTableLab.Generator
{
    public class Generator
    {
        private static string[] _genres;

        public Generator()
        {
            _genres = Enum.GetNames(typeof(Genres));
        }

        public IEnumerable<User> Generate(int usersCount)
        {
            var rnd = new Random();
            for (var i = 0; i < usersCount; i++)
            {
                var id = GenerateId();
                var genres = GenerateGenres(rnd.Next(1, 5), rnd);
                yield return new User(id, DateTime.Now, genres);
            }
        }

        private static string GenerateId()
        {
            var stringBuilder = new StringBuilder();
            var rnd = new Random();
            var wordsCount = rnd.Next(3, 10);
            var domain = new[] {"@mail.ru", "@gmail.com", "@csu.ru"};
            var domainNumber = rnd.Next(0, 3);

            for (var i = 0; i < wordsCount; i++)
            {
                stringBuilder.Append(Convert.ToChar(rnd.Next(97, 122)));
            }
            stringBuilder.Append(domain[domainNumber]);

            return stringBuilder.ToString();
        }

        private static string[] GenerateGenres(int genresCount, Random random)
        {
            var hashSetGenres = new HashSet<string>();
            while (hashSetGenres.Count < genresCount)
            {
                hashSetGenres.Add(_genres[random.Next(0, 6)]);
            }

            return hashSetGenres.ToArray();
        }
    }
}
