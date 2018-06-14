using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedure
{
  class Program
  {
    static void Main(string[] args)
    {
      SqlConnection conection = new SqlConnection("Data Source=IP; Initial Catalog=db_name;User ID=user;Password=pass;");
      string procedure = "PROCEDURE";
      SqlCommand cmd = new SqlCommand(procedure, conection);
      conection.Open();
      cmd.Parameters.AddWithValue("@PARAM1", "Value");
      cmd.Parameters.AddWithValue("@PARAM2", "Value");
      cmd.CommandType = CommandType.StoredProcedure;
      SqlDataReader reader = cmd.ExecuteReader();
      var csv = new StringBuilder();
      if (reader.HasRows)
      {
        while (reader.Read())
        {
          //For every record, creates a new line.
          csv.AppendLine(string.Format("{0},{1}", '"' + reader["VALUE1"].ToString() + '"', '"' + reader["VALUE2"].ToString() + '"'));
        }
      }
      reader.Close();
      conection.Close();
      string path_file = ".csv";
      //Writes the csv file.
      File.WriteAllText(path_file, csv.ToString());
    }
  }
}
