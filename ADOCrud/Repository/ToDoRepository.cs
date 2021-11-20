using ADOCrud.Interfaces;
using ADOCrud.Models;
using ADOCrud.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADOCrud.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        public void ChangeComplete(ToDo toDo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Connection.ConnectionString))
                {
                    string sqlCommand = "UPDATE tb_ToDoList set complete = @complete where @id = id";
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("id", toDo.Id);
                    cmd.Parameters.AddWithValue("@complete", toDo.Complete);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ToDo Create(ToDo toDo)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand();

                    cn.ConnectionString = Connection.ConnectionString;

                    sqlCommand.Connection = cn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "INSERT INTO tb_ToDoList (NAME, TIME, COMPLETE) VALUES (@name, @time, @complete)";

                    sqlCommand.Parameters.AddWithValue("name", toDo.Name);
                    sqlCommand.Parameters.AddWithValue("time", toDo.Time);
                    sqlCommand.Parameters.AddWithValue("complete", toDo.Complete);

                    cn.Open();
                    sqlCommand.ExecuteNonQuery();

                    cn.Close();
                }

                return toDo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Connection.ConnectionString))
                {
                    string sqlCommand = "Delete from tb_ToDoList where id = @id";
                    SqlCommand cmd = new SqlCommand(sqlCommand, cn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ToDo> GetAll()
        {
            try
            {
                List<ToDo> listToDo = new List<ToDo>();

                using (SqlConnection cn = new SqlConnection(Connection.ConnectionString))
                {

                    SqlCommand sqlCommand = new SqlCommand("SELECT id, name, time, complete from tb_ToDoList", cn);
                    sqlCommand.CommandType = CommandType.Text;

                    cn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        ToDo toDo = new ToDo();

                        toDo.Id = Convert.ToInt32(reader["id"]);
                        toDo.Name = reader["name"].ToString();
                        toDo.Time = reader["time"].ToString();
                        toDo.Complete = Convert.ToBoolean(reader["complete"]);

                        listToDo.Add(toDo);
                    }

                    cn.Close();

                }
                return listToDo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ToDo GetById(int id)
        {
            try
            {
                ToDo result = new ToDo();

                using (SqlConnection cn = new SqlConnection(Connection.ConnectionString))
                {
                    string sqlQuery = "SELECT * FROM tb_ToDoList WHERE id= " + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, cn);

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Id = Convert.ToInt32(reader["id"]);
                        result.Name = reader["name"].ToString();
                        result.Time = reader["time"].ToString();
                        result.Complete = Convert.ToBoolean(reader["complete"]);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ToDo Update(ToDo toDo)
        {
            throw new System.NotImplementedException();
        }
    }
}
