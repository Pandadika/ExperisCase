using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Models
{
    internal class User
    {
        public readonly int id;
        public readonly string name;
        public List<int> shownList;
        public List<int> boughtList;
        public User(int id, string name, List<int> shown, List<int> bought)
        {
            this.id = id;
            this.name = name;
            shownList = shown;
            boughtList = bought;
        }


        public override string ToString()
        {
            return $"id = {id}, Name {name}, Shown {string.Join(',', shownList)}, Bought = {string.Join(',', boughtList)}";
        }
    }

}
