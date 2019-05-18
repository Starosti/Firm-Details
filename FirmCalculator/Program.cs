using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;

namespace FirmCalculator
{
    class Program
    {
        public static string ceoName = "ceo";
        public static string cfoName = "cfo";
        public static string cooName = "coo";
        public static string execName = "exec";
        public static string assocName = "assoc";

        static void Color(ConsoleColor _color)
        {
            Console.ForegroundColor = _color;
        }
        private static bool CheckInt(string firmNum)
        {
            if(int.TryParse(firmNum, out int a))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void Startup()
        {
            Color(ConsoleColor.DarkGray);

            Console.WriteLine(@"MemeEconomy Firm Payout App by /u/Starosti_
Last Firm Distrubition Update Date:13.03.2019

10% of the firm's money is taxed by MemeEconomy

50% of the remaining money is paid out

30% of total payroll is split equally between the chief officers/board members (CEO, COO, and CFO)

28% of total payroll is split equally between all Executives

21% of total payroll is split equally between all Associates

21% of total payroll is split equally between all Floor Traders
");
            Color(ConsoleColor.Cyan);
            Console.WriteLine("Example: https://meme.market/firm.html?firm=80 is firm \"80\"");
        }
        static void CalculateOutput(long firmBal, int board, int exec, int asso, int floor, string firmName, int taxP,int rank,int totalM)
        {
            if (firmName == "")
            {
                Color(ConsoleColor.Red);
                Console.WriteLine("This firm doesn't exist!");
                return;
            }
            long floorMemPay = 0;
            long assoPay = 0;
            long execPay = 0;
            //calculations
            double tax = Convert.ToInt64(firmBal * 0.1);
            long payout = Convert.ToInt64((firmBal - tax) * 0.5);
            long boardPay = Convert.ToInt64((payout * 0.3) / board);
            if (!(exec == 0)) execPay = Convert.ToInt64((payout * 0.28) / exec);        
            if (!(asso == 0)) assoPay = Convert.ToInt64((payout * 0.21) / asso);
            if (exec + asso + board != totalM) floorMemPay = Convert.ToInt64((payout * 0.21) / floor);
            int total = board + exec + asso + floor;
            //output
            Color(ConsoleColor.Gray);
            Console.WriteLine("");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("");
            Color(ConsoleColor.Magenta);
            Console.WriteLine("Firm Name:" + firmName);
            Console.WriteLine("Firm Level:" + rank);
            Console.WriteLine("Firm Tax:"+taxP+"%");
            Console.WriteLine("Firm Balance:" + firmBal);
            Color(ConsoleColor.Gray);
            Console.WriteLine("");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("");
            Color(ConsoleColor.Yellow);
            Console.WriteLine("Total Trader Count:" + total);
            Console.WriteLine("Board Member Count:" + board);
            Console.WriteLine("Executive Count:" + exec);
            Console.WriteLine("Associate Count:" + asso);
            Console.WriteLine("Floor Trader Count:" + floor);
            Color(ConsoleColor.Gray);
            Console.WriteLine("");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("");
            Color(ConsoleColor.Red);
            Console.WriteLine("Payout Tax Amount:" + tax);
            Color(ConsoleColor.Gray);
            Console.WriteLine("");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("");
            Color(ConsoleColor.Green);
            Console.WriteLine("Total Payout Amount:" + payout);
            Console.WriteLine("Board Member Payout(per member):" + boardPay);
            Console.WriteLine("Executive Payout(per member):" + execPay);
            Console.WriteLine("Associate Payout(per member):" + assoPay);
            Console.WriteLine("Floor Trader Payout(per member):" + floorMemPay);
            Color(ConsoleColor.Gray);
            Console.WriteLine("");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("");
            Color(ConsoleColor.Red);
            Console.WriteLine("Press a key to start a new search.");
            Console.ReadKey();
            Console.Clear();
        }
        static Firm GetFirm(string url)
        {
            Firm _firm = new Firm();
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            string jsonData = "";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {                                 
                StreamReader reader = new StreamReader(response.GetResponseStream());
                jsonData = reader.ReadToEnd();             
            }
            _firm = JsonConvert.DeserializeObject<Firm>(jsonData);
            return _firm;
        }
        static void Main(string[] args)
        {
            Startup();

            while (true)
            {
                Color(ConsoleColor.Cyan);
                Console.WriteLine("");
                Console.Write("Enter the firm num:");
                string firmNum = Console.ReadLine();
                if (CheckInt(firmNum))
                {
                    Firm firm = GetFirm($"https://meme.market/api/firm/{firmNum}");
                    CalculateOutput(firm.balance, firm.cfo + firm.coo + 1, firm.execs, firm.assocs, (firm.size - firm.cfo - firm.coo - 1 - firm.execs - firm.assocs), firm.name, firm.tax, firm.rank,firm.size);
                }
                else
                {
                    Color(ConsoleColor.Red);
                    Console.WriteLine("Please write a firm number!");
                }                
            }
        }       
    }
}
