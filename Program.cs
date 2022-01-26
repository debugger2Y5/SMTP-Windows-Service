using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace K181185_QS1
{
    static class Program
    {
       
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Q1Service()
            };
            ServiceBase.Run(ServicesToRun);


        }
    }
}
