using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SortingNames.Data.Repositories;
using SortingNames.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace SortingNames.Tests
{
    [TestClass]
    public class SortingNameTest
    {
        [TestMethod]
        public void TestSortByLastName()
        {
            //Arrange
            string savedFolder = "../../../SortingNames/App_Data";
            string unsortedFileName = "sample-for-unit-test.txt";
            string sortedFileName = "sorted-names-list.txt";
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, savedFolder, unsortedFileName);
            SortingNameRepository sortingNames = new SortingNameRepository();
            List<string> expectedResult = new List<string>();

            expectedResult.Add("Hailey Avie Annakin");
            expectedResult.Add("Erna Dorey Battelle");
            expectedResult.Add("Selle Bellison");
            expectedResult.Add("Flori Chaunce Franzel");
            expectedResult.Add("Orson Milka Iddins");
            expectedResult.Add("Odetta Sue Kaspar");
            expectedResult.Add("Roy Ketti Kopfen");
            expectedResult.Add("Madel Bordie Mapplebeck");
            expectedResult.Add("Debra Micheli");
            expectedResult.Add("Leonerd Adda Mitchell Monaghan");

            //Act
            IEnumerable<PersonModel> model = sortingNames.SortNames(fullPath, savedFolder, sortedFileName);

            IEnumerable<string> fullNames = from p in model
                                select p.FullName;

            List<string> actualResult = fullNames.ToList();

            //Assert
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }
    }
}
