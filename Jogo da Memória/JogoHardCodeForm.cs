using System.Windows.Forms;

namespace Jogo_da_Memória
{
    public partial class JogoHardCodeForm : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "K", "K",
            "b", "b", "v", "v", "$", "$", "z", "z"
        };

        Label? firstClicked = null;
        Label? secondClicked = null;

        public JogoHardCodeForm()
        {
            InitializeComponent();
            CriarLabelsAutomaticamente();
            AssignIconsToSquares();



            timer1.Interval = 750; // tempo de exibição das cartas antes de esconder
            timer1.Tick += timer1_Tick;
        }

        private void CriarLabelsAutomaticamente()
        {
            int rows = 4;
            int cols = 4;
            int count = 1;

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnCount = cols;
            tableLayoutPanel1.RowCount = rows;

            for (int i = 0; i < cols; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));

            for (int i = 0; i < rows; i++)
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Label lbl = new Label
                    {
                        Text = "?",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Webdings", 60, FontStyle.Bold),
                        BackColor = Color.CornflowerBlue,
                        ForeColor = Color.CornflowerBlue,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    lbl.Click += label1_Click;
                    tableLayoutPanel1.Controls.Add(lbl, col, row);
                    count++;
                }
            }
        }

        private void AssignIconsToSquares()
        {
            int labelCount = tableLayoutPanel1.Controls.OfType<Label>().Count();
            if (icons.Count != labelCount)
            {
                MessageBox.Show("Erro: número de ícones diferente do número de labels.");
                Close();
                return;
            }

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label? iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);

                    iconLabel.ForeColor = iconLabel.BackColor;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;

            Label? clickedLabel = sender as Label;
            if (clickedLabel == null || clickedLabel.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            if (clickedLabel == firstClicked)
                return; // evita duplo clique no mesmo label

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
                return;
            }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (firstClicked != null)
                firstClicked.ForeColor = firstClicked.BackColor;

            if (secondClicked != null)
                secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label? iconLabel = control as Label;
                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return;
            }

            DialogResult confirmResult = MessageBox.Show("Deseja jogar uma nova partida?", "Parabéns!", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Até a próxima!");
                System.Windows.Forms.Application.Exit();
            }
        }

        private void JogoHardCodeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
