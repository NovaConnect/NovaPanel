using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualBasic;
using NovaPanel.Model;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using NovaPanel.Model.ServerMonitorM;
using System.Timers;
using Timer = System.Timers.Timer;
using NovaPanel.Model.Project;
using System.Threading.Tasks;

namespace NovaPanel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseModels.CheckAndCreateTables();
            _ = ApplicationRuntimes.State.StartAsync();
            ApplicationRuntimes.LoadConfig();
            LoggerModels.Initialize();
            var tasks = DatabaseModels.GetAllScheduleTasks();
            foreach (var task in tasks)
            {
                Task.Run(() =>
                {
                    var timer = new Timer(task.Minute * 60000);
                    timer.Elapsed += (sender, e) =>
                    {
                        var time = (sender as Timer);
                        if (task.Enable == true)
                        {
                            RunCodeModels.Run(task);
                            LoggerModels.AddToLog($"运行任务[{task.Name}],类型[{task.Type}],周期[{task.Minute}min]", InfoLevel.Normal);
                        }
                        else
                        {
                            time.Stop();
                            return;
                        }
                    };
                    timer.Start();
                });
            }
            Console.WriteLine($"导入{tasks.Count}个计划任务.");

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