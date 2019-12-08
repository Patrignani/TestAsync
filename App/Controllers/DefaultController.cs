using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System;

namespace TestAsync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly string _connectionString;

        public DefaultController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TestAsync");
        }

        [HttpGet]
        [Route("Async")]
        public async Task<ActionResult<string>> Async()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sequence = Guid.NewGuid();
                var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Logs");
                var insert = "INSERT INTO Logs(Message, DataExec, Sequence, Async) VALUES(@Message, @DataExec, @Sequence, 1)";
                connection.Execute(insert, new
                {
                    Message = $"Método Assíncrono iniciado. Contagem {count}",
                    DataExec = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss"),
                    Sequence = sequence
                });

                await Task.Delay(10);
                await Task.Delay(48);
                await Task.Delay(61);
                await Task.Delay(120);
                await Task.Delay(4);

                connection.Execute(insert, new
                {
                    Message = $"Método Assíncrona Finalizado. Contagem {count}",
                    DataExec = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss"),
                    Sequence = sequence
                });
            }
            return "Ok";
        }

        [HttpGet]
        [Route("Sync")]
        public ActionResult<string> Sync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sequence = Guid.NewGuid();
                var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Logs");
                var insert = "INSERT INTO Logs(Message, DataExec, Sequence, Async) VALUES(@Message, @DataExec, @Sequence, 0)";
                connection.Execute(insert, new
                {
                    Message = $"Método Síncrona iniciado. Contagem {count}",
                    DataExec = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss"),
                    Sequence = sequence
                });

                Task.Delay(10).Wait();
                Task.Delay(48).Wait();
                Task.Delay(61).Wait();
                Task.Delay(120).Wait();
                Task.Delay(4).Wait();

                connection.Execute(insert, new
                {
                    Message = $"Método Síncrona Finalizado. Contagem {count}",
                    DataExec = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss"),
                    Sequence = sequence
                });
            }
            return "Ok";
        }

    }
}