using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BE.QuizByGPT.BLL.Models
{
    public class ErrorHandlingModel
    {
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
