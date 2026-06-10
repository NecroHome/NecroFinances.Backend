using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Helpers
{
    public static class DiasHorasUteisHelper
    {
        public static int CalcularDiasUteis(DateOnly date)
        {
            int ano = date.Year;
            int mes = date.Month;

            int diasNoMes = DateTime.DaysInMonth(ano, mes);
            int diasUteis = 0;

            var feriados = FeriadosNacionais(ano);

            for (int dia = 1; dia <= diasNoMes; dia++)
            {
                var data = new DateOnly(ano, mes, dia);

                if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                if (feriados.Contains(data))
                    continue;

                diasUteis++;
            }

            return diasUteis;
        }

        private static HashSet<DateOnly> FeriadosNacionais(int ano)
        {
            var feriados = new HashSet<DateOnly>();

            feriados.Add(new DateOnly(ano, 1, 1));
            feriados.Add(new DateOnly(ano, 4, 21));
            feriados.Add(new DateOnly(ano, 5, 1));
            feriados.Add(new DateOnly(ano, 9, 7));
            feriados.Add(new DateOnly(ano, 10, 12));
            feriados.Add(new DateOnly(ano, 11, 2));
            feriados.Add(new DateOnly(ano, 11, 15));
            feriados.Add(new DateOnly(ano, 12, 25));

            DateOnly pascoa = DataPascoa(ano);
            feriados.Add(pascoa.AddDays(-48));
            feriados.Add(pascoa.AddDays(-47));
            feriados.Add(pascoa.AddDays(-2));
            feriados.Add(pascoa.AddDays(60));

            return feriados;
        }

        private static DateOnly DataPascoa(int ano)
        {
            int a = ano % 19;
            int b = ano / 100;
            int c = ano % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int mes = (h + l - 7 * m + 114) / 31;
            int dia = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateOnly(ano, mes, dia);
        }

        public static int DeterminarMesBase(DateTime data)
        {
            if (data.Day >= 10)
            {
                return data.Month;
            }

            int mesAnterior = data.Month - 1;

            if (mesAnterior == 0)
            {
                mesAnterior = 12;
            }

            return mesAnterior;
        }

        public static DateTime ObterInicioPeriodo(DateTime data)
        {
            DateTime dataLocal = data.Kind switch
            {
                DateTimeKind.Utc => data.ToLocalTime(),
                DateTimeKind.Unspecified => DateTime.SpecifyKind(data, DateTimeKind.Local),
                _ => data
            };

            if (dataLocal.Day < 10)
                dataLocal = dataLocal.AddMonths(-1);

            return new DateTime(
                dataLocal.Year,
                dataLocal.Month,
                10,
                0,
                0,
                0,
                DateTimeKind.Local
            );
        }
    }
}
