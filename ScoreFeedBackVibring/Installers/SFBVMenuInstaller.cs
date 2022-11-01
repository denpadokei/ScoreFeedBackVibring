using ScoreFeedBackVibring.Views;
using Zenject;

namespace ScoreFeedBackVibring.Installers
{
    internal class SFBVMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SettingView>().FromNewComponentAsViewController().AsSingle();
        }
    }
}
