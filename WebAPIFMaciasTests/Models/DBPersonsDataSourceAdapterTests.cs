using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using WebAPIFMacias.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace WebAPIFMacias.Models.Tests
{
    [TestFixture()]
    public class DBPersonsDataSourceAdapterTests
    {
        [Test()]
        public void CreatePersonPersonHasNoAttributesTest()
        {
            DbContextOptionsBuilder contextOptionsBuilder = new DbContextOptionsBuilder<PersonsContext>().UseInMemoryDatabase("Test");
            using (PersonsContext personsContext = new PersonsContext((DbContextOptions<PersonsContext>)contextOptionsBuilder.Options))
            {
                DBPersonsDataSourceAdapter adapter = new DBPersonsDataSourceAdapter(personsContext,new PersonsFactory());
                adapter.InsertPerson(new Person(){});
                adapter.InsertPerson(new Person() {});
                adapter.InsertPerson(new Person() { 
                    City="City", Color=Color.Gelb, Name="Name", Surname="Surname"
                });
                Assert.AreEqual(1, adapter.SelectPersonById(1).Id);
                Assert.AreEqual(2, adapter.SelectPersonById(2).Id);
                Person thirdPerson = adapter.SelectPersonById(3);
                Assert.AreEqual(3, adapter.SelectPersonById(3).Id);
                Assert.AreEqual(string.Format("{0} {1} {2} {3} {4} {5}", thirdPerson.Id,
                    thirdPerson.City, (int) thirdPerson.Color, thirdPerson.Name, thirdPerson.Surname, thirdPerson.ColorName),
                    "3 City 4 Name Surname Gelb");

            }
        }
    }
}