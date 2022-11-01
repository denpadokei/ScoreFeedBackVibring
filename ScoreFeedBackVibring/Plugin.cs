using IPA;
using IPA.Config;
using IPA.Config.Stores;
using ScoreFeedBackVibring.Installers;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace ScoreFeedBackVibring
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        // TODO: If using Harmony, uncomment and change YourGitHub to the name of your GitHub account, or use the form "com.company.project.product"
        //       You must also add a reference to the Harmony assembly in the Libs folder.
        // public const string HarmonyId = "com.github.YourGitHub.ScoreFeedBackVibring";
        // internal static readonly HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(HarmonyId);

        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public Plugin(IPALogger logger, Config conf, Zenjector zenjector)
        {
            Instance = this;
            Plugin.Log = logger;
            Plugin.Log?.Debug("Logger initialized.");
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Plugin.Log?.Debug("Config loaded");
            zenjector.Install<SFBVMenuInstaller>(Location.Menu);
            zenjector.Install<SFBVGameInstaller>(Location.Player);
        }
    }
}
