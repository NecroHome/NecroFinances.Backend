using NecroFinances.Application.Dtos;
using NecroFinances.Application.Dtos.Settings;
using NecroFinances.Application.Enums;
using NecroFinances.Application.Helpers;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;

namespace NecroFinances.Application.Services
{
    public class GastosService : IGastosService
    {
        private readonly IGastosRepositorie _gastoRepositorie;

        public GastosService(
            IGastosRepositorie gastoRepositorie
            )
        {
            _gastoRepositorie = gastoRepositorie;
        }

        public async Task<bool> AddGasto(GastosDTO dto, long userID)
        {
            long serie = await _gastoRepositorie.GetLastSerie();

            if (dto.TipoGasto == IndicadorTipoGasto.PARCELADO)
            {
                for (int i = 0; i < dto.TotalParcelas; i++)
                {
                    DateTime dataBase = dto.DataGasto.AddMonths(i);
                    DateTime data = DiasHorasUteisHelper.AjustarParaDia(dataBase, 15);

                    GastosModel parcela = new GastosModel
                    {
                        DataGasto = data,
                        Valor = dto.Valor,
                        Descricao = dto.Descricao,
                        TipoGasto = dto.TipoGasto,
                        Parcela = i + 1,
                        TotalParcelas = dto.TotalParcelas,
                        Icone = dto.Icone,
                        Serie = serie,
                        UserID = userID
                    };

                    await _gastoRepositorie.AddGasto(parcela);
                }
            }
            else if (dto.TipoGasto == IndicadorTipoGasto.RECORRENTE)
            {
                DateTime inicio = DiasHorasUteisHelper.AjustarParaDia(dto.DataGasto, 10);

                for (int mes = inicio.Month; mes <= 12; mes++)
                {
                    DateTime data = new DateTime(inicio.Year, mes, 10,
                        inicio.Hour, inicio.Minute, inicio.Second, inicio.Kind);

                    GastosModel gasto = new GastosModel
                    {
                        DataGasto = data,
                        Valor = dto.Valor,
                        Descricao = dto.Descricao,
                        TipoGasto = dto.TipoGasto,
                        Icone = dto.Icone,
                        Parcela = 0,
                        TotalParcelas = 0,
                        Serie = serie,
                        UserID = userID
                    };

                    await _gastoRepositorie.AddGasto(gasto);
                }
            }
            else
            {
                GastosModel gasto = new GastosModel
                {
                    DataGasto = dto.DataGasto,
                    Valor = dto.Valor,
                    Descricao = dto.Descricao,
                    TipoGasto = dto.TipoGasto,
                    Icone = dto.Icone,
                    Parcela = dto.Parcela,
                    TotalParcelas = dto.TotalParcelas,
                    Serie = dto.Serie,
                    UserID = userID
                };

                await _gastoRepositorie.AddGasto(gasto);
            }

            return true;
        }

        public async Task<bool> DeleteGasto(long gastoId, long userID)
        {
            GastosModel model = await _gastoRepositorie.GetGastoById(gastoId, userID);
            if (model == null)
            {
                throw new Exception("Gasto não encontrado para o ID informado");
            }
            await _gastoRepositorie.DeleteGasto(model);
            return true;
        }

        public async Task<GastosDTO> UpdateGasto(GastosDTO dto, long userID)
        {
            GastosModel model = await _gastoRepositorie.GetGastoById(dto.Id.Value, userID);
            if (model == null)
            {
                throw new Exception("Gasto não encontrado");
            }

            model.Serie = dto.Serie;
            model.DataGasto = dto.DataGasto;
            model.TipoGasto = dto.TipoGasto;
            model.Valor = dto.Valor;
            model.Icone = dto.Icone;
            model.Descricao = dto.Descricao;
            model.Parcela = dto.Parcela;
            model.TotalParcelas = dto.Parcela;

            model = await _gastoRepositorie.UpdateGasto(model);
            return new GastosDTO(model);
        }
    }
}
