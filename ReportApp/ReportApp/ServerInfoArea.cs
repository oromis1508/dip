using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReportApp
{
    class ServerInfoArea : TextBox
    {
        public ServerInfoArea()
        {
            Multiline = true;
            Location = new Point(0, 0);
            Name = "serverInfoArea";
            Enabled = false;
            Dock = DockStyle.Fill;
        }
    }
}
