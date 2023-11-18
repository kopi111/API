using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Looger
{
    public class LoggingResponse
    {
        public bool Success { get; set; }
        public int LogId { get; set; }

        public LoggingResponse(bool success, int logId)
        {
            Success = success;
            LogId = logId;
        }

        public LoggingResponse(bool success)
        {
            Success = success;

        }
    }
}
