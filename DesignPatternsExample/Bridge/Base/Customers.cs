namespace Bridge.Base
{
    using System;

    /// <summary>
    /// The 'RefinedAbstraction' class
    /// </summary>
    public class Customers : CustomersBase
    {
        public Customers(string group)
          : base(group)
        {
        }

        public override void ShowAll()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------");
            base.ShowAll();
            Console.WriteLine("------------------------");
        }
    }
}
