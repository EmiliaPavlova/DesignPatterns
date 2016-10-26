namespace Factories.AbstractFactory
{
    using global::AbstractFactory.AnimalClassification;
    using global::AbstractFactory.Animals;

    /// <summary>
    /// A 'ConcreteFactory' class
    /// </summary>
    class AmericaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Bison();
        }

        public override Carnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }
}
