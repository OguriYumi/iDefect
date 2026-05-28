namespace iDefect
{
    /// <summary>
    /// 処理中であることを示すオーバーレイフォーム（別スレッドで動作）
    /// </summary>
    public class ProgressOverlay : IDisposable
    {
        private Thread? _thread;
        private System.Windows.Forms.Form? _overlayForm;
        private volatile bool _closing;
        private readonly int _x;
        private readonly int _y;

        public ProgressOverlay(System.Windows.Forms.Form owner)
        {
            // オーナーフォームの中央座標を事前に計算
            _x = owner.Left + (owner.Width - 200) / 2;
            _y = owner.Top + (owner.Height - 60) / 2;
        }

        public void ShowOverlay()
        {
            _closing = false;
            _thread = new Thread(RunOverlay);
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.IsBackground = true;
            _thread.Start();
            // 表示されるまで少し待つ
            Thread.Sleep(100);
        }

        private void RunOverlay()
        {
            var form = new System.Windows.Forms.Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true,
                Size = new Size(300, 100),
                Location = new Point(_x, _y)
            };

            var label = new Label
            {
                Text = "処理中...",
                Font = new Font("Yu Gothic UI", 14, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            form.Controls.Add(label);

            form.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.Gray, 2);
                e.Graphics.DrawRectangle(pen, 0, 0, form.Width - 1, form.Height - 1);
            };

            int dotCount = 3;
            var timer = new System.Windows.Forms.Timer { Interval = 500 };
            timer.Tick += (s, e) =>
            {
                dotCount = (dotCount % 6) + 1;
                label.Text = "処理中" + new string('.', dotCount);
            };

            form.Shown += (s, e) => timer.Start();
            form.FormClosed += (s, e) => timer.Stop();

            _overlayForm = form;
            Application.Run(form);
        }

        public void CloseOverlay()
        {
            if (_closing) return;
            _closing = true;

            if (_overlayForm != null && _overlayForm.IsHandleCreated)
            {
                _overlayForm.Invoke(() => _overlayForm.Close());
            }
            _thread?.Join(2000);
        }

        public void Dispose()
        {
            CloseOverlay();
        }
    }
}
