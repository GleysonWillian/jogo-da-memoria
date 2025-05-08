using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_da_Memória
{
    public partial class Form2 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "K", "K",
            "b", "b", "v", "v", "w", "w", "z", "z",
            "h", "h", "f", "f", "s", "s", "x", "x",
            "A", "A", "B", "B", "C", "C", "D", "D",
            "E", "E", "F", "F"
        };


        Label? firstClicked = null;
        Label? secondClicked = null;

        private void AssignIconsToSquares()
        {
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

        public Form2()
        {
            InitializeComponent();
            CriarLabelsAutomaticamente();
            AssignIconsToSquares();

            timer1.Interval = 750; // tempo de exibição das cartas antes de esconder
            timer1.Tick += timer1_Tick;
        }

        private void CriarLabelsAutomaticamente()
        {
            int rows = 6;
            int cols = 6;
            int count = 1;

            // Garante que o painel esteja limpo
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
                    Label lbl = new Label();
                    lbl.Name = "label" + count;
                    lbl.Text = "?";
                    lbl.Dock = DockStyle.Fill;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Font = new Font("Arial", 24, FontStyle.Bold);
                    lbl.BackColor = Color.CornflowerBlue;
                    lbl.BorderStyle = BorderStyle.FixedSingle;

                    // Adicione o evento de clique, se desejar:
                    lbl.Click += label_Click;

                    tableLayoutPanel1.Controls.Add(lbl, col, row);
                    count++;
                }
            }
        }


        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label? clickedLabel = sender as Label;

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

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Você emparelhou todos os ícones!", "Parabéns!");
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
