using AccioOracleKit;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.AccioHelpers
{
    internal class AccioEasyHelpers
    {
        /// <summary>
        /// Get relative location to me executaive application...
        /// </summary>
        /// <returns></returns>
        public static string MeExistanceLocation()
        {
            return System.Reflection.Assembly.GetEntryAssembly().Location;
        }
        /// <summary>
        /// easy func to read strings line by line from text file..
        /// </summary>
        /// <param name="pathToFile">path to file to read</param>
        /// <returns></returns>
        public static string[] ReadTxTFiles(string pathToFile)
        {
            List<string> data = new List<string>();


            data.AddRange(System.IO.File.ReadAllLines(pathToFile));



            return data.ToArray();
        }
        /// <summary>
        /// get text on specific position..
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        public static string GetTxTBettwen(string txt, string first, string last)
        {

            StringBuilder sb = new StringBuilder(txt);
            int pos1 = txt.IndexOf(first) + first.Length;
            int len = (txt.Length) - pos1;

            string reminder = txt.Substring(pos1, len);


            int pos2 = reminder.IndexOf(last) - last.Length + 1;




            return reminder.Substring(0, pos2);



        }

        /// <summary>
        /// Test connectivity to database 
        /// </summary>
        /// <param name="autoclose">show if automatic connection needs to be closed or not</param>
        /// <returns></returns>
        public static OracleConnection ReadParamsThenConnectToDB(bool autoclose)
        {
            //read params from config
            var data = AccioEasyHelpers.ReadTxTFiles(AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring.exe").Length) + "init\\params.info");

            var server_adress = AccioEasyHelpers.GetTxTBettwen(data[4], "::", ",");
            var port = AccioEasyHelpers.GetTxTBettwen(data[5], "::", ",");

            return Scripts.TestConnection(new[] { server_adress, port, "store", "store" }, autoclose);
        }
        /// <summary>
        /// This method can print all properties any object can heve
        /// </summary>
        /// <typeparam name="T"><Type/typeparam>
        /// <param name="_obj">object itself</param>
        public static void InspectMyObject<T>(T _obj) where T : new()
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(_obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(_obj);
                Console.WriteLine("{0}={1}", name, value);
            }
           
        }
    }
}
