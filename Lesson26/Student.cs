using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Lesson26
{
    [DataContract]
     public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }

        //[DataMember]
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
