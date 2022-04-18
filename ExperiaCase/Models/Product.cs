using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Models
{
    internal class Product : IComparable<Product>
    {
        public readonly int id;
        public readonly string name;
        public readonly int year;
        public List<string> searchWord;
        public double score;
        public double price;
        public int TimesViewed { get; set; } = 0;
        public int TimesBought { get; set; } = 0;


        public Product(int id, string name, int year, List<string> searchWords, double score, double price)
        {
            this.id = id;
            this.name = name;
            this.year = year;
            searchWord = searchWords;
            this.score = score;
            this.price = price;
        }


        public override string ToString()
        {
            return $"Id = {id}, Name = {name}, Year = {year}, Genre(s)= {string.Join(',', searchWord)}, Score = {score}, Price = {price}";
        }



        //sort Product by id
        int IComparable<Product>.CompareTo(Product other)
        {
            return id.CompareTo(other.id);
        }

    }
}
