using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortingNames.Data.Models;
using System.IO;
using System.Configuration;

namespace SortingNames.Data.Repositories
{
    public class SortingNameRepository
    {
        public IEnumerable<PersonModel> SortNames(string fullPath, string savedFolder, string fileName)
        {
            string[] names = File.ReadAllLines(fullPath);

            List<PersonModel> people = new List<PersonModel>();
            foreach (string name in names)
            {
                PersonModel person = new PersonModel();
                string[] sLastNames = name.Split(' ');
                person.FullName = name;
                person.FirstName = sLastNames[0];
                person.LastName = sLastNames[sLastNames.Count() - 1];
                people.Add(person);
            }

            var model = SortByLastName(people);

            SavedFile(model, savedFolder, fileName);

            return model;
        }

        public IEnumerable<PersonModel> SortByLastName(List<PersonModel> people)
        {
            return people.OrderBy(p => p.LastName);
        }

        public IEnumerable<PersonModel> SortByFirstName(List<PersonModel> people)
        {
            return people.OrderBy(p => p.FirstName);
        }

        public void SavedFile(IEnumerable<PersonModel> model, string filePath, string fileName)
        {
            var savedFullName = from p in model
                                select p.FullName;

            System.IO.File.WriteAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath, fileName), savedFullName);
        }
    }
}
