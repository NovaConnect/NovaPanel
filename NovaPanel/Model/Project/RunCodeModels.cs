using IronPython.Hosting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using NovaPanel.Model;
namespace NovaPanel.Model.Project
{
    public class RunCodeModels
    {
        public static async void Run(NovaPanel.Model.ScheduleTask task)
        {
            var eIns = EnvironmentModels.CheckInstall();
            switch (task.Type)
            {
                case "CS":
                    if (eIns.Net)
                    {
                        try
                        {
                            var imports = JsonConvert.DeserializeObject<string[]>(Program.ReadFile("import.json"));
                            var options = ScriptOptions.Default.WithImports(imports);
                            var script = CSharpScript.Create(task.Code, options);

                            await script.RunAsync();
                        }catch(Exception ex)
                        {
                            LoggerModels.AddToLog($"C#脚本执行错误:{ex.Message}", InfoLevel.Error);
                        }
                    }
                    break;

                case "PY":
                    if (eIns.Python)
                    {
                        try
                        {
                            var engine = Python.CreateEngine();
                            var scope = engine.CreateScope();

                            engine.Execute(task.Code, scope);
                        }
                        catch (Exception ex)
                        {
                            LoggerModels.AddToLog($"Python脚本执行错误:{ex.Message}", InfoLevel.Error);
                        }
                    }
                    break;


                case "PS":

                    break;

                case "BAT":

                    break;

                default:
                    break;
            }
        }
    }
}
