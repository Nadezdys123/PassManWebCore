using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class PasswordDataController : ControllerBase
    {
        SqlConnectionStringBuilder builder;
        public PasswordDataController()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "tcp:mypassmansqlserver.database.windows.net";
            builder.UserID = "Nadezdys";
            builder.Password = "******";
            builder.InitialCatalog = "PassManSqlDB";

        }

        // GET: api/PasswordData
        [EnableCors("CorsPolicy")]
        [HttpGet]
        public ActionResult Get()
        {
            string query = @"
                        select ID, Title, Username, Password, Webpage, Note, DBUser
                        from dbo.PasswordData
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

        // POST: api/PasswordData
        [EnableCors("CorsPolicy")]
        [HttpPost]
        public ActionResult Post([FromBody] PasswordData data)
        {
            try
            {
                string result;
                string query = @"
                        insert into dbo.PasswordData 
                        values(
                        '" + data.Title + @"'
                        ,'" + data.Username + @"'
                        ,'" + data.Password + @"'
                        ,'" + data.Webpage + @"'
                        ,'" + data.Note + @"'
                        ,'" + data.DBUser + @"'
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

                return Ok(new { mess = "Added Successfully" });

            }
            catch (Exception)
            {
                return Ok(new { mess = "Add Failed" });
            }
        }

        // PUT: api/PasswordData/5
        [EnableCors("CorsPolicy")]
        [HttpPut]
        public ActionResult Put([FromBody] PasswordData data)
        {
            try
            {
                string result;
                string query = @"
                        update dbo.PasswordData set
                        Title='" + data.Title + @"'
                        ,Username='" + data.Username + @"'
                        ,Password='" + data.Password + @"'
                        ,Webpage='" + data.Webpage + @"'
                        ,Note='" + data.Note + @"'
                        ,Note='" + data.DBUser + @"'
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

                return Ok(new { mess = "Updated Successfully" } );

            }
            catch (Exception)
            {
                return Ok(new { mess = "Update Failed" } );
            }
        }

        // DELETE: api/ApiWithActions/5
        [EnableCors("CorsPolicy")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                string query = @"
                        delete from dbo.PasswordData 
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
                return Ok(new { mess = "Delete Failed" } );
            }
        }
    }
}
