using ControlzEx.Standard;
using System;
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
    public class Purpose
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string valute { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public static string _NAME = "purposes";

        public bool isCurrentPurpose { get; set; }


        private static object lofile = new object();

        [NonSerialized()]
        List<Payment> payments;



        [NonSerialized()]
        public double collect;

        [NonSerialized()]
        public string ave;


        [NonSerialized()]
        public string think;


        [NonSerialized()]
        public string ost;


        public string DateToString
        {
            get
            {
                return Date.ToString("yyy-MM-dd");
            }
        }


        public void work(List<Payment> payments)
        {
            this.payments = payments.Where(o => o.purposeId == Id).OrderByDescending(o=>o.Date).ToList();
            double s = 0; //sum collected
            foreach (var p in this.payments)
            {
                s = s + p.Price;
            }
            if (s > Price)
            {
                s = Price;
            }

            var startPrice = this.payments.Last();
            DateTime now = DateTime.Now;
            if (s == Price)
            {
                now = this.payments[0].Date;
            }
            var span = now - startPrice.Date;
            double span_dayts = span.TotalDays;

            span_dayts = Math.Floor(span_dayts) + 1;
            double ave = s / span_dayts;
            collect = s;
            //result = "Collect: " + s.ToString();
            this.ave = ave.ToString("#.##");
            //result = result + "\nAverage: " + ave.ToString("#.##");
            double proc_days = Math.Ceiling(Price / ave);
            DateTime purposeday = startPrice.Date.AddDays(proc_days);
            this.think = purposeday.ToString("yyyy-MM-dd");
            //result = result + "\nThink: : " + purposeday.ToString("yyyy-MM-dd");

            var span2 = purposeday - now;
            int ost = (int)span2.TotalDays;
            if (ost < 0)
            {
                ost = 0;
            }
            this.ost = ost.ToString();
            //result = result + "\nOst Days: : " + ost;
            //b.ToolTip = result;
        }


        public static String LogfileName()
        {
            String path = new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            return path + @"\db\db-" + _NAME + "";
        }


        public static String LogfileName(string name)
        {
            String path = new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            return path + @"\db\db-" + name + "";
        }

        public static String LogFileDir()
        {
            return new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName + @"\db";
        }

        public static void SaveToFile(List<Purpose> accounts)
        {
            lock (lofile)
            {
                string name = _NAME;
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
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    using (Stream serializationStream =
                        new FileStream(LogfileName(name), FileMode.Create, FileAccess.Write))
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


        public static List<Purpose> LoadFromFile()
        {
            lock (lofile)
            {
                String fn = LogfileName();


                if (!File.Exists(fn))
                    return new List<Purpose>();


                try
                {
                    using (Stream serializationStream = new FileStream(fn, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        List<Purpose> sav = (List<Purpose>)formatter.Deserialize(serializationStream);
                        List<Purpose> res = new List<Purpose>();
                        foreach (Purpose acc in sav)
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
                    return new List<Purpose>();
                }
            }


        }
    }
}
