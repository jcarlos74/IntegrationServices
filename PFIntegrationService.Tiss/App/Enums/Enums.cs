namespace PFIntegrationService.Tiss.App.Enums
{
    /// <summary>
    /// Códigos de retorno da chamada dos métodos
    /// </summary>
    public enum RetornoValidacao
    {
        /// <summary>
        /// Método não executado. Deve ser utilizado como padrão
        /// </summary>
        NaoExecutado,

        /// <summary>
        /// Método executado com sucesso
        /// </summary>
        Sucesso,

        /// <summary>
        /// Ocorreu algum erro durante a execução do método
        /// </summary>
        Falha,

        /// <summary>
        /// Hash informado não é válido
        /// </summary>
        HashInvalido,

        /// <summary>
        /// O método executou, mas a estrutura do Xml não foi validado. Utilizado somente no retorno do método ValidarXML
        /// </summary>
        ErroValidacaoEstruturaXml,

        /// <summary>
        /// O método executou, mas as informações do Xml não foi validado.
        /// </summary>
        ErroValidacaoDadosXml

    }
}
