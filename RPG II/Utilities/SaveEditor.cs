using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

public class SaveEditor
{
    string mysqlquary;
    string mysqlconnection = "server=localhost;uid=root;database=rpgthegame";
    int saveslot;
    MySqlConnection myconnection;
    MySqlCommand mycommand;
    MySqlDataAdapter myadapter;
    MySqlDataReader myreader;

    DataTable dtplayer = new DataTable();
    public void SelectSaveSlot(string slot)
    {
        dtplayer.Clear();
        mysqlquary = $"SELECT * FROM savefile where id_savefile like 'SF{slot}%';";
        myconnection = new MySqlConnection(mysqlconnection);
        mycommand = new MySqlCommand(mysqlquary, myconnection);
        myadapter = new MySqlDataAdapter(mycommand);
        myadapter.Fill(dtplayer);
        saveslot = Convert.ToInt32(slot) - 1;
    }
    public void ChangeSaveData(string category, string data)
    {
        string[] teamdata = dtplayer.Rows[0][3].ToString().Split(' ');
        switch (category)
        {
            case "name":
                teamdata[0] = "name:" + data;
                break;
            case "cash":
                teamdata[1] = "cash:" + data;
                break;
            case "map":
                teamdata[2] = "map:" + data;
                break;
            case "difficulty":
                teamdata[3] = "difficulty" + data;
                break;
            case "pos":
                teamdata[4] = "pos:" + data;
                break;
        }
        string changeddata = teamdata[0] + " " + teamdata[1] + " " + teamdata[2] + " " + teamdata[3] + " " + teamdata[4];
        myconnection.Open();
        mysqlquary = $"update Savefile set save_data = '{changeddata}' where id_savefile = 'SF{saveslot+1}-0';";
        mycommand = new MySqlCommand(mysqlquary, myconnection);
        myreader = mycommand.ExecuteReader();
        myconnection.Close();
        SelectSaveSlot((saveslot+1).ToString());
    }
    public void ChangePlayerData(int player, string wanteddata, string newvalue)
    {
        string[] playerdata = dtplayer.Rows[player][3].ToString().Split(' ');
        int whatdata = 0;
        if (wanteddata == "id")
        {
            whatdata = 0;
        }
        else if (wanteddata == "name")
        {
            whatdata = 1;
        }
        else if (wanteddata == "vit")
        {
            whatdata = 2;
        }
        else if (wanteddata == "str")
        {
            whatdata = 3;
        }
        else if (wanteddata == "dex")
        {
            whatdata = 4;
        }
        else if (wanteddata == "agi")
        {
            whatdata = 5;
        }
        else if (wanteddata == "int")
        {
            whatdata = 6;
        }
        else if (wanteddata == "wis")
        {
            whatdata = 7;
        }
        else if (wanteddata == "lvl")
        {
            whatdata = 8;
        }
        else if (wanteddata == "exp")
        {
            whatdata = 9;
        }
        else if (wanteddata == "sp")
        {
            whatdata = 10;
        }
        playerdata[whatdata] = newvalue;
        string result = "";
        for (int i = 0; i < 10; i++)
        {
            result = result + playerdata[i] + " ";
        }
        result = result + playerdata[10];

        myconnection.Open();
        mysqlquary = $"update Savefile set save_data = '{result}' where id_savefile = 'SF{saveslot + 1}-{player}';";
        mycommand = new MySqlCommand(mysqlquary, myconnection);
        myreader = mycommand.ExecuteReader();
        myconnection.Close();
        SelectSaveSlot((saveslot + 1).ToString());
    }
    public string GetTeamData(string category)
    {
        string[] teamdata = dtplayer.Rows[0][3].ToString().Split(' ');
        string result = "";
        switch (category)
        {
            case "name":
                result = teamdata[0].Substring(5);
                break;
            case "cash":
                result = teamdata[1].Substring(5);
                break;
            case "map":
                result = teamdata[2].Substring(4);
                break;
            case "difficulty":
                result = teamdata[3].Substring(10);
                break;
            case "pos":
                result = teamdata[4].Substring(4);
                break;
        }
        return result;
    }
    public DataTable GetEquipment(int player)
    {
        DataTable dtequip = new DataTable();
        dtequip.Clear();
        mysqlquary = $"SELECT t.id_equip, e.eq_level, e.eq_name  FROM equipment e, teaminventory t where e.id_equip = t.id_equip and equipwho = {player.ToString()};";
        myconnection = new MySqlConnection(mysqlconnection);
        mycommand = new MySqlCommand(mysqlquary, myconnection);
        myadapter = new MySqlDataAdapter(mycommand);
        myadapter.Fill(dtequip);
        return dtequip;
    }
    public string GetPlayerData(int player,string wanteddata)
    {
        string result = "";
        int whatdata = 0;
        if (wanteddata == "id")
        {
            whatdata = 0;
        }
        else if (wanteddata == "name")
        {
            whatdata = 1;
        }
        else if (wanteddata == "vit")
        {
            whatdata = 2;
        }
        else if (wanteddata == "str")
        {
            whatdata = 3;
        }
        else if (wanteddata == "dex")
        {
            whatdata = 4;
        }
        else if (wanteddata == "agi")
        {
            whatdata = 5;
        }
        else if (wanteddata == "int")
        {
            whatdata = 6;
        }
        else if (wanteddata == "wis")
        {
            whatdata = 7;
        }
        else if (wanteddata == "lvl")
        {
            whatdata = 8;
        }
        else if (wanteddata == "exp")
        {
            whatdata = 9;
        }
        else if (wanteddata == "sp")
        {
            whatdata = 10;
        }
        string[] playerdata = dtplayer.Rows[player][3].ToString().Split(' ');
        return playerdata[whatdata].ToString();
    }
}



