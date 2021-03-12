using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SampleApp
{
    /// <summary>
    /// This class is power hungry and wants to do everything.
    /// </summary>
    public class Multitasker
    {
        public DatabaseManager DatabaseManager {get;set;}

        public async Task<List<SomeEntity>> ReadAndFilterFromDatabase(User user)
        {
            var entities = DatabaseManager.GetEntities().GetAwaiter().GetResult();

            var results = new List<SomeEntity>();

            foreach (var entity in entities)
            {
                //see if user is authorized to view entity
                if (await UserCanViewEntity(user, entity))
                    results.Add(entity);
            }

            return results;
        }

        public async Task FetchAndSaveEntities()
        {
            var httpClient = new HttpClient();

            //Assume https://myawesomesite316/entities is valid and returns valid data
            var response = await httpClient.GetAsync("https://myawesomesite316/entities");

            var externalData = System.Text.Json.JsonSerializer.Deserialize<List<SomeEntity>>(await response.Content.ReadAsStringAsync());

            await DatabaseManager.SaveEntities(externalData);
        }

        public async Task SendEmailToUser(User user)
        {
            var smtpClient = new SmtpClient("mysmtpserver.com")
            {
                EnableSsl = false
            };

            smtpClient.Send("sample@sampleapp.com", user.Email, "Friendly email",  $"Hello {user.Username}!");
        }

        private Task<bool> UserCanViewEntity(User user, SomeEntity entity)
        {
            return Task.FromResult(user.Username == "SUPERADMIN");
        }
    }
}
