﻿using System.Linq;

namespace Workshop.Tests._01._Fundamentals.Data
{
    public class Park
    {
        public string Name { get; }
        public PetType[] AuthorizedPetTypes { get; }

        public Park(string name)
            : this(name, [])
        {
        }

        public Park(string name, PetType[] authorizedPetTypes)
        {
            Name = name;
            AuthorizedPetTypes = authorizedPetTypes;
        }

        public Park AddAuthorizedPetType(PetType petType) => new(Name, AuthorizedPetTypes.Append(petType).ToArray());
    }
}