using System;

namespace ChainOfResponsibility.Approvers
{
    public class Lector : Approver
    {
        public override void ProcessRequest(Rate exam)
        {
            if (exam.Result < 200.0)
            {
                Console.WriteLine($"{this.GetType().Name} approved exam# {exam.Id} in {exam.Subject} as 'Taken' with {exam.Result}.");
            }
            else if (successor != null)
            {
                successor.ProcessRequest(exam);
            }
        }
    }
}
