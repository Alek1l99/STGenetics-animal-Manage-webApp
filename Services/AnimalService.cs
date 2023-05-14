using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AnimalModel.Model;

namespace stGenetics.Services
{
    public class AnimalService : IAnimalService
    {

        public List<Animal> Animals { get; set; }
        private readonly string _jsonPath = Path.Combine("Services", "animalData.json");
        public string SelectedDropdownValue { get; set; }

        public AnimalService()
        {
            LoadAnimalData();
        }

        public async Task<List<Animal>> GetAnimals()
        {
            return Animals;
        }

        public async Task<Animal> GetSingleAnimal(int id)
        {
            return Animals.FirstOrDefault(a => a.AnimalId == id);
        }

        public async Task CreateAnimal(Animal animal)
        {
            int newId = Animals.Max(a => a.AnimalId) + 1;
            animal.AnimalId = newId;

            Animals.Add(animal);
            await SaveAnimalsToJson();
        }

        public async Task UpdateAnimal(Animal animal)
        {
            var existingAnimal = Animals.FirstOrDefault(a => a.AnimalId == animal.AnimalId);
            if (existingAnimal != null)
            {
                existingAnimal.Name = animal.Name;
                existingAnimal.Breed = animal.Breed;
                existingAnimal.BirthDate = animal.BirthDate;
                existingAnimal.Sex = animal.Sex;
                existingAnimal.Price = animal.Price;
                existingAnimal.Status = animal.Status;

                await SaveAnimalsToJson();
            }
        }

        public async Task DeleteAnimal(int id)
        {
            var animal = Animals.FirstOrDefault(a => a.AnimalId == id);
            if (animal != null)
            {
                Animals.Remove(animal);
                await SaveAnimalsToJson();
            }
        }

        public List<Animal> FilterAnimals(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Animals;

            searchTerm = searchTerm.ToLowerInvariant();

            return Animals.Where(animal =>
                animal.Name.ToLowerInvariant().Contains(searchTerm) ||
                animal.Breed.ToLowerInvariant().Contains(searchTerm) ||
                animal.Sex.ToLowerInvariant().Contains(searchTerm) ||
                animal.Status.ToLowerInvariant().Contains(searchTerm)).ToList();
        }

        public void LoadAnimalData()
        {
            if (File.Exists(_jsonPath))
            {
                string jsonData = File.ReadAllText(_jsonPath);
                Animals = JsonSerializer.Deserialize<List<Animal>>(jsonData);
            }
            else
            {
                Animals = new List<Animal>();
            }
        }

        private async Task SaveAnimalsToJson()
        {
            string jsonData = JsonSerializer.Serialize(Animals);
            await File.WriteAllTextAsync(_jsonPath, jsonData);
        }
    }
}
