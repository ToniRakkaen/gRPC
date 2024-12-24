using Gosu.MobileGateway.Services;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGrpcClient.Services
{
    internal class LoginService
    {
        private readonly UtilityService utilityService;

        public LoginService(UtilityService utilityService)
        {
            this.utilityService = utilityService;
        }

        public Login_Response Login(string username, string password, string clientId, string clientSecret, string signature)
        {
            string hashedPassword = utilityService.HashPasswordWithMD5(password);
            string deviceId = utilityService.GetDeviceId();
            string secretCombined = username + hashedPassword + clientId + deviceId + clientSecret;
            string securityCode = utilityService.HashPasswordWithMD5(secretCombined);

            // Generate signature
            string deviceBranch = Environment.MachineName;
            //string combined = clientId + username + clientSecret;
            //string signature = utilityService.HashPasswordWithMD5(combined);

            var grpcChannel = GrpcChannel.ForAddress(utilityService.GetServerAddress());
            var gosuClient = new grpcMobileGatewayService.grpcMobileGatewayServiceClient(grpcChannel);
            return gosuClient.Login(new Login_Request
            {
                UserName = username,
                Password = hashedPassword,
                ClientID = clientId,
                DeviceBrand = deviceBranch,
                DeviceID = deviceId,
                GameID = "",
                SdkSignature = "",
                SecurityCode = securityCode,
                Signature = signature,
            });
        }
    }
}
