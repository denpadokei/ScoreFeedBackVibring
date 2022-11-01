using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ScoreFeedBackVibring.Configuration
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        [UseConverter(typeof(ListConverter<VibroParam>))]
        [NonNullable]
        public virtual List<VibroParam> Params { get; set; } = new List<VibroParam>();

        public PluginConfig()
        {
            var list = new List<VibroParam>()
            {
                new VibroParam()
                {
                    MaxDistanceToCenter = 0.01f,
                    Duration = 0.05f,
                    Strength = 1f,
                    Frequency = 0.5f
                },
                new VibroParam()
                {
                    MaxDistanceToCenter = 0.08f,
                    Duration = 0.05f,
                    Strength = 0.7f,
                    Frequency = 0.3f
                },
                new VibroParam()
                {
                    MaxDistanceToCenter = 10f,
                    Duration = 0.05f,
                    Strength = 0.1f,
                    Frequency = 0.2f
                }
            };
            this.Params.AddRange(list);
        }

        /// <summary>
        /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
        /// </summary>
        public virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}
