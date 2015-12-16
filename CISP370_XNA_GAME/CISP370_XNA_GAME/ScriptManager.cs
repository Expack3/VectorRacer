using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CISP370_XNA_GAME
{
    public class ScriptManager
    {
        private LinkedList<String> script = new LinkedList<string>();
        private Matrix tempRotation = Matrix.Identity;
        static int toInteger(String s)
        {
            return Convert.ToInt32(s.Trim());
        }

        static float toFloat(String s)
        {
            return Convert.ToSingle(s.Trim());
        }

        static bool toBool(String s)
        {
            return Convert.ToBoolean(s.Trim());
        }

        public void loadScript(String filename)
        {

            StreamReader reader;
            String line = String.Empty;

            //
            //load script
            //
            if (File.Exists(filename))
            {
#if DEBUG
                Console.WriteLine("Loading script...");
#endif
                reader = new StreamReader(filename);
                line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    if (!line.StartsWith("#"))
                    {
                        script.AddLast(line);
                        Console.WriteLine(line);
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            //
            //script loaded
            //
            GameState.modelManager.deleteAllModels();
            GameState.staticManager.deleteAllModels();
            readScript();
        }

        private void readScript()
        {
            LinkedListNode<String> current;

            current = script.First;

            while (current != null)
            {
                String[] s = current.Value.Split(',');
                trimStringArray(s);
                switch (s[0].ToLower())
                {
                    case "spawnpoint":
                        spawnPoint(s);
                        break;
                    case "loadprimitive":
                        loadPrimitive(s);
                        break;
                }
                current = current.Next;
            }
            deleteAllNodes();
        }

        private String[] trimStringArray(String[] s)
        {
            for (int i = 0; i != s.Length; i++)
            {
                s[i] = s[i].Trim();
            }
            return s;
        }

        private void deleteAllNodes()
        {
            while (script.First != null)
                script.RemoveFirst();
        }

        private void spawnPoint(String[] s)
        {
            GameState.camera = new Camera(new Vector3(toInteger(s[1]), toInteger(s[2]), toInteger(s[3])), new Vector3(toInteger(s[4]), toInteger(s[5]), toInteger(s[6])), Vector3.Up, 1, toFloat(s[7]));
        }

        public int Count
        {
            get
            {
                return script.Count;
            }
        }

        private void loadPrimitive(String[] s)
        {
            String baseString = @"{0}\{1}";
            String environment = s[10];
            String fileName = s[2];
            String combined = string.Format(baseString, environment, fileName);
            switch (s[1].ToLower())
            {
                case "rectangle":
                    if (s.Length == 11)
                    {
                        GameState.modelManager.addModel(new Rectangle(new Vector3(toFloat(s[3]), toFloat(s[4]), toFloat(s[5])), combined, new Vector2(2.0f, 1.0f), new Vector2(1.0f, 1.0f), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[6])));
                    }
                    else
                    {
                        GameState.modelManager.addModel(new Rectangle(new Vector3(toFloat(s[3]), toFloat(s[4]), toFloat(s[5])), combined, new Vector2(toFloat(s[11]), toFloat(s[12])), new Vector2(toFloat(s[13]), toFloat(s[14])), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[6])));
                    }
                    break;
                case "square":
                    if (s.Length == 11)
                    {
                        GameState.modelManager.addModel(new Rectangle(new Vector3(toFloat(s[3]), toFloat(s[4]), toFloat(s[5])), combined, new Vector2(1.0f, 1.0f), new Vector2(1.0f, 1.0f), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[6])));
                    }
                    else
                    {
                        GameState.modelManager.addModel(new Rectangle(new Vector3(toFloat(s[3]), toFloat(s[4]), toFloat(s[5])), combined, new Vector2(toFloat(s[11]), toFloat(s[11])), new Vector2(toFloat(s[12]), toFloat(s[13])), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[6])));
                    }
                    break;
                case "triangle":
                    if (s.Length == 11)
                    {
                        GameState.modelManager.addModel(new Triangle(new Vector3(toFloat(s[3]), toFloat(s[4]), toFloat(s[5])), combined, new Vector2(1.0f, 1.0f), new Vector2(1.0f, 1.0f), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[6]), -999.0f));
                    }
                    else
                    {
                        GameState.modelManager.addModel(new Triangle(new Vector3(toFloat(s[3]), toFloat(s[4]), toFloat(s[5])), combined, new Vector2(toFloat(s[11]), toFloat(s[11])), new Vector2(toFloat(s[12]), toFloat(s[13])), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[6]), -999.0f));
                    }
                    break;
                case "model":
                    if (s.Length == 13)
                        GameState.staticManager.addModel(new _3D_Model(s[3], GameState.content.Load<Model>(combined), new Vector3(toFloat(s[4]), toFloat(s[5]), toFloat(s[6])), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[11]), toFloat(s[12])));
                    else if(s.Length == 14)
                        GameState.staticManager.addModel(new _3D_Model(s[3], GameState.content.Load<Model>(combined), new Vector3(toFloat(s[4]), toFloat(s[5]), toFloat(s[6])), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[11]), toFloat(s[12]), toBool(s[13])));
                    break;
                case "border":
                    if (s.Length == 13)
                    {
                        for (float i = 0; i > -(GameState.camera.TargetDistance); i = i - 20f)
                        {
                            GameState.staticManager.addModel(new _3D_Model(s[3], GameState.content.Load<Model>(combined), new Vector3(65, toFloat(s[5]), i), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[11]), toFloat(s[12])));
                            GameState.staticManager.addModel(new _3D_Model(s[3], GameState.content.Load<Model>(combined), new Vector3(-65, toFloat(s[5]), i), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[11]), toFloat(s[12])));
                        }
                    }
                    else if (s.Length == 14)
                    {
                        for (float i = 0f; i > -(GameState.camera.TargetDistance); i = i - 20f)
                        {
                            GameState.staticManager.addModel(new _3D_Model(s[3], GameState.content.Load<Model>(combined), new Vector3(65, toFloat(s[5]), i), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[11]), toFloat(s[12]), toBool(s[13])));
                            GameState.staticManager.addModel(new _3D_Model(s[3], GameState.content.Load<Model>(combined), new Vector3(-65, toFloat(s[5]), i), new Vector3(toFloat(s[7]), toFloat(s[8]), toFloat(s[9])), toFloat(s[11]), toFloat(s[12]), toBool(s[13])));
                        }
                    }
                    break;
            }
        }
    }
}
