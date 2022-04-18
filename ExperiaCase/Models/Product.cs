using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Models
{
    internal class Product : IComparable<Product>
    {
        public int id;
        public string name;
        private int _year;
        public List<string> _searchWord;
        public double _score;
        private double _price;
        public int TimesViewed { get; set; } = 0;
        public int TimesBought { get; set; } = 0;


        public Product(int id, string name, int year, List<string> searchWords, double score, double price)
        {
            this.id = id;
            this.name = name;
            _year = year;
            _searchWord = searchWords;
            _score = score;
            _price = price;  
        }


        public override string ToString()
        {
            return $"Id = {id}, Name = {name}, Year = {_year}, Genre(s)= {string.Join(',',_searchWord)}, Score = {_score}, Price = {_price}";
        }

        public List<string> GetSearchWords() { return _searchWord; }


        //sort Product by id
        int IComparable<Product>.CompareTo(Product other)
        {
            return other._score.CompareTo(_score);
        }

    }
}
