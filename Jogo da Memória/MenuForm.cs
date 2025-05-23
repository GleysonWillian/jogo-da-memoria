﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_da_Memória
{
    public partial class MenuForm : Form
    {

        List<string> iconsNormal = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "K", "K",
            "b", "b", "v", "v", "w", "w", "z", "z",
            "h", "h", "f", "f", "s", "s", "x", "x",
            "A", "A", "B", "B", "C", "C", "D", "D",
            "E", "E", "F", "F"
        };

        List<string> iconsDificil = new List<string>()
        {
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","A","B","C","D","E","F","G","H","I","J","K","L","M","N",
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","A","B","C","D","E","F","G","H","I","J","K","L","M","N"

        };
        public MenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nivel = cmbNivel.Text;
            if (nivel == "Fácil")
            { 
                new JogoHardCodeForm().ShowDialog();
            } else if(nivel == "Normal")
            {
                new JogoDinamicoForm(iconsNormal, nivel).ShowDialog();
            } else if (nivel == "Difícil")
            {
                new JogoDinamicoForm(iconsDificil, nivel).ShowDialog();
            }
        }
    }
}
