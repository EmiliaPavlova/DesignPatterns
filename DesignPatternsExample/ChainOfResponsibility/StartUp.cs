using ChainOfResponsibility.Approvers;

namespace ChainOfResponsibility
{
    class StartUp
    {
        static void Main()
        {
            // Setup Chain of Responsibility
            Approver junior = new JuniorLector();
            Approver lector = new Lector();
            Approver senior = new SeniorLector();

            junior.SetSuccessor(lector);
            lector.SetSuccessor(senior);

            // Generate and process rating exams requests
            Rate p = new Rate(2034, 75.00, "Databases");
            junior.ProcessRequest(p);

            p = new Rate(2035, 101.00, "Databases");
            junior.ProcessRequest(p);

            p = new Rate(2036, 295.00, "Design Patterns");
            junior.ProcessRequest(p);
        }
    }
}