using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

internal static class ActivityTest
{
    [STAThread]
    private static int Main(string[] args)
    {
        string executable = Path.GetFileName(Application.ExecutablePath);
        string game;
        if (executable.Equals("wwm.exe", StringComparison.OrdinalIgnoreCase))
            game = "Where Winds Meet";
        else if (executable.Equals("deadzone2steam.exe", StringComparison.OrdinalIgnoreCase))
            game = "Deadzone Rogue 2";
        else if (executable.Equals("endfield.exe", StringComparison.OrdinalIgnoreCase))
            game = "Arknights: Endfield";
        else if (executable.Equals("FC27.exe", StringComparison.OrdinalIgnoreCase))
            game = "EA Sports FC 27";
        else if (executable.Equals("RobloxPlayerBeta.exe", StringComparison.OrdinalIgnoreCase))
            game = "Roblox";
        else if (executable.Equals("once_human.exe", StringComparison.OrdinalIgnoreCase))
            game = "Once Human";
        else if (executable.Equals("firefox.exe", StringComparison.OrdinalIgnoreCase))
            game = "Firefox";
        else if (executable.Equals("MapleStory.exe", StringComparison.OrdinalIgnoreCase))
            game = "MapleStory Classic World";
        else if (executable.Equals("StarRail.exe", StringComparison.OrdinalIgnoreCase))
            game = "Honkai: Star Rail";
        else if (executable.Equals("Asphalt9_Steam_x64_rtl.exe", StringComparison.OrdinalIgnoreCase))
            game = "ASPHALT LEGENDS";
        else
            return 2;

        if (args.Length == 1 && args[0] == "--check") return 0;

        bool created;
        using (Mutex mutex = new Mutex(true, "Local\\CodexGameActivityTest_" + executable.ToLowerInvariant(), out created))
        {
            if (!created)
            {
                MessageBox.Show(game + " is already running. You can start other games at the same time.", "Activity test");
                return 1;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Form window = new Form())
            using (System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer())
            {
                window.Text = game + " - activity test";
                window.ClientSize = new Size(560, 280);
                window.MinimumSize = new Size(576, 319);
                window.StartPosition = FormStartPosition.CenterScreen;
                window.Font = new Font("Segoe UI", 11);
                window.BackColor = Color.FromArgb(245, 247, 250);
                window.AutoScaleMode = AutoScaleMode.Dpi;

                Label heading = new Label();
                heading.Text = game;
                heading.Font = new Font("Segoe UI", 17, FontStyle.Bold);
                heading.SetBounds(24, 22, 512, 38);
                heading.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                window.Controls.Add(heading);

                Label description = new Label();
                description.Text = "Dummy game process is running.\r\nKeep Discord desktop open and check the quest's progress.\r\nDetection and quest credit are unverified.";
                if (executable.Equals("FC27.exe", StringComparison.OrdinalIgnoreCase))
                    description.Text = "Experimental FC27.exe process is running.\r\nDiscord's public list provides no executable name for FC 27.\r\nCheck Discord; detection and quest credit are unverified.";
                if (executable.Equals("firefox.exe", StringComparison.OrdinalIgnoreCase))
                    description.Text = "Experimental dummy process; this is not a web browser.\r\nDiscord's public list provides no executable name for Firefox.\r\nCheck Discord; detection and quest credit are unverified.";
                description.SetBounds(24, 73, 512, 80);
                description.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                window.Controls.Add(description);

                Label elapsed = new Label();
                elapsed.SetBounds(24, 168, 512, 32);
                elapsed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                window.Controls.Add(elapsed);
                Stopwatch clock = Stopwatch.StartNew();
                Action update = delegate {
                    elapsed.Text = "Local running time: " + clock.Elapsed.ToString(@"hh\:mm\:ss");
                };
                update();
                timer.Interval = 1000;
                timer.Tick += delegate { update(); };
                timer.Start();

                Button stop = new Button();
                stop.Text = "Stop and close";
                stop.SetBounds(24, 221, 160, 36);
                stop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                stop.Click += delegate { window.Close(); };
                window.Controls.Add(stop);
                window.CancelButton = stop;
                Application.Run(window);
            }
            mutex.ReleaseMutex();
        }
        return 0;
    }
}
