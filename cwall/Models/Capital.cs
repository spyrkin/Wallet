﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cwall.Models
{
    [Serializable()]
    public class Capital
    {

        public int Id { get; set; }
        public double dollar { get; set; }
        public double euro { get; set; }
        public double lary { get; set; }
        public double rubl { get; set; }


        public double Summ
        {
            get
            {
                return lary + dollar * K.dollar;
            }
            set
            {

            }
        }


        public bool isProfit {

            get
            {
                return Summ > 0;
            }
        }
       

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public static string _NAME = "capital";
        private static object lofile = new object();

        public string DateToString
        {
            get
            {
                return Date.ToString("yyy-MM-dd");
            }
        }

        public static String LogfileName()
        {
            String path = new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            return path + @"\db\db-" + _NAME + "";
        }

        public static String LogFileDir()
        {
            return new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName + @"\db";
        }

        public static void SaveToFile(List<Capital> accounts)
        {
            lock (lofile)
            {
                try
                {
                    String dir_name = LogFileDir();
                    if (!Directory.Exists(dir_name))
                    {
                        Directory.CreateDirectory(dir_name);
                    } //создать директорию если не существует
                }
                catch (Exception ex)
                {
                }
                try
                {
                    using (Stream serializationStream =
                        new FileStream(LogfileName(), FileMode.Create, FileAccess.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(serializationStream, accounts);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ошибка при записи в локальную базу данных!");
                }

                lofile = new object();
            }
        }


        public static List<Capital> LoadFromFile()
        {
            lock (lofile)
            {
                String fn = LogfileName();


                if (!File.Exists(fn))
                    return new List<Capital>();


                try
                {
                    using (Stream serializationStream = new FileStream(fn, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        List<Capital> sav = (List<Capital>)formatter.Deserialize(serializationStream);
                        List<Capital> res = new List<Capital>();
                        foreach (Capital acc in sav)
                            if (acc != null)
                            {

                                res.Add(acc);
                            }

                        return res;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return new List<Capital>();
                }
            }


        }
    }
}
