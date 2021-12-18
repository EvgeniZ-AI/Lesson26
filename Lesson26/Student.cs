using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson26
{
    [Serializable]
    class Student
    {
        public string Name { get; }
        public int Age { get; }

        public Group Group { get; set; }

        public Student(string name, int age)
        {
            // Проверка данных

            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
