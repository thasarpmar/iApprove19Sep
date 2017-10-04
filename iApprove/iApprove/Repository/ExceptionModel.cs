using System;

namespace iApprove.Repository
{
    public class ExceptionModel 
    {
        public DateTime TimeStamp { get; set; }

        //public string ExceptionInnerMessage { get; set; }

        public string ExceptionMessage { get; set; }

        /// <summary>
        /// This property is used for storing stacktrace information whenever errors occurs.
        /// </summary>
        public string ExceptionOrigin { get; set; }            
    }
}
