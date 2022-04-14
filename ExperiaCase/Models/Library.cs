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
    }
}
