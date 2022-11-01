using ScoreFeedBackVibring.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
