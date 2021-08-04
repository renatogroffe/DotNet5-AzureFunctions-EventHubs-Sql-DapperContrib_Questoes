using System;
using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using FunctionAppProcessarVotos.EventHubs;

namespace FunctionAppProcessarVotos.Data
{
    public static class VotacaoRepository
    {
        public static void SaveHistoricoProcessamento(
            QuestaoEventData questaoEventData)
        {
            using var conexao = new SqlConnection(
                Environment.GetEnvironmentVariable("BaseVotacaoEventHub"));
            conexao.Insert<HistoricoProcessamento>(new ()
            {
                IdVoto = questaoEventData.IdVoto,
                Horario = Convert.ToDateTime(questaoEventData.Horario),
                Producer = questaoEventData.Instancia,
                Consumer = Environment.MachineName
            });
        }

        public static void SaveVotoTecnologia(
            QuestaoEventData questaoEventData)
        {
            using var conexao = new SqlConnection(
                Environment.GetEnvironmentVariable("BaseVotacaoEventHub"));
            conexao.Insert<VotoTecnologia>(new ()
            {
                IdVoto = questaoEventData.IdVoto,
                Horario = Convert.ToDateTime(questaoEventData.Horario),
                Tecnologia = questaoEventData.Tecnologia,
                Consumer = Environment.MachineName
            });
        }
    }
}