using Gosu.MobileGateway.Services;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGrpcClient.Services
{
    internal class LogoutService
    {
        private readonly UtilityService utilityService;

        public LogoutService(UtilityService utilityService)
        {
            this.utilityService = utilityService;
        }

        public Empty_Response Logout(string accessToken, string clientId, string userName, string signature)
        {


            var grpcChannel = GrpcChannel.ForAddress(utilityService.GetServerAddress());
            var gosuClient = new grpcMobileGatewayService.grpcMobileGatewayServiceClient(grpcChannel);
            return gosuClient.Logout(new Logout_Request
            {
                AccessToken = accessToken,
                ClientID = clientId,
                DeviceID = utilityService.GetDeviceId(),
                UserName = userName,
                Signature = signature
            });
        }
    }
}
