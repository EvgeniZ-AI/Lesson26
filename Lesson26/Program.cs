using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

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

            Console.WriteLine();

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

            Console.WriteLine();

            // xml перестал работать:(

            try
            {
                var xnlFarmatter = new XmlSerializer(typeof(List<Group>));
                //решил проблему
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
            catch (Exception ex)
            {
                //Видно можно записывать только базовые типы.
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();


            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));


            using (var file = new FileStream("students.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(file, students);
            }

            using (var file = new FileStream("students.json", FileMode.OpenOrCreate))
            {
                var newStudent = jsonFormatter.ReadObject(file) as List<Student>;

                if (newStudent != null)
                {
                    foreach (var student in newStudent)
                    {
                        Console.WriteLine(student);
                    }
                }
            }
            //Json рулит!!!
        }
    }
}
