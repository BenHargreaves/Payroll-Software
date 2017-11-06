using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PayRollSoftware
{
    public class PaySlip
    {
        private int month;
        private int year;

        enum MonthsOfYear
        { January = 1, February, March, April, May, June, July, August, September, October, November, December}

        public PaySlip(int payMonth, int payYear)
        {
            month = payMonth;
            year = payYear;
        }

        public void GeneratePaySlip(List<Staff> mystaff)
        {
            string path;

            foreach (Staff staff in mystaff)
            {
                path = staff.NameOfStaff + ".txt";

                using (StreamWriter sw = new StreamWriter(path))
                {

                    sw.WriteLine("Payslip for {0} {1}", (MonthsOfYear)month, year);
                    sw.WriteLine("=====================================");
                    sw.WriteLine("Name of Staff: {0}", staff.NameOfStaff);
                    sw.WriteLine("Hours Worked: {0}", staff.HoursWorked);
                    sw.WriteLine("");
                    sw.WriteLine("Basic Pay: {0:C}", staff.BasicPay);
                    
                    if(staff.GetType() == typeof(Manager))
                    {
                        sw.WriteLine("Allowance: {0:C}", ((Manager)staff).Allowance);
                    }
                    else if (staff.GetType() == typeof(Admin))
                    {
                        sw.WriteLine("Overtime: {0:C}", ((Admin)staff).Overtime);
                    }

                    sw.WriteLine("");
                    sw.WriteLine("=====================================");
                    sw.WriteLine("TotalPay: {0:C}", staff.TotalPay);
                    sw.WriteLine("=====================================");

                    sw.Close();
                }

                
            }
        }

        public void GenerateSummary(List<Staff> myStaff)
        {
            string path = "summary.txt";

            var result =
                from staff in myStaff
                where staff.HoursWorked < 10
                orderby staff.NameOfStaff ascending
                select new { staff.NameOfStaff, staff.HoursWorked };

            using (StreamWriter summary = new StreamWriter(path)) {

                summary.WriteLine("Staff with less than 10 working hours");
                summary.WriteLine("");

                foreach (var staff in result)
                {
                    summary.WriteLine("Name of Staff: {0},   Hours Worked: {1}", staff.NameOfStaff, staff.HoursWorked);
                }
                summary.Close();
            }
        }

        public override string ToString()
        {
            return "Month = " + month + "\nYear = " + year;
        }
    }
}
