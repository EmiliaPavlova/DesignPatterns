namespace Adapter.Compounds
{
    using System;

    /// <summary>
    /// The 'Target' class
    /// </summary>
    public class Compound
    {
        protected string chemical;
        protected float boilingPoint;
        protected float meltingPoint;
        protected double molecularWeight;
        protected string molecularFormula;

        public Compound(string chemical)
        {
            this.chemical = chemical;
        }

        public virtual void Display()
        {
            Console.WriteLine($"\nCompound: {chemical}:");
        }
    }
}
