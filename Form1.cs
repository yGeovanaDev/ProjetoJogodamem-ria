using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace JogoMemoria
{
    public partial class Form1 : Form
    {

        int movimentos, cliques, cartasEncontradas, tagIndex;

        Image[] img = new Image[10];

        List<string> lista = new List<string>();

        int[] tags = new int[2];
        public Form1()
        {
            InitializeComponent();
            Inicio();
        }

        private void Inicio()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                int tagIndex = int.Parse(String.Format("{0}", item.Tag));
                img[tagIndex] = item.Image;
                item.Image = Properties.Resources.verso;
                item.Enabled = true;
            }

            Posicoes();
        }
        private void Posicoes()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                Random rdn = new Random();

                int[] xP = { 42, 232, 422, 612, 802, 992 };
                int[] yP = { 12, 190, 368, };

             Repete:
                var X = xP[rdn.Next(0, xP.Length)];
                var Y = yP[rdn.Next(0, yP.Length)];

                string verificacao = X.ToString() + Y.ToString();

                if (lista.Contains(verificacao))
                {
                    goto Repete;
                }
                else
                {
                    item.Location = new Point(X, Y);
                    lista.Add(verificacao);

                }
            }
        }
        private void ImagensClick_Click(object sender, EventArgs e)
        {
            bool parEncontrado = false;
            PictureBox pic = (PictureBox)sender;
            cliques++;

            tagIndex = int.Parse(String.Format("{0}", pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();

            if (cliques == 1)
            {
                tags[0] = int.Parse(String.Format("{0}", pic.Tag));

            }
            else if (cliques == 2)
            {

                movimentos++;
                lblMovimentos.Text = "Movimentos" + movimentos.ToString();
                tags[1] = int.Parse(String.Format("{0}", pic.Tag));
                parEncontrado = ChecagemPares();
                Desvirar(parEncontrado);

            }

        }
        private bool ChecagemPares()
        {
            cliques = 0;

            if (tags[0] == tags[1]) { return true; } else { return false; }
        }


        private void Desvirar(bool check)
        {
            Thread.Sleep(500);

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (int.Parse(String.Format("{0}", item.Tag)) == tags[0] ||
                    int.Parse(String.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check == true)
                    {
                        item.Enabled = false;
                        cartasEncontradas++;

                    }
                    else
                    {
                        item.Image = Properties.Resources.verso;
                        item.Refresh();
                    }
                }
            }
            FinalJogo();
        }
        private void FinalJogo()
        {
            if (cartasEncontradas == (img.Length * 1))
            {
                MessageBox.Show("Parabéns, você terminou o jogo com" + movimentos.ToString() + "movimentos");
                DialogResult msg = MessageBox.Show("Deseja continuar o jogo?", "Caixa de pergunta", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {
                    cliques = 0; movimentos = 0; cartasEncontradas = 0;
                    lista.Clear();
                    Inicio();
                }
                else if (msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigada por jogar!");
                    Application.Exit();
                }
            }
        }

    }
}