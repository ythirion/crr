﻿namespace Workshop.Tests._01._Fundamentals.Data
{
    public readonly struct Pet(PetType type, string name, int age)
    {
        public PetType Type { get; } = type;
        public string Name { get; } = name;
        public int Age { get; } = age;
    }
}