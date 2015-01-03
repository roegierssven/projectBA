using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.model.Organisation
{
    public class Errorlog
    {
        private int _registerID;
        public int RegisterID
        {
            get { return _registerID; }
            set { _registerID = value; }
        }

        
        private int _timestamp;
        public int Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _stackTrace;
        public string StackTrace
        {
            get { return _stackTrace; }
            set { _stackTrace = value; }
        } 
    }
}
