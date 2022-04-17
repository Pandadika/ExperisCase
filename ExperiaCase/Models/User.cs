using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Models
{
    internal class User
    {
        private string _id;
        private string _name;
        public List<int> _shownList;
        public List<int> _boughtList;
        public User(string id, string name, List<int> shown, List<int> bought)
        {
            _id = id;
            _name = name;
            _shownList = shown;
            _boughtList = bought;
        }


        public override string ToString()
        {
            return $"id = {_id}, Name {_name}, Shown {string.Join(',',_shownList)}, Bought = {string.Join(',',_boughtList)}";
        }
    }

}
