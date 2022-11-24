using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassManWebCore.Models;

namespace PassManWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        SqlConnectionStringBuilder builder;
        public LoginController()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "tcp:mypassmansqlserver.database.windows.net";
            builder.UserID = "Nadezdys";
            builder.Password = "c3yKy:*4%V:Wqj9h";
            builder.InitialCatalog = "PassManSqlDB";
        }

        [EnableCors("CorsPolicy")]
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                string query = @"
                        select ID, Username, Email, Password
                        from [dbo].[LoginData]
                        ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(builder.ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Ok(table);
            }
            catch (Exception)
            {
                return Ok(new {  mess = "Load Failed" });
            }
            
        }

        // POST: api/Login
        [EnableCors("CorsPolicy")]
        [HttpPost]
        public ActionResult Post([FromBody] LoginData data)
        {
            try
            {
                string query = @"
                        insert into dbo.LoginData 
                        values(
                        '" + data.Username + @"'
                        ,'" + data.Email + @"'
                        ,'" + data.Password + @"'
                        )
                        ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(builder.ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Ok(new { mess = "Registered Successfully" });

            }
            catch (Exception)
            {
                return Ok(new { mess = "Register Failed" });
            }

        }

        // PUT: api/Login/5
        [EnableCors("CorsPolicy")]
        [HttpPut]
        public ActionResult Put([FromBody] LoginData data)
        {
            try
            {
                string query = @"
                        update dbo.LoginData set
                        Username='" + data.Username + @"'
                        ,Email='" + data.Email + @"'
                        ,Password='" + data.Password + @"'
                        where ID=" + data.ID + @" 
                        ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(builder.ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Ok(new { mess = "Updated Successfully" });

            }
            catch (Exception)
            {
                return Ok(new { mess = "Update Failed" });
            }
        }

        // DELETE: api/Login/5
        [EnableCors("CorsPolicy")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                string query = @"
                        delete from dbo.LoginData 
                        where ID=" + id + @" 
                        ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(builder.ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Ok(new { mess = "Deleted Successfully" } );

            }
            catch (Exception)
            {
                return Ok(new { mess = "Delete Failed" });
            }
        }
    }
}