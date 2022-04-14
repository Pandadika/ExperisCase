using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Models
{
    internal class Product : IComparable<Product>
    {
        private int _id;
        private string _name;
        private int _year;
        private List<string> _searchWord;
        private double _score;
        private double _price;

        public Product(int id, string name, int year, List<string> searchWords, double score, double price)
        {
            _id = id;
            _name = name;
            _year = year;
            _searchWord = searchWords;
            _score = score;
            _price = price;
        }


        public override string ToString()
        {

            return $"Id = {_id}, Name = {_name}, Year = {_year}, Genre(s)= {string.Join(',',_searchWord)}, Score = {_score}, Price = {_price}";
        }



        //sort list by Score
        int IComparable<Product>.CompareTo(Product other)
        {
            return _name.CompareTo(other._name);
        }
    }
}
