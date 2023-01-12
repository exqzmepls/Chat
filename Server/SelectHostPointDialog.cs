using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Server
{
    public partial class SelectHostPointDialog : Form
    {
        public SelectHostPointDialog()
        {
            InitializeComponent();
        }

        public string GetIpAddress() => hostComboBox.SelectedItem as string;

        public int GetPort() => (int)portNumericUpDown.Value;

        private void SelectHostPointDialog_Load(object sender, EventArgs e)
        {
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            hostComboBox.Items.AddRange(hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).Select(a => a.ToString()).ToArray());
            hostComboBox.SelectedIndex = 0;

            portNumericUpDown.Value = 30125;
        }
    }
}
