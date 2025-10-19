using System.Threading.Tasks;
using Standard.AI.PeerLLM.Clients;
using Standard.AI.PeerLLM.Models.Configurations;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace PeerLLM.Demoes.Consoles.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var peerLLMConfigurations =
                new PeerLLMConfiguration();

            var peerLLMClient = new PeerLLMClient(
                peerLLMConfigurations);

            var chatSessionConfiguration = new ChatSessionConfig
            {
                ModelName = "mistral-7b-instruct-v0.1.Q8_0",
                Role = "System",
                RoleContent = "You are a helpful assistant."
            };

            Guid conversationId =
                await peerLLMClient.V1.Chats.StartChatAsync(
                    chatSessionConfiguration);

            while(true)
            {
                Console.Write("User:");
                string userPrompt = Console.ReadLine();

                IAsyncEnumerable<string> responseStream =
                    peerLLMClient.V1.Chats.StreamChatAsync(
                        conversationId,
                        text: userPrompt);

                await foreach (string responsePart in responseStream)
                {
                    Console.Write(responsePart);
                }
            }


            Console.WriteLine("Session Complete");
        }
    }
}
