using System;

namespace ChainOfResponsibility.Approvers
{
    public class SeniorLector : Approver
    {
        public override void ProcessRequest(Rate exam)
        {
            if (exam.Result < 300.0)
            {
                Console.WriteLine($"{this.GetType().Name} approved exam# {exam.Id} in {exam.Subject} as 'Excellent' with {exam.Result}.");
            }
            else
            {
                Console.WriteLine($"Exam# {exam.Id} in {exam.Subject} requires an additional review!");
            }
        }
    }
}
