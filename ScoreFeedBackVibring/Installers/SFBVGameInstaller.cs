using Zenject;

namespace ScoreFeedBackVibring.Installers
{
    internal class SFBVGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreFeedBackVibringController>().AsCached();
        }
    }
}
