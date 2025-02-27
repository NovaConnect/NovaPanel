using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualBasic;
using NovaPanel.Model;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using NovaPanel.Model.ServerMonitorM;

namespace NovaPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApplicationRuntimes.NavItems = JsonConvert.DeserializeObject<NavItem[]>(ReadFile("navs.json"));
            ApplicationRuntimes.VersionInfo = JsonConvert.DeserializeObject<VersionItem>(ReadFile("ver.json"));
            DatabaseModels.CheckAndCreateTables();
            _ = ApplicationRuntimes.State.StartAsync();

            ApplicationRuntimes.configModel = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("config.json"));
            ApplicationRuntimes.Theme = Themes.Parse(ApplicationRuntimes.configModel.Theme);

            LoggerModels.Initialize();

            //var token = AuthModels.Generate("9e8be9f63");
            //Console.WriteLine(token);
            //Console.WriteLine(AuthModels.Auth("MDk5ZThiZTlmNjNQNEdzWkRJcWZiWnhXUlhEZWlMK2xOZ3dhcGZSRzVseEIyNzEyUHBhNjB3PQ[D][D]"));

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSession();
            builder.Services.AddBlazorContextMenu();
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        public static string ReadFile(string file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = $"NovaPanel.Data.{file}";

            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    Console.WriteLine($"not found {file}.");
                    return "";
                }

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}