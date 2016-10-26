namespace AbstractFactory.Animals
{
    using AnimalClassification;
    using System;

    public class Lion : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            // Eat Wildebeest
            Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
        }
    }
}
