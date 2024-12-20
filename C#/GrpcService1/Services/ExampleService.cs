using Grpc.Core;

namespace GrpcService1.Services
{
    public class ExampleService : ExampleRPC.ExampleRPCBase
    {
        private readonly ILogger<ExampleService> _logger;
        public ExampleService(ILogger<ExampleService> logger)
        {
            _logger = logger;
        }

        public override Task<Login_Response> Login(Login_Request request, ServerCallContext context)
        {
            return Task.FromResult(new Login_Response
            {
                UserName = request.UserName,
                Password = request.Password,
                FullName = "Khang",
            });
        }
    }
}
