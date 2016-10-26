namespace FactoryMethod
{
    using Creators;
    using Pages;
    using System;

    /// <summary>
    /// MainApp startup class for Real-World 
    /// Factory Method Design Pattern.
    /// </summary>
    public class StartUp
    {
        static void Main()
        {
            // Constructors call Factory Method
            Document[] documents = new Document[2];

            documents[0] = new Resume();
            documents[1] = new Report();

            // Display document pages
            foreach (Document document in documents)
            {
                Console.WriteLine(document.GetType().Name + ":");
                foreach (Page page in document.Pages)
                {
                    Console.WriteLine(" " + page.GetType().Name);
                }
                Console.WriteLine();
            }
        }
    }
}