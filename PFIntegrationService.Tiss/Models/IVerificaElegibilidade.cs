using PFIntegrationService.Tiss.Models.V3_05_00;
using System;
using System.ServiceModel;

namespace PFIntegrationService.Tiss.Models
{
    [ServiceContract]
    public interface IVerificaElegibilidade
    {
        [OperationContract, XmlSerializerFormat(Style = OperationFormatStyle.Document)]
        respostaElegibilidadeWS tissVerificaElegibilidade(pedidoElegibilidadeWS pedidoElegibilidadeWs);
    }
}
