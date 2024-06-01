using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Policy;

namespace RPG_II
{
    public partial class FormMainGame : Form
    {
        Thread thread;
        string mapdata,posdata, threatlevel;
        int slot;
        DataTable dtPlayer = new DataTable();
        SaveEditor Editor = new SaveEditor();

        string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
        MySqlConnection myconnection;
        MySqlCommand mycommand;
        MySqlDataAdapter myadapter;
        string mysqlquary;
        public FormMainGame(int slot)
        {
            this.slot = slot;
            Editor.SelectSaveSlot(slot.ToString());
            InitializeComponent();
            CoolTransition(17);
            ExtractData();
            AddToPanel("map");
            UpdateMoney(Convert.ToInt32(Editor.GetTeamData("cash")));
        }
        #region Functions
        private async void CoolTransition(int start)
        {
            if (pnl_main.Controls.Count > 0)
            {
                pnl_main.Controls.Clear();
            }
            int[] posx = { 12,  229, 333, 404, 471, 517, 547, 558, 570, 574, 576, 576, 576, 576, 576, 576, 576, 576, 576, 576, 574, 570, 558, 547, 517, 471, 404, 333, 229, 12,  12,  12,  12,  12,  12,  12,  12, 12};
            int[] posy = { 35,  35,  35,  35,  35,  35,  35,  35,  35,  35,  35,  35, 139, 209, 240, 261, 280, 295, 290, 290, 290, 290, 290, 290, 290, 290, 290, 290, 290, 290, 290, 295, 280, 261, 240, 209, 139, 35};
            int[] sizex = {1132,701, 488, 350, 214, 121, 60,  41,  20,  10,  5,   5,   5,   5,   5,   5,   5,   5,   5,  5, 10, 20, 41, 60, 121, 214, 350, 488, 701, 1132, 1132, 1132, 1132, 1132, 1132, 1132, 1132, 1132};
            int[] sizey = {524, 524, 524, 524, 524, 524, 524, 524, 524, 524, 524, 524, 298, 153, 85,  40,  20,  10,  5,  5, 5,  5,  5,  5,  5,   5,   5,   5,   5,   5,    5, 10, 20, 40, 85, 153, 298, 524};
            for (int i = start; i < 38; i++)
            {
                pnl_main.Location = new Point(posx[i], posy[i]);
                pnl_main.Size = new Size(sizex[i], sizey[i]);
                await Task.Delay(10);
            }
        }
        private void ExtractData()
        {
            mysqlquary = $"SELECT * FROM savefile where id_savefile like 'SF{slot}%';";
            myconnection = new MySqlConnection(mysqlconnection);
            mycommand = new MySqlCommand(mysqlquary, myconnection);
            myadapter = new MySqlDataAdapter(mycommand);
            myadapter.Fill(dtPlayer);

            string [] teamdata = dtPlayer.Rows[0][3].ToString().Split(' ');
            mapdata = teamdata[2].ToString().Substring(4);
            posdata = teamdata[4].ToString().Substring(4);
        }
        public void UpdateMoney(int cash)
        {
            lbl_cash.Text = "Cash: " + cash + " ඞ";
        }
        private void AddToPanel(string panel)
        {
            pnl_main.Controls.Clear();
            switch (panel)
            {
                case "map":
                    FormMap form = new FormMap(mapdata, posdata, slot.ToString(),0);
                    form.FormRef = this;
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Top;
                    pnl_main.Controls.Add(form);
                    form.Show();
                    break;
                case "mapwin":
                    FormMap formw = new FormMap(mapdata, posdata, slot.ToString(),1);
                    formw.FormRef = this;
                    formw.TopLevel = false;
                    formw.FormBorderStyle = FormBorderStyle.None;
                    formw.Dock = DockStyle.Top;
                    pnl_main.Controls.Add(formw);
                    formw.Show();
                    break;
                case "equipment":
                    FormEquipment forme = new FormEquipment(slot.ToString());
                    forme.TopLevel = false;
                    forme.FormBorderStyle = FormBorderStyle.None;
                    forme.Dock = DockStyle.Top;
                    pnl_main.Controls.Add(forme);
                    forme.Show();
                    break;
                case "gacha":
                    FormGacha formg = new FormGacha(slot.ToString(),0);
                    formg.TopLevel = false;
                    formg.FormRef = this;
                    formg.FormBorderStyle = FormBorderStyle.None;
                    formg.Dock = DockStyle.Top;
                    pnl_main.Controls.Add(formg);
                    formg.Show();
                    break;
                case "stats":
                    FormStats formc = new FormStats(slot.ToString());
                    formc.TopLevel = false;
                    formc.FormBorderStyle = FormBorderStyle.None;
                    formc.Dock = DockStyle.Top;
                    pnl_main.Controls.Add(formc);
                    formc.Show();
                    break;
                case "battle":
                    FormBattle formb = new FormBattle(slot.ToString(), threatlevel);
                    formb.TopLevel = false;
                    formb.FormRef = this;
                    formb.FormBorderStyle = FormBorderStyle.None;
                    formb.Dock = DockStyle.Top;
                    pnl_main.Controls.Add(formb);
                    formb.Show();
                    break;
            }
            
        }
        #endregion
        public async void StartBattle(string threatlevel)
        {
            this.threatlevel = threatlevel;
            CoolTransition(0);
            await Task.Delay(200);
            AddToPanel("battle");
        }

        private async void btn_check_Click(object sender, EventArgs e)
        {
          
            CoolTransition(0);
            await Task.Delay(200);
            AddToPanel("stats");
            
        }

        private async void btn_equipment_Click(object sender, EventArgs e)
        {
        
            CoolTransition(0);
            await Task.Delay(200);
            AddToPanel("equipment");
        }
        public async void Win()
        {
           
            CoolTransition(0);
            await Task.Delay(200);
            AddToPanel("mapwin");
        }
        public async void Lose()
        {
            CoolTransition(0);
            await Task.Delay(200);
            AddToPanel("map");
        }

        private async void btn_map_Click(object sender, EventArgs e)
        {
            CoolTransition(0);
            await Task.Delay(200);
            AddToPanel("map");
        }

        private async void btn_gacha_Click(object sender, EventArgs e)
        {
            CoolTransition(0);
            await Task.Delay(200);
            AddToPanel("gacha");
        }

        private void btn_exit_Click(object sender, EventArgs e)
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
        
    }
}
