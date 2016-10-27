namespace Adapter.Compounds
{
    using System;

    /// <summary>
    /// The 'Adapter' class
    /// </summary>
    public class RichCompound : Compound
    {
        private ChemicalDatabank bank;

        public RichCompound(string name)
          : base(name)
        {
        }

        public override void Display()
        {
            // The Adaptee
            bank = new ChemicalDatabank();

            boilingPoint = bank.GetCriticalPoint(chemical, "B");
            meltingPoint = bank.GetCriticalPoint(chemical, "M");
            molecularWeight = bank.GetMolecularWeight(chemical);
            molecularFormula = bank.GetMolecularStructure(chemical);

            base.Display();
            Console.WriteLine($"   Formula: {molecularFormula}");
            Console.WriteLine($"   Weight : {molecularWeight}");
            Console.WriteLine($"   Melting Pt: {meltingPoint}");
            Console.WriteLine($"   Boiling Pt: {boilingPoint}");
        }
    }
}
