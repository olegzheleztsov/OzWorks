using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimplePages.Models;

namespace SimplePages.Services.Interfaces
{
    public interface IZooService
    {
        Task<IEnumerable<AnimalSlot>> GetAnimalsAsync(AnimalKind animalKind);
        Task<bool> AddAnimalAsync(AnimalSlot animalSlot);
        int ZooCapacity { get; }
        Task<IEnumerable<AnimalSlot>> GetAnimalsAsync(Func<AnimalSlot, bool> predicate);
    }
}