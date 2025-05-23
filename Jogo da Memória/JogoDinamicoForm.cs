﻿using System;
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
    public partial class JogoDinamicoForm : Form
    {
        Random random = new Random();

        List<string> icons;


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

        // icons deve sempre ter uma quantidade de elemento com raiz quadrada exata. exemplo.. 4, 9, 16, 25, 36, 49...
        public JogoDinamicoForm(List<string> icons, string nivel)
        {
            this.icons = icons;
            InitializeComponent();
            this.Text = "Modo " + nivel;
            CriarLabelsAutomaticamente();
            AssignIconsToSquares();

            timer1.Interval = 750; // tempo de exibição das cartas antes de esconder
            timer1.Tick += timer1_Tick;
        }

        private void CriarLabelsAutomaticamente()
        {
            int rows = (int) Math.Sqrt(this.icons.Count);
            int cols = (int) Math.Sqrt(this.icons.Count);
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
                    lbl.Font = new Font("Webdings", 40, FontStyle.Bold);
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

    }
}
