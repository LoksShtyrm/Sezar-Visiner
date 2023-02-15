using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sezar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text, text_conv = "";
            int sdwig;
            if (textBox1.Text != "" && textBox4.Text != "")
            {

                text = textBox1.Text; sdwig = int.Parse(textBox4.Text);
                for (int i = 0; i < text.Length; i++)
                {
                    char c = Convert.ToChar(Convert.ToInt32(text[i]) + sdwig);
                    text_conv += c;
                }
                textBox2.Text = text_conv;
            }



        }

        private int[][]  counting(string text)
        {
            int[][] count = new int[128][];

            for (int i = 0; i <=127; i++)
            {
                count[i] = new int[2];
                count[i][0] = i;
            }
            for (int i = 0; i < text.Length; i++)
            {
                int acii_poz = Convert.ToInt32(text[i]);
                count[acii_poz][1]++;
                
            }
          return count;
        }
        private static void Sort<T>(T[][] data, int col)
        {
            Comparer<T> comparer = Comparer<T>.Default;
            Array.Sort<T[]>(data, (x, y) => comparer.Compare(x[col], y[col]));
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                string text_unconv = "";
                for (int sdwig = 26; sdwig >= 1; sdwig--)
                {
                    for (int j = 0; j < textBox2.Text.Length; j++)
                    {
                        if (Convert.ToInt32(textBox2.Text[j])+sdwig<=122)
                        { 
                            text_unconv+= Convert.ToChar(Convert.ToInt32(textBox2.Text[j]) + sdwig);
                        }
                        else
                        {
                            text_unconv += Convert.ToChar(Convert.ToInt32(textBox2.Text[j]) - 27 + sdwig);
                        }
                        
                    }
                    textBox3.Text += text_unconv + Environment.NewLine; ; text_unconv = "";
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //int[] frequency = mass(textBox2.Text);
            //int sdwig = frequency[0];
            //for (int i = 0; i < textBox2.Text.Length; i++)
            //{
            //}
            int[][] frequency = counting(textBox2.Text);
            int max_count_1 = 0,poz_acii=0;
            for (int i = 0; i <= 127; i++)
            {
                if (frequency[i][1]>max_count_1)
                {
                    max_count_1 = frequency[i][1];
                    poz_acii= frequency[i][0];
                }
                

            }
            //Сравнение с 'e' нашу часто повторяющуюся букву 

            int sdwig = 101 - poz_acii;

            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                textBox5.Text += Convert.ToChar(Convert.ToInt32(textBox2.Text[i]) + sdwig);
            }

           // Sort<int>(frequency, 2);
           //for (int i = 0; i < length; i++)
           //{

            //}

        }

        private void button4_Click(object sender, EventArgs e)
        {
           // int[] frequency = mass(textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           // int[] frequency = mass(textBox2.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            if (textBox1.Text!=""&& textBox6.Text != "")
            {
             

                string key_word = textBox6.Text,text= textBox1.Text;
                int[,] mass = new int[text.Length, 3];
                for (int j = 0; j < text.Length; j++)
                {
                   
                    for (int i = 0; i < key_word.Length && j < text.Length; i++)
                    {
                        mass[j, 0] = Convert.ToInt32(text[j]) - 97;
                        mass[j, 1] = Convert.ToInt32(key_word[i]) - 97;
                        if (mass[j, 0] + mass[j, 1] >= 26)
                        {
                            mass[j, 2] =  (mass[j, 0] + mass[j, 1]) % 26;
                        }
                        else
                        {
                            mass[j, 2] = mass[j, 0] + mass[j, 1];
                        }
                        j++;
                    }
                }
                for (int i = 0; i < text.Length; i++)
                {
                    textBox7.Text += Convert.ToChar(mass[i, 2]+97);
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            string cipherText = textBox7.Text;
            int keyLength = textBox7.Text.Length;
            string key = FindKey(cipherText, keyLength);
            string plainText = Decrypt(cipherText, key);
            textBox8.Text = plainText;
        }
        static string FindKey(string cipherText, int keyLength)
        {
            string key = "";
            for (int i = 0; i < keyLength; i++)
            {
                string column = "";
                for (int j = i; j < cipherText.Length; j += keyLength)
                {
                    column += cipherText[j];
                }
                int[] freq = new int[32];
                foreach (char c in column)
                {
                    freq[(int)c - 1040]++;
                }
                int maxIndex = 0;
                for (int j = 1; j < freq.Length; j++)
                {
                    if (freq[j] > freq[maxIndex])
                    {
                        maxIndex = j;
                    }
                }
                int keyVal = (maxIndex - 14 + 32) % 32;
                key += (char)(keyVal + 1040);
            }
            return key;
        }
        static string Decrypt(string cipherText, string key)
        {
            string plainText = "";
            int keyIndex = 0;
            foreach (char c in cipherText)
            {
                int cVal = (int)c - (int)key[keyIndex];
                if (cVal < 0)
                {
                    cVal += 32;
                }
                plainText += (char)(cVal + 1040);
                keyIndex = (keyIndex + 1) % key.Length;
            }
            return plainText;
        }
    }
}

