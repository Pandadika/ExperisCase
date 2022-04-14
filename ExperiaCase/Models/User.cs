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
        private List<string> _shown;
        private List<string> _bought;
        public User(string id, string name, List<string> shown, List<string> bought)
        {
            _id = id;
            _name = name;
            _shown = shown;
            _bought = bought;
        }


        public override string ToString()
        {
            return $"id = {_id}, Name {_name}, Shown {string.Join(',',_shown)}, Bought = {string.Join(',',_bought)}";
        }
    }

}
