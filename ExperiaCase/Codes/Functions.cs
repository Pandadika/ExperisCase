using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Codes
{
    internal class Functions
    {
        static public void PrintByMostViewed(Library library)
        {
            library.SortByMostViewed();
            Console.WriteLine("Most viewed");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(library.Films[i].name + " Viewed " + library.Films[i].TimesViewed);
            }
        }

        static public void PrintByMostBought(Library library)
        {
            library.SortByMostBought();
            Console.WriteLine("Most bought Action");
            foreach (var item in library.Films)
            {
                Console.WriteLine(item.name + " has Action and was bought " + item.TimesBought + " times");
            }
        }

        static public void PrintByHighestReview(Library library)
        {
            library.SortByHigestReview();
            Console.WriteLine("Highest reviews");
            foreach (var item in library.Films)
            {
                Console.WriteLine(item.name + " Score = " + item.score);
            }
        }

        static public List<Product> GetAllInGenre(List<Product> products, string keyword)
        {
            keyword = keyword.Capitalize();
            return products.FindAll(f => f.searchWord.Exists(s => s.Contains(keyword)));
        }

        static public void PrintAllInGenre(Library library, string keyword)
        {
            List<Product> GenreProducts = new List<Product>();
            GenreProducts = GetAllInGenre(library.Films, keyword);
            Console.WriteLine($"{keyword} Movies");
            foreach (var item in GenreProducts)
            {
                Console.WriteLine(item.name);
            }
        }

        static public void PrintAllUsers(List<User> users)
        {
            foreach (var item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }

        static public void PrintAllFilms(List<Product> products)
        {
            foreach (var item in products)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// OtherUsersAlso is the main function of the program, that tells the current user what other similar users did
        /// It will printing an output of other users prefered viewings. IF other users bought the one you are looking at, print a list of other products they bought, 
        /// IF they did not buy that product, print a list of the Items they also viewed
        /// IF no user either bought or viewed the product, print a list of popular movies in a random genre the product has.
        /// </summary>
        static public void OtherUsersAlso(int userid, int viewing, List<User> UsersList, Library FilmLibrary)
        {
            // finding the user, the products they are viewing and a list of other users who has bought it.
            User currentUser = UsersList.Find(u => u.id.Equals(userid));
            Product currentViewing = FilmLibrary.Films.Find(p => p.id.Equals(viewing));
            List<User> otherUsers = UsersList.FindAll(u => u.boughtList.Exists(i => i == viewing));
            otherUsers.Remove(currentUser);
            List<Product> otherUserProducts = new List<Product>();
            bool viewed = false;
            Random random = new Random();



            //if other user bought, make list of products bought.
            if (otherUsers.Any())
            {
                foreach (User user in otherUsers)
                {
                    foreach (int p in user.boughtList)
                    {
                        if (!otherUserProducts.Contains(FilmLibrary.Films.Find(prod => prod.id.Equals(p))))
                        {
                            otherUserProducts.Add(FilmLibrary.Films.Find(prod => prod.id.Equals(p)));
                        }
                    }
                }
                otherUserProducts.Remove(currentViewing);
                //Printing other bought items
                if (otherUserProducts.Any())
                {
                    Console.WriteLine($"Other viewers who bought{currentViewing.name} also bought");
                    foreach (var item in otherUserProducts)
                    {
                        Console.WriteLine(item.name);
                    }
                }
            }
            else // else making list of other users who viewed
            {
                viewed = true;
                otherUsers = UsersList.FindAll(u => u.shownList.Exists(i => i == viewing));
                otherUsers.Remove(currentUser);
            }

            //if users viewed make list of products viewed.
            if (otherUsers.Any() && viewed)
            {
                foreach (User user in otherUsers)
                {
                    foreach (int p in user.shownList)
                    {
                        if (!otherUserProducts.Contains(FilmLibrary.Films.Find(prod => prod.id.Equals(p))))
                        {
                            otherUserProducts.Add(FilmLibrary.Films.Find(prod => prod.id.Equals(p)));
                        }
                    }
                }
                otherUserProducts.Remove(currentViewing);
                //printing other viewed items
                if (otherUserProducts.Any())
                {
                    Console.WriteLine($"Other viewers who viewed{currentViewing.name} also viewed");
                    otherUserProducts.Sort();
                    foreach (var item in otherUserProducts)
                    {
                        Console.WriteLine(item.name + " Score " + item.score);
                    }
                }
            }
            else
            {
                Console.WriteLine("No similar users found");
                FilmLibrary.TopFilms(3, currentViewing.searchWord[random.Next(currentViewing.searchWord.Count)], SearchCriteria.bought);
            }
        }

    }
}
