using Microsoft.Extensions.Options;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly object _loggerLock = new();
        private readonly LoggerSettings _settings;

        public LoggerService(
            IOptions<LoggerSettings> options
            )
        {
            _settings = options.Value;
        }

        public void Log(string message)
        {
            Write(message);
        }

        public void Log(string message, Exception exception)
        {
            StringBuilder sb = new();

            sb.AppendLine(message);
            AppendException(sb, exception);

            Write(sb.ToString());
        }

        private void Write(string content)
        {
            DateTime now = DateTime.Now;

            string yearFolder = Path.Combine(_settings.RootPath, now.Year.ToString());
            string monthFolder = Path.Combine(yearFolder, GetMonthName(now.Month));

            Directory.CreateDirectory(monthFolder);

            string filePath = Path.Combine(monthFolder, $"{now.Day:00}.log");
            string entry = $"{now:dd/MM/yyyy HH:mm:ss:fff}: {content}" + Environment.NewLine + Environment.NewLine;

            lock (_loggerLock)
            {
                File.AppendAllText(filePath, entry, Encoding.UTF8);
            }
        }

        private void AppendException(StringBuilder sb, Exception exception, int level = 0)
        {
            string indent = new(' ', level * 2);

            sb.AppendLine();
            sb.AppendLine($"{indent}Exception:");
            sb.AppendLine($"{indent}{exception.Message}");

            sb.AppendLine();
            sb.AppendLine($"{indent}StackTrace:");
            sb.AppendLine($"{indent}------------------");
            sb.AppendLine(exception.StackTrace);

            if (exception.InnerException != null)
            {
                sb.AppendLine();
                sb.AppendLine($"{indent}InnerException:");
                sb.AppendLine($"{indent}------------------");

                AppendException(sb, exception.InnerException, level + 1);
            }
        }

        private string GetMonthName(int month)
        {
            switch (month)
            {
                case 1: return "Janeiro";
                case 2: return "Fevereiro";
                case 3: return "Marco";
                case 4: return "Abril";
                case 5: return "Maio";
                case 6: return "Junho";
                case 7: return "Julho";
                case 8: return "Agosto";
                case 9: return "Setembro";
                case 10: return "Outubro";
                case 11: return "Novembro";
                case 12: return "Dezembro";
                default: throw new ArgumentException("Mes inválido");
            }
        }
    }
}
