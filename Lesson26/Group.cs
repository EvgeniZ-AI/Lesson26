using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson26
{
    [Serializable]
    public class Group
    {
        [NonSerialized]
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        public string Name { get; set; }
        public int Number { get; set; }
        public Group() 
        {
            Number = rnd.Next(1, 10);
            Name = "Гпуппа " + rnd;
        }

        public Group(string name,int number)
        {
            //Проверка данных

            Name = name;
            Number = number;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
