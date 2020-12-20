using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetOwner.Data;
using PetOwner.Helpers;
using PetOwner.Repository.Implementations;
using PetOwner.Repository.Interfaces;
using PetOwner.Services.Implementations;
using PetOwner.Services.Interfaces;

namespace PetOwner
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
			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;

			services.AddDbContext<PetOwnerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddTransient<IAchievementRepository, AchievementRepository>();
			services.AddTransient<IActivityRepository, ActivityRepository>();
			services.AddTransient<IGamificationRepository, GamificationRepository>();
			services.AddTransient<IGroupRepository, GroupRepository>();
			services.AddTransient<IItemRepository, ItemRepository>();
			services.AddTransient<IPetActivityRepository, PetActivityRepository>();
			services.AddTransient<IPetRepository, PetRepository>();
			services.AddTransient<ITipRepository, TipRepository>();
			services.AddTransient<IUserAchievementRepository, UserAchievementRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IVipRepository, VipRepository>();

			services.AddScoped<IRegisterService, RegisterService>();
			services.AddScoped<IGroupService, GroupService>();
			services.AddScoped<ILoginService, LoginService>();

			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseMiddleware<JWTMiddleware>();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
