namespace Bridge.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'ConcreteImplementor' class
    /// </summary>
    public class CustomersData : DataObject
    {
        private List<string> customers = new List<string>();
        private int current = 0;

        public CustomersData()
        {
            // Loaded from a database 
            customers.Add("Jim Jones");
            customers.Add("Samual Jackson");
            customers.Add("Allen Good");
            customers.Add("Ann Stills");
            customers.Add("Lisa Giolani");
        }

        public override void NextRecord()
        {
            if (current <= customers.Count - 1)
            {
                current++;
            }
        }

        public override void PriorRecord()
        {
            if (current > 0)
            {
                current--;
            }
        }

        public override void AddRecord(string customer)
        {
            customers.Add(customer);
        }

        public override void DeleteRecord(string customer)
        {
            customers.Remove(customer);
        }

        public override void ShowRecord()
        {
            Console.WriteLine(customers[current]);
        }

        public override void ShowAllRecords()
        {
            foreach (string customer in customers)
            {
                Console.WriteLine("   " + customer);
            }
        }
    }
}
