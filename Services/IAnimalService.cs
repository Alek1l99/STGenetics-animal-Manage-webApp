using AnimalModel.Model;

namespace stGenetics.Services
{
    public interface IAnimalService
    {
        List<Animal> Animals { get; set; }
        Task<List<Animal>> GetAnimals();
        Animal GetSingleAnimal(int animalId);
        Task CreateAnimal(Animal animal);
        Task UpdateAnimal(Animal animal);
        Task DeleteAnimal(int id);
        List<Animal> FilterAnimals(string searchTerm);
        void LoadAnimalData();
    }
}
