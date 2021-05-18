using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechnoChat.Hubs;
using TechnoChat.Infrastructure;
using TII = TechnoChat.Infrastructure.Interfaces;
//localhost: 44348

namespace TechnoChat
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Ajout du 'GroupManager' en Singleton
			services.AddSingleton<TII.IGroupManager, GroupManager>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						options.SaveToken = true;
						options.TokenValidationParameters = new TokenValidationParameters()
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = Configuration["jwt:issuer"],
							ValidAudience = Configuration["jwt:audience"],
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["jwt:key"]))
						};
						options.Events = new JwtBearerEvents
						{
							OnMessageReceived = context =>
							{
								var accessToken = context.Request.Query["access_token"];
								var path = context.HttpContext.Request.Path;
								if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chathub")))
								{
									context.Token = accessToken;
									context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
								}
								return Task.CompletedTask;
							}
						};
					});


			services.AddSignalR();

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins",
					builder => builder.AllowAnyOrigin());
			});

			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseCors(); // Toujours placer avant le 'endpoints'

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "api",
					pattern: "api/{controller}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapHub<ChatHub>("/chathub");
			});
		}
	}
}
