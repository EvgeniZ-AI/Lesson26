using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lesson26
{
    // Я просто начинаю путаться. тут нечего смотреть :(
    class Program
    {
        static void Main(string[] args)
        {
            var groups = new List<Group>();
            var students = new List<Student>();

            for (int i = 1; i < 10; i++)
            {
                groups.Add(new Group($"Группа - {i}",i));
            }

            for (int i = 0; i < 300; i++)
            {
                var student = new Student(Guid.NewGuid().ToString().Substring(0, 5), i % 100)
                {
                    Group = groups[i % 9]
                };

                students.Add(student);
            }

            var binformatter = new BinaryFormatter();

            using(var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
                binformatter.Serialize(file, groups);
            }

            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
                var newGroups = binformatter.Deserialize(file) as List<Group>;

                if (newGroups != null)
                {
                    foreach (var group in newGroups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }

            var soapFormatter = new SoapFormatter();

            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
            {
                soapFormatter.Serialize(file, groups.ToArray());
            }
            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
            {
                var newGroups = soapFormatter.Deserialize(file) as Group[];

                if (newGroups != null)
                {
                    foreach (var group in newGroups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }

            var xnlFarmatter = new XmlSerializer(typeof(List<Group>));

            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
            {
                xnlFarmatter.Serialize(file, groups);
            }

            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
            {
                var newGroups = xnlFarmatter.Deserialize(file) as List<Group>;

                if (newGroups != null)
                {
                    foreach (var group in newGroups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }
        }
    }
}
