using System;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionAppProcessarVotos.EventHubs;
using FunctionAppProcessarVotos.Data;

namespace FunctionAppProcessarVotos
{
    public static class ProcessarVotosEventHub
    {
        [Function("ProcessarVotosEventHub")]
        public static void Run([EventHubTrigger("votacao", ConsumerGroup = "groffesql", Connection = "AzureEventHubs")] string[] input, FunctionContext context)
        {
            var logger = context.GetLogger("ProcessarVotosEventHub");
            foreach (string eventData in input)
                ProcessEvent(eventData, logger);
            logger.LogInformation(
                $"Execução concluída - número de eventos processados: {input.Length}");
        }

        private static void ProcessEvent(string eventData, ILogger logger)
        {
            logger.LogInformation($"[Evento] " + eventData);

            QuestaoEventData questaoEventData = null;
            try
            {
                questaoEventData = JsonSerializer.Deserialize<QuestaoEventData>(
                    eventData,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                logger.LogError(
                    "Erro durante a deserializacao dos dados recebidos!");
            }

            if (questaoEventData is not null)
            {
                if (!String.IsNullOrWhiteSpace(questaoEventData.IdVoto) &
                    !String.IsNullOrWhiteSpace(questaoEventData.Horario))
                {
                    if (!String.IsNullOrWhiteSpace(questaoEventData.Tecnologia))
                    {
                        VotacaoRepository.SaveVotoTecnologia(questaoEventData);
                        logger.LogInformation($"Voto = {questaoEventData.IdVoto} | " +
                            $"Tecnologia = {questaoEventData.Tecnologia} | " +
                            "Evento computado com sucesso!");
                        return;
                    }
                    else if (!String.IsNullOrWhiteSpace(questaoEventData.Instancia))
                    {
                        VotacaoRepository.SaveHistoricoProcessamento(questaoEventData);
                        logger.LogInformation($"Voto = {questaoEventData.IdVoto} | " +
                            $"Instância = {questaoEventData.Instancia} | " +
                            "Evento computado com sucesso!");
                        return;
                    }
                }

                logger.LogError($"Formato dos dados do evento inválido!");
            }
        }
    }
}