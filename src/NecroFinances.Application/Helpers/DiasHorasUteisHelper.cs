using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Helpers
{
    public static class DiasHorasUteisHelper
    {
        public static int CalcularDiasUteis(DateTime date)
        {
            int ano = date.Year;
            int mes = date.Month;

            int diasNoMes = DateTime.DaysInMonth(ano, mes);
            int diasUteis = 0;

            var feriados = FeriadosNacionais(ano);

            for (int dia = 1; dia <= diasNoMes; dia++)
            {
                var data = new DateTime(ano, mes, dia);

                if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                if (feriados.Contains(data))
                    continue;

                diasUteis++;
            }

            return diasUteis;
        }

        private static HashSet<DateTime> FeriadosNacionais(int ano)
        {
            var feriados = new HashSet<DateTime>();

            feriados.Add(new DateTime(ano, 1, 1));   // Confraternização Universal
            feriados.Add(new DateTime(ano, 4, 21));  // Tiradentes
            feriados.Add(new DateTime(ano, 5, 1));   // Dia do Trabalho
            feriados.Add(new DateTime(ano, 9, 7));   // Independência
            feriados.Add(new DateTime(ano, 10, 12)); // Nossa Senhora Aparecida
            feriados.Add(new DateTime(ano, 11, 2));  // Finados
            feriados.Add(new DateTime(ano, 11, 15)); // Proclamação da República
            feriados.Add(new DateTime(ano, 12, 25)); // Natal

            DateTime pascoa = DataPascoa(ano);
            feriados.Add(pascoa.AddDays(-48)); // Carnaval (segunda)
            feriados.Add(pascoa.AddDays(-47)); // Carnaval (terça)
            feriados.Add(pascoa.AddDays(-2));  // Sexta-feira Santa
            feriados.Add(pascoa.AddDays(60));  // Corpus Christi

            return feriados;
        }

        private static DateTime DataPascoa(int ano)
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

            return new DateTime(ano, mes, dia);
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

        public static DateTime AjustarParaDia(DateTime data, int dia)
        {
            DateTime dataLocal = data.Kind switch
            {
                DateTimeKind.Utc => data.ToLocalTime(),
                DateTimeKind.Unspecified => DateTime.SpecifyKind(data, DateTimeKind.Local),
                _ => data
            };

            var baseDate = dataLocal.Day < dia ? dataLocal.AddMonths(-1) : dataLocal;

            int diaFinal = Math.Min(dia, DateTime.DaysInMonth(baseDate.Year, baseDate.Month));

            return new DateTime(
                baseDate.Year,
                baseDate.Month,
                diaFinal,
                dataLocal.Hour,
                dataLocal.Minute,
                dataLocal.Second,
                DateTimeKind.Local
            );
        }
    }
}
