namespace AbstractFactory
{
    using AnimalClassification;
    using Factories.AbstractFactory;

    /// <summary>
    /// The 'Client' class 
    /// </summary>
    public class AnimalWorld
    {
        private Herbivore herbivore;
        private Carnivore carnivore;

        public AnimalWorld(ContinentFactory factory)
        {
            carnivore = factory.CreateCarnivore();
            herbivore = factory.CreateHerbivore();
        }

        public void RunFoodChain()
        {
            carnivore.Eat(herbivore);
        }
    }

}
