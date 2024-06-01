using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RPG_II
{
    public partial class FormCharacterCreatorClassSelector : Form
    {
        string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
        MySqlConnection myconnection;
        MySqlCommand mycommand;
        MySqlDataAdapter myadapter;
        string mysqlquary;
        string data;
        string selectedplayer;

        int slot;
        int player;
        int buttonstate = 0;
        string[] pick = {"","","","",""};
        Label lbl_desc = new Label();
        Label lbl_desc_I = new Label();

        DataTable dtPlayerClass = new DataTable();
        
        public FormCharacterCreatorClassSelector(int slot)
        {
            InitializeComponent();
            //Tagmarker();
            this.slot = slot;
            Labeler();
            CreateAllClassData();
            ResetPanelStatShow();
        }
        public FormCharacterCreator FormRef { get; set; }
        private void CreateAllClassData()
        {
            mysqlquary = $"select * from pclass;";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dtPlayerClass);
        }
        private void Labeler()
        {
            lbl_desc.Location = new Point(15, 215);
            lbl_desc.AutoSize = true;
            lbl_desc_I.Location = new Point(15, 237);
            lbl_desc_I.AutoSize = true;
            Controls.Add(lbl_desc);
            Controls.Add(lbl_desc_I);
        }
        private void btn_cat_str_Click(object sender, EventArgs e)
        {
            ButtonCategorySetter(1);
        }
        private void btn_cat_pow_Click(object sender, EventArgs e)
        {
            ButtonCategorySetter(2);
        }

        private void btn_cat_mag_Click(object sender, EventArgs e)
        {
            ButtonCategorySetter(3);
        }

        private void btn_cat_bal_Click(object sender, EventArgs e)
        {
            ButtonCategorySetter(4);
        }
        private void ButtonCategorySetter(int val)
        {
            if (buttonstate == val)
            {
                buttonstate = 0;
                RemoveButton();
                SetCategoryClass();
            }
            else
            {
                buttonstate = val;
                SetCategoryClass();
                GenerateButton();
            }
        }
        private void SetCategoryClass()
        {
            if (buttonstate != 0)
            {
                foreach (Button button in this.Controls.OfType<Button>())
                {
                    if (button.Tag.ToString() == "Cat_" + buttonstate)
                    {
                        button.Enabled = true;
                    }
                    else
                    {
                        button.Enabled = false;
                    }
                }
            }
            else
            {
                int counter = 1;
                foreach (Button button in this.Controls.OfType<Button>())
                {
                    if (button.Tag.ToString() == "Cat_1" || button.Tag.ToString() == "Cat_2" || button.Tag.ToString() == "Cat_3" || button.Tag.ToString() == "Cat_4")
                    {
                        button.Enabled = true;
                        counter++;
                    }
                }
            }
        }
        private void RemoveButton()
        {
            ResetPanelStatShow();
            for (int i = 0; i < 3; i++)
            {
                foreach (Button button in this.Controls.OfType<Button>())
                {
                    if (button.Tag.ToString().Contains("buttonclass"))
                    {
                        this.Controls.Remove(button);
                    }
                }
            }
            lbl_desc.Text = "";
            lbl_desc_I.Text = "";
        }
        private void GenerateButton()
        {
            int initialx = 111;
            int currentstate = 0;
            int initialy = 48;
            int counter = initialx;
            
            
                currentstate = 5*(buttonstate-1);
                pick[0] = dtPlayerClass.Rows[currentstate + 0][2].ToString();
                pick[1] = dtPlayerClass.Rows[currentstate + 1][2].ToString();
                pick[2] = dtPlayerClass.Rows[currentstate + 2][2].ToString();
                pick[3] = dtPlayerClass.Rows[currentstate + 3][2].ToString();
                pick[4] = dtPlayerClass.Rows[currentstate + 4][2].ToString();
                
                lbl_desc_I.Text = "-please select a class-";

            switch (buttonstate)
            {
                case 1:
                    lbl_desc.Text = "Survivability Based Classes";
                    break;
                case 2:
                    lbl_desc.Text = "Power Based Classes";
                    break;
                case 3:
                    lbl_desc.Text = "Magic Based Classes";
                    break;
                case 4:
                    lbl_desc.Text = "Balanced Stats Classes";
                    break;
            }
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(90, 25);
                btn.Text = pick[i];
                btn.Location = new Point(initialx, initialy);
                btn.Tag = "buttonclass id-" + (currentstate + i);
                btn.Text = pick[i];
                btn.Click += ButtonPressClassSelectStatShow;
                Controls.Add(btn);
                counter += 100;
                initialy += 31;
            }
            
        }
        private void ResetPanelStatShow()
        {
            btn_save.Enabled = false;
            pnl_main.Visible = false;
            pnl_1stcd.Size = new Size(30, 0);
            pnl_2stcd.Size = new Size(30, 0);
            pnl_3stcd.Size = new Size(30, 0);
            pnl_4stcd.Size = new Size(30, 0);
            pnl_5stcd.Size = new Size(30, 0);
            pnl_6stcd.Size = new Size(30, 0);
        }
        private async void ButtonPressClassSelectStatShow(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ResetPanelStatShow();
            pnl_main.Visible = true;
            btn_save.Enabled = true;
            string[] data = button.Tag.ToString().Split('-');
            int id = Convert.ToInt32(data[1]);
            lbl_desc_I.Text = dtPlayerClass.Rows[id][11].ToString();
            selectedplayer = dtPlayerClass.Rows[id][0].ToString();
            player = id;
            int VIT = Convert.ToInt32(dtPlayerClass.Rows[id][3]);
            int STR = Convert.ToInt32(dtPlayerClass.Rows[id][4]);
            int DEX = Convert.ToInt32(dtPlayerClass.Rows[id][5]);
            int AGI = Convert.ToInt32(dtPlayerClass.Rows[id][6]);
            int INT = Convert.ToInt32(dtPlayerClass.Rows[id][7]);
            int WIS = Convert.ToInt32(dtPlayerClass.Rows[id][8]);
            int[] STAT = { VIT, STR, DEX, AGI, INT, WIS };
            //-Animation-
            Panel[] pnllist = { pnl_1stcd, pnl_2stcd, pnl_3stcd, pnl_4stcd, pnl_5stcd, pnl_6stcd };
            int counter = 0;
            int yaxis = 18;
            foreach (int stats in STAT)
            {
                int converttopos1 = 7 * stats ;
                int converttopos2 = 10 * stats ;
                int converttopos3 = 12 * stats ;
                int converttopos4 = 13 * stats ;
                int converttopos5 = 14 * stats ;
                int difference = 2 + (3 * (stats-1));
                if (stats > 0)
                {
                    pnllist[counter].BackColor = Color.LawnGreen;
                    pnllist[counter].Size = new Size(30, converttopos1);
                    pnllist[counter].Location = new Point(yaxis, (69 + ((6 - stats) * 2))-(difference * 0));
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos2);
                    pnllist[counter].Location = new Point(yaxis, (69 + ((6 - stats) * 2)) - (difference * 1));
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos3);
                    pnllist[counter].Location = new Point(yaxis, (69 + ((6 - stats) * 2)) - (difference * 2));
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos4);
                    pnllist[counter].Location = new Point(yaxis, (69 + ((6 - stats) * 2)) - (difference * 3));
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos5);
                    pnllist[counter].Location = new Point(yaxis, (69 + ((6 - stats) * 2)) - (difference * 4));
                    await Task.Delay(10);
                }
                else if (stats < 0)
                {
                    pnllist[counter].Size = new Size(30, converttopos1*-1);
                    pnllist[counter].Location = new Point(yaxis, 99);
                    pnllist[counter].BackColor = Color.OrangeRed;
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos2 * -1);
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos3 * -1);
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos4 * -1);
                    await Task.Delay(10);
                    pnllist[counter].Size = new Size(30, converttopos5 * -1);
                    await Task.Delay(10);
                }
                else
                {

                }
                counter++;
                yaxis+=45;
            }
        }
        //useless misclicks
        private void pnl_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string classid = dtPlayerClass.Rows[player][0].ToString();
            DataTable dtsave = new DataTable();
            mysqlquary = $"select * from pclass where id_pclass='{classid}';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dtsave);
            string id = dtsave.Rows[0][0].ToString();
            string classname = dtsave.Rows[0][2].ToString();
            double vitmod = (10 + Convert.ToSingle(dtsave.Rows[0][3].ToString()))/10;
            double strmod = (10 + Convert.ToSingle(dtsave.Rows[0][4].ToString())) / 10; 
            double dexmod = (10 + Convert.ToSingle(dtsave.Rows[0][5].ToString())) / 10;
            double agimod = (10 + Convert.ToSingle(dtsave.Rows[0][6].ToString())) / 10;
            double intmod = (10 + Convert.ToSingle(dtsave.Rows[0][7].ToString())) / 10;
            double wismod = (10 + Convert.ToSingle(dtsave.Rows[0][8].ToString())) / 10;
            int level = 1;
            int exp = 0;
            int SP = 5;
            string connect = $"{id} {classname} 0 0 0 0 0 0 {level} {exp} {SP}";
            FormRef.addtolistofplayer(slot, connect);
            this.Close();
        }
    }
}




