# ScoreFeedBackVibring

[![Build and Release](https://github.com/denpadokei/ScoreFeedBackVibring/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/ScoreFeedBackVibring/actions/workflows/build-and-release.yml)

ノーツを切った位置の正確さに応じて、コントローラーの振動を変える MOD です。切り口の中心に近いほど強い振動を返します。

## 機能

- ゲーム標準のノーツカット振動 (`NoteCutHapticEffect.HitNote`) を SiraUtil Affinity の prefix で抑制し、独自の振動に置き換える
- 切り口中心からの距離 (`cutDistanceToCenter`) を、設定したプリセットの `MaxDistanceToCenter` と小さい順に比較し、最初に一致した振動を再生する
- 既定では 3 段階のプリセット (0.02 / 0.08 / 10) が用意されている
- 爆弾を切った場合はゲーム標準の振動プリセットを使用する
- ノーツの判定時刻から 0.5 秒以上遅れたカットは対象外にする
- 設定で機能全体の ON/OFF を切り替えられる

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.25.1 |
| 依存 MOD | BSIPA ^4.2.0 / SiraUtil ^3.1.2 / BeatSaberMarkupLanguage ^1.6.8 |

## インストール

1. Beat Saber を 1.25.1 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/ScoreFeedBackVibring/releases) から `ScoreFeedBackVibring-1.0.0-bs1.25.1.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

インストール後は自動で有効になります。ノーツを切ると、切り口の中心に近いほど強い振動が返ります。振動の強さや距離の閾値を変えたい場合は、設定ファイルの `Params` を編集してください。

## 設定

ゲーム内の `設定` から `ScoreFeedBackVibring` を開きます。

| 項目 | 型 | 既定値 | 説明 |
| --- | --- | --- | --- |
| Enable | bool | true | カット位置に応じた振動を有効にする |
| Params[].MaxDistanceToCenter | float | 0.02 / 0.08 / 10 | 切り口中心からの距離の閾値。小さい順に評価され、距離がこの値以下ならそのプリセットが使われる |
| Params[].Duration | float | 0.13 | 振動の長さ (秒) |
| Params[].Strength | float | 1 / 0.7 / 0.3 | 振動の強さ |
| Params[].Frequency | float | 0.5 | 振動の周波数 |

`Params` は 3 件のプリセットの配列で、ゲーム内 UI からは `Enable` のみ変更できます。プリセットを変える場合は `UserData/ScoreFeedBackVibring.json` を直接編集してください。`Enable` の変更はプレイヤー用の installer が読み込み時に評価するため、反映にはゲームの再起動が必要です。

## ビルド

```powershell
git clone https://github.com/denpadokei/ScoreFeedBackVibring.git
cd ScoreFeedBackVibring
dotnet build .\ScoreFeedBackVibring\ScoreFeedBackVibring.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT
