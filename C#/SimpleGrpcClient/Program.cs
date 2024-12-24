using Gosu.MobileGateway.Services;
using Grpc.Net.Client;
using GrpcService1;
using SimpleGrpcClient.Services;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SimpleGrpcClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize services
            var utilityService = new UtilityService();
            var loginService = new LoginService(utilityService);
            var userService = new UserService(utilityService);
            var logoutServive = new LogoutService(utilityService);
            // Define input data
            string userName = "minhkhangg32";
            string password = "Panda_1005";
            string hashedPass = utilityService.HashPasswordWithMD5(password);
            string clientId = "m300.lzl3yhzrksOZqzNBXjZQhsN4QaQIMw";
            string clientSecret = "Z6LNilucKdHwXG8QLxZEPI4tAoC5Rj";
            string signature = utilityService.HashPasswordWithMD5(clientId + userName + clientSecret);
            // Perform login
            try
            {
                var response = loginService.Login(userName, password, clientId, clientSecret, signature);
                Console.WriteLine("Login successful!");
                Console.WriteLine($"Response: {response}");
                var profile = userService.GetProfile(userName, response.AccessToken, clientId, signature);
                Console.WriteLine("Get infor successful!");
                Console.WriteLine($"Profile: {profile}");
                var logout = logoutServive.Logout(response.AccessToken, clientId, userName, signature);
                Console.WriteLine("Logout successful!");
                Console.WriteLine($"Logout: {logout}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred during login:");
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
