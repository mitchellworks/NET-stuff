using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MegaEscritorio
{
    
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }
        int materialPrice = 0;
        int rushPrice = 0;
        int surfacePrice = 0;
        int drawerPrice = 0;
        int[] rushPrices = new int[9];

        public void button1_Click(object sender, EventArgs e)
        {
            MegaProgram megaProgram = new MegaProgram();
            DeskOrder deskOrder = new DeskOrder();
            deskOrder.width = int.Parse(textBox1.Text);
            deskOrder.length = int.Parse(textBox2.Text);
            deskOrder.drawers = int.Parse(textBox3.Text);
            deskOrder.material = listBox1.Text;
            deskOrder.rushDays = int.Parse(textBox4.Text);

            MegaProgram.ReadPrices(rushPrices);
            MegaProgram.CalcPrice(ref deskOrder, ref surfacePrice, ref drawerPrice, ref materialPrice, ref rushPrice, rushPrices);

            label8.Text = "$" + deskOrder.totalPrice.ToString();
        }
    }
}
