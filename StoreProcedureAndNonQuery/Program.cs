using Microsoft.Data.SqlClient;
using System.Data;

class Program
{  public string connectionString= "data source= SALEEJK; database= master;integrated security=SSPI;TrustServerCertificate=True";
    public static void Main(string[] args)
    {
        Program prog=new Program();
        prog.ViewStudents();
        prog.AddStudent("navas", 21, "cs");
        prog.UpdateStudent(1, "fiddd", 20, "botony");

    }
    public  void ViewStudents()
    {
        using(SqlConnection connection =new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("viewStudentsSP", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader=command.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine($"{reader[1]}{reader[2]}");
            }
        }
    }
    public void AddStudent(string name,int age,string department)
    {
        using(SqlConnection connection =new SqlConnection(connectionString))
        {
            try
            {
                 SqlCommand command = new SqlCommand("insert into student values(@name,@age,@department)", connection);
            command.Parameters.AddWithValue ("name", name);
            command.Parameters.AddWithValue("age", age);
            command.Parameters.AddWithValue("department", department);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                command.ExecuteReader();
                Console.WriteLine("successfully submitted");
            }
            }catch(Exception ex)
            {
                Console.WriteLine (ex.Message);
            }
        }
    }
    public void UpdateStudent(int id,string name,int age,string depart)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("Update student set name=@name,age=@age,department=@department where id=@id", connection);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("age", age);
                command.Parameters.AddWithValue("department", depart);
                command.Parameters.AddWithValue("id", id);                
                connection.Open();
                command.ExecuteReader();
                Console.WriteLine("Updated successfully");

            }
        }catch (SqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


}