using Gosu.MobileGateway.Services;
using Grpc.Net.Client;
using GrpcService1;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SimpleGrpcClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userName = "minhkhangg32";

            string password = "Panda_1005";
            string hashedPassword = HashPasswordWithMD5(password);
            string clientId = "m300.lzl3yhzrksOZqzNBXjZQhsN4QaQIMw";
            string clientSecret = "Z6LNilucKdHwXG8QLxZEPI4tAoC5Rj";
            string deviceId = GetDeviceId();
            string deviceBranch = Environment.MachineName;
            string combined = clientId + userName + clientSecret;
            string signature = HashPasswordWithMD5(combined);
            var grpcChannel = GrpcChannel.ForAddress("http://123.30.106.114:5001");
            var gosuClient = new grpcMobileGatewayService.grpcMobileGatewayServiceClient(grpcChannel);
            var gosuReply = gosuClient.Login(new Gosu.MobileGateway.Services.Login_Request
            {
                UserName = userName,
                Password = hashedPassword,
                DeviceID = deviceId,
                DeviceBrand = deviceBranch,
                GameID = "",
                ClientID = clientId,
                SdkSignature = "",
                SecurityCode = clientSecret,
                Signature = signature,
            });
            static string HashPasswordWithMD5(string password)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(password);

                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            string GetDeviceId()
            {
                string result = "";
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        result = obj["UUID"]?.ToString();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                return result;
            }
            Console.WriteLine("Data: " + gosuReply.ToString());
            Console.ReadKey();
        }
    }
}
