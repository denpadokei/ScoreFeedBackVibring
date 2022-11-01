using IPA.Utilities;
using Libraries.HM.HMLib.VR;
using ScoreFeedBackVibring.Configuration;
using SiraUtil.Affinity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.XR;
using Zenject;

namespace ScoreFeedBackVibring
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class ScoreFeedBackVibringController : IInitializable, IDisposable, IAffinity
    {
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プロパティ
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // コマンド用メソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // オーバーライドメソッド
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // パブリックメソッド
        public void Initialize()
        {
            this._beatmapObjectManager.noteWasCutEvent += this.BeatmapObjectManager_noteWasCutEvent;
            this._normalPreset = this._hapticFeedbackController.GetField<HapticPresetSO, HapticFeedbackController>("_normalPreset");
            var dic = this._hapticFeedbackController.GetField<Dictionary<XRNode, Dictionary<object, HapticFeedbackController.RumbleData>>, HapticFeedbackController>("_rumblesByNode");
            var presetDic = new Dictionary<VibroParam, HapticPresetSO>();
            foreach (var rumbleDataDic in dic.Values) {
                foreach (var item in PluginConfig.Instance.Params) {
                    var priset = new HapticPresetSO()
                    {
                        _duration = item.Duration,
                        _strength = item.Strength,
                        _frequency = item.Frequency,
                        _continuous = false
                    };
                    var rumbleData = new HapticFeedbackController.RumbleData()
                    {
                        active = false,
                        continuous = false,
                        strength = item.Strength,
                        frequency = item.Frequency,
                        endTime = 0
                    };
                    rumbleDataDic.Add(priset, rumbleData);
                    presetDic.Add(item, priset);
                }
            }
            this._presets = new ReadOnlyDictionary<VibroParam, HapticPresetSO>(presetDic);
        }

        [AffinityPatch(typeof(NoteCutHapticEffect), nameof(NoteCutHapticEffect.HitNote), argumentTypes: new Type[] { typeof(SaberType), typeof(NoteCutHapticEffect.Type) })]
        [AffinityPrefix]
        public bool HitNotePrefix(NoteCutHapticEffect.Type type)
        {
            return type != NoteCutHapticEffect.Type.Normal;
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // プライベートメソッド
        private void BeatmapObjectManager_noteWasCutEvent(NoteController noteController, in NoteCutInfo noteCutInfo)
        {
            if (noteController.noteData.time + 0.5f < this._audioTimeSource.songTime) {
                return;
            }
            this.Vibring(noteController.noteData.gameplayType, noteCutInfo);
        }

        private void Vibring(NoteData.GameplayType noteType, in NoteCutInfo noteCutInfo)
        {
            switch (noteType) {
                case NoteData.GameplayType.Normal:
                    var distanceToCenter = noteCutInfo.cutDistanceToCenter;
                    foreach (var priset in PluginConfig.Instance.Params.OrderBy(x => x.MaxDistanceToCenter)) {
                        if (priset.MaxDistanceToCenter < distanceToCenter) {
                            continue;
                        }
                        if (this._presets.TryGetValue(priset, out var hapticPresetSO)) {
                            this._hapticFeedbackController.PlayHapticFeedback(noteCutInfo.saberType.Node(), hapticPresetSO);
                            return;
                        }
                    }
                    break;
                case NoteData.GameplayType.Bomb:
                    this._hapticFeedbackController.PlayHapticFeedback(noteCutInfo.saberType.Node(), this._normalPreset);
                    break;
                case NoteData.GameplayType.BurstSliderHead:
                case NoteData.GameplayType.BurstSliderElement:
                case NoteData.GameplayType.BurstSliderElementFill:
                default:
                    break;
            }
            if (noteType != NoteData.GameplayType.Normal && noteType != NoteData.GameplayType.Bomb) {
                return;
            }
        }
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // メンバ変数
        private HapticFeedbackController _hapticFeedbackController;
        private BeatmapObjectManager _beatmapObjectManager;
        private bool _disposedValue;
        private ReadOnlyDictionary<VibroParam, HapticPresetSO> _presets;
        private HapticPresetSO _normalPreset;
        private IAudioTimeSource _audioTimeSource;
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
        #region // 構築・破棄
        [Inject]
        public void Constractor(HapticFeedbackController hapticFeedbackController, BeatmapObjectManager beatmapObjectManager, IAudioTimeSource audioTimeSource)
        {
            this._hapticFeedbackController = hapticFeedbackController;
            this._beatmapObjectManager = beatmapObjectManager;
            this._audioTimeSource = audioTimeSource;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue) {
                if (disposing) {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    this._beatmapObjectManager.noteWasCutEvent -= this.BeatmapObjectManager_noteWasCutEvent;
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                this._disposedValue = true;
            }
        }
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            this.Dispose(disposing: true);
        }
        #endregion
        // These methods are automatically called by Unity, you should remove any you aren't using.
        #region Monobehaviour Messages
        #endregion
        //ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*ﾟ+｡｡+ﾟ*｡+ﾟ ﾟ+｡*
    }
}
