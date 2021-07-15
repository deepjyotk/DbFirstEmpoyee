using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFirstEmployeeTask.Global
{
    abstract public class Failure
    {
        public string Message { get; set; }
    }


    //public class InvalidDataFailure : Failure {
    //    string Message { get; set; }
    //    InvalidDataFailure() { }
    //    InvalidDataFailure(string msg)
    //    {
    //        this.Message = msg; 
    //    }
    //}

    public class BadRequestFailure : Failure
    {
        public BadRequestFailure() { }
        public BadRequestFailure(string msg)
        {
            this.Message = msg;
        }
    }

    public class AlreadyLoggedInFailure : Failure
    {
        public AlreadyLoggedInFailure() { }
        public AlreadyLoggedInFailure(string msg)
        {
            this.Message = msg;
        }
    }


}
