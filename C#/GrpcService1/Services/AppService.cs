using Gosu.MobileGateway.Services;
using Grpc.Core;

namespace GrpcService1.Services
{
    public class AppService : grpcMobileGatewayService.grpcMobileGatewayServiceBase
    {
        private readonly ILogger<AppService> _logger;
        public AppService(ILogger<AppService> logger)
        {
            _logger = logger;
        }

        public override Task<GetProfile_Response> GetProfile(GetProfile_Request request, ServerCallContext context)
        {
            return Task.FromResult(new GetProfile_Response()
            {
                UserName = request.UserName,
                Fullname = "Test fullname",
            });
        }
    }
}