using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class Measurer
    {
        private int _measureId;

        private AccelServer.AccelServer _server;

        public Measurer()
        {
            _server = new AccelServer.AccelServer(5555);
        }

        public void Start()
        {
            _measureId++;

            
        }

        public void Stop()
        {

        }
    }
}
