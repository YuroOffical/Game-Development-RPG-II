using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Policy;
using RPG_II.Properties;
using System.Reflection;

namespace RPG_II
{
    public partial class FormSaveFile : Form
    {
        int state;
        int selectedsavefile;
        Thread thread;
        string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
        MySqlConnection myconnection;
        MySqlCommand mycommand;
        MySqlDataAdapter myadapter;
        MySqlDataReader myreader;
        string mysqlquary;
        DataTable dtsavefile1 = new DataTable();
        DataTable dtsavefile2 = new DataTable();
        DataTable dtsavefile3 = new DataTable();
        DataTable dtsavefile4 = new DataTable();
        Object Image;

        public FormSaveFile(int state)
        {
            InitializeComponent();
            this.state = state;
            CreatePreset();
            Checksavefile();
        }
        private void CreatePreset()
        {
            string[] savefiledataid = { "SF1", "SF2", "SF3", "SF4" };
            for (int i = 0; i < 4; i++)
            {
                updatesavedata(i, savefiledataid[i]);
            }
            

            Button[] butdel = { btn_del1, btn_del2 ,btn_del3,btn_del4};
            int counter = 1;
            foreach(Button button in butdel)
            {
                button.Click += deletesave;
                button.Tag = $"{counter}";
                counter++;
            }
        }
        private void Checksavefile()
        {
            CreatePreset();
            if (dtsavefile1.Rows[0][1].ToString() == "")
            {
                resetsavefile(1);
            }
            else
            {
                givedatafile(1);
            }
            if (dtsavefile2.Rows[0][1].ToString() == "")
            {
                resetsavefile(2);
            }
            else
            {
                givedatafile(2);
            }
            if (dtsavefile3.Rows[0][1].ToString() == "")
            {
                resetsavefile(3);
            }
            else
            {
                givedatafile(3);
            }
            if (dtsavefile4.Rows[0][1].ToString() == "")
            {
                resetsavefile(4);
            }
            else
            {
                givedatafile(4);
            }
        }
        private void givedatafile(int slot)
        {
            string[] datateam;
            string[] datap1;
            string[] datap2;
            string[] datap3;
            string[] datap4;
            switch (slot)
            {
                case 1:
                    PictureBox [] pboxlist1 = { pbox_11, pbox_12, pbox_13, pbox_14 };
                    Label[] lbllist1 = { lbl_lvl_1_1, lbl_lvl_1_2, lbl_lvl_1_3, lbl_lvl_1_4 };
                    datateam = dtsavefile1.Rows[0][3].ToString().Split(' ');
                    datap1 = dtsavefile1.Rows[1][3].ToString().Split(' ');
                    datap2 = dtsavefile1.Rows[2][3].ToString().Split(' ');
                    datap3 = dtsavefile1.Rows[3][3].ToString().Split(' ');
                    datap4 = dtsavefile1.Rows[4][3].ToString().Split(' ');
                    Image = Resources.ResourceManager.GetObject(datap1[1].ToString());
                    pboxlist1[0].Image = (Image)Image;
                    lbllist1[0].Text = $"Level: {datap1[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap2[1].ToString());
                    pboxlist1[1].Image = (Image)Image;
                    lbllist1[1].Text = $"Level: {datap2[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap3[1].ToString());
                    pboxlist1[2].Image = (Image)Image;
                    lbllist1[2].Text = $"Level: {datap3[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap4[1].ToString());
                    pboxlist1[3].Image = (Image)Image;
                    lbllist1[3].Text = $"Level: {datap4[8].ToString()}";
                    lbl_1.Text = datateam[0].ToString().Replace('-', ' ').Substring(5);
                    btn_con1.Tag = "1";
                    btn_con1.Click += PlaytheGame;
                    break;
                case 2:
                    PictureBox[] pboxlist2 = { pbox_21, pbox_22, pbox_23, pbox_24 };
                    Label[] lbllist2 = { lbl_lvl_2_1, lbl_lvl_2_2, lbl_lvl_2_3, lbl_lvl_2_4 };
                    datateam = dtsavefile2.Rows[0][3].ToString().Split(' ');
                    datap1 = dtsavefile2.Rows[1][3].ToString().Split(' ');
                    datap2 = dtsavefile2.Rows[2][3].ToString().Split(' ');
                    datap3 = dtsavefile2.Rows[3][3].ToString().Split(' ');
                    datap4 = dtsavefile2.Rows[4][3].ToString().Split(' ');
                    Image = Resources.ResourceManager.GetObject(datap1[1].ToString());
                    pboxlist2[0].Image = (Image)Image;
                    lbllist2[0].Text = $"Level: {datap1[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap2[1].ToString());
                    pboxlist2[1].Image = (Image)Image;
                    lbllist2[1].Text = $"Level: {datap2[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap3[1].ToString());
                    pboxlist2[2].Image = (Image)Image;
                    lbllist2[2].Text = $"Level: {datap3[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap4[1].ToString());
                    pboxlist2[3].Image = (Image)Image;
                    lbllist2[3].Text = $"Level: {datap4[8].ToString()}";
                    lbl_2.Text = datateam[0].ToString().Replace('-', ' ').Substring(5);
                    btn_con2.Tag = "2";
                    btn_con2.Click += PlaytheGame;
                    break;
                case 3:
                    PictureBox[] pboxlist3 = { pbox_31, pbox_32, pbox_33, pbox_34 };
                    Label[] lbllist3 = { lbl_lvl_3_1, lbl_lvl_3_2, lbl_lvl_3_3, lbl_lvl_3_4 };
                    datateam = dtsavefile3.Rows[0][3].ToString().Split(' ');
                    datap1 = dtsavefile3.Rows[1][3].ToString().Split(' ');
                    datap2 = dtsavefile3.Rows[2][3].ToString().Split(' ');
                    datap3 = dtsavefile3.Rows[3][3].ToString().Split(' ');
                    datap4 = dtsavefile3.Rows[4][3].ToString().Split(' ');
                    Image = Resources.ResourceManager.GetObject(datap1[1].ToString());
                    pboxlist3[0].Image = (Image)Image;
                    lbllist3[0].Text = $"Level: {datap1[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap2[1].ToString());
                    pboxlist3[1].Image = (Image)Image;
                    lbllist3[1].Text = $"Level: {datap2[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap3[1].ToString());
                    pboxlist3[2].Image = (Image)Image;
                    lbllist3[2].Text = $"Level: {datap3[8].ToString()}";
                    Image = Resources.ResourceManager.GetObject(datap4[1].ToString());
                    pboxlist3[3].Image = (Image)Image;
                    lbllist3[3].Text = $"Level: {datap4[8].ToString()}";
                    lbl_3.Text = datateam[0].ToString().Replace('-', ' ').Substring(5);
                    btn_con3.Tag = "3";
                    btn_con3.Click += PlaytheGame;
                    break;
                case 4:
                    PictureBox[] pboxlist4 = { pbox_41, pbox_42, pbox_43, pbox_44 };
                    Label[] lbllist4 = { lbl_lvl_4_1, lbl_lvl_4_2, lbl_lvl_4_3, lbl_lvl_4_4 };
                    datateam = dtsavefile4.Rows[0][3].ToString().Split(' ');
                    datap1 = dtsavefile4.Rows[1][3].ToString().Split(' ');
                    datap2 = dtsavefile4.Rows[2][3].ToString().Split(' ');
                    datap3 = dtsavefile4.Rows[3][3].ToString().Split(' ');
                    datap4 = dtsavefile4.Rows[4][3].ToString().Split(' ');

                    Image = Resources.ResourceManager.GetObject(datap1[1].ToString());
                    pboxlist4[0].Image = (Image)Image;
                    lbllist4[0].Text = $"Level: {datap1[8].ToString()}";

                    Image = Resources.ResourceManager.GetObject(datap2[1].ToString());
                    pboxlist4[1].Image = (Image)Image;
                    lbllist4[1].Text = $"Level: {datap2[8].ToString()}";

                    Image = Resources.ResourceManager.GetObject(datap3[1].ToString());
                    pboxlist4[2].Image = (Image)Image;
                    lbllist4[2].Text = $"Level: {datap3[8].ToString()}";

                    Image = Resources.ResourceManager.GetObject(datap4[1].ToString());
                    pboxlist4[3].Image = (Image)Image;
                    lbllist4[3].Text = $"Level: {datap4[8].ToString()}";

                    lbl_4.Text = datateam[0].ToString().Replace('-', ' ').Substring(5);
                    btn_con4.Tag = "4";
                    btn_con4.Click += PlaytheGame;
                    break;
            }
        }
        private void updatesavedata(int slot, string data)
        {
            switch (slot)
            {
                case 0:
                    dtsavefile1.Clear();
                    mysqlquary = $"select * from Savefile where id_savefile like '{data}%';";
                    myconnection = new MySqlConnection(mysqlconnection);
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myadapter = new MySqlDataAdapter(mycommand);
                    myadapter.Fill(dtsavefile1);
                    break;
                case 1:
                    dtsavefile2.Clear();
                    mysqlquary = $"select * from Savefile where id_savefile like '{data}%';";
                    myconnection = new MySqlConnection(mysqlconnection);
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myadapter = new MySqlDataAdapter(mycommand);
                    myadapter.Fill(dtsavefile2);
                    break;
                case 2:
                    dtsavefile3.Clear();
                    mysqlquary = $"select * from Savefile where id_savefile like '{data}%';";
                    myconnection = new MySqlConnection(mysqlconnection);
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myadapter = new MySqlDataAdapter(mycommand);
                    myadapter.Fill(dtsavefile3);
                    break;
                case 3:
                    dtsavefile4.Clear();
                    mysqlquary = $"select * from Savefile where id_savefile like '{data}%';";
                    myconnection = new MySqlConnection(mysqlconnection);
                    mycommand = new MySqlCommand(mysqlquary, myconnection);
                    myadapter = new MySqlDataAdapter(mycommand);
                    myadapter.Fill(dtsavefile4);
                    break;
            }
        }
        private void deletesave(object sender, EventArgs e)
        {
            Button[] btnlist = { btn_con1, btn_con2, btn_con3, btn_con4 };
            Button btn = sender as Button;
            int savefile = Convert.ToInt32(btn.Tag.ToString());
            int buttoncontinuechange = savefile - 1;
            btnlist[buttoncontinuechange].Click -= PlaytheGame;
            btnlist[buttoncontinuechange].Click -= CreatePlayerClass;
            clearall(savefile);
            try
            {
                myconnection.Open();
                mysqlquary = $"update Savefile set save_teamname = '' where id_savefile = 'SF{btn.Tag.ToString()}-0';";
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myreader = mycommand.ExecuteReader();
                myconnection.Close();
                myconnection.Open();
                mysqlquary = $"update Savefile set save_data = '' where id_savefile = 'SF{btn.Tag.ToString()}-0';";
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myreader = mycommand.ExecuteReader();
                myconnection.Close();
                myconnection.Open();
                mysqlquary = $"update Savefile set save_data = '' where id_savefile = 'SF{btn.Tag.ToString()}-1';";
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myreader = mycommand.ExecuteReader();
                myconnection.Close();
                myconnection.Open();
                mysqlquary = $"update Savefile set save_data = '' where id_savefile = 'SF{btn.Tag.ToString()}-2';";
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myreader = mycommand.ExecuteReader();
                myconnection.Close();
                myconnection.Open();
                mysqlquary = $"update Savefile set save_data = '' where id_savefile = 'SF{btn.Tag.ToString()}-3';";
                mycommand = new MySqlCommand(mysqlquary, myconnection);
                myreader = mycommand.ExecuteReader();
                myconnection.Close();
                myconnection.Open();
                mysqlquary = $"update Savefile set save_data = '' where id_savefile = 'SF{btn.Tag.ToString()}-4';";
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
            }
            Checksavefile();
        }
        private void resetsavefile(int savefile)
        {
            Label[] labelteamtitle = { lbl_1,lbl_2,lbl_3,lbl_4};
            Button[] butdel = { btn_del1, btn_del2, btn_del3, btn_del4 };
            Button[] butcon = {btn_con1, btn_con2 , btn_con3, btn_con4 };
            clearall(savefile);
            labelteamtitle[savefile - 1].Text = "Available Slot";
            butdel[savefile - 1].Visible = false;
            butcon[savefile - 1].Text = "Create";
            butcon[savefile - 1].Click += CreatePlayerClass;
            butcon[savefile - 1].Tag = savefile.ToString();
        }
        private void clearall(int savefile)
        {
            Label[] labelteam1 = { lbl_lvl_1_1, lbl_lvl_1_2, lbl_lvl_1_3, lbl_lvl_1_4 };
            Label[] labelteam2 = { lbl_lvl_2_1, lbl_lvl_2_2, lbl_lvl_2_3, lbl_lvl_2_4 };
            Label[] labelteam3 = { lbl_lvl_3_1, lbl_lvl_3_2, lbl_lvl_3_3, lbl_lvl_3_4 };
            Label[] labelteam4 = { lbl_lvl_4_1, lbl_lvl_4_2, lbl_lvl_4_3, lbl_lvl_4_4 };
            PictureBox[] pboxteam1 = { pbox_11, pbox_12, pbox_13, pbox_14 };
            PictureBox[] pboxteam2 = { pbox_21, pbox_22, pbox_23, pbox_24 };
            PictureBox[] pboxteam3 = { pbox_31, pbox_32, pbox_33, pbox_34 };
            PictureBox[] pboxteam4 = { pbox_41, pbox_42, pbox_43, pbox_44 };
            switch (savefile)
            {
                case 1:
                    foreach(Label lbl in labelteam1)
                    {
                        lbl.Visible = false;
                    }
                    foreach (PictureBox pbox in pboxteam1)
                    {
                        pbox.Visible = false;
                    }
                    btn_del1.Enabled = false;
                    btn_del1.Visible = false;
                    btn_con1.Text = "Create";
                    lbl_1.Text = "Available Slot";
                    break;
                case 2:
                    foreach (Label lbl in labelteam2)
                    {
                        lbl.Visible = false;
                    }
                    foreach (PictureBox pbox in pboxteam2)
                    {
                        pbox.Visible = false;
                    }
                    btn_del2.Enabled = false;
                    btn_del2.Visible = false;
                    btn_con2.Text = "Create";
                    lbl_2.Text = "Available Slot";
                    break;
                case 3:
                    foreach (Label lbl in labelteam3)
                    {
                        lbl.Visible = false;
                    }
                    foreach (PictureBox pbox in pboxteam3)
                    {
                        pbox.Visible = false;
                    }
                    btn_del3.Enabled = false;
                    btn_del3.Visible = false;
                    btn_con3.Text = "Create";
                    lbl_3.Text = "Available Slot";
                    break;
                case 4:
                    foreach (Label lbl in labelteam4)
                    {
                        lbl.Visible = false;
                    }
                    foreach (PictureBox pbox in pboxteam4)
                    {
                        pbox.Visible = false;
                    }
                    btn_del4.Enabled = false;
                    btn_del4.Visible = false;
                    btn_con4.Text = "Create";
                    lbl_4.Text = "Available Slot";
                    break;
            }
        }
        private void CreatePlayerClass(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            selectedsavefile = Convert.ToInt32(btn.Tag);
            this.Close();
            thread = new Thread(openplayercreator);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void PlaytheGame(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            selectedsavefile = Convert.ToInt32(btn.Tag);
            this.Close();
            thread = new Thread(opengame);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void openplayercreator(object obj)
        {
            Application.Run(new FormCharacterCreator(selectedsavefile));
        }
        private void opengame(object obj)
        {
            Application.Run(new FormMainGame(selectedsavefile));
        }
        private void closegame(object obj)
        {
            Application.Run(new FormGame());
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
            thread = new Thread(closegame);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

    }
}
