namespace Factories.AbstractFactory
{
    using global::AbstractFactory.AnimalClassification;
    using global::AbstractFactory.Animals;

    /// <summary>
    /// A 'ConcreteFactory' class
    /// </summary>
    public class AfricaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Wildebeest();
        }

        public override Carnivore CreateCarnivore()
        {
            return new Lion();
        }
    }
}
