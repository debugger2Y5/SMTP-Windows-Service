using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K181185_QS1
{
    class JSONClass
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }

        //example json file looks like this:
        //{
        //"To":"toobaxd@gmail.com",
        //"Subject":"Congratulations",
        //"MessageBody":"Congrats on ur 4.0 in IPT!"
        //}
    }
}
