using System;

namespace ChainOfResponsibility.Approvers
{
    public class JuniorLector : Approver
    {
        public override void ProcessRequest(Rate exam)
        {
            if (exam.Result < 100.0)
            {
                Console.WriteLine($"{this.GetType().Name} approved exam# {exam.Id} in {exam.Subject} as 'Not Taken' with {exam.Result}.");
            }
            else if (successor != null)
            {
                successor.ProcessRequest(exam);
            }
        }
    }
}
