# NovaPanel - Windows Server Web 操作パネル

**NovaPanel** は、サーバー管理を簡素化し、運用と保守の効率を向上させることを目的として、Windows Server 専用に設計された強力な Web 運用および保守パネルです。 NovaPanel は、シミュレートされたタスク マネージャー、リソース マネージャー、および柔軟にスケジュールされたタスクを通じて、直感的で効率的なサーバー管理プラットフォームを提供します。

## 主な特徴

* **Windows Server 専用:** Windows Server 向けに最適化されており、安定した効率的な O&M エクスペリエンスを提供します。
* **シミュレーション タスク マネージャーとリソース マネージャー:** 直感的なインターフェイス、サーバー リソースの使用状況のリアルタイム監視、プロセスとサービスの簡単な管理。
* **柔軟なスケジュールされたタスク:** さまざまな自動化ニーズを満たすために、Python、JavaScript、C#、PowerShell スクリプト、Bat バッチ処理をサポートします。
* **多言語サポート:** 世界中のユーザーの利便性のために、多言語インターフェースを提供します。
* **カスタム プロジェクトのサポート:** 簡単かつ迅速な展開と管理のために、新しい C#、Web サイト、その他のプロジェクトの作成をサポートします。
* **柔軟なユーザー権限管理:** サーバーのセキュリティを確保するための詳細なユーザー権限制御。
* **安全なログイン メカニズム:** 安全な入場 + トークン メカニズムを採用して、ログイン プロセスのセキュリティを確保します。
* **ドメイン バインドの制限:** バインドされたドメイン名へのアクセスのみを有効にして、サーバーのセキュリティを強化できます。
* **SQL インジェクション対策:** 悪意のある攻撃を効果的に防ぐための組み込みの SQL インジェクション対策メカニズム。

## 機能のハイライト

* **直感的な監視:** CPU、メモリ、ディスク、ネットワークの使用状況をリアルタイムで表示し、パフォーマンスのボトルネックをすばやく特定します。
* **強力な自動化:** スケジュールされたタスクを通じて自動化された操作とメンテナンスを実現し、作業効率を向上できます。
* **便利なプロジェクト管理:** さまざまなプロジェクトを簡単に作成、展開、管理して、開発プロセスを簡素化します。
* **包括的なセキュリティ保護:** サーバーおよびデータのセキュリティを確保するための複数のセキュリティ対策。
* **テーマのカスタマイズ:**
 * `wwwwroot\style\app.css` を変更して、一部のコンポーネントのスタイルを変更します。
 * デフォルトのテーマ設定は、`config.json` ファイルを変更することで変更できます。
 * ブラウザコンソールを使用して、`document.body.classList.toggle("theme-toggle")` コマンドを実行することで、テーマを一時的に切り替えることができます。


## クイックスタート

1. **.NET SDK をインストールします:**
 * .NET SDK 8.0 または 9.0 以降をインストールします。
 * `NovaPanel/NovaPanel.csproj` ファイルで .NET SDK バージョンを変更できます。
2. **ソースコードを複製する:**
 * Git を使用して NovaPanel ソース コードをクローンします。
 「バッシュ」
 git クローン https://github.com/NovaConnect/NovaPanel.git
 「」
3. **プロジェクトディレクトリを入力します:**
 * ターミナルを開き、NovaPanel プロジェクト ディレクトリに移動します。
 「バッシュ」
 cd NovaPanel/NovaPanel
 「」
4. **ソフトウェアを実行します:**
 * .NET CLI を使用して NovaPanel を実行します。
 「バッシュ」
 ドットネット実行
 「」

＃＃ 説明書

* **ダッシュボード:** サーバーのリソース使用状況とシステム情報を表示します。
* **タスク マネージャー:** プロセスとサービスを管理し、プロセスを終了または再起動します。
* **エクスプローラー:** ファイルとフォルダーを管理し、ファイルをアップロード、ダウンロード、削除します。
* **スケジュールされたタスク:** スケジュールされたタスクを作成および管理して、自動化された操作とメンテナンスを実現します。
* **プロジェクト管理:** プロジェクトを作成および管理し、Web サイトとアプリケーションを展開します。
* **ユーザー管理:** ユーザーと権限を管理し、役割を割り当てます。
* **セキュリティ設定:** セキュリティ オプションを構成し、ドメイン名のバインドとセキュリティ エントリを設定します。
* **テーマ設定:** `config.json` またはブラウザコンソールからテーマを変更します。

＃＃ 貢献する

貢献、提案、または問題報告を歓迎します。 GitHub Issues または Pull Requests を通じてプロジェクトに貢献してください。

＃＃ 接触

ご質問やご提案がございましたら、以下の方法でお問い合わせください。

* 仕事用メールアドレス: [Sm4Z0n3Mua@gmail.com]
* GitHub: [NovaConnect コミュニティ](https://github.com/NovaConnect)

## 謝辞
* **サードパーティライブラリのサポート:**
 * 右クリック メニュー機能を提供するには、`Blazor.ContextMenu` を使用します。
 * 美しくモダンなユーザー インターフェイスを提供するには、`Shadcn/UI` を使用します。
 * このプロジェクトに貢献してくれたすべての開発者とユーザーに感謝します。
