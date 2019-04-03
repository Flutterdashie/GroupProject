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


                cmd.Parameters.AddWithValue("@Title", BlogPost.Title);
                cmd.Parameters.AddWithValue("@Message", BlogPost.Message);
                cmd.Parameters.AddWithValue("@DateAdded", BlogPost.DateAdded);
                cmd.Parameters.AddWithValue("@DateEdited", BlogPost.DateEdited);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<BlogPost> GetAllPosts()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetAllPosts", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        yield return new BlogPost
                        {
                            BlogPostId = (int) dr["PostID"],
                            DateAdded = DateTime.Parse(dr["DateAdded"].ToString()),
                            Message = dr["Message"].ToString(),
                            Title = dr["Title"].ToString(),
                            DateEdited = DateTime.Parse(dr["DateEdited"].ToString())
                        };
                    }
                }
            }
        }

        public void BlogDeletePost(int BlogPostId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BlogPostDelete", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public BlogPost BlogPostSelectById(int BlogPostId)
        {

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BlogPostSelectById", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BlogPostId", BlogPostId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.Read())
                    {
                        return new BlogPost
                        {
                            DateAdded = (DateTime)dr["DateAdded"],
                            Title = dr["BlogPostTitle"].ToString(),
                            Message = dr["BlogPostMessage"].ToString(),
                            DateEdited = (DateTime)dr["DateEdited"]
                        };
                    }
                }
                return null;
            }
        }

        public BlogPost BlogUpdatePost(int BlogPostId)
        {
            throw new NotImplementedException();
        }
    }
}