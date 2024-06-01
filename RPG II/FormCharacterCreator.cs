using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;
using RPG_II.Properties;
using System.Timers;
using System.Diagnostics;
using System.Xml.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace RPG_II
{
    public partial class FormCharacterCreator : Form
    {
        int selectedsavefile;
        string[] savedplayer = { "", "", "", "" };
        Thread thread;
        MapGenerator mapgen = new MapGenerator();

        string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
        MySqlConnection myconnection;
        MySqlCommand mycommand;
        MySqlDataAdapter myadapter;
        MySqlDataReader myreader;

        string mysqlquary;
        string data;
        string selectedplayer;
        string player1img,player2img, player3img,player4img;
        Object Image;

        public FormCharacterCreator(int selectedsavefile)
        {
            InitializeComponent();
            tbar_dif.Value = 200;
            this.selectedsavefile = selectedsavefile;
        }
        #region Exit Button
        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
            thread = new Thread(openmenu);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void openmenu(object obj)
        {
            Application.Run(new FormGame());
        }
        #endregion
        #region Difficulty and Player Setter

        

        private void pbox_p1_Click(object sender, EventArgs e)
        {
            GotoClassStats(1);
        }

        private void pbox_p2_Click(object sender, EventArgs e)
        {
            GotoClassStats(2);
        }

        private void pbox_p3_Click(object sender, EventArgs e)
        {
            GotoClassStats(3);
        }

        private void pbox_p4_Click(object sender, EventArgs e)
        {
            GotoClassStats(4);
        }

        private void trackbardifficultychange(object sender, EventArgs e)
        {
            pnl_dif.Size = new Size(tbar_dif.Value, 30);
            if (tbar_dif.Value < 133)
            {
                pnl_dif.BackColor = Color.Pink;
                lbl_dif.Text = "Easy";
                lbl_dif_desc.Text = "Enemy Stats are lowered";
            }
            else if (tbar_dif.Value > 267)
            {
                pnl_dif.BackColor = Color.Red;
                lbl_dif.Text = "Hard";
                lbl_dif_desc.Text = "Extremely Hard to Beat";
            }
            else
            {
                pnl_dif.BackColor = Color.Orange;
                lbl_dif.Text = "Medium";
                lbl_dif_desc.Text = "Standard Difficulty, no buffs or debuffs";
            }
        }
        #endregion
        #region Continue Button
        private void btn_continue_Click(object sender, EventArgs e)
        {
            //map
            mapgen.SetMapLevel("1");
            string map = mapgen.MapGeneratorThing();
            //naming of team and cash
            int cash = 0;
            if (tbox_teamname.Text == "")
            {
                tbox_teamname.Text = "*Insert Cool Name Here*";
            }
            if (rbtn_1.Checked)
            {
                cash = 1500;
            }
            else if (rbtn_2.Checked)
            {
                cash = 500;
            }
            else if (rbtn_3.Checked)
            {
                cash = 0;
            }
            else 
            {
                cash = 1000;
            }
            if (savedplayer[0] == "" || savedplayer[1] == "" || savedplayer[2] == "" || savedplayer[3] == "")
            {
                MessageBox.Show("Team Incomplete");
            }
            else
            {
                try
                {
                    myconnection.Open();
                    mysqlquary = $"update Savefile set save_teamname = '{tbox_teamname.Text.Replace(' ','-')}' where id_savefile = 'SF{selectedsavefile}-0';";
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myreader = mycommand.ExecuteReader();
                    myconnection.Close();
                    myconnection.Open();
                    mysqlquary = $"update Savefile set save_data = 'name:{tbox_teamname.Text.Replace(' ', '-')} cash:{cash} map:{map} difficulty{tbar_dif.Value} pos:first' where id_savefile = 'SF{selectedsavefile}-0';";
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myreader = mycommand.ExecuteReader();
                    myconnection.Close();
                    myconnection.Open();
                    mysqlquary = $"update Savefile set save_data = '{savedplayer[0]}' where id_savefile = 'SF{selectedsavefile}-1';";
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myreader = mycommand.ExecuteReader();
                    myconnection.Close();
                    myconnection.Open();
                    mysqlquary = $"update Savefile set save_data = '{savedplayer[1]}' where id_savefile = 'SF{selectedsavefile}-2';";
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myreader = mycommand.ExecuteReader();
                    myconnection.Close();
                    myconnection.Open();
                    mysqlquary = $"update Savefile set save_data = '{savedplayer[2]}' where id_savefile = 'SF{selectedsavefile}-3';";
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myreader = mycommand.ExecuteReader();
                    myconnection.Close();
                    myconnection.Open();
                    mysqlquary = $"update Savefile set save_data = '{savedplayer[3]}' where id_savefile = 'SF{selectedsavefile}-4';";
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myreader = mycommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    myconnection.Close();
                    this.Close();
                    thread = new Thread(closeplayercreator);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
            }
            
        }
        #endregion
        private void GotoClassStats(int slot)
        {
            pnl_cccs.Visible = true;
            FormCharacterCreatorClassSelector FCCCS = new FormCharacterCreatorClassSelector(slot);
            FCCCS.TopLevel = false;
            pnl_cccs.Controls.Add(FCCCS);
            FCCCS.FormRef = this;
            FCCCS.Show();
        }
        public void addtolistofplayer(int slot, string data)
        {

            savedplayer[slot - 1] = data;
            updateimage();
        }
        private void updateimage()
        {
            PictureBox[] pboxlist = { pbox_p1, pbox_p2, pbox_p3, pbox_p4 };
            int counter = 0;
            foreach (string dataid in savedplayer)
            {
                if (dataid == "")
                {

                }
                else
                {
                    string[] wat = dataid.Split(' ');
                    DataTable dtsave = new DataTable();
                    mysqlquary = $"select * from pclass where id_pclass='{wat[0].ToString()}';";
                    myconnection = new MySqlConnection(mysqlconnection);
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myadapter = new MySqlDataAdapter(mycommand);
                    myadapter.Fill(dtsave);
                    player1img = dtsave.Rows[0][10].ToString();
                    Image = Resources.ResourceManager.GetObject(dtsave.Rows[0][2].ToString());
                    pboxlist[counter].Image = (Image)Image;
                }
                counter++;
            }
            pnl_cccs.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbox_teamname.Text = "Greatest Team";
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (savedplayer[0] == "" || savedplayer[1] == "" || savedplayer[2] == "" || savedplayer[3] == "")
            {
                MessageBox.Show("Team Incomplete");
            }
            else
            {
                pnl_confirm.Visible = true;
            }
            
        }

        private void btn_con_back_Click(object sender, EventArgs e)
        {
            pnl_confirm.Visible = false;
        }

        
        
        private void closeplayercreator(object obj)
        {
            Application.Run(new FormSaveFile(0));
        }
    }
}
