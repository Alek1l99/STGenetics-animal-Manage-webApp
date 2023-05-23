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
        public List<Animal> SelectedAnimals { get; set; }

        public async Task<List<Animal>> GetAnimals()
        {
            try
            {
                if (File.Exists(_jsonPath))
                {
                    string jsonData = File.ReadAllText(_jsonPath);
                    var animals = JsonSerializer.Deserialize<List<Animal>>(jsonData);
                    return animals;
                }
                else
                {
                    return new List<Animal>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar los datos del archivo JSON: {ex.Message}");
                return new List<Animal>();
            }
        }
        public void LoadAnimalData()
        {
            try
            {
                if (File.Exists(_jsonPath))
                {
                    string jsonData = File.ReadAllText(_jsonPath);
                    Animals = JsonSerializer.Deserialize<List<Animal>>(jsonData);
                }
                else
                {
                    Animals = new List<Animal>
            {
                new Animal { AnimalId = 1, Name = "Animal 1", Breed = "Breed 1", BirthDate = DateTime.Now, Sex = "Male", Price = 100.0m, Status = "Active" },
                new Animal { AnimalId = 2, Name = "Animal 2", Breed = "Breed 2", BirthDate = DateTime.Now, Sex = "Female", Price = 150.0m, Status = "Active" },
                new Animal { AnimalId = 3, Name = "Animal 3", Breed = "Breed 3", BirthDate = DateTime.Now, Sex = "Male", Price = 200.0m, Status = "Inactive" }
            };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar los datos del archivo JSON: {ex.Message}");
                Animals = new List<Animal>();
            }

            Console.WriteLine("Animals loaded successfully."); 
        }

        public Animal GetSingleAnimal(int animalId)
        {
            // Logic to retrieve the details of an animal based on its ID
            // You can query a database, call an external service, or any other data source
            // Return the found animal or null if no animal was found with the specified ID

            // Example implementation using an in-memory list of animals
            var animal = Animals.FirstOrDefault(a => a.AnimalId == animalId);

            if (animal != null)
            {
                // Check if the animal is already selected in the cart
                if (SelectedAnimals.Any(a => a.AnimalId == animal.AnimalId))
                {
                    // If the animal is already in the cart, return null indicating an error
                    return null;
                }

                // Apply a 5% discount if the number of selected animals is greater than or equal to 5
                if (SelectedAnimals.Count >= 5)
                {
                    decimal discountPercentage = 0.05m;
                    animal.Price -= animal.Price * discountPercentage;
                }

                // Add the animal to the list of selected animals
                SelectedAnimals.Add(animal);
            }

            return animal;
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

        private async Task SaveAnimalsToJson()
        {
            string jsonData = JsonSerializer.Serialize(Animals);
            await File.WriteAllTextAsync(_jsonPath, jsonData);
        }
    }
}
