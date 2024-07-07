using EventApp;

namespace EventManagement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataService service = new DataService();
            DbContext dbContext = new DbContext();

            MessageBox.Show("Account size: " + dbContext.accounts.Count);
        }
    }
}
