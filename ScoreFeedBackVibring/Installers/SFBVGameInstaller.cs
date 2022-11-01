using Zenject;

namespace ScoreFeedBackVibring.Installers
{
    internal class SFBVGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<ScoreFeedBackVibringController>().AsCached();
        }
    }
}
