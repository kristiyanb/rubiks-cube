using RubiksCubeSolver.Services;
using System.Text.Json.Serialization;

namespace RubiksCubeSolver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IRubiksCubeService, RubiksCubeService>();
            builder.Services
                .AddControllers()
                .AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                });

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

           if (app.Environment.IsDevelopment())
            {
                app.UseCors(opt =>
                {
                    opt.AllowAnyHeader();
                    opt.AllowAnyMethod();
                    opt.AllowAnyOrigin();
                });
            }

            app.Run();
        }
    }
}