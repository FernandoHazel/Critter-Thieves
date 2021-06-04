Silent's Cel Shading ShaderはVRchatおよびUnity用のシェーダーです。最新技術を駆使したトゥーンレンダリングを提供し、多彩な機能で多様なスタイルを表現できます。Unityのあらゆるライティングに対応しています。

ワールドのライティングを最も正確に描写することを目標としています。

※注： 画像のアバターはデモ用です。モデルは含まれていません。

機能一覧

▢ ライティング
トゥーンレンダリングの基本的要素であるライト階調は、トーンマッピングシステムが影をコントロールすることでUnityのライティングに対して自然に調和します。リアルタイムの影であっても違和感はありません。「オート」トーンマッピングアルゴリズムは、ギラついたライティングのもとでも鮮やかな色の影を提供します。上級者向けになりますが、トーンマップテクスチャでシェーディングの色を直接操作できます。本当の意味でアニメ調のマテリアルコントロールが可能です。

▢ アウトライン
アウトラインはVR向けに最適化されています。カメラの距離に応じてサイズを縮小し、近接距離でモデルがおかしくなるのを回避します。さらに、頂点の色成分を用いてアウトラインを細かく調整できます。

▢ NPR
SCSSには独自のマットキャップシステムが搭載されています。複数のマットキャップをそれぞれのブレンドモードで組み合わせることができます。ワールドまたは接面空間に固定し、VRの頭の動きに合わせて変化してしまうのを防ぐことができます。
調整可能なリムライトがいくつかのアプリケーションモードで用意されています。
トゥーン調スペキュラーではハイライトを調整できるようになっています。

▢ PBR 
Unityのスタンダードシェーダーと同じようなメタルネス機能とスムースネス機能を搭載しています。トゥーンレンダリングのマテリアルに、リアルな金属感や光沢感を組み合わせることができます。
近接時にマテリアルのテクスチャを高品質のものにできるディテールマップもサポートしています。

▢ 高度なオプション
ブレンドモードやステンシル等のための多くの高度なオプションがあり、周囲の環境に自然に溶け込むような光沢感のある透明オブジェクトのための Premultiplied Transparency も使えます。

▢ 詳細情報
シェーダーの詳細はマニュアルをご覧ください！
https://gitlab.com/s-ilent/SCSS/wikis/Manual/Setting-Overview

開発について

Boothのバージョンは、現在のバージョン1.7のスナップショットです。以下のURLで開発を進めています。

https://gitlab.com/s-ilent/SCSS

本シェーダはMITライセンスのもとで配布されています。気兼ねなく改造してください。コントリビュートも歓迎します。

---

Silent's Cel Shading Shader is a shader designed for use in VRchat and Unity. It provides anime cel shading based on state of the art techniques. Many additional features are included, which allow many different styles to be portrayed. All forms of lighting within Unity are properly supported.

Accurately portraying the lighting of worlds in the best way is our goal. 

* Note: The demonstration avatars in the images are for reference only. No models are included.

Feature List

▢ Customisable lighting 
The traditional building blocks of cel shading, light ramps, are integrated seamlessly into Unity lighting through a tone mapping system that controls the shadows. Even realtime shadows look correct! 
A special "Auto" tone mapping algorithm provides vibrant shade colours, even in harsh lighting.
For advanced users, tone map textures can be used to directly control the shading colour, providing true anime-style material control.

▢ Outlines and control
The outline system is optimised for VR, with outlines that reduce size based on camera proximity to avoid models breaking up at close inspection.
In addition, outlines can be finely controlled using the vertex colour channels. 

▢ NPR
SCSS contains a unique matcap system. You can combine multiple matcaps with individual blend modes. They can be anchored in world or tangent space, stopping them from shifting with head movement in VR. 
Customisable rim lights with several unique application modes are available.
Cel-style specular is included that features an adjustable highlight. 

▢ PBR 
Contains metalness and smoothness functionality accurate to Unity's Standard shader. You can combine a cel-shaded material with realistic metal and gloss.
Detail maps are supported, allowing you to give materials a realistic fine texture close-up.

▢ Advanced Options
Many advanced options for blend mode, stencil, and more. Includes support for using premultiplied transparency, which allows for glossy transparent objects that naturally fit into their surroundings.

▢ More info
For more details on the shader's functionality, check the manual!
https://gitlab.com/s-ilent/SCSS/wikis/Manual/Setting-Overview

Development

This Booth version is a fixed snapshot of the current version, 1.9. Development versions are at the following URL.

https://gitlab.com/s-ilent/SCSS

This shader is under MIT license. Please feel free to edit it as you see fit. Contributions are welcome too.