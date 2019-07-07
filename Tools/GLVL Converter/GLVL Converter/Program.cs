using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using HedgeLib.Sets;

namespace GLvl_Converter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        static public void ConvertSET(string filepath, string templates)
        {
            S06SetData setTarget = new S06SetData();
            GensSetData setSource = new GensSetData();

            setSource.Load(filepath);

            //Dud Object at 0,0,0 to work around HedgeLib often breaking the first object in the set.
            SetObject dubObject = new SetObject();
            dubObject.ObjectID = 0;
            dubObject.Parameters.Add(new SetObjectParam(typeof(string), "HedgeLib Workaround"));
            dubObject.Parameters.Add(new SetObjectParam(typeof(bool), false));
            List<SetObjectParam> parameters2 = dubObject.Parameters;
            SetObject item2 = new SetObject
            {
                ObjectType = "objectphysics",
                ObjectID = dubObject.ObjectID,
                Parameters = parameters2,
            };
            setTarget.Objects.Add(item2);

            //Object Conversion
            foreach (SetObject gensObject in setSource.Objects)
            {
                SetObject s06Object = new SetObject();
                Console.WriteLine(gensObject.ObjectType);

                int uintCheck;

                switch (gensObject.ObjectType)
                {
                    case "spring":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        if (int.TryParse(gensObject.Parameters[2].Data.ToString(), out uintCheck)) { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[2].Data.ToString()))); }
                        else { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), 4294967295u)); }
                        break;
                    case "enemy":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "objectphysics":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[1].Data));
                        break;
                    case "itemboxg":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "widespring":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "wvo_revolvingnet":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "wvo_doorB":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "common_guillotine":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        break;
                    case "wvo_waterslider":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "wvo_orca":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[5].Data));
                        break;
                    case "eventbox":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString()) * 50));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "cameraeventbox":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString()) * 50));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        var cameraPosString_cev = gensObject.Parameters[6].Data.ToString();
                        string[] cameraPosArray_cev = cameraPosString_cev.Split(' ');
                        cameraPosArray_cev[0] = cameraPosArray_cev[0].Remove(0, 1);
                        cameraPosArray_cev[0] = cameraPosArray_cev[0].Remove(cameraPosArray_cev[0].Length - 1, 1);
                        cameraPosArray_cev[1] = cameraPosArray_cev[1].Remove(cameraPosArray_cev[1].Length - 1, 1);
                        cameraPosArray_cev[2] = cameraPosArray_cev[2].Remove(cameraPosArray_cev[2].Length - 1, 1);
                        float cameraPosX_cev = float.Parse(cameraPosArray_cev[0]) * 100;
                        float cameraPosY_cev = float.Parse(cameraPosArray_cev[1]) * 100;
                        float cameraPosZ_cev = float.Parse(cameraPosArray_cev[2]) * 100;
                        Vector3 s06CameraPos_cev = new Vector3(cameraPosX_cev, cameraPosY_cev, cameraPosZ_cev);
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraPos_cev)); //Camera Position

                        var cameraTargetString_cev = gensObject.Parameters[7].Data.ToString();
                        string[] cameraTargetArray_cev = cameraTargetString_cev.Split(' ');
                        cameraTargetArray_cev[0] = cameraTargetArray_cev[0].Remove(0, 1);
                        cameraTargetArray_cev[0] = cameraTargetArray_cev[0].Remove(cameraTargetArray_cev[0].Length - 1, 1);
                        cameraTargetArray_cev[1] = cameraTargetArray_cev[1].Remove(cameraTargetArray_cev[1].Length - 1, 1);
                        cameraTargetArray_cev[2] = cameraTargetArray_cev[2].Remove(cameraTargetArray_cev[2].Length - 1, 1);
                        float cameraTargetX_cev = float.Parse(cameraTargetArray_cev[0]) * 100;
                        float cameraTargetY_cev = float.Parse(cameraTargetArray_cev[1]) * 100;
                        float cameraTargetZ_cev = float.Parse(cameraTargetArray_cev[2]) * 100;
                        Vector3 s06CameraTarget_cev = new Vector3(cameraTargetX_cev, cameraTargetY_cev, cameraTargetZ_cev);
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraTarget_cev)); //Camera Target
                        break;
                    case "itemboxa":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "dashpanel":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "common_water_collision":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString()) * 50));
                        break;
                    case "enemyextra":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[5].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[6].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[7].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[8].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[10].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[11].Data));
                        if (int.TryParse(gensObject.Parameters[12].Data.ToString(), out uintCheck)) { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[12].Data.ToString()))); }
                        else { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), 4294967295u)); }
                        if (int.TryParse(gensObject.Parameters[13].Data.ToString(), out uintCheck)) { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[13].Data.ToString()))); }
                        else { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), 4294967295u)); }
                        break;
                    case "ring":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        break;
                    case "player_start2":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[2].Data));
                        break;
                    case "pointsample":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "jumppanel":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        if (int.TryParse(gensObject.Parameters[3].Data.ToString(), out uintCheck)) { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString()))); }
                        else { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), 4294967295u)); }
                        break;
                    case "common_dashring":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "common_stopplayercollision":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString()) * 50));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "chainjump":
                        if (int.TryParse(gensObject.Parameters[0].Data.ToString(), out uintCheck)) { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString()))); }
                        else { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), 4294967295u)); }
                        break;
                    case "wvo_jumpsplinter":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "common_hint_collision":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString()) * 50));
                        break;
                    case "wvo_battleship":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "player_goal":
                        var cameraPosString_pg = gensObject.Parameters[0].Data.ToString();
                        string[] cameraPosArray_pg = cameraPosString_pg.Split(' ');
                        cameraPosArray_pg[0] = cameraPosArray_pg[0].Remove(0, 1);
                        cameraPosArray_pg[0] = cameraPosArray_pg[0].Remove(cameraPosArray_pg[0].Length - 1, 1);
                        cameraPosArray_pg[1] = cameraPosArray_pg[1].Remove(cameraPosArray_pg[1].Length - 1, 1);
                        cameraPosArray_pg[2] = cameraPosArray_pg[2].Remove(cameraPosArray_pg[2].Length - 1, 1);
                        float cameraPosX_pg = float.Parse(cameraPosArray_pg[0]) * 100;
                        float cameraPosY_pg = float.Parse(cameraPosArray_pg[1]) * 100;
                        float cameraPosZ_pg = float.Parse(cameraPosArray_pg[2]) * 100;
                        Vector3 s06CameraPos_pg = new Vector3(cameraPosX_pg, cameraPosY_pg, cameraPosZ_pg);
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraPos_pg)); //Camera Position

                        var cameraTargetString_pg = gensObject.Parameters[1].Data.ToString();
                        string[] cameraTargetArray_pg = cameraTargetString_pg.Split(' ');
                        cameraTargetArray_pg[0] = cameraTargetArray_pg[0].Remove(0, 1);
                        cameraTargetArray_pg[0] = cameraTargetArray_pg[0].Remove(cameraTargetArray_pg[0].Length - 1, 1);
                        cameraTargetArray_pg[1] = cameraTargetArray_pg[1].Remove(cameraTargetArray_pg[1].Length - 1, 1);
                        cameraTargetArray_pg[2] = cameraTargetArray_pg[2].Remove(cameraTargetArray_pg[2].Length - 1, 1);
                        float cameraTargetX_pg = float.Parse(cameraTargetArray_pg[0]) * 100;
                        float cameraTargetY_pg = float.Parse(cameraTargetArray_pg[1]) * 100;
                        float cameraTargetZ_pg = float.Parse(cameraTargetArray_pg[2]) * 100;
                        Vector3 s06CameraTarget_pg = new Vector3(cameraTargetX_pg, cameraTargetY_pg, cameraTargetZ_pg);
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), s06CameraTarget_pg)); //Camera Target
                        break;
                    case "townsgoal":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString()) * 50));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "dtd_pillar":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[3].Data));
                        break;
                    case "common_cage":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "common_thorn":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "dtd_movingfloor":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "physicspath":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        break;
                    case "common_laser":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        if (int.TryParse(gensObject.Parameters[2].Data.ToString(), out uintCheck)) { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[1].Data.ToString()))); }
                        else { s06Object.Parameters.Add(new SetObjectParam(typeof(uint), 4294967295u)); }
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[10].Data));
                        break;
                    case "wap_pathsnowball":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "wap_brokensnowball":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "wap_door":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "snowboardjump":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        break;
                    case "wap_searchlight":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[10].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[11].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[12].Data));
                        break;
                    case "common_switch":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "amigo_collision":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString()) * 100));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString()) * 50));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[4].Data));
                        break;
                    case "objectphysics_item":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "bldgexplode":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "cscglassbuildbomb":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "cscglass":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "impulsesphere":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "common_rainbowring":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "pole":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        break;
                    case "positionSample":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "inclinedstonebridge":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "flc_door":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "crater":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "flc_volcanicbomb":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[10].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[11].Data.ToString())));
                        break;
                    case "flamesingle":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[9].Data.ToString())));
                        break;
                    case "flamesequence":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        break;
                    case "common_warphole":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[2].Data));
                        break;
                    case "normal_train":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[9].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[10].Data.ToString())));
                        break;
                    case "rct_door":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "rct_seesaw":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        break;
                    case "rct_belt":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "tarzan":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[10].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[11].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[12].Data));
                        break;
                    case "bungee":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "hangingrock":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[0].Data));
                        break;
                    case "turtle":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[5].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[6].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[7].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[8].Data));
                        break;
                    case "fruit":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "lotus":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[6].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        break;
                    case "brickwall":
                        break;
                    case "eagle":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[2].Data));
                        break;
                    case "rope":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        break;
                    case "windswitch":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "windroad":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        break;
                    case "inclinedbridge":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "cameraeventcylinder":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[5].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[6].Data));
                        break;
                    case "common_windcollision_box":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[6].Data));
                        break;
                    case "brokenstairs_right":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "kdv_door":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "kdv_rainbow":
                        break;
                    case "robustdoor":
                        break;
                    case "common_psimarksphere":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        break;
                    case "espstairs_left":
                        break;
                    case "gate":
                        break;
                    case "aqa_door":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "updownreel":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "aqa_mercury_small":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "aqa_lamp":
                        break;
                    case "aqa_balancer":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "aqa_magnet":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "aqa_launcher":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[6].Data.ToString())));
                        break;
                    case "aqa_pond":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        break;
                    case "aqa_glass_blue":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "vehicle":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        break;
                    case "common_key":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        break;
                    case "common_terrain":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "player_npc":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "switch_collector":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[10].Data));
                        break;
                    case "eventcylinder":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "dtd_pillar_eagle":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[2].Data));
                        break;
                    case "dtd_door":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "common_hint":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        break;
                    case "dtd_sandwave":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "ironspring":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[5].Data.ToString())));
                        break;
                    case "espswing":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "dtd_billiardswitch":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        break;
                    case "dtd_billiard":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "dtd_billiardcounter":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[10].Data.ToString())));
                        break;
                    case "dtd_switchcounter":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[10].Data));
                        break;
                    case "common_jumpboard":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "rct_seesaw_silver":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[10].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[11].Data));
                        break;
                    case "espstairs_right":
                        break;
                    case "pendulum":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "flc_flamecore":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        break;
                    case "freezedmantle":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "lockonsphere":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "freight_train":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[7].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[8].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[9].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[10].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[11].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[12].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[13].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[14].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[15].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[16].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[17].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[18].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[19].Data));
                        break;
                    case "particle":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[5].Data));
                        break;
                    case "aqa_wyvern_fall":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "cameraeventsphere":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[5].Data));
                        break;
                    case "ambience_collision":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[5].Data));
                        break;
                    case "changelight":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[5].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        break;
                    case "lightanimation":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[5].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[6].Data.ToString())));
                        break;
                    case "common_path_obj":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "pointlight":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        break;
                    case "medal_of_royal_silver":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "scrollbldg":
                        break;
                    case "csctornadochase":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[5].Data));
                        break;
                    case "csctornado":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[5].Data));
                        break;
                    case "common_chaosemerald":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "brokenstairs_left":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "kdv_decalog":
                        break;
                    case "eventsphere":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "trial_post":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "bell":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        break;
                    case "disk":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        break;
                    case "present":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        break;
                    case "download_obj":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        break;
                    case "townsman":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[5].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[6].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[7].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[8].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[10].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[11].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[12].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[13].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[14].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[15].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[16].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[17].Data.ToString())));
                        break;
                    case "gondola":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        break;
                    case "aqa_glass_red":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "common_ringswitch":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        break;
                    case "common_lensflare":
                        break;
                    case "dtd_sandeffect":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "end_soleannaswitch":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "end_outputwarp":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[4].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[5].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[6].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[7].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[8].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[9].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[10].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[11].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[12].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[13].Data.ToString())));
                        break;
                    case "end_inputwarp":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "end_timekeeper":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "common_object_event":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        break;
                    case "eggman_train":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[6].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[7].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[8].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[9].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[10].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[11].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[12].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[13].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[14].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[15].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[16].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[17].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[18].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[19].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[20].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[21].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[22].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[23].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[24].Data.ToString())));
                        break;
                    case "player_start":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "glidewire":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[0].Data));
                        break;
                    case "twn_hardrock":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        break;
                    case "darkness":
                        break;
                    case "candlestick":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        break;
                    case "passring":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "warpgate":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "kingdomcrest":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "memory_of_past":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "shopTV":
                        break;
                    case "tamaire_collision_box":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        break;
                    case "twn_door":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[6].Data));
                        break;
                    case "venthole":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(Vector3), gensObject.Parameters[1].Data));
                        break;
                    case "medal_of_royal_bronze":
                        break;
                    case "wvo_doorA":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        break;
                    case "tpj_runninground":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "particlesphere":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[0].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        break;
                    case "objecttownscar":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[3].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[4].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[5].Data.ToString())));
                        break;
                    case "radarmapmark":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[2].Data));
                        break;
                    case "twn_gflag_stopplayercollision":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "twn_flagdoor":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "itembox_next":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[1].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(bool), gensObject.Parameters[2].Data));
                        break;
                    case "spring_twn":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(uint), uint.Parse(gensObject.Parameters[2].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;
                    case "tamaire_collision_sphere":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        break;
                    case "tamaire_collision_cylinder":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        break;
                    case "score_collision_cylinder":
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[0].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(float), float.Parse(gensObject.Parameters[1].Data.ToString())));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(string), gensObject.Parameters[2].Data));
                        s06Object.Parameters.Add(new SetObjectParam(typeof(int), int.Parse(gensObject.Parameters[3].Data.ToString())));
                        break;

                    default:
                        if (File.Exists(templates + "\\" + gensObject.ObjectType + ".xml"))
                        {
                            if (gensObject.Parameters.Count != 0) //Object that has a template but no conversion code
                            {
                                SetObject missingObject = new SetObject();
                                missingObject.ObjectID = 0;
                                missingObject.Parameters.Add(new SetObjectParam(typeof(string), gensObject.ObjectType));
                                missingObject.Parameters.Add(new SetObjectParam(typeof(bool), false));

                                List<SetObjectParam> parametersMissing = missingObject.Parameters;
                                var missingTrans = GenTransform(gensObject.Transform);
                                SetObject missingItem = new SetObject
                                {
                                    ObjectType = "objectphysics",
                                    ObjectID = 0,
                                    Parameters = parametersMissing,
                                    Transform = missingTrans,
                                };
                                setTarget.Objects.Add(missingItem);
                                Console.WriteLine("Templates exists for " + gensObject.ObjectType + ". But no code to convert it currently exists right now.");
                                continue;
                            }
                        }
                        break;
                }


                //Common Things Shared Across All Objects
                List<SetObjectParam> parameters = s06Object.Parameters;
                var trans = GenTransform(gensObject.Transform);
                SetObject item = new SetObject
                {
                    ObjectType = gensObject.ObjectType,
                    ObjectID = 0,
                    Parameters = parameters,
                    Transform = trans,
                };
                setTarget.Objects.Add(item);
            }

            //Saving Converted Set
            setTarget.Save(Properties.Settings.Default.lastSavedOutput, true);
        }

        public static SetObjectTransform GenTransform(SetObjectTransform trans)
        {
            return new SetObjectTransform
            {
                Position = trans.Position * 100,
                //Normalize Quaternions.
                Rotation = Quaternion.Normalize(trans.Rotation),
                Scale = trans.Scale
            };
        }
    }
}
