using RPG_II.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_II
{
    public partial class FormStats : Form
    {
        string slot;
        int playerselected;
        int SP;
        SaveEditor Editor = new SaveEditor();
        Calculator Calc = new Calculator();
        Object Image;
        public FormStats(string slot)
        {
            this.slot = slot;
            Editor.SelectSaveSlot(slot);
            Calc.GetSlotData(slot);
            InitializeComponent();
            ResetAll();
            GetParty();
        }
        public void ResetAll()
        {
            Label[] lblstats = { lbl_vit, lbl_str, lbl_dex, lbl_agi, lbl_int, lbl_wis };
            Label[] lbldisplay = { lbl_hp,lbl_mp, lbl_atk, lbl_def, lbl_armor, lbl_speed, lbl_special, lbl_crit };
            Label[] lblequip = { lbl_weapon, lbl_helm, lbl_chest, lbl_boot, lbl_aux};
            foreach (Label label in lblstats)
            {
                label.Text = label.Name.Substring(4).ToUpper() + " : 0";
            }
            foreach (Label label in lbldisplay)
            {
                label.Text = label.Name.Substring(4).ToUpper() + " : 0";
            }
            foreach (Label label in lblequip)
            {
                label.Text = label.Name.Substring(4).ToUpper() + " : -None-";
            }

        }
        public void GetParty()
        {
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(1, "name"));
            pbox_p1.Image = (Image)Image;
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(2, "name"));
            pbox_p2.Image = (Image)Image;
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(3, "name"));
            pbox_p3.Image = (Image)Image;
            Image = Resources.ResourceManager.GetObject(Editor.GetPlayerData(4, "name"));
            pbox_p4.Image = (Image)Image;
        }
        public void GetInfo(int player)
        {
            ResetAll();
            Label[] lblstats = { lbl_vit, lbl_str, lbl_dex, lbl_agi, lbl_int, lbl_wis };
            Label[] lbldisplay = { lbl_hp, lbl_mp,lbl_atk, lbl_def, lbl_armor, lbl_speed, lbl_special, lbl_crit };
            Label[] lblequip = { lbl_weapon, lbl_helm, lbl_chest, lbl_boot, lbl_aux };
            string[] stats = { "VIT", "STR", "DEX", "AGI", "WIS", "INT" };
            string[] display = { "HP", "MP","ATK", "DEF", "ARMOR", "SPEED", "SPECIAL", "CRIT"};
            string[] addedtext = { " Health Points", " Mana Points", " Attack Power", "% Mitigation", " Negated Damage", " Speed Points", " Special Attack Power", "% Critical Rate" };

            DataTable dtequipment = Editor.GetEquipment(player);

            SP = Convert.ToInt32(Editor.GetPlayerData(player, "sp"));
            lbl_class.Text = "Class : " + Editor.GetPlayerData(player, "name");
            lbl_lvl.Text = "Level : " + Editor.GetPlayerData(player, "lvl");
            lbl_sp.Text = "Available SP : " + Editor.GetPlayerData(player, "sp");
            for(int i = 0; i < 6; i++)
            {
                lblstats[i].Text = lblstats[i].Name.Substring(4).ToUpper() + " : " + Editor.GetPlayerData(player, stats[i].ToLower());
            }
            for (int i = 0; i < 8; i++)
            {
                lbldisplay[i].Text = lbldisplay[i].Name.Substring(4).ToUpper() + " : " + Calc.PlayerCalculator(display[i],player,slot) + addedtext[i];
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < dtequipment.Rows.Count; j++)
                {
                    if (dtequipment.Rows[j][0].ToString().Substring(1, 1) == (i+1).ToString())
                    {
                        lblequip[i].Text = lblequip[i].Name.Substring(4).ToUpper() + " : " + dtequipment.Rows[j][2].ToString();
                        break;
                    }
                    else
                    {
                        lblequip[i].Text = lblequip[i].Name.Substring(4).ToUpper() + " : -None-";
                    }
                }
            }
        }
        private void CheckSP()
        {
            if (SP == 0)
            {
                btn_agi.Enabled = false;
                btn_dex.Enabled = false;
                btn_int.Enabled = false;
                btn_str.Enabled = false;
                btn_vit.Enabled = false;
                btn_wis.Enabled = false;

                btn_agi.Visible = false;
                btn_dex.Visible = false;
                btn_int.Visible = false;
                btn_str.Visible = false;    
                btn_vit.Visible = false;
                btn_wis.Visible = false;
            }
            else
            {
                btn_agi.Enabled = true;
                btn_dex.Enabled = true;
                btn_int.Enabled = true;
                btn_str.Enabled = true;
                btn_vit.Enabled = true;
                btn_wis.Enabled = true;

                btn_agi.Visible = true;
                btn_dex.Visible = true;
                btn_int.Visible = true;
                btn_str.Visible = true;
                btn_vit.Visible = true;
                btn_wis.Visible = true;

            }
        }

        private void pbox_p1_Click(object sender, EventArgs e)
        {
            GetInfo(1);
            playerselected = 1;
        }
        private void pbox_p2_Click(object sender, EventArgs e)
        {
            GetInfo(2);
            playerselected = 2;
        }

        private void pbox_p3_Click(object sender, EventArgs e)
        {
            GetInfo(3);
            playerselected = 3;
        }

        private void pbox_p4_Click(object sender, EventArgs e)
        {
            GetInfo(4);
            playerselected = 4;
        }

        private void btn_vit_Click(object sender, EventArgs e)
        {
            SP = SP - 1;
            int newvalue = Convert.ToInt32(Editor.GetPlayerData(Convert.ToInt32(playerselected), "vit"));
            newvalue = newvalue + 1;
            Editor.ChangePlayerData(playerselected, "vit", newvalue.ToString());
            Editor.ChangePlayerData(playerselected,"sp",SP.ToString());
            CheckSP();
            GetInfo(playerselected);
        }

        private void btn_str_Click(object sender, EventArgs e)
        {
            SP = SP - 1;
            int newvalue = Convert.ToInt32(Editor.GetPlayerData(Convert.ToInt32(playerselected), "str"));
            newvalue = newvalue + 1;
            Editor.ChangePlayerData(playerselected, "str", newvalue.ToString());
            Editor.ChangePlayerData(playerselected, "sp", SP.ToString());
            CheckSP();
            GetInfo(playerselected);
        }

        private void btn_dex_Click(object sender, EventArgs e)
        {
            SP = SP - 1;
            int newvalue = Convert.ToInt32(Editor.GetPlayerData(Convert.ToInt32(playerselected), "dex"));
            newvalue = newvalue + 1;
            Editor.ChangePlayerData(playerselected, "dex", newvalue.ToString());
            Editor.ChangePlayerData(playerselected, "sp", SP.ToString());
            CheckSP();
            GetInfo(playerselected);
        }

        private void btn_agi_Click(object sender, EventArgs e)
        {
            SP = SP - 1;
            int newvalue = Convert.ToInt32(Editor.GetPlayerData(Convert.ToInt32(playerselected), "agi"));
            newvalue = newvalue + 1;
            Editor.ChangePlayerData(playerselected, "agi", newvalue.ToString());
            Editor.ChangePlayerData(playerselected, "sp", SP.ToString());
            CheckSP();
            GetInfo(playerselected);
        }

        private void btn_int_Click(object sender, EventArgs e)
        {
            SP = SP - 1;
            int newvalue = Convert.ToInt32(Editor.GetPlayerData(Convert.ToInt32(playerselected), "int"));
            newvalue = newvalue + 1;
            Editor.ChangePlayerData(playerselected, "int", newvalue.ToString());
            Editor.ChangePlayerData(playerselected, "sp", SP.ToString());
            CheckSP();
            GetInfo(playerselected);
        }

        private void btn_wis_Click(object sender, EventArgs e)
        {
            SP = SP - 1;
            int newvalue = Convert.ToInt32(Editor.GetPlayerData(Convert.ToInt32(playerselected), "wis"));
            newvalue = newvalue + 1;
            Editor.ChangePlayerData(playerselected, "wis", newvalue.ToString());
            Editor.ChangePlayerData(playerselected, "sp", SP.ToString());
            CheckSP();
            GetInfo(playerselected);
        }
    }
}
