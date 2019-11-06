using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lekarna
{
    class Subgroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string State { get; set; }
        public string FreeBuyCategory { get; set; }
        public string RecipeCategory { get; set; }
        public int FreeBuy { get; set; }
        public int RecipeBuy { get; set; }

        public Subgroup(int id, string name, string group, string state, string category, string category2, int freeBuy, int recipeBuy)
        {
            Id = id;
            Name = name;
            Group = group;
            State = state;
            FreeBuyCategory = category;
            RecipeCategory = category2;
            FreeBuy = freeBuy;
            RecipeBuy = recipeBuy;
        }

        public Subgroup()
        {
        }
    }
}
