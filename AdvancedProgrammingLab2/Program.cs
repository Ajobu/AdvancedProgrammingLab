using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AdvancedProgrammingLab2
{
    public class City
    {
        public string name { get; set; }
        public int population { get; set; }

        public City(string name, int population)
        {
            this.name = name;
            this.population = population;
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Allergies { get; set; }

        public Person(string firstName, string lastName, string city, int age, string allergies)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Age = age;
            Allergies = allergies;
        }
    }

    class Program
    {
        static void Main()
        {
            
            int[] numbers = { 106, 104, 10, 5, 117, 174, 95, 61, 74, 145, 77, 95, 72, 59, 114, 95, 61, 116, 106, 66, 75, 85, 104, 62, 76, 87, 70, 17, 141, 39, 199, 91, 37, 139, 88, 84, 15, 166, 118, 54, 42, 123, 53, 183, 95, 101, 112, 26, 41, 135, 70, 48, 59, 69, 109, 93, 110, 153, 178, 117, 5 };

            
            City[] cities = {
                new City("Toronto", 100200),
                new City("Hamilton", 80923),
                new City("Ancaster", 4039),
                new City("Brantford", 500890)
            };

            
            Person[] persons = {
                new Person("Cedric", "Coltrane", "Toronto", 157, null),
                new Person("Hank", "Spencer", "Peterborough", 158, "Sulfa, Penicillin"),
                new Person("Sara", "di", "29", 145, null),
                new Person("Daphne", "Seabright", "Ancaster", 146, null),
                new Person("Rick", "Bennett", "Ancaster", 220, null),
                new Person("Amy", "Leela", "Hamilton", 172, "Penicillin"),
                new Person("Woody", "Bashir", "Barrie", 153, null),
                new Person("Tom", "Halliwell", "Hamilton", 179, "Codeine, Sulfa"),
                new Person("Rachel", "Winterbourne", "Hamilton", 163, null),
                new Person("John", "West", "Oakville", 138, null),
                new Person("Jon", "Doggett", "Hamilton", 194, "Peanut Oil"),
                new Person("Angel", "Edwards", "Brantford", 176, null),
                new Person("Brodie", "Beck", "Carlisle", 157, null),
                new Person("Beanie", "Foster", "Ancaster", 154, "Ragweed, Codeine"),
                new Person("Nino", "Andrews", "Hamilton", 186, null),
                new Person("John", "Farley", "Hamilton", 213, null),
                new Person("Nea", "Kobayakawa", "Toronto", 147, null),
                new Person("Laura", "Halliwell", "Brantford", 146, null),
                new Person("Lucille", "Maureen", "Hamilton", 184, null),
                new Person("Jim", "Thoma", "Ottawa", 173, null),
                new Person("Roderick", "Payne", "Halifax", 58, null),
                new Person("Sam", "Threep", "Hamilton", 199, null),
                new Person("Bertha", "Crowley", "Delhi", 125, "Peanuts, Gluten"),
                new Person("Roland", "Edge", "Brantford", 199, null),
                new Person("Don", "Wiggum", "Hamilton", 189, null),
                new Person("Anthony", "Maxwell", "Oakville", 92, null),
                new Person("James", "Sullivan", "Delhi", 139, null),
                new Person("Anne", "Marlowe", "Pickering", 165, "Peanut Oil"),
                new Person("Kelly", "Hamilton", "Stoney", 84, null),
                new Person("Charles", "Andonuts", "Hamilton", 62, null),
                new Person("Temple", "Russert", "Hamilton", 166, "Sulphur"),
                new Person("Don", "Edwards", "Hamilton", 215, null),
                new Person("Alice", "Donovan", "Hamilton", 167, null),
                new Person("Stone", "Cutting", "Hamilton", 110, null),
                new Person("Neil", "Allan", "Cambridge", 203, null),
                new Person("Cross", "Gordon", "Ancaster", 125, null),
                new Person("Phoebe", "Bigelow", "Thunder", 183, null),
                new Person("Harry", "Kuramitsu", "Hamilton", 210, null)
            };
            // 1

            // a. Select numbers > 80
            var greaterThan80 = numbers.Where(n => n > 80);
            Console.WriteLine("Numbers greater than 80:");
            foreach (var n in greaterThan80) Console.WriteLine(n);

            // b. Descending Order
            var orderedNumbers = numbers.OrderByDescending(n => n);
            Console.WriteLine("\nNumbers in descending order:");
            foreach (var n in orderedNumbers) Console.WriteLine(n);

            // c. Transform numbers into a string format: "Have number #n"
            var transformedNumbers = numbers.Select(n => $"Have number {n}");
            Console.WriteLine("\nTransformed numbers:");
            foreach (var n in transformedNumbers) Console.WriteLine(n);

            // d. Count numbers < 100 and > 70
            int count = numbers.Count(n => n > 70 && n < 100);
            Console.WriteLine($"\nCount of numbers between 70 and 100: {count}");

            // 2

            // a. Person (height)
            var personsWithSpecificHeight = persons.Where(p => p.Age == 157);
            Console.WriteLine("\nPersons with a height of 157:");
            foreach (var p in personsWithSpecificHeight)
                Console.WriteLine($"{p.FirstName} {p.LastName}");

            // b. Transform name "J. Doe"
            var formattedNames = persons.Select(p => $"{p.FirstName[0]}. {p.LastName}");
            Console.WriteLine("\nFormatted names:");
            foreach (var name in formattedNames)
                Console.WriteLine(name);

            // c. Distinct allergies
            var distinctAllergies = persons
                .SelectMany(p => p.Allergies?.Split(',') ?? new string[] { })
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrEmpty(a))
                .Distinct();
            Console.WriteLine("\nDistinct allergies:");
            foreach (var allergy in distinctAllergies)
                Console.WriteLine(allergy);

            // d. No.of Cities starts in "H"
            var citiesStartingWithH = cities.Count(c => c.name.StartsWith("H"));
            Console.WriteLine($"\nCities starting with 'H': {citiesStartingWithH}");

            // e. Join list of cities...
            var personsInLargeCities = persons
                .Join(cities, p => p.City, c => c.name, (p, c) => new { p, c })
                .Where(x => x.c.population > 100000)
                .Select(x => x.p);
            Console.WriteLine("\nPersons in cities with population > 100,000:");
            foreach (var p in personsInLargeCities)
                Console.WriteLine($"{p.FirstName} {p.LastName}");

            // f. 3 different cities
            List<string> selectedCities = new List<string> { "Toronto", "Hamilton", "Ottawa" };
            var personsInSelectedCities = persons
                .Where(p => selectedCities.Contains(p.City));
            Console.WriteLine("\nPersons in selected cities:");
            foreach (var p in personsInSelectedCities)
                Console.WriteLine($"{p.FirstName} {p.LastName}");

            // f. Person not in cities
            var personsNotInSelectedCities = persons
                .Where(p => !selectedCities.Contains(p.City));
            Console.WriteLine("\nPersons not in selected cities:");
            foreach (var p in personsNotInSelectedCities)
                Console.WriteLine($"{p.FirstName} {p.LastName}");

            // 3

            // Convert persons to XML
            var personsXml = new XElement("Persons",
                from p in persons
                select new XElement("Person",
                    new XElement("FirstName", p.FirstName),
                    new XElement("LastName", p.LastName),
                    new XElement("City", p.City),
                    new XElement("Age", p.Age),
                    p.Allergies == null ? null : new XElement("Allergies", p.Allergies)
                )
            );

            Console.WriteLine("\nPersons in XML format:");
            Console.WriteLine(personsXml);
        }
    }
}
