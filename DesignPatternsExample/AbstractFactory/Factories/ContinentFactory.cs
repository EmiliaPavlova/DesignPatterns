namespace Factories.AbstractFactory
{
    using global::AbstractFactory.AnimalClassification;

    /// <summary>
    /// The 'AbstractFactory' abstract class
    /// </summary>
    public abstract class ContinentFactory
    {
        // an animal that feeds on plants
        public abstract Herbivore CreateHerbivore();
        // an animal that eats meat
        public abstract Carnivore CreateCarnivore();
    }
}
