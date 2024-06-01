using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Calculator
{
    // HP ATK DEF ARMOR SPECIAL CRIT
    int HP, ATK, DEF, ARMOR, SPEED ,SPECIAL, CRIT;
    int VIT, STR, DEX, AGI, INT, WIS;
    int WEAPON, HELM, CHEST, BOOT, AUX;

    string mysqlquary;
    string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
    int saveslot;
    MySqlConnection myconnection;
    MySqlCommand mycommand;
    MySqlDataAdapter myadapter;
    MySqlDataReader myreader;

    DataTable dtplayerstats = new DataTable();
    DataTable dtplayer = new DataTable();
    Random rng = new Random();
    public void GetSlotData(string slot)
    {
        dtplayer.Clear();
        mysqlquary = $"SELECT * FROM savefile where id_savefile like 'SF{slot}%';";
        myconnection = new MySqlConnection(mysqlconnection);
        mycommand = new MySqlCommand(mysqlquary, myconnection);
        myadapter = new MySqlDataAdapter(mycommand);
        myadapter.Fill(dtplayer);
        saveslot = Convert.ToInt32(slot) - 1;

        dtplayerstats.Clear();
        mysqlquary = $"SELECT id_pclass,p_vit,p_str,p_dex,p_agi,p_int,p_wis FROM pclass;";
        myconnection = new MySqlConnection(mysqlconnection);
        mycommand = new MySqlCommand(mysqlquary, myconnection);
        myadapter = new MySqlDataAdapter(mycommand);
        myadapter.Fill(dtplayerstats);
    }
    public int GetMultiplier(string stats, string ID, double init)
    {
        int result = 0;
        double tempresult = 0;
        double vitm = 0;
        double strm = 0;
        double dexm = 0;
        double agim = 0;
        double intm = 0;
        double wism = 0;
        for (int i = 0; i< dtplayerstats.Rows.Count; i++)
        {
            if (dtplayerstats.Rows[i][0].ToString() == ID)
            {
                vitm = Convert.ToDouble(1 + (Convert.ToDouble(dtplayerstats.Rows[i][1].ToString())/10));
                strm = Convert.ToDouble(1 + (Convert.ToDouble(dtplayerstats.Rows[i][2].ToString()) / 10));
                dexm = Convert.ToDouble(1 + (Convert.ToDouble(dtplayerstats.Rows[i][3].ToString()) / 10));
                agim = Convert.ToDouble(1 + (Convert.ToDouble(dtplayerstats.Rows[i][4].ToString()) / 10));
                intm = Convert.ToDouble(1 + (Convert.ToDouble(dtplayerstats.Rows[i][5].ToString()) / 10));
                wism = Convert.ToDouble(1 + (Convert.ToDouble(dtplayerstats.Rows[i][6].ToString()) / 10));
                break;
            }
        }
        switch (stats)
        {
            case "VIT":
                tempresult = init * vitm;
                break;
            case "STR":
                tempresult = init * strm;
                break;
            case "DEX":
                tempresult = init * dexm;
                break;
            case "AGI":
                tempresult = init * agim;
                break;
            case "INT":
                tempresult = init * intm;
                break;
            case "WIS":
                tempresult = init * wism;
                break;
        }
        result = Convert.ToInt32(tempresult);
        
        return result;
    }
    public int PlayerCalculator(string stats, int player, string slot)
    {
        GetSlotData(slot);
        string[] data = dtplayer.Rows[player][3].ToString().Split(' ');
        string ID = data[0].ToString();
        VIT = GetMultiplier("VIT", ID, Convert.ToDouble(data[2].ToString()));
        STR = GetMultiplier("STR", ID, Convert.ToDouble(data[3].ToString()));
        DEX = GetMultiplier("DEX", ID, Convert.ToDouble(data[4].ToString()));
        AGI = GetMultiplier("AGI", ID, Convert.ToDouble(data[5].ToString()));
        INT = GetMultiplier("INT", ID, Convert.ToDouble(data[6].ToString()));
        WIS = GetMultiplier("WIS", ID, Convert.ToDouble(data[7].ToString()));

        WEAPON = 0;
        HELM = 0;
        CHEST = 0;
        BOOT = 0;
        AUX = 0;

        DataTable dtequip = new DataTable();
        dtequip.Clear();
        mysqlquary = $"SELECT t.id_equip, e.eq_level  FROM equipment e, teaminventory t where e.id_equip = t.id_equip and equipwho = {player.ToString()} and id_savefile = '{slot}';";
        myconnection = new MySqlConnection(mysqlconnection);
        mycommand = new MySqlCommand(mysqlquary, myconnection);
        myadapter = new MySqlDataAdapter(mycommand);
        myadapter.Fill(dtequip);

        for (int i = 0; i < dtequip.Rows.Count; i++)
        {
            if (dtequip.Rows[i][0].ToString().Substring(1, 1) == "1")
            {
                WEAPON = Convert.ToInt32(dtequip.Rows[i][1].ToString());
            }
            if (dtequip.Rows[i][0].ToString().Substring(1, 1) == "2")
            {
                HELM = Convert.ToInt32(dtequip.Rows[i][1].ToString());
            }
            if (dtequip.Rows[i][0].ToString().Substring(1, 1) == "3")
            {
                CHEST = Convert.ToInt32(dtequip.Rows[i][1].ToString());
            }
            if (dtequip.Rows[i][0].ToString().Substring(1, 1) == "4")
            {
                BOOT = Convert.ToInt32(dtequip.Rows[i][1].ToString());
            }
            if (dtequip.Rows[i][0].ToString().Substring(1, 1) == "5")
            {
                AUX = Convert.ToInt32(dtequip.Rows[i][1].ToString());
            }
        }

        //MessageBox.Show(WEAPON + " " + HELM + " " + CHEST + " " + BOOT + " " + AUX + " ");

        int result = 0;
        switch (stats)
        {
            case "HP":
                result = 100 + (VIT * 10) + (STR * 5) + (CHEST * 5) + (HELM * 2);
                break;
            case "MP":
                result = 50 + (INT * 10) + (WIS * 5) + (HELM * 3) + (AUX * 5);
                break;
            case "ATK":
                result = 10 + (STR * 2) + (DEX * 4) + (WEAPON * 5) + (AUX * 3);
                break;
            case "DEF":
                result = 0 + (STR * 2) + (VIT * 1) + (CHEST * 5) + (HELM * 1);
                break;
            case "ARMOR":
                result = 0 + (STR * 1) + (CHEST * 2) + (HELM * 1) + (BOOT * 1);
                break;
            case "SPEED":
                result = 100 + (AGI * 5) + (DEX * 2) + (STR * -2) + (CHEST * -2) + (BOOT * 2);
                break;
            case "SPECIAL":
                result = 10 + (INT * 5) + (WIS * 2) + (AUX * 5) + (WEAPON * 2);
                break;
            case "CRIT":
                result = 10 + (WIS * 3) + (DEX * 3) + (WEAPON * 3) + (BOOT * 2) + (AUX * 2);
                break;
        }

        return result;
    }
    public int DamageCalculator(int atk, int crit,int tdef, int tarm)
    {
        int result = 0;
        int damagemultiplier = 1;
        if (crit >= rng.Next(1, 101))
        {
            damagemultiplier = 3;
        }
        result = atk * damagemultiplier;
        result = result - tarm;
        result = (result * (100 - tdef)) / 100;
        return result;
    }
    public int SkillDamageCalculator(int val, int atk, int special, int crit, int tdef, int tarm)
    {
        int result = 0;
        int damagemultiplier = 1;
        if (crit >= rng.Next(1, 101))
        {
            damagemultiplier = 3;
        }
        result = ((atk/2) + special + val)* damagemultiplier;
        result = result - tarm;
        result = (result * (100 - tdef)) / 100;
        return result;
    }
    public int HealCalculator(int val, int special, int hp, int hpinit)
    {
        int result = 0;
        result = val + special;
        result = hp + result;
        if (result > hpinit)
        {
            result = hpinit;
        }
        return result;
    }
}

