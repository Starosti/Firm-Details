using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace FirmCalculatorJson
{
    class Program
    {
        static void Color(ConsoleColor _color)
        {
            Console.ForegroundColor = _color;
        }

        private static void Between()
        {
            Color(ConsoleColor.Gray);
            Console.WriteLine("");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("");
        }
        private static bool CheckInt(string firmNum)
        {
            if(int.TryParse(firmNum,out int a) )
            {
                if (Convert.ToInt32(firmNum) > 0) return true;
                return false;
            }
            return false;
        }
        static void Startup()
        {
            Color(ConsoleColor.DarkGray);

            Console.WriteLine(@"MemeEconomy Firm Payout App by /u/Starosti_
Last Firm Distribution Update Date:13.03.2019

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
            long _floorMemPay = 0;
            long _assoPay = 0;
            long _execPay = 0;
            //calculations
            string comma = "#,###";
            double tax = Convert.ToInt64(firmBal * 0.1);
            long payout = Convert.ToInt64((firmBal - tax) * 0.5);
            long _boardPay = Convert.ToInt64((payout * 0.3) / board);
            if (exec != 0) _execPay = Convert.ToInt64((payout * 0.28) / exec);        
            if (asso != 0) _assoPay = Convert.ToInt64((payout * 0.21) / asso);
            if (exec + asso + board != totalM) _floorMemPay = Convert.ToInt64((payout * 0.21) / floor);
            string boardPay = _boardPay.ToString(comma);
            string execPay = _execPay.ToString(comma);
            string assoPay = _assoPay.ToString(comma);
            string floorMemPay = _floorMemPay.ToString(comma);
            string _payout = payout.ToString(comma);
            int total = board + exec + asso + floor;
            //output
            Between();
            Color(ConsoleColor.Magenta);
            Console.WriteLine("Firm Name:" + firmName);
            Console.WriteLine("Firm Level:" + rank);
            Console.WriteLine("Firm Tax:"+taxP+"%");
            Console.WriteLine("Firm Balance:" + firmBal.ToString(comma));
            Between();
            Color(ConsoleColor.Yellow);
            Console.WriteLine("Total Trader Count:" + total);
            Console.WriteLine("Board Member Count:" + board);
            Console.WriteLine("Executive Count:" + exec);
            Console.WriteLine("Associate Count:" + asso);
            Console.WriteLine("Floor Trader Count:" + floor);
            Between();
            Color(ConsoleColor.Red);
            Console.WriteLine("Payout Tax Amount:" + tax.ToString(comma));
            Between();
            Color(ConsoleColor.Green);
            Console.WriteLine("Total Payout Amount:" + _payout);
            Console.WriteLine("Board Member Payout(per member):" + boardPay);
            Console.WriteLine("Executive Payout(per member):" + execPay);
            Console.WriteLine("Associate Payout(per member):" + assoPay);
            Console.WriteLine("Floor Trader Payout(per member):" + floorMemPay);
            Between();
            Color(ConsoleColor.Red);
            Console.WriteLine("Press a key to start a new search.");
            Console.ReadKey();
            Console.Clear();
        }
        static Firm GetFirm(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null) throw new Exception("Unreachable Site");
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response == null) throw new Exception("Unreachable Site");
                else
                {
                    StreamReader reader =
                        new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
                    var jsonData = reader.ReadToEnd();
                    var _firm = JsonConvert.DeserializeObject<Firm>(jsonData);
                    return _firm;
                }
            }
        }
        static void Main()
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
                    CalculateOutput(firm.balance, firm.board, firm.execs, firm.assocs, (firm.size - firm.board - 1 - firm.execs - firm.assocs), firm.name, firm.tax, firm.rank,firm.size);
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
