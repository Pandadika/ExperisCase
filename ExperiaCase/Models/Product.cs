using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Models
{
    internal class Product : IComparable<Product>
    {
        private int _id { get;}
        private string _name;
        private int _year;
        private List<string> _searchWord;
        private double _score;
        private double _price;

        public Product(string name, List<string> searchWords, double score, double price)
        {
            _name = name;
            _searchWord = searchWords;
            _score = score;
            _price = price;
        }

        public override string ToString()
        {
            string genre = "";
            foreach (string item in _searchWord)
            {
                genre += $"{item}";
            }
            return $"Id = {_id}, Name = {_name}, Year = {_year}, Genre(s)= {genre}, Score = {_score}, Price = {_price}";
        }



        //sort list by Score
        int IComparable<Product>.CompareTo(Product other)
        {
            return _score.CompareTo(other._score);
        }
    }
}
