using System.Windows.Forms;

namespace Client
{
    public partial class JoinChatDialog : Form
    {
        public JoinChatDialog()
        {
            InitializeComponent();
        }

        internal string GetChatName()
        {
            return chatTextBox.Text;
        }

        internal string GetLogin()
        {
            return loginTextBox.Text;
        }
    }
}
