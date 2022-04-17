using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
