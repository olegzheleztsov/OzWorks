using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimplePages.Models;
using SimplePages.Services.Interfaces;

namespace SimplePages.Services
{
    public class ZooService : IZooService
    {
        private static readonly Lazy<List<AnimalSlot>> _animalSlots = new Lazy<List<AnimalSlot>>(() =>
        {
            return new List<AnimalSlot>()
            {
                new AnimalSlot()
                {
                    Animal = new Animal()
                    {
                        Age = 1,
                        Kind = AnimalKind.Crocodile,
                        Name = "Crocodile Mike",
                        Sex = AnimalSex.M,
                        Weight = 100
                    },
                    Count = 2
                },
                new AnimalSlot()
                {
                    Animal = new Animal()
                    {
                        Age = 2,
                        Kind = AnimalKind.Eagle,
                        Name = "Eagle",
                        Sex = AnimalSex.W,
                        Weight = 20
                    },
                    Count = 1
                },
                new AnimalSlot()
                {
                    Animal = new Animal()
                    {
                        Age = 3,
                        Kind = AnimalKind.Leon,
                        Name = "Leon",
                        Sex = AnimalSex.M,
                        Weight = 150
                    },
                    Count = 3
                },
                new AnimalSlot()
                {
                    Animal = new Animal()
                    {
                        Age = 4,
                        Kind = AnimalKind.Monkey,
                        Name = "Monkey",
                        Sex = AnimalSex.W,
                        Weight = 23
                    },
                    Count = 10
                },
                new AnimalSlot()
                {
                    Animal = new Animal()
                    {
                        Age = 1,
                        Kind = AnimalKind.Penguin,
                        Name = "Penguin",
                        Sex = AnimalSex.M,
                        Weight = 15
                    },
                    Count = 4
                },
                new AnimalSlot()
                {
                    Animal = new Animal()
                    {
                        Age = 3,
                        Kind = AnimalKind.Tiger,
                        Name = "Tiger",
                        Sex = AnimalSex.W,
                        Weight = 150
                    },
                    Count = 2
                },
                new AnimalSlot()
                {
                    Animal = new Animal()
                    {
                        Age = 1,
                        Kind = AnimalKind.Zebra,
                        Name = "Zebra",
                        Sex = AnimalSex.W,
                        Weight = 50
                    },
                    Count = 3
                }
            };
        }, true);

        public async Task<IEnumerable<AnimalSlot>> GetAnimalsAsync(AnimalKind animalKind)
        {
            return await Task.FromResult(_animalSlots.Value.Where(animal => animal.Animal.Kind == animalKind));
        }

        public async Task<bool> AddAnimalAsync(AnimalSlot animalSlot)
        {
            _animalSlots.Value.Add(animalSlot);
            return await Task.FromResult(true);
        }

        public int ZooCapacity => 100;
        
        public async Task<IEnumerable<AnimalSlot>> GetAnimalsAsync(Func<AnimalSlot, bool> predicate)
        {
            return await Task.FromResult(_animalSlots.Value.Where(predicate));
        }
    }
}