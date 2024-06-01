using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


public class MapGenerator
{
    string level;
    public void SetMapLevel(string level)
    {
        this.level = level;
    }
    public string MapGeneratorThing()
    {
        Random random = new Random();
        string dangerlevel = "";
        string mapdata = "lvl"+level+"-";
        dangerlevel = level;
        //string[] data = mapdata.Split('-');
        //dangerlevel = data[0].Substring(3);
        string result = "";
        // 1 = easy enemy, 2 = medium enemy, 3 = hard enemy, 5 = treasure chest, 4 = DO NOT, this is a boss you wont win
        if (dangerlevel == "1")
        {
            for (int i = 0; i < 35; i++)
            {
                int rng = random.Next(1, 101);
                if (rng <= 60)
                {
                    result = result + "1";
                }
                else if (rng <= 75)
                {
                    result = result + "2";
                }
                else if (rng <= 100)
                {
                    result = result + "5";
                }
                else
                {
                    result = result + "1";
                }
            }
        }
        else if (dangerlevel == "2")
        {
            for (int i = 0; i < 35; i++)
            {
                int rng = random.Next(1, 101);
                if (rng <= 50)
                {
                    result = result + "1";
                }
                else if (rng <= 75)
                {
                    result = result + "2";
                }
                else if (rng <= 85)
                {
                    result = result + "3";
                }
                else if (rng <= 86)
                {
                    result = result + "4";
                }
                else if (rng <= 100)
                {
                    result = result + "5";
                }
                else
                {
                    result = result + "1";
                }
            }
        }
        else
        {
            for (int i = 0; i < 35; i++)
            {
                int rng = random.Next(1, 101);
                if (rng <= 70)
                {
                    result = result + "2";
                }
                else if (rng <= 85)
                {
                    result = result + "3";
                }
                else if(rng <= 90)
                {
                    result = result + "4";
                }
                else if (rng <= 100)
                {
                    result = result + "5";
                }
                else
                {
                    result = result + "4";
                }
            }
        }
        mapdata = mapdata + result;
        mapdata = mapdata + "-SSSHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHF";
        return mapdata;
    }
}

