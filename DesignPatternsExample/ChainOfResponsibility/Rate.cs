namespace ChainOfResponsibility
{
    public class Rate
    {
        private double result;
        private string subject;

        public Rate(int id, double result, string subject)
        {
            this.Id = id;
            this.Result = result;
            this.Subject = subject;
        }

        public int Id { get; private set; }


        public double Result
        {
            get { return this.result; }
            set { this.result = value; }
        }

        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }
    }
}
