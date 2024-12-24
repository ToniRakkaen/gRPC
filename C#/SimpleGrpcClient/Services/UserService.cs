using Gosu.MobileGateway.Services;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGrpcClient.Services
{
    internal class UserService
    {
        private readonly UtilityService utilityService;

        public UserService(UtilityService utilityService)
        {
            this.utilityService = utilityService;
        }
        public GetProfile_Response GetProfile(string username, string accessToken, string clientId, string signature)
        {
            string deviceId = utilityService.GetDeviceId();
            var grpcChannel = GrpcChannel.ForAddress(utilityService.GetServerAddress());
            var gosuClient = new grpcMobileGatewayService.grpcMobileGatewayServiceClient(grpcChannel);
            return gosuClient.GetProfile(new GetProfile_Request
            {
                UserName = username,
                AccessToken = accessToken,
                DeviceID = deviceId,
                ClientID = clientId,
                Signature = signature
            });
        }
    }
}
