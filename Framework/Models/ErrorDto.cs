using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    public class ErrorDto
    {
        /*These are the fields for the ErrorLog Table in Database
        This will be used as a reference to set and get the data*/
        public string Source { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
