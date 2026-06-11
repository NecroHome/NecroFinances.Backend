using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Interfaces
{
    public interface ILoggerService
    {
        void Log(string log);
        void Log(string log, Exception ex);
    }
}
