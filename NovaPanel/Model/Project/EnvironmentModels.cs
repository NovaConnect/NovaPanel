using Newtonsoft.Json;

namespace NovaPanel.Model.Project
{
    public class EnvInstall
    {
        public bool Net { get; set; } = false;
        public bool PHP { get; set; } = false;
        public bool GCC { get; set; } = false;
        public bool Python { get; set; } = false;
        public bool NodeJS { get; set; } = false;
    }

    public class EnvironmentModels
    {

        public static EnvInstall CheckInstall()
        {
            var ist = new EnvInstall();
            var cEnv = ApplicationRuntimes.configModel.EnvironmentPath;
            if (Path.Exists(cEnv.Net))
                ist.Net = true;
            if (Path.Exists(cEnv.PHP))
                ist.PHP = true;
            if (Path.Exists(cEnv.Python))
                ist.Python = true;
            if (Path.Exists(cEnv.GCC))
                ist.GCC = true;
            if (Path.Exists(cEnv.NodeJS))
                ist.NodeJS = true;
            return ist;
        }

    }
}
