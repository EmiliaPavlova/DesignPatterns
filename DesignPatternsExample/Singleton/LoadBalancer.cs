namespace Singleton
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'Singleton' class
    /// </summary>
    public class LoadBalancer
    {
        private static LoadBalancer instance;
        private List<string> servers = new List<string>();
        private Random random = new Random();

        // Lock synchronization object
        private static object syncLock = new object();

        protected LoadBalancer()
        {
            servers.Add("ServerI");
            servers.Add("ServerII");
            servers.Add("ServerIII");
            servers.Add("ServerIV");
            servers.Add("ServerV");
        }

        public static LoadBalancer GetLoadBalancer()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new LoadBalancer();
                    }
                }
            }

            return instance;
        }


        public string Server
        {
            get
            {
                int r = random.Next(servers.Count);
                return servers[r].ToString();
            }
        }
    }
}
