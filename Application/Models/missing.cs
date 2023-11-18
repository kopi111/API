using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public record missingPerson
    {
        public picture ImagePath { get; init; }
        public string Alias { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; init; }
        public address HomeAddress { get; init; }
        public string AreaFrequent { get; init; }
        public String LastSeen { get; init; }
        public string AdditionalNotes { get; init; }

        public missingPerson(
            picture imagePath,
            string alias,
            string firstName,
            string lastName,
            int age,
            DateTime dateOfBirth,
            address homeAddress,
            string areaFrequent,
            
            string lastSeen,
            string additionalNotes)
        {
            ImagePath = imagePath;
            Alias = alias;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            DateOfBirth = dateOfBirth;
            HomeAddress = homeAddress;
            AreaFrequent = areaFrequent;
           
            LastSeen = lastSeen;
            AdditionalNotes = additionalNotes;
        }

        public missingPerson() { } // Add a default constructor
    }
}
