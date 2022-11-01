using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using ScoreFeedBackVibring.Configuration;
using Zenject;

namespace ScoreFeedBackVibring.Views
{
    [HotReload]
    internal class SettingView : BSMLAutomaticViewController, IInitializable
    {
        [UIValue("enable")]
        public bool Enable
        {
            get => PluginConfig.Instance.Enable;
            set => PluginConfig.Instance.Enable = value;
        }

        // For this method of setting the ResourceName, this class must be the first class in the file.
        public string ResourceName => string.Join(".", this.GetType().Namespace, this.GetType().Name);

        public void Initialize()
        {
            BSMLSettings.instance.AddSettingsMenu("<size=80%>ScoreFeedBackVibring</size>", this.ResourceName, this);
        }
    }
}
