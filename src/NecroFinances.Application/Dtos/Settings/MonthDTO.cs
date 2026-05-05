using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NecroFinances.Application.Dtos.Settings
{
    public class MesDTO
    {
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public int DiasUteis { get; set; }
        public int HorasUteis { get; set; }
        public int HorasExtras { get; set; }

        public MesDTO()
        {

        }

        public MesDTO(MesModel model) 
        {
            Id = model.Id;
            Data = model.Data;
            DiasUteis = model.DiasUteis;
            HorasUteis = model.HorasUteis;
            HorasExtras = model.HorasExtras;
        }

        public List<MesDTO> GenerateList(List<MesModel> list)
        {
            List<MesDTO> retorno = new List<MesDTO>();
            foreach (var model in list)
            {
                retorno.Add(new MesDTO(model));
            }
            return retorno;
        }
    }
}
