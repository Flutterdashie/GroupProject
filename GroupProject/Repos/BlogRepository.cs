using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using GroupProject.Models;

namespace GroupProject.Repos
{
    public class BlogRepository
    {
        public void Insert(BlogPost BlogPost)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BlogPostInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter param = new SqlParameter("@CustomerId", SqlDbType.Int);
                //param.Direction = ParameterDirection.Output;

                //cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", BlogPost.Title);
                cmd.Parameters.AddWithValue("@Message", BlogPost.Message);

                cn.Open();

                cmd.ExecuteNonQuery();

                //customer.CustomerId = (int)param.Value;
            }
        }
    }
}