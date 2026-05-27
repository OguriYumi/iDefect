namespace iDefect
{
    internal static class Program
    {
        // 多重起動防止用のMutex名（アプリケーション固有の一意な名前）
        private const string MutexName = "iDefect_SingleInstance_Mutex_2024";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Mutexを使用して多重起動をチェック
            using (var mutex = new Mutex(true, MutexName, out bool createdNew))
            {
                if (!createdNew)
                {
                    // 既に起動している場合
                    MessageBox.Show(
                        "既に起動済みです。",
                        "起動エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return; // アプリケーションを終了
                }

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new Form());

                // Mutexはusingブロックを抜けると自動的に解放される
            }
        }
    }
}
