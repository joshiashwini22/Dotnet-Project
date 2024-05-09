using BisleriumProject.Application.Common.Interface.IRepositories;
using BisleriumProject.Application.Common.Interface.IServices;
using BisleriumProject.Domain.Auth;
using BisleriumProject.Domain.Entities;
using BisleriumProject.Infrastructures.Persistence;
using BisleriumProject.Infrastructures.Repositories;
using BisleriumProject.Infrastructures.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumProject.Infrastructures.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("dev"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)), ServiceLifetime.Transient);
            

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));


            services.AddDbContext<AppDbContext>();


            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IBlogVoteRepository, BlogVoteRepository>();
            services.AddTransient<IBlogLogsheetRepository, BlogLogsheetRepository>();
            services.AddTransient<ICommentVoteRepository, CommentVoteRepository>();
            services.AddTransient<ICommentLogsheetRepository, CommentLogsheetRepository>();

            //services.AddTransient<IAuthenticationService, AuthenticationService>();


            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IBlogVoteService, BlogVoteService>();
            services.AddTransient<IBlogLogsheetService, BlogLogsheetService>();
            services.AddTransient<ICommentVoteService, CommentVoteService>();
            services.AddTransient<ICommentLogsheetService, CommentLogsheetService>();
            services.AddTransient<IAdminDashboardService, AdminDashboardService>();

            services.AddSingleton<EmailConfiguration>();

            return services;
        }
    }
}
