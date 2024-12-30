using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WordApp.Core.Extensions;
using WordApp.Data;

namespace WordApp.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddWordAppContext(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            
            // MongoDB Identity
            builder.Services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole>(identityOptions =>
            {
                // Password kontrollerini kod içerisinden sağlayacağız. Password validator yazılabilir.
                //identityOptions.Password.RequiredLength = 6;
                //identityOptions.Password.RequireDigit = true;
            },
            mongoIdentityOptions =>
            {
                mongoIdentityOptions.ConnectionString = "mongodb://localhost:29999/WordApp";
            });

            Console.WriteLine($"BuilderConfiguration Jwt:SecretKey : {builder.Configuration["Jwt:SecretKey"]}");
            builder.Services.AddAuthentication(options =>
            {
                //Uygulamanın kullanıcıyı doğrulamak için hangi yöntemi kullanacağını belirtir.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //Kimlik doğrulama hatası olduğunda (örneğin, token eksik veya geçersiz olduğunda) hangi yöntemle yanıt verileceğini belirtir.
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
            .AddJwtBearer(options =>
            {
                // JWT tabanlı kimlik doğrulama yapılandırması
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Token'ı oluşturan sunucunun doğrulanıp doğrulanmayacağını belirtir.
                    ValidateAudience = true, // Token'ın hangi hedef kitle için geçerli olduğunu kontrol eder.
                    ValidateLifetime = true, // Token'ın geçerlilik süresinin kontrol edilip edilmeyeceğini belirtir.
                    ValidateIssuerSigningKey = true, // Token'ın imzasının kontrol edilip edilmeyeceğini belirtir.
                    
                    // Token'ı oluşturan sunucunun (issuer) adı veya adresi
                    ValidIssuer = "yourissuer", // Örn: "https://yourapp.com" (token'ı kim oluşturdu)
            
                    // Token'ın hedef kitleyi (audience) temsil eden bir isim veya adres
                    ValidAudience = "youraudience", // Örn: "https://yourapi.com" (token kimin için geçerli)

                    // Token'ın imzasını doğrulamak için kullanılan gizli anahtar
                    // HOC -> Hangi anahtarla imzalandıysa onunla doğrulama yapılmalı. YANİ TOKEN ÜRETİLİRKEN KULLANILAN ANAHTARLA DOĞRULAMA YAPILMALI
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
