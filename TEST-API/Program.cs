using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using TEST_API.Helpers.API;
using TEST_API.Middlewares;

namespace TEST_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(op =>
            {
                op.InvalidModelStateResponseFactory = context =>
                {
                    return ApiReponseHelpers.ResponseErrorApi(400,
                        JsonConvert.SerializeObject(context.ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage)));
                };
            });

            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddControllers().AddJsonOptions(op =>
                op.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration.GetSection("JWT_SECURITY_KEY").Value!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            //app.UseAuthorization();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/v1/protected-by-api-key/db"), appBuilder =>
            {
                appBuilder.UseMiddleware<ApiKeyMiddleware>();
            });

            app.MapControllers();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
