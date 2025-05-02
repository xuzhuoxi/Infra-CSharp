//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Windows.Forms;
//using Language.Lua.Library;
//
//namespace Language.Lua
//{
//    class Program
//    {
//        [STAThread]
//        static void Main(string[] args)
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//
//            if (args.Length > 0)
//            {
//                string file = args[0];
//                if (File.Exists(file))
//                {
//                    try
//                    {
//                        LuaTable global = LuaInterpreter.CreateGlobalEnviroment();
//                        WinFormLib.RegisterModule(global);
//                        global.SetNameValue("_WORKDIR", new LuaString(Path.GetFullPath(Path.GetDirectoryName(file))));
//                        LuaInterpreter.RunFile(file, global);
//                    }
//                    catch (Exception error)
//                    {
//                        MessageBox.Show(string.Format("{0}: {1}",error.GetType().Name, error.Message), "Error");
//                    }
//                }
//                else
//                {
//                    MessageBox.Show(file + " not found.");
//                }
//            }
//        }
//    }
//}
