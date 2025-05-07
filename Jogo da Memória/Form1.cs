namespace Jogo_da_Memória
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "K", "K",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };


        Label firstClicked = null;
        Label secondClicked = null;

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);

                    iconLabel.ForeColor = iconLabel.BackColor;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label2.Click += new System.EventHandler(this.label1_Click);
            this.label3.Click += new System.EventHandler(this.label1_Click);
            this.label4.Click += new System.EventHandler(this.label1_Click);
            this.label5.Click += new System.EventHandler(this.label1_Click);
            this.label6.Click += new System.EventHandler(this.label1_Click);
            this.label7.Click += new System.EventHandler(this.label1_Click);
            this.label8.Click += new System.EventHandler(this.label1_Click);
            this.label9.Click += new System.EventHandler(this.label1_Click);
            this.label10.Click += new System.EventHandler(this.label1_Click);
            this.label11.Click += new System.EventHandler(this.label1_Click);
            this.label12.Click += new System.EventHandler(this.label1_Click);
            this.label13.Click += new System.EventHandler(this.label1_Click);
            this.label14.Click += new System.EventHandler(this.label1_Click);
            this.label15.Click += new System.EventHandler(this.label1_Click);
            this.label16.Click += new System.EventHandler(this.label1_Click);

            AssignIconsToSquares();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Você emparelhou todos os ícones!", "Parabéns!");
            Close();
        }
    }
}