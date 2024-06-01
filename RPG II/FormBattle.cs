using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Numerics;
using Google.Protobuf.Reflection;
using RPG_II.Properties;
using Org.BouncyCastle.Crypto;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace RPG_II
{
    public partial class FormBattle : Form
    {
        string slot, threatlevel, enemylevel, selectedskill, targetskill, targettype, costtype;
        int enemycount, selecteduser, skilluser,costval, targetselected;
        DataTable dtMainProcessor = new DataTable();
        Object Image;
        List<string> AvailableEnemy = new List<string>();
        Thread thread;

        string mysqlquary;
        string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
        int saveslot;
        MySqlConnection myconnection;
        MySqlCommand mycommand;
        MySqlDataAdapter myadapter;
        MySqlDataReader myreader;

        Random rng = new Random();
        SaveEditor Editor = new SaveEditor();
        Calculator Calc = new Calculator();
        public FormBattle(string slot, string threatlevel)
        {
            this.threatlevel = threatlevel;
            this.slot = slot;
            Editor.SelectSaveSlot(slot);
            Calc.GetSlotData(slot);
            InitializeComponent();
            InitiateProcessor();
            GenerateEntity();
            Gamespeed.Enabled = true;
        }
        public void InitiateProcessor()
        {
            // Basic Stats
            dtMainProcessor.Columns.Add("active"); // 0
            dtMainProcessor.Columns.Add("pboxid");
            dtMainProcessor.Columns.Add("type");
            dtMainProcessor.Columns.Add("ID");
            dtMainProcessor.Columns.Add("Image");
            dtMainProcessor.Columns.Add("Name");
            // Stats
            dtMainProcessor.Columns.Add("HP"); // 6
            dtMainProcessor.Columns.Add("MP");
            dtMainProcessor.Columns.Add("ATK");
            dtMainProcessor.Columns.Add("DEF");
            dtMainProcessor.Columns.Add("ARMOR"); // 10
            dtMainProcessor.Columns.Add("SPEED");
            dtMainProcessor.Columns.Add("SPECIAl");
            dtMainProcessor.Columns.Add("CRIT");
            dtMainProcessor.Columns.Add("TURNLIMIT");
            // 9 columns Effects
            dtMainProcessor.Columns.Add("FIRE");//15
            dtMainProcessor.Columns.Add("ICE");
            dtMainProcessor.Columns.Add("POISON");
            dtMainProcessor.Columns.Add("HASTE");
            dtMainProcessor.Columns.Add("POWER");
            dtMainProcessor.Columns.Add("CHARGE");
            dtMainProcessor.Columns.Add("FORT");
            dtMainProcessor.Columns.Add("FOCUS");
            dtMainProcessor.Columns.Add("REGEN");
            // 4 columns Effects
            dtMainProcessor.Columns.Add("RAGE");//24
            dtMainProcessor.Columns.Add("TURTLE");
            dtMainProcessor.Columns.Add("RECKLESS");
            dtMainProcessor.Columns.Add("DESPERATE");
            // 6 columns Effects
            dtMainProcessor.Columns.Add("BURN");//28
            dtMainProcessor.Columns.Add("SLOW");
            dtMainProcessor.Columns.Add("FROZEN");
            dtMainProcessor.Columns.Add("POISONED");
            dtMainProcessor.Columns.Add("BREAK");
            dtMainProcessor.Columns.Add("TARGETED");
            // Absolute Data
            dtMainProcessor.Columns.Add("HPINIT");//34
            dtMainProcessor.Columns.Add("MPINIT");
            dtMainProcessor.Columns.Add("ATKINIT");
            dtMainProcessor.Columns.Add("DEFINIT");
            dtMainProcessor.Columns.Add("ARMORINIT");//38
            dtMainProcessor.Columns.Add("SPEEDINIT");
            dtMainProcessor.Columns.Add("SPECIAlINIT");
            dtMainProcessor.Columns.Add("CRITINIT");

            dgv_debug.DataSource = dtMainProcessor;

            // Enemy Spawner
            if (threatlevel == "MT_1")
            {
                enemylevel = "safe";
            }
            else if (threatlevel == "MT_2")
            {
                enemylevel = "low";
            }
            else if (threatlevel == "MT_3")
            {
                enemylevel = "med";
            }
            else if (threatlevel == "MT_Danger")
            {
                enemylevel = "high";
            }
            else if (threatlevel == "MT_boss")
            {
                enemylevel = "boss";
            }
            DataTable dttemp = new DataTable();
            mysqlquary = $"SELECT * FROM enemy e where e_loc = '{enemylevel}';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dttemp);
            enemycount = rng.Next(1, 5);
            string[] pboxidname = { "e1", "e2", "e3", "e4" ,"eb" };
            if (enemylevel == "boss")
            {
                int row = 0;
                string name = dttemp.Rows[row][2].ToString();
                string image = dttemp.Rows[row][11].ToString();
                image = image.Substring(0, image.Length - 4);
                string id = dttemp.Rows[row][0].ToString();
                string hp = dttemp.Rows[row][4].ToString();
                string mp = "9999";
                string atk = dttemp.Rows[row][5].ToString();
                string def = dttemp.Rows[row][6].ToString();
                string armor = dttemp.Rows[row][7].ToString();
                string speed = dttemp.Rows[row][8].ToString();
                string special = dttemp.Rows[row][9].ToString();
                string crit = dttemp.Rows[row][10].ToString();
                dtMainProcessor.Rows.Add(1, eb, "enemy", image, name, id, hp, mp, atk, def, armor, speed, special, crit, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, hp, mp, atk, def, armor, speed, special, crit);
                AvailableEnemy.Add("eb");
                
            }
            else
            {
                for (int i = 0; i < enemycount; i++)
                {
                    int row = rng.Next(0, dttemp.Rows.Count);
                    string name = dttemp.Rows[row][2].ToString();
                    string image = dttemp.Rows[row][11].ToString();
                    image = image.Substring(0, image.Length - 4);
                    string id = dttemp.Rows[row][0].ToString();
                    string hp = dttemp.Rows[row][4].ToString();
                    string mp = "9999";
                    string atk = dttemp.Rows[row][5].ToString();
                    string def = dttemp.Rows[row][6].ToString();
                    string armor = dttemp.Rows[row][7].ToString();
                    string speed = dttemp.Rows[row][8].ToString();
                    string special = dttemp.Rows[row][9].ToString();
                    string crit = dttemp.Rows[row][10].ToString();
                    dtMainProcessor.Rows.Add(1, pboxidname[i], "enemy", image, name, id, hp, mp, atk, def, armor, speed, special, crit, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, hp, mp, atk, def, armor, speed, special, crit);
                }
                for (int i = 0; i < dtMainProcessor.Rows.Count - 4; i++)
                {
                    AvailableEnemy.Add(dtMainProcessor.Rows[i][1].ToString());
                }
            }
            
            // Player Spawner
            string[] pboxidnamep = { "p1", "p2", "p3", "p4"};
            for (int i = 1;i < 5; i++)
            {
                string name = Editor.GetPlayerData(i, "name");
                string id = Editor.GetPlayerData(i, "id");
                string hp = Convert.ToString(Calc.PlayerCalculator("HP", i, slot));
                string mp = Convert.ToString(Calc.PlayerCalculator("MP", i, slot));
                string atk = Convert.ToString(Calc.PlayerCalculator("ATK", i, slot));
                string def = Convert.ToString(Calc.PlayerCalculator("DEF", i, slot));
                string armor = Convert.ToString(Calc.PlayerCalculator("ARMOR", i, slot));
                string speed = Convert.ToString(Calc.PlayerCalculator("SPEED", i, slot));
                string special = Convert.ToString(Calc.PlayerCalculator("SPECIAL", i, slot));
                string crit = Convert.ToString(Calc.PlayerCalculator("CRIT", i, slot));
                dtMainProcessor.Rows.Add(1, pboxidnamep[i-1], "player",name+".png", name, id, hp, mp, atk, def, armor, speed, special, crit, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, hp, mp, atk, def, armor, speed, special, crit);
            }
        }
        public FormMainGame FormRef { get; set; }
        private void e4_Click(object sender, EventArgs e)
        {

        }

        private void e3_Click(object sender, EventArgs e)
        {

        }

        private void Gamespeed_tick(object sender, EventArgs e)
        {
            //CheckWin();
            CheckHP();
            int speed = 0;
            int turn = 0;
            for (int i = 0; i < dtMainProcessor.Rows.Count; i++)
            {
                speed = Convert.ToInt32(dtMainProcessor.Rows[i][11].ToString());
                turn = Convert.ToInt32(dtMainProcessor.Rows[i][14].ToString());
                if (dtMainProcessor.Rows[i][0].ToString() != "0")
                {
                    turn = turn + speed;
                }
                foreach(Panel pnl in pbox_terrain.Controls.OfType<Panel>())
                {
                    if (pnl.Name.ToString().Contains(dtMainProcessor.Rows[i][1].ToString()) && pnl.Name.ToString().Contains("TURN"))
                    {
                        pnl.Size = new Size(turn / 100, 3);
                    }
                }
                #region status effect
                for(int k =15;k<34;k++)
                if (Convert.ToInt32(dtMainProcessor.Rows[i][k]) > 0)
                {
                        dtMainProcessor.Rows[i][k] = Convert.ToInt32(dtMainProcessor.Rows[i][k]) - 1;
                }

                #endregion
                dtMainProcessor.Rows[i][14] = turn.ToString();
                if (turn > 7000)
                {
                    Gamespeed.Enabled = false;
                    EntityTurn(dtMainProcessor.Rows[i][1].ToString());
                    break;
                }
            }
        }
        private void CheckWin()
        {
            int deadenemy = 0;
            int deadplayer = 0;
            for (int i = 0; i < dtMainProcessor.Rows.Count - 4; i++)
            {
                if (dtMainProcessor.Rows[i][0].ToString() == "0")
                {
                    deadenemy = deadenemy + 1;
                }
            }
            for (int i = dtMainProcessor.Rows.Count - 4; i < dtMainProcessor.Rows.Count; i++)
            {
                if (dtMainProcessor.Rows[i][0].ToString() == "0")
                {
                    deadplayer = deadplayer + 1;
                }
            }
            if (deadenemy == dtMainProcessor.Rows.Count - 4)
            {

                FormRef.Win();
            }
            if (deadplayer == 4)
            {
                FormRef.Lose();
            }

        }
        private void CheckHP()
        {
            for (int i = 0; i < dtMainProcessor.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtMainProcessor.Rows[i][6].ToString())<0)
                {
                    dtMainProcessor.Rows[i][0] = 0;
                    Image = Resources.ResourceManager.GetObject("RIP");
                    foreach(PictureBox pbox in this.Controls.OfType<PictureBox>())
                    {
                        if (pbox.Name.ToString().Contains(dtMainProcessor.Rows[i][1].ToString()))
                        {
                            pbox.Image = (System.Drawing.Image)Image;
                        }
                    }
                }
            }
        }
        private void UpdateHealthBar()
        {
            for(int i = 0;i < dtMainProcessor.Rows.Count;i++)
            {
                int inithp = Convert.ToInt32(dtMainProcessor.Rows[i][34].ToString());
                int hp = Convert.ToInt32(dtMainProcessor.Rows[i][6].ToString());
                string id = dtMainProcessor.Rows[i][1].ToString();
                foreach(Panel p in pbox_terrain.Controls.OfType<Panel>())
                {
                    if (p.Name.ToString().Contains(id) && p.Name.ToString().Contains("HP"))
                    {
                        double valx = 70 * (Convert.ToDouble(hp) / Convert.ToDouble(inithp));
                        p.Size = new Size(Convert.ToInt32(valx), 4);
                    }
                }
            }

        }
        private void UpdateManaBar()
        {
            for (int i = 0; i < dtMainProcessor.Rows.Count; i++)
            {
                int initmp = Convert.ToInt32(dtMainProcessor.Rows[i][35].ToString());
                int mp = Convert.ToInt32(dtMainProcessor.Rows[i][7].ToString());
                string id = dtMainProcessor.Rows[i][1].ToString();
                foreach (Panel p in pbox_terrain.Controls.OfType<Panel>())
                {
                    if (p.Name.ToString().Contains(id) && p.Name.ToString().Contains("MP"))
                    {
                        int valx = 70 * (mp / initmp);
                        p.Size = new Size(valx, 3);
                    }
                }
            }
        }
        private void EntityTurn(string entity)
        {
            PictureBox[] pbox_entity = { eb, e1, e2, e3, e4, p1, p2, p3, p4 };
            if (entity.Contains("p"))
            {
                for (int i = 0; i < pbox_entity.Length; i++)
                {
                    if (pbox_entity[i].Name.ToString() == entity)
                    {
                        PlayerTurn(pbox_entity[i]);
                    }
                }
            }
            else if (entity.Contains("e"))
            {
                selecteduser = FindID(entity);
                while (true)
                {
                    int diceroll = rng.Next(1, 101);
                    if (diceroll <= 25)
                    {
                        if (dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 4][0].ToString() != "0")
                        {
                            EnemyTurn(dtMainProcessor.Rows.Count - 4);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (diceroll <= 50)
                    {
                        if (dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 3][0].ToString() != "0")
                        {
                            EnemyTurn(dtMainProcessor.Rows.Count - 3);
                            break;
                        }
                        else
                        {
                            
                            continue;
                        }
                    }
                    else if (diceroll <= 75)
                    {
                        if (dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 2][0].ToString() != "0")
                        {
                            EnemyTurn(dtMainProcessor.Rows.Count - 2);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (diceroll <= 100)
                    {
                        if (dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 1][0].ToString() != "0")
                        {
                            EnemyTurn(dtMainProcessor.Rows.Count - 1);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
        
        private void EnemyTurn(int id)
        {
            DataTable dtenemyability = new DataTable();
            mysqlquary = $"SELECT e.*, s.skill_type FROM enemyskillset e ,skill s where e.id_skill = s.id_skill and id_enemy = '{dtMainProcessor.Rows[selecteduser][3].ToString()}';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dtenemyability);
            int percentage = 10;
            int atk = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][8].ToString());
            int crit = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][13].ToString());
            int special = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][12].ToString());
            int tdef = 0;
            int tarm = 0;
            int damage = 0;
            if (percentage <= rng.Next(1, 101) && dtenemyability.Rows.Count != 0)
            {
                targetskill = "single";
                switch (dtenemyability.Rows[0][4].ToString())
                {
                    case "DAMG":
                        if (targetskill == "single")
                        {
                            tdef = Convert.ToInt32(dtMainProcessor.Rows[id][9].ToString());
                            tarm = Convert.ToInt32(dtMainProcessor.Rows[id][10].ToString());
                            damage = Calc.SkillDamageCalculator(10, atk, special, crit, tdef, tarm);
                            dtMainProcessor.Rows[id][6] = Convert.ToInt32(dtMainProcessor.Rows[id][6]) - damage;
                        }
                        else
                        {
                            for (int i = 0; i < AvailableEnemy.Count; i++)
                            {
                                tdef = Convert.ToInt32(dtMainProcessor.Rows[id][9].ToString());
                                tarm = Convert.ToInt32(dtMainProcessor.Rows[id][9].ToString());
                                damage = Calc.SkillDamageCalculator(10, atk, special, crit, tdef, tarm);
                                dtMainProcessor.Rows[id][6] = Convert.ToInt32(dtMainProcessor.Rows[id][6]) - damage;
                            }
                        }
                        break;
                    case "HEAL":
                        if (targetskill == "single")
                        {
                            int hp = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][6].ToString());
                            int hpinit = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][34].ToString());
                            dtMainProcessor.Rows[selecteduser][6] = Calc.HealCalculator(10, special, hp, hpinit);
                        }
                        else
                        {
                            for (int i = dtMainProcessor.Rows.Count - 4; i < dtMainProcessor.Rows.Count; i++)
                            {
                                int hp = Convert.ToInt32(dtMainProcessor.Rows[i][6].ToString());
                                int hpinit = Convert.ToInt32(dtMainProcessor.Rows[i][34].ToString());
                                int finalhp = Calc.HealCalculator(10, special, hp, hpinit);
                            }
                        }
                        break;
                    case "STTS":
                        CastStatuesEffect();
                        break;

                }
            }
            else // normal attack
            {

                tdef = Convert.ToInt32(dtMainProcessor.Rows[id][9].ToString());
                tarm = Convert.ToInt32(dtMainProcessor.Rows[id][10].ToString());
                damage = Calc.DamageCalculator(atk, crit, tdef, tarm);
                dtMainProcessor.Rows[id][6] = Convert.ToInt32(dtMainProcessor.Rows[id][6]) - damage;
                EndTurn();
            }
        }
        private async void PlayerTurn(PictureBox pbox)
        {
            string id = "";
            int idindex = 0;
            int x = pbox.Location.X;
            int y = pbox.Location.Y;
            //pbox.BorderStyle = BorderStyle.FixedSingle;
            skilluser = FindID(pbox.Name.ToString());
            Button btnna = new Button();
            Button btns1 = new Button();
            Button btns2 = new Button();
            Button btns3 = new Button();
            for (int i = 0; i < dtMainProcessor.Rows.Count; i++)
            {
                if (dtMainProcessor.Rows[i][1].ToString()==pbox.Name.ToString())
                {
                    id = dtMainProcessor.Rows[i][5].ToString();
                    idindex = i;
                    break;
                }
            }

            //Generating Button

            DataTable dttemp = new DataTable();
            mysqlquary = $"SELECT c.id_skill, s.skill_name , s.skill_cost, s.skill_cost_type FROM classskillset c, skill s where c.id_skill = s.id_skill and id_pclass = '{id}';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dttemp);

            btnna.Location = new Point(x + 70, y-20);
            btnna.Name = idindex + "-1*"+ dttemp.Rows[0][3].ToString() + "-" + dttemp.Rows[0][2].ToString();
            btnna.Text = dttemp.Rows[0][1].ToString();
            btnna.Tag = dttemp.Rows[0][0].ToString();
            btnna.Click += SkillPrepare;
            pbox_terrain.Controls.Add(btnna);

            btns1.Location = new Point(x + 70, y+5);
            btns1.Name = idindex + "-2*" + dttemp.Rows[1][3].ToString() + "-"+ dttemp.Rows[1][2].ToString();
            btns1.Text = dttemp.Rows[1][1].ToString();
            btns1.Tag = dttemp.Rows[1][0].ToString();
            btns1.Click += SkillPrepare;
            pbox_terrain.Controls.Add(btns1);

            btns2.Location = new Point(x + 150, y-20);
            btns2.Name = idindex + "-3*" + dttemp.Rows[2][3].ToString() + "-" + dttemp.Rows[2][2].ToString();
            btns2.Text = dttemp.Rows[2][1].ToString();
            btns2.Tag = dttemp.Rows[2][0].ToString();
            btns2.Click += SkillPrepare;
            pbox_terrain.Controls.Add(btns2);

            btns3.Location = new Point(x + 150, y+5);
            btns3.Name = idindex + "-4*" + dttemp.Rows[3][3].ToString() + "-" + dttemp.Rows[3][2].ToString();
            btns3.Text = dttemp.Rows[3][1].ToString();
            btns3.Tag = dttemp.Rows[3][0].ToString();
            btns3.Click += SkillPrepare;
            pbox_terrain.Controls.Add(btns3);

            //Checking for Valid


            int HP = Convert.ToInt32(dtMainProcessor.Rows[idindex][6].ToString());
            int MP = Convert.ToInt32(dtMainProcessor.Rows[idindex][7].ToString());
            int cash = Convert.ToInt32(Editor.GetTeamData("cash"));
            int cost = 0;
            Button[] btnlist = { btns1, btns2, btns3 };

            selecteduser = idindex;


            for (int i = 0; i < 3; i++)
            {
                cost = Convert.ToInt32(dttemp.Rows[i][2].ToString());
                if (dttemp.Rows[i][3].ToString() == "HP")
                {
                    if (HP <= cost)
                    {
                        btnlist[i].Enabled = false;
                    }
                }
                if (dttemp.Rows[i][3].ToString() == "MP")
                {
                    if (MP < cost)
                    {
                        btnlist[i].Enabled = false;
                    }
                }
                if (dttemp.Rows[i][3].ToString() == "CS")
                {
                    if (cash < cost)
                    {
                        btnlist[i].Enabled = false;
                    }
                }
            }

        }
        private void SkillPrepare(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            string[] data = btn.Name.ToString().Split('*');
            string[] cost = data[1].ToString().Split('-');
            costtype = cost[0];
            costval = Convert.ToInt32(cost[1]);

            DataTable dtskill = new DataTable();
            mysqlquary = $"SELECT * FROM skill s where id_skill = '{btn.Tag.ToString()}';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dtskill);

            List<string>availabletarget=new List<string>();
            PictureBox[] pbox_entity = { eb, e1, e2, e3, e4, p1, p2, p3, p4 };
            for(int i = 0;i< pbox_entity.Length; i++)
            {
                for(int j = 0; j< dtMainProcessor.Rows.Count; j++)
                {
                    if (dtMainProcessor.Rows[j][1].ToString() == pbox_entity[i].Name.ToString())
                    {
                        availabletarget.Add(dtMainProcessor.Rows[j][1].ToString());
                        break;
                    }
                }
            }
            selectedskill = "";
            targettype = "";
            switch (dtskill.Rows[0][4].ToString())
            {
                case "TS":
                    CreateTarget(pbox_entity,availabletarget,"e", dtskill.Rows[0][0].ToString());
                    targetskill = "single";
                    targettype = "enemy";
                    break;
                case "TA":
                    CreateTarget(pbox_entity, availabletarget, "e", dtskill.Rows[0][0].ToString());
                    targetskill = "all";
                    targettype = "enemy";
                    break;
                case "TF":
                    CreateTarget(pbox_entity, availabletarget, "p", dtskill.Rows[0][0].ToString());
                    targetskill = "single";
                    targettype = "ally";
                    break;
                case "TP":
                    CreateTarget(pbox_entity, availabletarget, "p", dtskill.Rows[0][0].ToString());
                    targetskill = "all";
                    targettype = "ally";
                    break;
                case "TE":
                    CreateTarget(pbox_entity, availabletarget, "", dtskill.Rows[0][0].ToString());
                    targetskill = "all";
                    targettype = "all";
                    break;
                case "TM":
                    CreateTarget(pbox_entity, availabletarget, dtMainProcessor.Rows[Convert.ToInt32(btn.Name.ToString().Substring(0, 1))][1].ToString(), dtskill.Rows[0][0].ToString());
                    targetskill = "me";
                    targettype = "me";
                    break;
            }
        }
        private void CreateTarget(PictureBox[]pbox_entity, List<string> availabletarget, string whomst, string skill_id)
        {
            ClearTarget();
            for (int j = 0; j < dtMainProcessor.Rows.Count; j++)
            {
                if (availabletarget.Contains(dtMainProcessor.Rows[j][1].ToString()) && dtMainProcessor.Rows[j][1].ToString().Contains(whomst))
                {
                    for (int i = 0; i < pbox_entity.Length; i++)
                    {
                        if (pbox_entity[i].Name == dtMainProcessor.Rows[j][1].ToString())
                        {
                            pbox_entity[i].BorderStyle = BorderStyle.FixedSingle;
                            selectedskill = skill_id;
                            pbox_entity[i].Click += SkillUse;
                        }
                    }
                }
            }
        }
        private void ClearTarget()
        {
            foreach (PictureBox pbox in this.Controls.OfType<PictureBox>())
            {
                if (pbox.BorderStyle == BorderStyle.FixedSingle)
                {
                    pbox.BorderStyle = BorderStyle.None;
                    pbox.Click -= SkillUse;
                }
            }
        }
        private void SkillUse(object sender, EventArgs e)
        {
            PictureBox pbox = sender as PictureBox; 
            DataTable dtskill = new DataTable();
            mysqlquary = $"SELECT * FROM skill s where id_skill = '{selectedskill}';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dtskill);
            int damage = 0;
            int val = Convert.ToInt32(dtskill.Rows[0][5].ToString());
            int atk = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][8].ToString());
            int special = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][12].ToString());
            int crit = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][13].ToString());
            int tdef = 0;
            int tarm = 0;

            switch (dtskill.Rows[0][3].ToString())
            {
                case "SATK":
                    string[] data = pbox.Tag.ToString().Split('-');
                    int id = FindID(data[1]);
                    tdef = Convert.ToInt32(dtMainProcessor.Rows[id][9].ToString());
                    tarm = Convert.ToInt32(dtMainProcessor.Rows[id][10].ToString());
                    damage = Calc.DamageCalculator(atk, crit, tdef, tarm);
                    dtMainProcessor.Rows[id][6] = Convert.ToInt32(dtMainProcessor.Rows[id][6]) - damage;
                    break;
                case "DAMG":
                    string[] datas = pbox.Tag.ToString().Split('-');
                    if (targetskill == "single")
                    {
                        int ids = FindID(datas[1]);
                        tdef = Convert.ToInt32(dtMainProcessor.Rows[ids][9].ToString());
                        tarm = Convert.ToInt32(dtMainProcessor.Rows[ids][10].ToString());
                        damage = Calc.SkillDamageCalculator(val, atk, special, crit, tdef, tarm);
                        dtMainProcessor.Rows[ids][6] = Convert.ToInt32(dtMainProcessor.Rows[ids][6]) - damage;
                    }
                    else
                    {
                        for (int i = 0; i < AvailableEnemy.Count; i++)
                        {
                            int ids = FindID(AvailableEnemy[i]);
                            tdef = Convert.ToInt32(dtMainProcessor.Rows[ids][9].ToString());
                            tarm = Convert.ToInt32(dtMainProcessor.Rows[ids][9].ToString());
                            damage = Calc.SkillDamageCalculator(val, atk, special, crit, tdef, tarm);
                            dtMainProcessor.Rows[ids][6] = Convert.ToInt32(dtMainProcessor.Rows[ids][6]) - damage;
                        }
                    }
                    break;
                case "HEAL":
                    if (targetskill == "single")
                    {
                        string[] datap = pbox.Tag.ToString().Split('-');
                        int idp = FindID(datap[1]);
                        int hp = Convert.ToInt32(dtMainProcessor.Rows[idp][6].ToString());
                        int hpinit = Convert.ToInt32(dtMainProcessor.Rows[idp][34].ToString());
                        int finalhp = Calc.HealCalculator(val, special, hp, hpinit);
                    }
                    else
                    {
                        for (int i = dtMainProcessor.Rows.Count-4; i < dtMainProcessor.Rows.Count; i++)
                        {
                            int hp = Convert.ToInt32(dtMainProcessor.Rows[i][6].ToString());
                            int hpinit = Convert.ToInt32(dtMainProcessor.Rows[i][34].ToString());
                            int finalhp = Calc.HealCalculator(val, special, hp, hpinit);
                        }
                    }
                    break;
                case "STTS":
                    CastStatuesEffect();
                    break;
            }
            EndTurn();
        }
        private void EndTurn()
        {
            dtMainProcessor.Rows[selecteduser][14] = 0;
            Gamespeed.Enabled = true;
            ClearTarget();
            EraseButton();
            PayPrice();
            UpdateHealthBar();
            UpdateManaBar();
            CheckWin();
        }
        private void PayPrice()
        {
            switch (costtype)
            {
                case "HP":
                    dtMainProcessor.Rows[selecteduser][6] = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][6].ToString()) - costval;
                    break;
                case "MP":
                    dtMainProcessor.Rows[selecteduser][7] = Convert.ToInt32(dtMainProcessor.Rows[selecteduser][7].ToString()) - costval;
                    break;
                case "CS":
                    int cash = Convert.ToInt32(Editor.GetTeamData("cash"));
                    cash = cash - costval;
                    Editor.ChangeSaveData("cash", cash.ToString());
                    FormRef.UpdateMoney(cash);
                    break;
            }
        }
        private void EraseButton()
        {
            foreach (Button btn in pbox_terrain.Controls.OfType<Button>())
            {
                pbox_terrain.Controls.Remove(btn);
            }
            foreach (Button btn in pbox_terrain.Controls.OfType<Button>())
            {
                pbox_terrain.Controls.Remove(btn);
            }
            foreach (Button btn in pbox_terrain.Controls.OfType<Button>())
            {
                pbox_terrain.Controls.Remove(btn);
            }
            foreach (Button btn in pbox_terrain.Controls.OfType<Button>())
            {
                pbox_terrain.Controls.Remove(btn);
            }
            foreach (Button btn in pbox_terrain.Controls.OfType<Button>())
            {
                pbox_terrain.Controls.Remove(btn);
            }
        }
        private void CastStatuesEffect()
        {
            DataTable dteffect = new DataTable();
            mysqlquary = $"SELECT * FROM skilleffect s where id_skill = '{selectedskill}';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dteffect);
            for(int i = 0; i < dteffect.Rows.Count; i++)
            {
                int duration = Convert.ToInt32(dteffect.Rows[i][3].ToString()) * 10;
                int affected = 0;
                string id = dteffect.Rows[i][1].ToString();
                string who = dteffect.Rows[i][2].ToString();
                switch (id)
                {
                    case "B01":
                        affected = 15;
                        break;
                    case "B02":
                        affected = 16;
                        break;
                    case "B03":
                        affected = 17;
                        break;
                    case "B04":
                        affected = 18;
                        break;
                    case "B05":
                        affected = 19;
                        break;
                    case "B06":
                        affected = 20;
                        break;
                    case "B07":
                        affected = 21;
                        break;
                    case "B09":
                        affected = 22;
                        break;
                    case "B10":
                        affected = 23;
                        break;
                    case "S01":
                        affected = 24;
                        break;
                    case "S02":
                        affected = 25;
                        break;
                    case "S03":
                        affected = 26;
                        break;
                    case "S04":
                        affected = 27;
                        break;
                    case "D01":
                        affected = 28;
                        break;
                    case "D02":
                        affected = 29;
                        break;
                    case "D03":
                        affected = 30;
                        break;
                    case "D04":
                        affected = 31;
                        break;
                    case "D05":
                        affected = 32;
                        break;
                    case "D06":
                        affected = 33;
                        break;
                    default:
                        affected = 15;
                        break;
                }
                switch (who)
                {
                    case "T":
                        dtMainProcessor.Rows[selecteduser][affected] = duration;
                        break;
                    case "M":
                        dtMainProcessor.Rows[skilluser][affected] = duration;
                        break;
                    case "P":
                        dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 1][affected] = duration;
                        dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 2][affected] = duration;
                        dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 3][affected] = duration;
                        dtMainProcessor.Rows[dtMainProcessor.Rows.Count - 4][affected] = duration;
                        break;
                    case "E":
                        for (int ic = 0; ic < AvailableEnemy.Count; ic++)
                        {
                            dtMainProcessor.Rows[FindID(AvailableEnemy[ic])][affected] = duration;
                        }
                        break;
                    case "A":
                        for (int ic = 0; ic < dtMainProcessor.Rows.Count; ic++)
                        {
                            dtMainProcessor.Rows[i][affected] = duration;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private int FindID(string pboxname)
        {
            int result = 0;
            for(int i = 0; i < dtMainProcessor.Rows.Count; i++)
            {
                if (pboxname == dtMainProcessor.Rows[i][1].ToString())
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        private void GenerateEntity()
        {
            
            PictureBox[] pbox_entity = { eb, e1, e2, e3, e4, p1, p2, p3, p4 };
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(1, "name"));
            p1.Image = (System.Drawing.Image)Image;
            GenerateBar(p1);
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(2, "name"));
            p2.Image = (System.Drawing.Image)Image;
            GenerateBar(p2);
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(3, "name"));
            p3.Image = (System.Drawing.Image)Image;
            GenerateBar(p3);
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(4, "name"));
            p4.Image = (System.Drawing.Image)Image;
            GenerateBar(p4);

            for (int i = 0; i < enemycount; i++)
            {
                Image = Resources.ResourceManager.GetObject(dtMainProcessor.Rows[i][3].ToString());
                pbox_entity[i+1].Image = (System.Drawing.Image)Image;
                GenerateBar(pbox_entity[i + 1]);

            }
            
        }
        private void GenerateBar(PictureBox pbox)
        {
            pbox.Tag = pbox.Tag.ToString() + "-active";

            int x = pbox.Location.X;
            int y = pbox.Location.Y;
            Panel pnl = new Panel();
            pnl.Name = pbox.Name + "_HP";
            pnl.Tag = pbox.Name + "_HP";
            pnl.Location = new Point(x, y - 8);
            pnl.Size = new Size(70, 4);
            pnl.BackColor = Color.GreenYellow;
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pbox_terrain.Controls.Add(pnl);

            Panel pnld = new Panel();
            pnld.Name = pbox.Name + "_MP";
            pnld.Tag = pbox.Name + "_MP";
            pnld.Location = new Point(x, y - 5);
            pnld.Size = new Size(70, 3);
            pnld.BackColor = Color.LightBlue;
            pnld.BorderStyle = BorderStyle.FixedSingle;
            pbox_terrain.Controls.Add(pnld);

            Panel pnls = new Panel();
            pnls.Name = pbox.Name + "_TURN";
            pnls.Tag = pbox.Name + "_TURN";
            pnls.Location = new Point(x, y - 12);
            pnls.Size = new Size(70, 3);
            pnls.BackColor = Color.Yellow;
            pnls.BorderStyle = BorderStyle.FixedSingle;
            pbox_terrain.Controls.Add(pnls);

            //Label lbl = new Label();
            //lbl.Name = pbox.Name + "_HP";
            //lbl.Location = new Point(x, y - 10);
            //for (int i = 0; i < dtMainProcessor.Rows.Count; i++)
            //{
            //    if (dtMainProcessor.Rows[i][1].ToString().Contains(pbox.Name.ToString()))
            //    {
            //        lbl.Text = "HP: " + dtMainProcessor.Rows[i][6].ToString();
            //    }
            //}
            //pbox_terrain.Controls.Add(lbl);

        }
    }
}
