﻿using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// アセンブリに関する一般情報は、以下の属性セットを通して管理されます。 
// アセンブリに関連付けられている情報を変更するには、これらの属性値を
// 変更してください。
[assembly: AssemblyTitle("TusWpfSplashLibrary")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("TusWpfSplashLibrary")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で
// COM コンポーネントでは見えなくなります。COM からこのアセンブリ内の型にアクセスする必要がある場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID がタイプ ライブラリの ID になります。
[assembly: Guid("c6bffb53-a09b-4e72-9f70-71a7d96142ce")]

//ローカライズ可能なアプリケーションのビルドを開始するには、
//.csproj ファイルの <UICulture>CultureYouAreCodingWith</UICulture> を
//<PropertyGroup> 内に設定します。たとえば、ソース ファイルで米国英語を
//使用している場合は、<UICulture> を en-US に設定します。次に、下の
//NeutralResourceLanguage 属性のコメントを解除し、下の行の "en-US" を
//プロジェクト ファイルのUICulture 設定と一致するよう更新します。

//[assembly: NeutralResourcesLanguage("en-US" , UltimateResourceFallbackLocation.MainAssembly)]

[assembly:ThemeInfo(
    ResourceDictionaryLocation.None, //テーマ固有のリソース ディクショナリが置かれている場所
                             //(リソースがページ、
                             // またはアプリケーション リソース ディクショナリに見つからない場合に使用されます)
    ResourceDictionaryLocation.SourceAssembly //汎用リソース ディクショナリが置かれている場所
                             //(リソースがページ、
                             // アプリケーション、またはいずれのテーマ固有リソース ディクショナリにも見つからない場合に使用されます)
)]

// アセンブリのバージョン情報は、以下の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン 
//      ビルド番号
//      リビジョン
//
// すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を
// 規定値にすることができます。
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]