using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Models
{
    internal class Library
    {
        public List<Product> Products { get; set; }

        public string PrintLibrary()
        {
            string output = "";
            foreach (Product product in Products)
            {
                output += $"{product.ToString()}{Environment.NewLine}";
            }
            return output;
        }
    }
}
