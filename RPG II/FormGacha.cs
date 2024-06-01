using MySql.Data.MySqlClient;
using RPG_II.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_II
{
    public partial class FormGacha : Form
    {
        int state, cost, difficulty, cash;
        string slot;
        Object Image;
        Random random = new Random();

        string mysqlquary;
        string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
        int saveslot;
        MySqlConnection myconnection;
        MySqlCommand mycommand;
        MySqlDataAdapter myadapter;
        MySqlDataReader myreader;

        DataTable dtreward = new DataTable();
        string[] imagelist = { "EQ_1", "EQ_2", "EQ_3", "EQ_4", "EQ_5" };
        SaveEditor Editor = new SaveEditor();
        public FormGacha(string slot, int state)
        {
            InitializeComponent();
            this.slot = slot;
            this.state = state;
            Editor.SelectSaveSlot(slot);
            cash = Convert.ToInt32(Editor.GetTeamData("cash"));
            difficulty = Convert.ToInt32(Editor.GetTeamData("difficulty"));
            cost = difficulty - (difficulty % 10);
            SetStage();
        }
        public FormMainGame FormRef { get; set; }
        private void SetStage()
        {
            //pay up boi
            if (state == 0)
            {
                btn_open.Text = cost+ " ඞ";
                if (cash < cost)
                {
                    btn_open.Enabled = false;
                }
            }
            //nah fam it's on the house
            if (state == 1)
            {
                btn_open.Text = "Free";
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            pbox_reward.Visible = false;
            btn_open.Enabled = false;
            Image = Resources.ResourceManager.GetObject("MT_treasure");
            pbox_chest.Image = (Image)Image;
            int x = pbox_chest.Location.X;
            int y = pbox_chest.Location.Y;
            for (int i = 1; i < 40; i++)
            {
                int rng = random.Next(0, 4);
                if (rng == 0)
                {
                    pbox_chest.Location = new Point(x + i, y + i);
                }
                else if (rng == 1)
                {
                    pbox_chest.Location = new Point(x + i, y - i);
                }
                else if (rng == 2)
                {
                    pbox_chest.Location = new Point(x - i, y + i);
                }
                else if (rng == 3)
                {
                    pbox_chest.Location = new Point(x - i, y - i);
                }
                await Task.Delay(10);
            }
            Image = Resources.ResourceManager.GetObject("MT_unlocked");
            pbox_chest.Image = (Image)Image;
            pbox_chest.Location = new Point(x, y);
            GetReward();
            if (state == 0)
            {
                PayUp();
            }
            pbox_reward.Visible = true;
            btn_open.Enabled = true;
            state = 0;
            SetStage();
        }
        private void PayUp()
        {
            int remainder = cash - cost;
            Editor.ChangeSaveData("cash", remainder.ToString());
            Editor.SelectSaveSlot(slot);
            cash = Convert.ToInt32(Editor.GetTeamData("cash"));
            FormRef.UpdateMoney(cash);
        }
        private void GetReward()
        {
            dtreward.Clear();
            int chance = random.Next(1, 101);
            if (chance <= 40)
            {
                mysqlquary = $"select id_equip from equipment where eq_img like '%1%';";
                myconnection = new MySqlConnection(mysqlconnection);
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myadapter = new MySqlDataAdapter(mycommand);
                myadapter.Fill(dtreward);
            }
            else if (chance <= 70)
            {
                mysqlquary = $"select id_equip from equipment where eq_img like '%2%';";
                myconnection = new MySqlConnection(mysqlconnection);
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myadapter = new MySqlDataAdapter(mycommand);
                myadapter.Fill(dtreward);
            }
            else if (chance <= 85)
            {
                mysqlquary = $"select id_equip from equipment where eq_img like '%3%';";
                myconnection = new MySqlConnection(mysqlconnection);
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myadapter = new MySqlDataAdapter(mycommand);
                myadapter.Fill(dtreward);
            }
            else if (chance <= 95)
            {
                mysqlquary = $"select id_equip from equipment where eq_img like '%4%';";
                myconnection = new MySqlConnection(mysqlconnection);
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myadapter = new MySqlDataAdapter(mycommand);
                myadapter.Fill(dtreward);
            }
            else if (chance <= 100)
            {
                mysqlquary = $"select id_equip from equipment where eq_img like '%5%';";
                myconnection = new MySqlConnection(mysqlconnection);
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myadapter = new MySqlDataAdapter(mycommand);
                myadapter.Fill(dtreward);
            }
            else
            {
                mysqlquary = $"select id_equip from equipment where eq_img like '%5%';";
                myconnection = new MySqlConnection(mysqlconnection);
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myadapter = new MySqlDataAdapter(mycommand);
                myadapter.Fill(dtreward);
            }

            myconnection.Open();
            string reward = dtreward.Rows[random.Next(0, dtreward.Rows.Count)][0].ToString();
            mysqlquary = $"insert into teaminventory (id_savefile,id_equip,equipwho) values ('{slot}','{reward}',0);";
            string rewardimage = reward.Substring(1, 1);
            Image = Resources.ResourceManager.GetObject(imagelist[(Convert.ToInt32(rewardimage))-1].ToString());
            pbox_reward.Image = (Image)Image;
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myreader = mycommand.ExecuteReader();
            myconnection.Close();
        }
    }
}
