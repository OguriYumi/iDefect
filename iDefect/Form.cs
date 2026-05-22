using System.Configuration;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace iDefect
{
    public partial class Form : System.Windows.Forms.Form
    {
        const string defaultTextCSV = "CSVファイルを選択してください";
        const string defaultTextShipping = "出荷ファイル名を指定してください";
        const short rowCnt = 5; // ヘッダ行数

        /// <summary>
        /// フォームのコンストラクタ
        /// </summary>
        public Form()
        {
            InitializeComponent();

            // 初期セット
            txtCsvFile1.Text = defaultTextCSV;
            txtCsvFile1.ForeColor = SystemColors.GrayText; // 初期値は薄い色にする
            txtCsvFile2.Text = defaultTextCSV;
            txtCsvFile2.ForeColor = SystemColors.GrayText; // 初期値は薄い色にする
            txtShippingFileName.Text = defaultTextShipping;
            txtShippingFileName.ForeColor = SystemColors.GrayText; // 初期値は薄い色にする

            // フォームを閉じるときの確認ダイアログ
            this.FormClosing += Form_FormClosing;

            string defaultChk = "未設定";
            // 設定ファイル（app.config）から各テキストボックスの初期値を読み込む
            try
            {
                string? readApp(string key)
                {
                    var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
                    if (exePath == null) return null;
                    var config = ConfigurationManager.OpenExeConfiguration(exePath);
                    return config.AppSettings.Settings[key]?.Value;
                }

                string? v;

                v = readApp("Xshift1"); if (!string.IsNullOrWhiteSpace(v)) txtXshift1.Text = v;
                v = readApp("Xshift2"); if (!string.IsNullOrWhiteSpace(v)) txtXshift2.Text = v;
                v = readApp("Xoverlap"); if (!string.IsNullOrWhiteSpace(v)) txtXoverlap.Text = v;
                v = readApp("Yoverlap"); if (!string.IsNullOrWhiteSpace(v)) txtYoverlap.Text = v;
                v = readApp("Start2DC"); if (!string.IsNullOrWhiteSpace(v)) txt2DCstart.Text = v;
                v = readApp("End2DC"); if (!string.IsNullOrWhiteSpace(v)) txt2DCend.Text = v;
                v = readApp("XlimitStart"); if (!string.IsNullOrWhiteSpace(v)) txtXlimitStart.Text = v;
                v = readApp("XlimitEnd"); if (!string.IsNullOrWhiteSpace(v)) txtXlimitEnd.Text = v;
                v = readApp("YlimitStart"); if (!string.IsNullOrWhiteSpace(v)) txtYlimitStart.Text = v;
                v = readApp("YlimitEnd"); if (!string.IsNullOrWhiteSpace(v)) txtYlimitEnd.Text = v;

                v = readApp("DefectKind1"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind1.Text = v; chkDefectKind1.Enabled = true; } else { chkDefectKind1.Text = defaultChk; chkDefectKind1.Enabled = false; }
                v = readApp("DefectKind2"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind2.Text = v; chkDefectKind2.Enabled = true; } else { chkDefectKind2.Text = defaultChk; chkDefectKind2.Enabled = false; }
                v = readApp("DefectKind3"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind3.Text = v; chkDefectKind3.Enabled = true; } else { chkDefectKind3.Text = defaultChk; chkDefectKind3.Enabled = false; }
                v = readApp("DefectKind4"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind4.Text = v; chkDefectKind4.Enabled = true; } else { chkDefectKind4.Text = defaultChk; chkDefectKind4.Enabled = false; }
                v = readApp("DefectKind5"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind5.Text = v; chkDefectKind5.Enabled = true; } else { chkDefectKind5.Text = defaultChk; chkDefectKind5.Enabled = false; }
                v = readApp("DefectKind6"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind6.Text = v; chkDefectKind6.Enabled = true; } else { chkDefectKind6.Text = defaultChk; chkDefectKind6.Enabled = false; }
                v = readApp("DefectKind7"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind7.Text = v; chkDefectKind7.Enabled = true; } else { chkDefectKind7.Text = defaultChk; chkDefectKind7.Enabled = false; }
                v = readApp("DefectKind8"); if (!string.IsNullOrWhiteSpace(v)) { chkDefectKind8.Text = v; chkDefectKind8.Enabled = true; } else { chkDefectKind8.Text = defaultChk; chkDefectKind8.Enabled = false; }

                v = readApp("AscOrder"); if (!string.IsNullOrWhiteSpace(v) && bool.TryParse(v, out bool asc)) { optAsc.Checked = asc; optDesc.Checked = !asc; }

                v = readApp("ShippingFile"); if (!string.IsNullOrWhiteSpace(v)) { txtShippingFileName.Text = v; txtShippingFileName.ForeColor = SystemColors.WindowText; }
            }
            catch
            {
                // 読み込み失敗は無視してデザイナの初期値を使う
            }
        }

        /// <summary>
        /// 設定ファイル（config）へ保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SaveAppSetting(string key, string value)
        {
            try
            {
                // 実行中のEXEのパスを取得して明示的に指定
                var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
                if (exePath == null) return;
                var config = ConfigurationManager.OpenExeConfiguration(exePath);
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value;
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value);
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch
            {
                // 保存失敗は無視
            }
        }

        /// <summary>
        /// 終了ボタン（右上の×）を非表示にする。クラススタイルにCS_NOCLOSEを追加すると、閉じるボタンが表示されなくなる
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }

        /// <summary>
        /// 閉じるボタンがクリックされたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// フォーム閉じるイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_FormClosing(object? sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("アプリケーションを終了します。" + Environment.NewLine + "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 工程1のCSVファイル選択テキストボックスにフォーカスが入ったときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>フォーカスインで初期テキストを消す</remarks>
        private void txtCsvFile1_Enter(object sender, EventArgs e)
        {
            if (txtCsvFile1.Text == defaultTextCSV)
            {
                txtCsvFile1.Text = "";
                txtCsvFile1.ForeColor = SystemColors.WindowText; // 入力時は黒い色にする
            }
        }

        /// <summary>
        /// 工程1のCSVファイル選択テキストボックスからフォーカスが外れた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>フォーカスアウトで空欄なら初期テキストをセット</remarks>
        private void txtCsvFile1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCsvFile1.Text))
            {
                txtCsvFile1.Text = defaultTextCSV;
                txtCsvFile1.ForeColor = SystemColors.GrayText; // 初期値に戻したら薄い色にする
            }
            else
            {
                txtCsvFile1.ForeColor = SystemColors.WindowText; // デフォルト色
            }
        }

        /// <summary>
        /// 工程2のCSVファイル選択テキストボックスにフォーカスが入ったときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>フォーカスインで初期テキストを消す</remarks>
        private void txtCsvFile2_Enter(object sender, EventArgs e)
        {
            if (txtCsvFile2.Text == defaultTextCSV)
            {
                txtCsvFile2.Text = "";
                txtCsvFile2.ForeColor = SystemColors.WindowText; // 入力時は黒い色にする
            }
        }

        /// <summary>
        /// 工程2のCSVファイル選択テキストボックスからフォーカスが外れた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>フォーカスアウトで空欄なら初期テキストをセット</remarks>
        private void txtCsvFile2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCsvFile2.Text))
            {
                txtCsvFile2.Text = defaultTextCSV;
                txtCsvFile2.ForeColor = SystemColors.GrayText; // 初期値に戻したら薄い色にする
            }
            else
            {
                txtCsvFile2.ForeColor = SystemColors.WindowText; // デフォルト色
            }
        }

        /// <summary>
        /// 工程1のCSVファイル選択ボタンがクリックされたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect1_Click(object sender, EventArgs e)
        {
            SelectCsvFile(txtCsvFile1);
        }

        /// <summary>
        /// 工程2のCSVファイル選択ボタンがクリックされたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect2_Click(object sender, EventArgs e)
        {
            SelectCsvFile(txtCsvFile2);
        }

        /// <summary>
        /// 指定したテキストボックスにCSVファイルパスをセットする共通処理
        /// </summary>
        /// <param name="targetTextBox">ファイルパスを設定するテキストボックス</param>
        private void SelectCsvFile(System.Windows.Forms.TextBox targetTextBox)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // ダイアログのタイトルを設定
                ofd.Title = "ファイルを選択してください";
                // 初期フォルダを設定
                ofd.InitialDirectory = @"C:\";
                // 選択可能なファイル形式を設定
                ofd.Filter = "CSVファイル(*.csv)|*.csv";
                // 複数ファイル選択を不可（単一選択）
                ofd.Multiselect = false;

                // 2. ダイアログを表示し、[開く]が押されたか確認
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // 3. 選択されたファイルのパスを取得
                    targetTextBox.Text = ofd.FileName;
                    targetTextBox.ForeColor = SystemColors.WindowText; // デフォルト色
                }
            }
        }

        /// <summary>
        /// 出荷ファイル名テキストボックスにフォーカスが入ったときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>フォーカスインで初期テキストを消す</remarks>
        private void txtShippingFileName_Enter(object sender, EventArgs e)
        {
            if (txtShippingFileName.Text == defaultTextShipping)
            {
                txtShippingFileName.Text = "";
                txtShippingFileName.ForeColor = SystemColors.WindowText; // 入力時は黒い色にする
            }
        }

        /// <summary>
        /// 出荷ファイル名テキストボックスからフォーカスが外れた時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>フォーカスアウトで空欄なら初期テキストをセット</remarks>
        private void txtShippingFileName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtShippingFileName.Text))
            {
                txtShippingFileName.Text = defaultTextShipping;
                txtShippingFileName.ForeColor = SystemColors.GrayText; // 初期値に戻したら薄い色にする
            }
            else
            {
                txtShippingFileName.ForeColor = SystemColors.WindowText; // デフォルト色
            }
        }

        /// <summary>
        /// ラジオボタンのチェック状態が変更されたときに呼び出されるイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (optAsc.Checked)
            {
                // ラジオボタン昇順が選択された時の処理
                optDesc.Checked = false;
            }
            else if (optDesc.Checked)
            {
                // ラジオボタン降順が選択された時の処理
                optAsc.Checked = false;
            }
        }

        /// <summary>
        /// 実行ボタンがクリックされたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            //--------------------------------
            // [事前処理1]
            // テキストボックス入力値チェック
            //--------------------------------
            if (!Chk入力チェック()){ return; }

            //----------------------------------
            // [事前処理2-1]
            // 工程1のCSVファイルの内容チェック
            // （ヘッダ：5行、列数：5列以上存在する想定）
            //----------------------------------
            if (!ChkCsv明細チェック(txtCsvFile1, "工程1")){ return; }

            //----------------------------------
            // [事前処理2-2]
            // 工程2のCSVファイルの内容チェック
            // （ヘッダ：5行、列数：5列以上存在する想定）
            //----------------------------------
            if (!ChkCsv明細チェック(txtCsvFile2, "工程2")) { return; }


            // ファイル保存先指定ダイアログ
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                // ダイアログタイトル
                sfd.Title = "保存先ファイルを指定してください";
                // 初期フォルダを設定（設定値があればそれを使う）
                string initialDir = @"C:\DNP";
                var appDefaultOutput = ConfigurationManager.AppSettings["DefaultOutputFolder"];
                if (!string.IsNullOrWhiteSpace(appDefaultOutput))
                {
                    initialDir = appDefaultOutput;
                }
                sfd.InitialDirectory = initialDir;
                // 初期ファイル名を設定（テキストがデフォルトプレースホルダなら既定名を使う）
                string initialFileName = "filename";
                if (!string.IsNullOrWhiteSpace(txtShippingFileName.Text) && txtShippingFileName.Text != defaultTextShipping)
                {
                    initialFileName = txtShippingFileName.Text;
                }
                else
                {
                    var appShipping = ConfigurationManager.AppSettings["ShippingFile"];
                    if (!string.IsNullOrWhiteSpace(appShipping)) initialFileName = appShipping;
                }
                sfd.FileName = initialFileName;
                sfd.Filter = "CSVファイル(*.csv)|*.csv";
                sfd.DefaultExt = "csv";
                sfd.AddExtension = true;
                sfd.OverwritePrompt = true;
                const string sExtension = ".csv";
                const string s設定パラメータ = "_para";
                const string sXシフト加工1 = "_shift1";
                const string sXシフト加工2 = "_shift2";
                const string s単純マージ = "_merge";
                const string sY座標ソート = "_sort";
                const string s除去加工 = "_exclusion";
                const string s重複処理 = "_overlap";
                const string s抽出加工 = "_extraction";
                const string sY座標昇降順 = "_ysort";
                const string sY座標変換 = "_coordinate";
                const string s出荷前 = "_previous";

                // ファイル保存先指定ダイアログ表示（キャンセルされたら処理を抜ける）
                if (sfd.ShowDialog() == DialogResult.Cancel){ return; }

                // 出荷データ保存先ファイルのパス取得
                string filePath = sfd.FileName;
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath) && IsFileLocked(filePath))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // 選択されたファイルの拡張子をチェック
                if (Path.GetExtension(filePath).ToLower() != ".csv")
                {
                    // 強制的に .csv に置き換える
                    filePath = Path.ChangeExtension(filePath, ".csv");
                }

                //-----------------------------------
                // [既存ファイルチェック]
                // 指定ファイル名から既存ファイルがあるかチェックし、存在する場合は上書き確認ダイアログを表示する
                //-----------------------------------
                // ディレクトリ、拡張子なしファイル名を抽出
                string directory = Path.GetDirectoryName(filePath) ?? "";
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                // 新しいフォルダパスを生成
                string newFolderPath = Path.Combine(directory, fileNameWithoutExt);
                // フォルダが存在する場合、フォルダ内にこれから生成するファイル名と一致するものがないかチェックする
                if (!string.IsNullOrEmpty(newFolderPath))
                {
                    // 重複ファイルの存在チェック
                    bool hasDuplicate = false;
                    List<string> lstFilename = [ fileNameWithoutExt + s設定パラメータ + sExtension,
                                                 fileNameWithoutExt + sXシフト加工1 + sExtension,
                                                 fileNameWithoutExt + sXシフト加工2 + sExtension,
                                                 fileNameWithoutExt + s単純マージ + sExtension,
                                                 fileNameWithoutExt + sY座標ソート + sExtension,
                                                 fileNameWithoutExt + s除去加工 + sExtension,
                                                 fileNameWithoutExt + s重複処理 + sExtension,
                                                 fileNameWithoutExt + s抽出加工 + sExtension,
                                                 fileNameWithoutExt + sY座標昇降順 + sExtension,
                                                 fileNameWithoutExt + sY座標変換 + sExtension];
                    foreach (string fileName in lstFilename)
                    {
                        // フォルダパスとリストのファイル名を結合
                        string checkPath = Path.Combine(newFolderPath, fileName);
                        // 1つでもファイルが存在すればフラグを立ててループを抜ける
                        if (File.Exists(checkPath))
                        {
                            hasDuplicate = true;
                            break;
                        }
                    }
                    // 重複がある場合のみ確認ダイアログを表示
                    if (hasDuplicate)
                    {
                        DialogResult result = MessageBox.Show("指定フォルダ内に同名のファイルが既に存在します。上書きしますか？\n（フォルダパス：" + newFolderPath + "）", "確認",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                        // 「いいえ」が押された場合は処理を抜ける
                        if (result == DialogResult.No){ return; }
                    }
                }


                //-----------------------------------
                // [設定パラメータ保存]
                // 画面入力値をCSVファイルに出力する
                //-----------------------------------
                try
                {
                    // 設定パラメータ保存先ファイルのパス生成
                    string filePath_para = AppendToFileName(filePath, s設定パラメータ);
                    // ファイルが既に存在し、かつロックされているかチェック
                    if (File.Exists(filePath_para) && IsFileLocked(filePath_para))
                    {
                        MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                        "\nファイルを閉じてから再度お試しください。" +
                                        "\n（ファイルパス：" + filePath_para + "）",
                                        "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    // Shift-JIS (Excelで文字化けしないように指定)
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Encoding sjis = Encoding.GetEncoding("shift_jis");
                    using (StreamWriter sw = new StreamWriter(filePath_para, false, sjis))
                    {
                        // ヘッダー行
                        sw.WriteLine("項目名,設定値,単位/補足");
                        // 各コントロールの値を書き込み
                        WriteRow(sw, "工程1ファイルパス", txtCsvFile1.Text);
                        WriteRow(sw, "工程2ファイルパス", txtCsvFile2.Text);

                        WriteRow(sw, "Xシフト量1", txtXshift1.Text, "mm");
                        WriteRow(sw, "Xシフト量2", txtXshift2.Text, "mm");
                        WriteRow(sw, "X距離", txtXoverlap.Text, "mm");
                        WriteRow(sw, "Y距離", txtYoverlap.Text, "mm");

                        // チェックボックス (True / False で保存)
                        WriteRow(sw, chkDefectKind1.Text, chkDefectKind1.Checked.ToString());
                        WriteRow(sw, chkDefectKind2.Text, chkDefectKind2.Checked.ToString());
                        WriteRow(sw, chkDefectKind3.Text, chkDefectKind3.Checked.ToString());
                        WriteRow(sw, chkDefectKind4.Text, chkDefectKind4.Checked.ToString());
                        WriteRow(sw, chkDefectKind5.Text, chkDefectKind5.Checked.ToString());
                        WriteRow(sw, chkDefectKind6.Text, chkDefectKind6.Checked.ToString());
                        WriteRow(sw, chkDefectKind7.Text, chkDefectKind7.Checked.ToString());
                        WriteRow(sw, chkDefectKind8.Text, chkDefectKind8.Checked.ToString());

                        WriteRow(sw, "2DC座標開始", txt2DCstart.Text);
                        WriteRow(sw, "2DC座標終了", txt2DCend.Text);

                        WriteRow(sw, "X座標限度値開始", txtXlimitStart.Text, "mm");
                        WriteRow(sw, "X座標限度値終了", txtXlimitEnd.Text, "mm");
                        WriteRow(sw, "Y座標限度値開始", txtYlimitStart.Text, "mm");
                        WriteRow(sw, "Y座標限度値終了", txtYlimitEnd.Text, "mm");

                        // ラジオボタン (選択されている方を文字列で保存)
                        string sortOrder = optAsc.Checked ? "昇順" : "降順";
                        WriteRow(sw, "データ並び順", sortOrder);

                        WriteRow(sw, "出荷ファイル名", txtShippingFileName.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"設定パラメータの保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //----------------------------------------------------
                // [CSVデータ加工1-1]
                // Xシフト処理1
                // 工程1のCSVファイル2列目（X座標）にXシフト量1を加算
                //----------------------------------------------------
                // Xシフト加工後保存先ファイルのパス生成
                string filePath_shift1 = AppendToFileName(filePath, sXシフト加工1);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_shift1) && IsFileLocked(filePath_shift1))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_shift1 + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    using (var fileStream = new FileStream(txtCsvFile1.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = new StreamReader(fileStream, Encoding.UTF8))        
                    using (var writer = new StreamWriter(filePath_shift1, false, Encoding.UTF8))
                    {
                        int iCount = 0;
                        string? line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            iCount++;

                            // 明細行（6行目以降）（インデックスは0開始）
                            if (iCount > rowCnt)
                            {
                                string[] columns = line.Split(',');
                                // 空行チェック
                                if (string.IsNullOrWhiteSpace(line)) continue;
                                // 2列目が存在するか確認
                                if (columns.Length < 2)
                                {
                                    MessageBox.Show($"[Xシフト処理1]\n工程1のCSVファイル{iCount}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                // 2列目（X座標）が数値であるか確認
                                if (!double.TryParse(columns[1], out double value))
                                {
                                    MessageBox.Show($"[Xシフト処理1]\n工程1のCSVファイル{iCount}行目のX座標（2列目）が数値ではありません（値: {columns[1]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                // 2列目にシフト量を加算
                                columns[1] = (value + double.Parse(txtXshift1.Text)).ToString();
                                // 配列をカンマで結合して更新
                                line = string.Join(",", columns);
                            }
                            // ヘッダ情報は工程1のCSVファイル1～5行目を使用し、それ以降は加工した行を書き込む
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xシフト処理1の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //----------------------------------------------------
                // [CSVデータ加工1-2]
                // Xシフト処理2
                // 工程2のCSVファイル2列目（X座標）にXシフト量2を加算
                //----------------------------------------------------
                // Xシフト加工後保存先ファイルのパス生成
                string filePath_shift2 = AppendToFileName(filePath, sXシフト加工2);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_shift2) && IsFileLocked(filePath_shift2))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_shift2 + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    using (var fileStream = new FileStream(txtCsvFile2.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = new StreamReader(fileStream, Encoding.UTF8))
                    using (var writer = new StreamWriter(filePath_shift2, false, Encoding.UTF8))
                    {
                        int iCount = 0;
                        string? line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            iCount ++;

                            // 明細行（6行目以降）（インデックスは0開始）
                            if (iCount > rowCnt)
                            {
                                string[] columns = line.Split(',');
                                // 空行チェック
                                if (string.IsNullOrWhiteSpace(line)) continue;
                                // 2列目が存在するか確認
                                if (columns.Length < 2)
                                {
                                    MessageBox.Show($"[Xシフト処理2]\n工程2のCSVファイル{iCount}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                // 2列目（X座標）が数値であるか確認
                                if (!double.TryParse(columns[1], out double value))
                                {
                                    MessageBox.Show($"[Xシフト処理2]\n工程2のCSVファイル{iCount}行目のX座標（2列目）が数値ではありません（値: {columns[1]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                // 2列目にシフト量を加算
                                columns[1] = (value + double.Parse(txtXshift2.Text)).ToString();
                                // 配列をカンマで結合して更新
                                line = string.Join(",", columns);
                            }
                            // ヘッダ情報は工程2のCSVファイル1～5行目を使用し、それ以降は加工した行を書き込む
                            writer.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Xシフト処理2の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //------------------------------------------------------------
                // [CSVデータ加工2]
                // 単純マージ
                // 工程2で生成したファイルに工程1の明細を追記（工程1のヘッダ情報は使用しない）
                //------------------------------------------------------------
                // 単純マージ後保存先ファイルのパス生成
                string filePath_merge = AppendToFileName(filePath, s単純マージ);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_merge) && IsFileLocked(filePath_merge))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_merge + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // マージ用ファイルを「新規作成（第2引数を false）」で開く
                    using (var writer = new StreamWriter(filePath_merge, false, Encoding.UTF8))
                    {
                        // まず工程2のCSVファイルの全内容をそのまま書き写す
                        if (File.Exists(filePath_shift2))
                        {
                            foreach (var line in File.ReadLines(filePath_shift2, Encoding.UTF8))
                            {
                                writer.WriteLine(line);
                            }
                        }
                        // 次に工程1のCSVファイルの「6行目以降（Skip(5)）」を末尾に追記する
                        if (File.Exists(filePath_shift1))
                        {
                            foreach (var line in File.ReadLines(filePath_shift1, Encoding.UTF8).Skip(rowCnt))
                            {
                                writer.WriteLine(line);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"単純マージの保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var allLines = new List<string>();

                //---------------------------------------------------
                // [CSVデータ加工3]
                // Y座標昇順ソート
                // CSVファイル3列目（Y座標）をキーにして全行を昇順でソート
                //---------------------------------------------------
                // Xシフト加工後保存先ファイルのパス生成
                string filePath_sort = AppendToFileName(filePath, sY座標ソート);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_sort) && IsFileLocked(filePath_sort))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_sort + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // 全行読み込み
                    allLines = File.ReadAllLines(filePath_merge, Encoding.UTF8).ToList();
                    // ヘッダ（1～5行目）を退避
                    var header = allLines.Take(rowCnt).ToList();
                    // 明細行（6行目以降）を取得しソートする
                    var dataWithIndex = allLines.Skip(rowCnt)
                                                 .Select((line, index) => new { Line = line, Index = index + rowCnt + 1, Columns = line.Split(',') })
                                                 .ToList();

                    // 3列目（Y座標）の数値変換チェック
                    foreach (var item in dataWithIndex)
                    {
                        if (string.IsNullOrWhiteSpace(item.Line)) continue;
                        if (item.Columns.Length < 3)
                        {
                            MessageBox.Show($"[Y座標昇順ソート]\n{item.Index}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (!double.TryParse(item.Columns[2], out _))
                        {
                            MessageBox.Show($"[Y座標昇順ソート]\n{item.Index}行目のY座標（3列目）が数値ではありません（値: {item.Columns[2]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // ソート処理
                    var sortedData = dataWithIndex
                                     .OrderBy(x => double.Parse(x.Columns[2]))
                                     .Select(x => x.Line);

                    // ヘッダとソート後のデータを結合して保存
                    File.WriteAllLines(filePath_sort, header.Concat(sortedData), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Y座標昇順ソートの保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //---------------------------------------------------
                // [CSVデータ加工4]
                // 除去加工
                // chkDefectKind1～8のチェックされている値がCSVファイル4列目（欠点種類）に含まれる行を除去
                //---------------------------------------------------
                //除去加工後保存先ファイルのパス生成
                string filePath_exclusion = AppendToFileName(filePath, s除去加工);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_exclusion) && IsFileLocked(filePath_exclusion))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_exclusion + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    var selectedList = gro_removal.Controls.OfType<System.Windows.Forms.CheckBox>()
                                                            .Where(cb => cb.Checked)
                                                            .Select(cb => cb.Text.Trim())
                                                            .ToList();
                    // 全行読み込み
                    allLines = File.ReadAllLines(filePath_sort, Encoding.UTF8).ToList();
                    // ヘッダ（1～5行目）を退避
                    var header = allLines.Take(rowCnt).ToList();
                    // 明細（6行目以降）をフィルタリング
                    var filteredData = allLines.Skip(rowCnt).Where(line =>
                    {
                        // 空行チェック
                        if (string.IsNullOrWhiteSpace(line)) return false;

                        string[] columns = line.Split(',');
                        // 配列長チェック（4列目が存在するか）
                        if (columns.Length < 4) return true; // 4列目がない場合は残す（除外対象外）

                        // 4列目（インデックス：3）を取得
                        string column4Value = columns[3].Trim();
                        // 4列目の文字列に、選択されたキーワードが含まれるか判定
                        bool isMatch = selectedList.Any(keyword => column4Value.Contains(keyword));
                        // 含まれる場合は「false（除外）」、含まれない場合は「true（残す）」
                        return !isMatch;
                    });
                    // ヘッダとフィルタリング後のデータを結合して上書き保存
                    File.WriteAllLines(filePath_exclusion, header.Concat(filteredData), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"除去加工の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //---------------------------------------------------
                // [CSVデータ加工5]
                // 重複処理
                // CSVファイル2列目（X座標）1行上との差分と、3列目（Y座標）1行上との差分が画面で指定された値より小さい場合、該当行を削除
                //---------------------------------------------------
                // 重複処理後保存先ファイルのパス生成
                string filePath_overlap = AppendToFileName(filePath, s重複処理);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_overlap) && IsFileLocked(filePath_overlap))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_overlap + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // 全行読み込み
                    allLines = File.ReadAllLines(filePath_exclusion, Encoding.UTF8).ToList();
                    // ヘッダ（1～5行目）を退避
                    var header = allLines.Take(rowCnt).ToList();
                    // 明細（6行目以降）をリストとして取得
                    var dataLines = allLines.Skip(rowCnt).ToList();
                    // 7行目（dataLinesの2行目、Index: 1）からループを開始
                    for (int i = 1; i < dataLines.Count; i++)
                    {
                        // 空行チェック
                        if (string.IsNullOrWhiteSpace(dataLines[i]) || string.IsNullOrWhiteSpace(dataLines[i - 1]))
                        {
                            continue; // 空行はスキップ
                        }

                        // 「1行上」のデータ
                        string[] prevCols = dataLines[i - 1].Split(',');
                        // 配列長チェック（3列以上必要）
                        if (prevCols.Length < 3)
                        {
                            MessageBox.Show($"[重複処理]\n{i + rowCnt}行目の前行の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (!double.TryParse(prevCols[1], out double prevCol2))
                        {
                            MessageBox.Show($"[重複処理]\n{i + rowCnt}行目の前行のX座標（2列目）が数値ではありません（値: {prevCols[1]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (!double.TryParse(prevCols[2], out double prevCol3))
                        {
                            MessageBox.Show($"[重複処理]\n{i + rowCnt}行目の前行のY座標（3列目）が数値ではありません（値: {prevCols[2]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 「現在の行」のデータ
                        string[] currentCols = dataLines[i].Split(',');
                        // 配列長チェック（3列以上必要）
                        if (currentCols.Length < 3)
                        {
                            MessageBox.Show($"[重複処理]\n{i + rowCnt + 1}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (!double.TryParse(currentCols[1], out double currentCol2))
                        {
                            MessageBox.Show($"[重複処理]\n{i + rowCnt + 1}行目のX座標（2列目）が数値ではありません（値: {currentCols[1]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (!double.TryParse(currentCols[2], out double currentCol3))
                        {
                            MessageBox.Show($"[重複処理]\n{i + rowCnt + 1}行目のY座標（3列目）が数値ではありません（値: {currentCols[2]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 差分の絶対値を計算
                        double diffCol2 = Math.Abs(currentCol2 - prevCol2);// X座標
                        double diffCol3 = Math.Abs(1000 * (currentCol3 - prevCol3));// Y座標（1000倍してm→mm変換）

                        // X座標の差分がX距離より小さい、かつ、Y座標の差分がY距離より小さい場合
                        if (diffCol2 < int.Parse(txtXoverlap.Text) && diffCol3 < int.Parse(txtYoverlap.Text))
                        {
                            // 該当行を削除
                            dataLines.RemoveAt(i);
                            // 行を削除したため、次のループで「新しく詰まってきた同じインデックスの行」を
                            // 再び「1行上の値（prev）」と比較させるためにインデックスを1つ戻す
                            i--;
                        }
                    }
                    // ヘッダとフィルタリング後のデータを結合して上書き保存
                    File.WriteAllLines(filePath_overlap, header.Concat(dataLines), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"重複処理の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //---------------------------------------------------
                // [CSVデータ加工6]
                // 抽出処理
                // CSVファイル1列目（2DC）を画面で指定された範囲で絞り込み
                //---------------------------------------------------
                // 抽出処理後保存先ファイルのパス生成
                string filePath_extraction = AppendToFileName(filePath, s抽出加工);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_extraction) && IsFileLocked(filePath_extraction))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_extraction + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // 全行読み込み
                    allLines = File.ReadAllLines(filePath_overlap, Encoding.UTF8).ToList();
                    // ヘッダ（1～5行目）を退避
                    var header = allLines.Take(rowCnt).ToList();
                    // 明細（6行目以降）をフィルタリング
                    var filteredData = allLines.Skip(rowCnt).Where(line =>
                    {
                        string[] columns = line.Split(',');
                        // 1列目（インデックス：0）を比較
                        if (int.TryParse(columns[0], out int column1Value))
                        {
                            // 2DC開始値以上かつ2DC終了値以下の行だけを残す
                            return ( int.Parse(txt2DCstart.Text) <= column1Value && column1Value <= int.Parse(txt2DCend.Text));
                        }
                        // 数値変換できない（空文字など）場合、削除
                        return false;
                    });
                    // ヘッダとフィルタリング後のデータを結合して上書き保存
                    File.WriteAllLines(filePath_extraction, header.Concat(filteredData), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"抽出処理の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //---------------------------------------------------
                // [CSVデータ加工7]
                // 昇降順指定
                // CSVファイル3列目（Y座標）をキーにして全行を画面で指定された順でソート
                //---------------------------------------------------
                // 昇降順指定後保存先ファイルのパス生成
                string filePath_ysort = AppendToFileName(filePath, sY座標昇降順);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_ysort) && IsFileLocked(filePath_ysort))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_ysort + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // 全行読み込み
                    allLines = File.ReadAllLines(filePath_extraction, Encoding.UTF8).ToList();
                    // ヘッダ（1～5行目）を退避
                    var header = allLines.Take(rowCnt).ToList();
                    // 明細（6行目以降）を取得し、ソート用に分割
                    var dataWithIndex = allLines.Skip(rowCnt)
                                                 .Select((line, index) => new { Line = line, Index = index + rowCnt + 1, Columns = line.Split(',') })
                                                 .ToList();

                    // 3列目（Y座標）の数値変換チェック
                    foreach (var item in dataWithIndex)
                    {
                        if (string.IsNullOrWhiteSpace(item.Line)) continue;
                        if (item.Columns.Length < 3)
                        {
                            MessageBox.Show($"[昇降順指定]\n{item.Index}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (!double.TryParse(item.Columns[2], out _))
                        {
                            MessageBox.Show($"[昇降順指定]\n{item.Index}行目のY座標（3列目）が数値ではありません（値: {item.Columns[2]}）。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // ラジオボタンの状態に応じて3列目（インデックス：2）をキーにソートを実行
                    IEnumerable<string> sortedLines;
                    if (optAsc.Checked)
                    {
                        // 昇順ソート
                        sortedLines = dataWithIndex
                            .OrderBy(x => double.Parse(x.Columns[2]))
                            .Select(x => x.Line);
                    }
                    else
                    {
                        // 降順ソート
                        sortedLines = dataWithIndex
                            .OrderByDescending(x => double.Parse(x.Columns[2]))
                            .Select(x => x.Line);
                    }
                    // ヘッダとソート後のデータを結合して保存
                    File.WriteAllLines(filePath_ysort, header.Concat(sortedLines), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"昇降順指定の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //---------------------------------------------------
                // [CSVデータ加工8]
                // Y座標変換
                // CSVファイル3列目（Y座標）の値から小数点以下を抜き出し1000倍した値で上書きする
                //---------------------------------------------------
                // Y座標変換後保存先ファイルのパス生成
                string filePath_coordinate = AppendToFileName(filePath, sY座標変換);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_coordinate) && IsFileLocked(filePath_coordinate))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_coordinate + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // 全行読み込み
                    allLines = File.ReadAllLines(filePath_ysort, Encoding.UTF8).ToList();
                    // ヘッダ（1～5行目）を退避
                    var header = allLines.Take(rowCnt).ToList();
                    // 明細（6行目以降）を取得し、3列目を変換して上書きする
                    var dataWithIndex = allLines.Skip(rowCnt)
                                                 .Select((line, index) => new { Line = line, Index = index + rowCnt + 1, Columns = line.Split(',') })
                                                 .ToList();

                    // 3列目（Y座標）の数値変換チェックと変換
                    var processedData = new List<string>();
                    foreach (var item in dataWithIndex)
                    {
                        // 空行チェック
                        if (string.IsNullOrWhiteSpace(item.Line))
                        {
                            processedData.Add(item.Line);
                            continue;
                        }

                        // 配列長チェック（3列以上必要）
                        if (item.Columns.Length < 3)
                        {
                            MessageBox.Show($"[Y座標変換]\n{item.Index}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 3列目（Y座標）が数値であるか確認
                        if (!double.TryParse(item.Columns[2], out double val))
                        {
                            MessageBox.Show($"[Y座標変換]\n{item.Index}行目のY座標（3列目）が数値ではありません（値: {item.Columns[2]}）。\nCSVファイルの形式をご確認ください。\n処理を中断します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 小数点以下を抽出して1000倍（例: 1.234 -> 234）
                        int newValue = (int)Math.Round((val % 1.0) * 1000);
                        // 3列目の値を書き換え
                        item.Columns[2] = newValue.ToString();
                        // カンマで結合して1行に戻す
                        processedData.Add(string.Join(",", item.Columns));
                    }
                    // ヘッダと変換後のデータを結合して保存
                    File.WriteAllLines(filePath_coordinate, header.Concat(processedData), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Y座標変換の保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //---------------------------------------------------
                // [CSVデータチェック1]
                // 出荷前処理
                // CSVファイル2列目（X座標）の値が画面で指定された範囲に入っているかチェック
                //---------------------------------------------------
                // 全行読み込み
                allLines = File.ReadAllLines(filePath_coordinate, Encoding.UTF8).ToList();
                // 明細行（6行目以降）をチェック
                var lineCount = rowCnt; // 初期値5（インデックス：5）
                foreach (var line in allLines.Skip(rowCnt))
                {
                    lineCount++;
                    // 空行チェック
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] columns = line.Split(',');
                    // 配列長チェック（2列以上必要）
                    if (columns.Length < 2)
                    {
                        MessageBox.Show($"[出荷前処理（X座標チェック）]\n{lineCount}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2列目（インデックス：1）を数値に変換して範囲をチェック
                    if (!double.TryParse(columns[1], out double col2Value))
                    {
                        MessageBox.Show($"[出荷前処理（X座標チェック）]\n{lineCount}行目のX座標が数値ではありません。\nCSVファイルをご確認ください。\n処理を中断します。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 開始値未満、または終了値を超えている（＝範囲外）の場合
                    if (col2Value < double.Parse(txtXlimitStart.Text) || col2Value > double.Parse(txtXlimitEnd.Text))
                    {
                        // メッセージを表示して処理を中断
                        MessageBox.Show($"[出荷前処理（X座標チェック）]\n{lineCount}行目のX座標が指定範囲を超えています（値: {col2Value}）。\n設定値やCSVファイルをご確認ください。\n処理を中断します。", "注意", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return; // メソッド全体の処理を抜ける
                    }
                }
                //---------------------------------------------------
                // [CSVデータチェック2]
                // 出荷前処理
                // CSVファイル3列目（Y座標）の値が画面で指定された範囲に入っているかチェック
                //---------------------------------------------------
                // 全行読み込み
                allLines = File.ReadAllLines(filePath_coordinate, Encoding.UTF8).ToList();
                // 明細行（6行目以降）をチェック
                lineCount = rowCnt; // 初期値5（インデックス：5）
                foreach (var line in allLines.Skip(rowCnt))
                {
                    lineCount++;
                    // 空行チェック
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] columns = line.Split(',');
                    // 配列長チェック（3列以上必要）
                    if (columns.Length < 3)
                    {
                        MessageBox.Show($"[出荷前処理（Y座標チェック）]\n{lineCount}行目の列数が不足しています。\nCSVファイルをご確認ください。\n処理を中断します。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3列目（インデックス：2）を数値に変換して範囲をチェック
                    if (!double.TryParse(columns[2], out double col3Value))
                    {
                        MessageBox.Show($"[出荷前処理（Y座標チェック）]\n{lineCount}行目のY座標が数値ではありません。\nCSVファイルをご確認ください。\n処理を中断します。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 開始値未満、または終了値を超えている（＝範囲外）の場合
                    if (col3Value < double.Parse(txtYlimitStart.Text) || col3Value > double.Parse(txtYlimitEnd.Text))
                    {
                        // メッセージを表示して処理を中断
                        MessageBox.Show($"[出荷前処理（Y座標チェック）]\n{lineCount}行目のY座標が指定範囲を超えています（値: {col3Value}）。\n設定値やCSVファイルをご確認ください。\n処理を中断します。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // メソッド全体の処理を抜ける
                    }
                }

                //---------------------------------------------------
                // [CSVデータ加工10]
                // 出荷ファイル生成
                // CSVファイル4列目以降のデータを削除（1列目～3列目だけを残す）
                //---------------------------------------------------
                // 出荷前保存先ファイルのパス生成
                string filePath_previous = AppendToFileName(filePath, s出荷前);
                // ファイルが既に存在し、かつロックされているかチェック
                if (File.Exists(filePath_previous) && IsFileLocked(filePath_previous))
                {
                    MessageBox.Show("指定されたファイルが別のプロセスで開かれているため保存できません。" +
                                    "\nファイルを閉じてから再度お試しください。" +
                                    "\n（ファイルパス：" + filePath_previous + "）",
                                    "ファイルオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    // 全行読み込み
                    allLines = File.ReadAllLines(filePath_coordinate, Encoding.UTF8).ToList();
                    // 1列目～3列目だけを取得を抽出
                    allLines = allLines.Select(line => 
                    {
                        var columns = line.Split(',');
                        // 1列目～3列目（Index 0, 1, 2）だけを抽出して配列を作る
                        var newColumns = columns.Take(3).ToArray();
                        // カンマで結合して1行に戻す
                        return string.Join(",", newColumns); 
                    }).ToList();
                    // 出荷前ファイル保存（文字コードはUTF-8を指定、既存ファイルがある場合は上書き）  
                    File.WriteAllLines(filePath_previous, allLines, Encoding.UTF8);
                    // 出荷データ保存（文字コードはUTF-8を指定、既存ファイルがある場合は上書き）  
                    File.WriteAllLines(filePath, allLines, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"出荷ファイルの保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }



                //---------------------------------------------------
                // [後処理1]
                // Readmeテキストファイル生成
                //---------------------------------------------------
                // 出力先のフォルダパスを指定
                string filePath_rm = Path.Combine(newFolderPath, "Readme.txt");
                // 書き込む固定内容
                string content = "加工途中のファイルを保存します。\n\n" +
                                 "１．　設定パラメータ：出荷ファイル名＋_para.csv\n" +
                                 "２．　Xシフト加工1　：出荷ファイル名＋_shift1.csv\n" +
                                 "３．　Xシフト加工2　：出荷ファイル名＋_shift2.csv\n" +
                                 "４．　単純マージ　　：出荷ファイル名＋_merge.csv\n" +
                                 "５．　Y座標ソート　 ：出荷ファイル名＋_sort.csv\n" +
                                 "６．　除去加工　　　：出荷ファイル名＋_exclusion.csv\n" +
                                 "７．　重複処理　　　：出荷ファイル名＋_overlap.csv\n" +
                                 "８．　昇降順指定　　：出荷ファイル名＋_ysort.csv\n" +
                                 "９．　Y座標変換　　 ：出荷ファイル名＋_coordinate.csv\n" +
                                 "１０．出荷前　　　　：出荷ファイル名＋_previous.csv";
                try
                {
                    // ファイルを出力（文字コードはUTF-8を指定、既存ファイルがある場合は上書き）
                    File.WriteAllText(filePath_rm, content, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Readmeテキストファイルの保存に失敗しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //---------------------------------------------------
                // [後処理2]
                // 選択したフォルダとファイル名と画面入力値を設定ファイル（.exe.config）に保存
                //---------------------------------------------------
                try
                {
                    string dir = System.IO.Path.GetDirectoryName(filePath) ?? initialDir;
                    SaveAppSetting("DefaultOutputFolder", dir);
                    SaveAppSetting("ShippingFile", System.IO.Path.GetFileName(filePath));

                    if (double.TryParse(txtXshift1.Text, out double v3)) SaveAppSetting("Xshift1", v3.ToString());
                    if (double.TryParse(txtXshift2.Text, out double v4)) SaveAppSetting("Xshift2", v4.ToString());
                    if (short.TryParse(txtXoverlap.Text, out short v1)) SaveAppSetting("Xoverlap", v1.ToString());
                    if (short.TryParse(txtYoverlap.Text, out short v2)) SaveAppSetting("Yoverlap", v2.ToString());
                    if (short.TryParse(txt2DCstart.Text, out short v5)) SaveAppSetting("Start2DC", v5.ToString());
                    if (short.TryParse(txt2DCend.Text, out short v6)) SaveAppSetting("End2DC", v6.ToString());
                    if (short.TryParse(txtXlimitStart.Text, out short v7)) SaveAppSetting("XlimitStart", v7.ToString());
                    if (short.TryParse(txtXlimitEnd.Text, out short v8)) SaveAppSetting("XlimitEnd", v8.ToString());
                    if (short.TryParse(txtYlimitStart.Text, out short v9)) SaveAppSetting("YlimitStart", v9.ToString());
                    if (short.TryParse(txtYlimitEnd.Text, out short v10)) SaveAppSetting("YlimitEnd", v10.ToString());
                    if (!string.IsNullOrWhiteSpace(txtShippingFileName.Text)) SaveAppSetting("ShippingFile", txtShippingFileName.Text);
                    if (optAsc.Checked) SaveAppSetting("AscOrder", "true"); else if (optDesc.Checked) SaveAppSetting("AscOrder", "false");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("設定の保存に失敗しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 処理完了のメッセージを表示
                MessageBox.Show("出荷データファイルを生成しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 各入力値のチェックを行います
        /// </summary>
        /// <returns>入力値がすべて有効であればtrue、それ以外はfalse</returns>
        private bool Chk入力チェック()
        {
            var errors = new System.Collections.Generic.List<string>();

            // CSVファイルの存在確認（プレースホルダと空欄は不可）
            if (string.IsNullOrWhiteSpace(txtCsvFile1.Text) || txtCsvFile1.Text == defaultTextCSV)
            {
                errors.Add("工程1のCSVファイルが選択されていません。");
            }
            else if (!System.IO.File.Exists(txtCsvFile1.Text))
            {
                errors.Add("工程1のCSVファイルが見つかりません: " + txtCsvFile1.Text);
            }
            else if (!Path.GetExtension(txtCsvFile1.Text).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                errors.Add("工程1のCSVファイルはCSVファイルではありません。");
            }
            if (string.IsNullOrWhiteSpace(txtCsvFile2.Text) || txtCsvFile2.Text == defaultTextCSV)
            {
                errors.Add("工程2のCSVファイルが選択されていません。");
            }
            else if (!System.IO.File.Exists(txtCsvFile2.Text))
            {
                errors.Add("工程2のCSVファイルが見つかりません: " + txtCsvFile2.Text);
            }
            else if (!Path.GetExtension(txtCsvFile2.Text).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                errors.Add("工程2のCSVファイルはCSVファイルではありません。");
            }
            if (!string.IsNullOrWhiteSpace(txtCsvFile1.Text + txtCsvFile2.Text) && txtCsvFile1.Text == txtCsvFile2.Text)
            {
                errors.Add("工程1と工程2のファイルが同じです。");
            }
            // 出荷ファイル名チェック（プレースホルダと空欄は不可、ファイル名として有効か）
            if (string.IsNullOrWhiteSpace(txtShippingFileName.Text) || txtShippingFileName.Text == defaultTextShipping)
            {
                errors.Add("出荷ファイル名を指定してください。");
            }
            else
            {
                // ファイル名に使用できない文字が含まれていないか
                var invalidChars = System.IO.Path.GetInvalidFileNameChars();
                if ((txtShippingFileName.Text + ".csv").IndexOfAny(invalidChars) >= 0)
                {
                    errors.Add("出荷ファイル名に使用できない文字が含まれています。");
                }
            }

            // 数値項目のパースチェック
            short xOverlap, yOverlap, start2DC, end2DC, xLimitStart, xLimitEnd, yLimitStart, yLimitEnd;
            double xShift1, xShift2;
            if (!double.TryParse(txtXshift1.Text, out xShift1))
            {
                errors.Add("Xシフト量1は数値で指定してください。");
            }
            else
            {// 小数第1位まで許容。小数点以下が2桁以上ある場合はエラーとする
                if (!Chk小数点以下桁数チェック(txtXshift1.Text, 1, out string err))
                {
                    errors.Add("Xシフト量1" + err);
                }
            }
            if (!double.TryParse(txtXshift2.Text, out xShift2))
            {
                errors.Add("Xシフト量2は数値で指定してください。");
            }
            else
            {// 小数第1位まで許容。小数点以下が2桁以上ある場合はエラーとする
                if (!Chk小数点以下桁数チェック(txtXshift2.Text, 1, out string err))
                {
                    errors.Add("Xシフト量2" + err);
                }
            }
            if (!short.TryParse(txtXoverlap.Text, out xOverlap)) errors.Add("重複判定X距離は整数で指定してください。");
            if (!short.TryParse(txtYoverlap.Text, out yOverlap)) errors.Add("重複判定Y距離は整数で指定してください。");
            if (!short.TryParse(txt2DCstart.Text, out start2DC)) errors.Add("2DC座標開始は整数で指定してください。");
            if (!short.TryParse(txt2DCend.Text, out end2DC)) errors.Add("2DC座標終了は整数で指定してください。");
            if (!short.TryParse(txtXlimitStart.Text, out xLimitStart)) errors.Add("X座標限度値開始は整数で指定してください。");
            if (!short.TryParse(txtXlimitEnd.Text, out xLimitEnd)) errors.Add("X座標限度値終了は整数で指定してください。");
            if (!short.TryParse(txtYlimitStart.Text, out yLimitStart)) errors.Add("Y座標限度値開始は整数で指定してください。");
            if (!short.TryParse(txtYlimitEnd.Text, out yLimitEnd)) errors.Add("Y座標限度値終了は整数で指定してください。");

            // 範囲チェック
            if (errors.Count == 0)
            {
                if (xShift1 < -1650.0 || 1650.0 < xShift1) errors.Add("Xシフト量1は-1650.0以上、1650.0以下の値を指定してください。");
                if (xShift2 < -1650.0 || 1650.0 < xShift2) errors.Add("Xシフト量2は-1650.0以上、1650.0以下の値を指定してください。");
                if (xOverlap < 0 || 99 < xOverlap) errors.Add("重複判定X距離は0以上、99以下の値を指定してください。");
                if (yOverlap < 0 || 99 < yOverlap) errors.Add("重複判定Y距離は0以上、99以下の値を指定してください。");
                if (start2DC < 0 || 9999 < start2DC) errors.Add("2DC座標開始は0以上、9999以下の値を指定してください。");
                if (end2DC < 0 || 9999 < end2DC) errors.Add("2DC座標終了は0以上、9999以下の値を指定してください。");
                if (start2DC > end2DC) errors.Add("2DC座標の開始値が終了値より大きくなっています。");
                if (xLimitStart < -1650 || 1650 < xLimitEnd) errors.Add("X座標限度値開始は-1650以上、1650以下の値を指定してください。");
                if (xLimitStart > xLimitEnd) errors.Add("X座標限度値の開始値が終了値より大きくなっています。");
                if (yLimitStart < 0 || 1001 < yLimitEnd) errors.Add("Y座標限度値開始は0以上、1001以下の値を指定してください。");
                if (yLimitStart > yLimitEnd) errors.Add("Y座標限度値の開始値が終了値より大きくなっています。");
            }
            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors), "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 小数点以下の桁数をチェックします。
        /// </summary>
        /// <param name="値">チェックする値</param>
        /// <param name="桁">許容される小数点以下の桁数</param>
        /// <param name="err">エラーメッセージ</param>
        /// <returns>桁数が許容範囲内であればtrue、それ以外はfalse</returns>
        private bool Chk小数点以下桁数チェック(string 値, short 桁, out string err)
        {
            err = string.Empty;
            if (値.Length > 0)
            {
                if (値.StartsWith("+") || 値.StartsWith("-")) 値 = 値.Substring(1);
                int idx = 値.IndexOfAny(new char[] { '.', ',' });
                if (idx >= 0)
                {
                    int fracLen = 値.Length - idx - 1;
                    if (fracLen > 桁)
                    {
                        err = "は小数第" + 桁 + "位までで指定してください。";
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// CSVファイルに明細行があるかどうかをチェックします
        /// </summary>
        /// <param name="txt">チェックするCSVファイルのパスを持つTextBox</param>
        /// <param name="name">CSVファイルの名前（エラーメッセージ用）</param>
        /// <returns>明細行がある場合true、ない場合false</returns>
        private bool ChkCsv明細チェック(System.Windows.Forms.TextBox txt,string name)
        {
            int lineCount = 0;
            using (var fileStream = new FileStream(txt.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    // 明細行（6行目以降）データチェック
                    if (lineCount > rowCnt)
                    {
                        string[] columns = line.Split(',');
                        // 要素数の確認（2DC、X座標、Y座標、欠点種類、付帯情報…最低でも5つは必要）
                        if (columns.Length < 5)
                        {
                            MessageBox.Show(name + "のCSVファイル" + $"{lineCount}行目の要素数が不足しています。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            // 明細行がない場合（ヘッダが5行ある前提なので、明細行がない＝5行以下）
            if (lineCount <= rowCnt)
            {
                MessageBox.Show(name + "のCSVファイルに明細行がありません。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 1行書き込む補助メソッド
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        private void WriteRow(StreamWriter sw, string key, string value, string unit = "")
        {
            // 値にカンマや改行、ダブルクォーテーションが含まれる場合はエスケープ
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                value = "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            sw.WriteLine($"{key},{value},{unit}");
        }

        /// <summary>
        /// ファイル名からフォルダを生成しファイル名にサフィックスを追加
        /// </summary>
        /// <param name="filePath">元のファイルパス</param>
        /// <param name="suffix">追加するサフィックス</param>
        /// <returns>元のファイルパスと同階層にファイル名と同じ名称のフォルダを作成し、そのフォルダ内にサフィックスを追加した新しいファイルのパスを返します</returns>
        /// <exception cref="ArgumentException"></exception>
        private static string AppendToFileName(string filePath, string suffix)
        {
            // ディレクトリ、拡張子なしファイル名、拡張子を抽出
            string directory = Path.GetDirectoryName(filePath) ?? "";
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            // 新しいファイル名を構築
            string newFileName = $"{fileNameWithoutExt}{suffix}{extension}";

            // 新しいフォルダパスを生成
            string newFolderPath = Path.Combine(directory, fileNameWithoutExt);

            // フォルダが存在しない場合は自動生成する（存在する場合は何もしない）
            if (!string.IsNullOrEmpty(newFolderPath))
            {
                Directory.CreateDirectory(newFolderPath);
            }

            // パスを再結合
            return Path.Combine(newFolderPath, newFileName);
        }

        /// <summary>
        /// ファイルが他のプロセスによって開かれている（ロックされている）か判定します。
        /// </summary>
        private static bool IsFileLocked(string path)
        {
            try
            {
                // 排他ロック（書き込み）モードでファイルを開いてみる
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    // 無事に開けたらロックされていない
                    return false;
                }
            }
            catch (IOException)
            {
                // 別のプロセスが開いているため、IOException（プロセスがファイルにアクセスできない）が発生する
                return true;
            }
        }
    }
}
