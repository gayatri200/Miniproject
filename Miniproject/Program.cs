using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniproject
{
    internal class Program
    {

        public static SqlConnection con, con1;
        public static SqlCommand cmd;
        public static SqlDataReader dr;


        static void Main(string[] args)
        {
            Console.WriteLine(".........WELCOME.........");
            Console.WriteLine("For Menu Press M/m");
            string Menu = Console.ReadLine();
            if (Menu == "M" || Menu == "m")
            {
                Console.WriteLine("1. To View The Table");
                Console.WriteLine("2. To Insert Data In The Table");
                Console.WriteLine("3. To Delete Data In The Table");
            }
            else
            {
                Console.WriteLine("Input is Wrong Please Give A Proper Input....");
            }
            int ans = Convert.ToInt32(Console.ReadLine());


            if (ans == 1)
            {
                SelectData();

            }
            else if (ans == 2)
            {
                InsertData();
            }
            else if (ans == 3)
            {
                DeleteData();
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

            Console.Read();


        }

        public static SqlConnection getConnection()
        {
            con = new SqlConnection("data source=192.168.10.18; database = TrainingDB; user id = TrainingDB_User; password =     'X1;xbhpUN#a5eGHt4ohF'");
            con.Open();
            return con;
        }

        public static void SelectData()
        {
            try
            {
                con = getConnection(); // gets the connection details after executing the getConnection method
                cmd = new SqlCommand("select * from Employees", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine(dr[0] + " |" + dr[1] + "| " + dr[2]);
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Employee Id : {0}", dr[0]);
                    Console.WriteLine("Employee Name : {0}", dr[1]);
                    Console.WriteLine("Employee Gender : {0}", dr[2]);
                    Console.WriteLine("Employee Salary : {0}", dr[3]);
                    Console.WriteLine("Employee Department : {0}", dr[4]);
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine("Some Error Occured.. Try after sometime");
                Console.WriteLine(se.Message);
            }



        }
        public static void InsertData()
        {
            con = getConnection();

            Console.WriteLine("Enter Employees Id, Name,Gender,Salary and DeptId");
            int Eid = Convert.ToInt32(Console.ReadLine());
            string Ename = Console.ReadLine();
            string Egender = Console.ReadLine();
            float Esal = Convert.ToSingle(Console.ReadLine());
            int Edept = Convert.ToInt32(Console.ReadLine());

            cmd = new SqlCommand("insert into Employees values(@Eid,@Ename,@Egender,@Esal,@Edept)", con);
            cmd.Parameters.AddWithValue("@Eid", Eid);
            cmd.Parameters.AddWithValue("@Ename", Ename);
            cmd.Parameters.AddWithValue("@Egender", Egender);
            cmd.Parameters.AddWithValue("@Esal", Esal);
            cmd.Parameters.AddWithValue("@Edept", Edept);


            int res = cmd.ExecuteNonQuery();
            if (res > 0)
                Console.WriteLine("inserted..");
            else
                Console.WriteLine("not inserted");
        }

        public static void DeleteData()
        {
            con = getConnection();
            Console.WriteLine("Enter Employee id :");
            int Eid = Convert.ToInt32(Console.ReadLine());
            SqlCommand cmd1 = new SqlCommand("Select * from Employees where Eid=@Eid");
            cmd1.Parameters.AddWithValue("@Eid", Eid);
            cmd1.Connection = con;

            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                for (int i = 0; i < dr1.FieldCount; i++)
                {
                    Console.WriteLine(dr1[i]);
                }
            }
            con.Close();
            Console.WriteLine("Are you sure to delete this employee ? Y/N");
            string answer = Console.ReadLine();
            if (answer == "y" || answer == "Y")
            {
                cmd = new SqlCommand("delete from Employees where Eid=@Eid", con);
                cmd.Parameters.AddWithValue("@Eid", Eid);
                con.Open();

                int rw = cmd.ExecuteNonQuery();
                if (rw > 0)
                    Console.WriteLine("Record Deleted..");
                else
                    Console.WriteLine("Not deleted");
            }
        }
    }
}
