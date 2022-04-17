using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperiaCase.Codes;

namespace ExperiaCase.Models
{
    internal class Library
    {
        public List<Product> Films { get; set; }

        public string PrintLibrary()
        {
            string output = "";
            foreach (Product product in Films)
            {
                output += product.ToString();
            }
            return output;
        }

        //Sorting
        public void SortByMostViewed()
        {
            Films.Sort(delegate (Product x, Product y)
            {
                return y.TimesViewed.CompareTo(x.TimesViewed);
            });
        }

        public void SortByMostBought()
        {
            Films.Sort(delegate (Product x, Product y)
            {
                return y.TimesBought.CompareTo(x.TimesBought);
            });
        }

        public void SortByHigestReview()
        {
            Films.Sort(delegate (Product x, Product y)
            {
                return y._score.CompareTo(x._score);
            });
        }

        public List<Product> GetAllInGenre (string keyword)
        {
            keyword = keyword.Capitalize();
            return Films.FindAll(f => f._searchWord.Exists(s => s.Contains(keyword))); 
        }

        public void TopFilms(int num, string keyword, SearchCriteria searchCriteria)
        {
            keyword = keyword.Capitalize();
            Console.WriteLine($"Top {num} {keyword} films.");
            int counter = num;
            string end = "";
            switch (searchCriteria)
            {
                case SearchCriteria.score:
                    this.SortByHigestReview();
                    break;
                case SearchCriteria.bought:
                    this.SortByMostBought();
                    break;
                case SearchCriteria.viewed:
                    this.SortByMostViewed();
                    break;
                default:
                    break;
            }
            foreach (var item in this.Films)
            {
                if (item._searchWord.Exists(s => s.Contains(keyword) && counter > 0))
                {
                    counter--;
                    switch (searchCriteria)
                    {
                        case SearchCriteria.score:
                            end = $"and has a score of {item._score}";
                            break;
                        case SearchCriteria.bought:
                            end = $"and was bought {item.TimesBought} times";
                            break;
                        case SearchCriteria.viewed:
                            end = $"and was viewed {item.TimesViewed} times";
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine($"{item.GetName()} has keyword {keyword} {end}");
                }
            }
        }
    }
}
