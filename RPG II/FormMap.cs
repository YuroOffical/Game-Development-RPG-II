using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RPG_II.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_II
{
    public partial class FormMap : Form
    {
        string mapdata,posdata;
        Object Image;
        SaveEditor Editor = new SaveEditor();
        PictureBox Currentpbox;
        string threatpos;
        string threatlevel;
        int state;
        
        public FormMainGame FormRef { get; set; }
        public FormMap(string mapdata, string posdata, string saveslot, int state)
        {
            this.state = state;
            InitializeComponent();
            this.mapdata = mapdata;
            this.posdata = posdata;
            Editor.SelectSaveSlot(saveslot);
            GenerateMap("normal", "");
            LocatePlayer();
            GenerateClick();
        }
        private void GenerateMap(string state, string data)
        {
            string[]truemapdata = mapdata.Split('-');
            string dangerlevel = truemapdata[0].Substring(3);
            string mappingtile = truemapdata[1];
            string hiddendesign = truemapdata[2];
            string beginnerstage = hiddendesign.Remove(3);
            string midstage = hiddendesign.Substring(3,35);
            string endstage = hiddendesign.Substring(hiddendesign.Length - 1);
            PictureBox[] MapTilePreset = { pbox_start, pbox_boss, pbox_12, pbox_13, pbox_14, pbox_92, pbox_93, pbox_94 };
            PictureBox[] MapTileRandom = { pbox_21,pbox_22,pbox_23,pbox_24,pbox_25,pbox_31,pbox_32,pbox_33,pbox_34,pbox_35,pbox_41,pbox_42,pbox_43,pbox_44,pbox_45,pbox_51,pbox_52,pbox_53,pbox_54,pbox_55,pbox_61,pbox_62,pbox_63,pbox_64,pbox_65,pbox_71,pbox_72,pbox_73,pbox_74,pbox_75,pbox_81,pbox_82,pbox_83,pbox_84,pbox_85};
            String[] MapTileImage = { "MT_first", "MT_1", "MT_2", "MT_3", "MT_Danger", "MT_treasure", "MT_unlocked", "MT_unknown", "MT_boss" };
            //Generating the Middle Section
            for(int i = 0; i < 35; i++)
            {
                if (midstage[i] == 'H')
                {
                    int value = Convert.ToInt32(mappingtile[i].ToString());
                    Image = Resources.ResourceManager.GetObject(MapTileImage[7].ToString());
                    MapTileRandom[i].Image = (Image)Image;
                }
                else
                {
                    int value = Convert.ToInt32(mappingtile[i].ToString());
                    Image = Resources.ResourceManager.GetObject(MapTileImage[value]);
                    MapTileRandom[i].Image = (Image)Image;
                    MapTileRandom[i].BorderStyle = BorderStyle.FixedSingle;
                    MapTileRandom[i].Tag = MapTileImage[value].ToString();
                }
                
            }
            //Generation the start
            for(int i = 2;i < 5; i++)
            {
                int value = Convert.ToInt32(dangerlevel);
                Image = Resources.ResourceManager.GetObject(MapTileImage[value]);
                MapTilePreset[i].Image = (Image)Image;
                MapTilePreset[i].BorderStyle = BorderStyle.FixedSingle;
                MapTilePreset[i].Tag = MapTileImage[value].ToString();
            }
            // Generate the boss section
            for (int i = 5; i < 8; i++)
            {
                int value = Convert.ToInt32(dangerlevel);
                if (endstage == "F")
                {
                    Image = Resources.ResourceManager.GetObject(MapTileImage[7]);
                    MapTilePreset[i].Image = (Image)Image;
                    Image = Resources.ResourceManager.GetObject(MapTileImage[4]);
                    MapTilePreset[1].Image = (Image)Image;
                }
                else
                {
                    Image = Resources.ResourceManager.GetObject(MapTileImage[value+1]);
                    MapTilePreset[i].Image = (Image)Image;
                    MapTilePreset[i].BorderStyle = BorderStyle.FixedSingle;
                    MapTilePreset[i].Tag = MapTileImage[value+1].ToString();
                    Image = Resources.ResourceManager.GetObject(MapTileImage[8]);
                    MapTilePreset[1].Image = (Image)Image;
                    MapTilePreset[1].Tag = MapTileImage[8].ToString();
                }
            }
            
            MapTilePreset[1].Image = (Image)Image;
            //Hide
            if (state == "reveal")
            {
                int localstate = 0;
                string result = "";
                string[] revealedarea = data.Split(' ');
                if (!data.Contains("showtime"))
                {
                    for (int i = 0; i < 35; i++)
                    {
                        localstate = 0;
                        for (int j = 0; j < 3; j++)
                        {
                            if (MapTileRandom[i].Name.Contains(revealedarea[j]))
                            {
                                localstate = 1;
                                break;
                            }
                        }
                        if (localstate == 1)
                        {
                            result = result + "S";
                        }
                        else
                        {
                            result = result + midstage[i].ToString();
                        }
                    }
                    midstage = result;
                }
                if (data.Contains("showtime"))
                {
                    endstage = "S";
                    if (data.Contains("bosstime"))
                    {
                        pbox_boss.BorderStyle = BorderStyle.FixedSingle;
                        pbox_boss.Click += MovetoTarget;
                    }
                }
                hiddendesign = beginnerstage + midstage + endstage;
                Editor.ChangeSaveData("pos", posdata);
                Editor.ChangeSaveData("map", "lvl"+dangerlevel+"-"+mappingtile+"-"+hiddendesign);
                mapdata = "lvl" + dangerlevel + "-" + mappingtile + "-" + hiddendesign;
                GenerateMap("normal", "");
            }
        }
        private void LocatePlayer()
        {
            switch (posdata)
            {
                case "first":
                    MovePlayer("start");
                    break;
                default:
                    MovePlayer(posdata);
                    break;
            }
        }
        private void MovePlayer(string locationdataname)
        {
            int x = 0;
            int y = 0;
            foreach(PictureBox pbox in this.Controls.OfType<PictureBox>())
            {
                if (pbox.Name.Contains(locationdataname))
                {
                    x = pbox.Location.X;
                    y = pbox.Location.Y;
                }
            }
            pbox_boiz.Location = new Point(x+15,y+15);
        }
        private void GenerateClick()
        {
            foreach (PictureBox pbox in this.Controls.OfType<PictureBox>())
            {
                if (pbox.BorderStyle == BorderStyle.FixedSingle && !pbox.Name.Contains(posdata) && !pbox.Name.Contains("boiz"))
                {
                    pbox.Click += MovetoTarget;
                }
            }
        }
        private void RemoveAllClick()
        {
            foreach (PictureBox pbox in this.Controls.OfType<PictureBox>())
            {
                if (pbox.BorderStyle == BorderStyle.FixedSingle)
                {
                    pbox.Click -= MovetoTarget;
                }
            }
        }
        private void RevealArea(PictureBox pbox)
        {
            string pos = pbox.Name.ToString().Substring(5);
            int lineoffire = Convert.ToInt32(pos[0].ToString());
            int pathoffire = Convert.ToInt32(pos[1].ToString());

            string frontup = $"{lineoffire+1}{pathoffire+1}";
            string frontmid = $"{lineoffire + 1}{pathoffire }";
            string frontdown = $"{lineoffire + 1}{pathoffire - 1}";

            GenerateMap("reveal", $"{frontup} {frontmid} {frontdown}");
        }
            private async void MovetoTarget(object sender, EventArgs e)
        {
            RemoveAllClick();
            PictureBox pbox = sender as PictureBox;
            int xinit = 0;
            int yinit = 0;
            int xfinal = 0;
            int yfinal = 0;
            xinit = pbox_boiz.Location.X;
            yinit = pbox_boiz.Location.Y;
            xfinal = pbox.Location.X + 15;
            yfinal = pbox.Location.Y + 15;
            int xdif = xfinal - xinit;
            int ydif = yfinal - yinit;
            for (int i = 1; i < 50; i++)
            {
                pbox_boiz.Location = new Point(xfinal-(xdif/i), yfinal - (ydif / i));
                await Task.Delay(10);
            }
            posdata = pbox.Name.ToString().Substring(5);
            await Task.Delay(10);
            pbox_boiz.Location=new Point(xfinal, yfinal);
            Currentpbox = pbox;
            threatlevel = pbox.Tag.ToString();
            
            if (!pbox.Name.Contains("start") && !pbox.Name.Contains("boss") && !pbox.Name.Contains("8") && !pbox.Name.Contains("9"))
            {
                RevealArea(pbox);
            }
            else if (pbox.Name.Contains("8"))
            {
                GenerateMap("reveal", "showtime");
            }
            else if (pbox.Name.Contains("9"))
            {
                GenerateMap("reveal", "showtime bosstime");
            }
            GenerateClick();
            if (!threatlevel.Contains("first") && !threatlevel.Contains("treasure") && !threatlevel.Contains("unlocked"))
            {
                FormRef.StartBattle(threatlevel);
            }
            GenerateMap("reveal", "99 99 99");
        }
        public void ActivateReveal()
        {
            
        }
    }
}
