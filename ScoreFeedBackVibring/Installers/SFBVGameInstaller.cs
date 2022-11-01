using ScoreFeedBackVibring.Configuration;
using Zenject;

namespace ScoreFeedBackVibring.Installers
{
    internal class SFBVGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            if (!PluginConfig.Instance.Enable) {
                return;
            }
            this.Container.BindInterfacesAndSelfTo<ScoreFeedBackVibringController>().AsCached();
        }
    }
}
