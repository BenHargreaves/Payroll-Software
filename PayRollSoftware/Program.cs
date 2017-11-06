using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSoftware
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Staff> mystaff = new List<Staff>();
            FileReader fr = new FileReader();
            int month = 0, year = 0;

            while (year == 0)
            {
                Console.Write("Please enter the Year: ");
                try
                {
                    year = Convert.ToInt32(Console.ReadLine());
                    
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "Please Try Again.");
                }
            }

            while (month == 0)
            {
                Console.Write("Please enter the Month: ");
                try
                {
                    month = Convert.ToInt32(Console.ReadLine());

                    if(month < 1 || month > 12)
                    {
                        Console.WriteLine("Month must be between 1 and 12");
                        month = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "Please Try Again.");
                }
            }

            mystaff = fr.ReadFile();

            for(int i = 0; i < mystaff.Count; i++)
            {
                try
                {
                    Console.WriteLine("Enter number of hours worked for {0}", mystaff[i].NameOfStaff);
                    mystaff[i].HoursWorked = Convert.ToInt32(Console.ReadLine());
                    mystaff[i].CalculatePay();
                    Console.WriteLine(mystaff[i].ToString());
                    Console.WriteLine("Payslip for " + mystaff[i].NameOfStaff + " created.\nFilename = " + mystaff[i].NameOfStaff + ".txt\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + " Please try Again.");
                    i--;
                }
            }

            PaySlip ps = new PaySlip(month, year);
            ps.GeneratePaySlip(mystaff);
            ps.GenerateSummary(mystaff);

            Console.WriteLine("PaySlips and summary generated in same directory as .exe file.");
            Console.Read();
        }
    }
}
