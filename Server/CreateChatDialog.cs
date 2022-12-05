using System.Windows.Forms;

namespace Server
{
    public partial class CreateChatDialog : Form
    {
        public CreateChatDialog()
        {
            InitializeComponent();
        }

        public string GetChatName()
        {
            return textBox.Text.Trim();
        }
    }
}
