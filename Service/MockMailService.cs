using Microsoft.Extensions.Logging;

namespace gestao.Service
{
    public class MockMailService : IMailService
    {
        private readonly ILogger<MockMailService> _logger;
        public MockMailService(ILogger<MockMailService> logger)
        {
            this._logger = logger;

        }
        public void SendMessage(string body)
        {
           // Log Message
            _logger.LogInformation($"Body:{body}");
        }


    }
}