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

                SqlParameter output = new SqlParameter("@BlogPostId", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                

                cmd.Parameters.AddWithValue("@BlogPostTitle", BlogPost.Title);
                cmd.Parameters.AddWithValue("@BlogPostMessage", BlogPost.Message);
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateEdited",DateTime.Now);

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
                            BlogPostId = (int) dr["BlogPostId"],
                            DateAdded = DateTime.Parse(dr["DateAdded"].ToString()),
                            Message = dr["BlogPostMessage"].ToString(),
                            Title = dr["BlogPostTitle"].ToString(),
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

                cmd.Parameters.AddWithValue("@BlogPostId", BlogPostId);

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
                            BlogPostId = (int)dr["BlogPostId"],
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

        public void BlogUpdatePost(BlogPost post)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BlogPostUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BlogPostId", post.BlogPostId);
                cmd.Parameters.AddWithValue("@BlogPostTitle", post.Title);
                cmd.Parameters.AddWithValue("@BlogPostMessage", post.Message);
                cmd.Parameters.AddWithValue("@DateEdited", DateTime.Now);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<BlogPost> SearchByTitle(string term)
        {
            List<BlogPost> list = new List<BlogPost>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {                   
                SqlCommand cmd = new SqlCommand("SearchByTitle", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BlogPostTitle", term);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BlogPost currentRow = new BlogPost();

                        currentRow.Title = dr["BlogPostTitle"].ToString();
                        currentRow.Message = dr["BlogPostMessage"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.DateEdited = (DateTime)dr["DateEdited"];
                        currentRow.BlogPostId = (int)dr["BlogPostId"];

                        list.Add(currentRow);
                    }
                }
                return list;
            }
        }

        public IEnumerable<BlogPost> SearchById(int Id)
        {
            List<BlogPost> list = new List<BlogPost>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("SearchById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BlogPostId", Id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BlogPost currentRow = new BlogPost();

                        currentRow.Title = dr["BlogPostTitle"].ToString();
                        currentRow.Message = dr["BlogPostMessage"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.DateEdited = (DateTime)dr["DateEdited"];
                        currentRow.BlogPostId = (int)dr["BlogPostId"];

                        list.Add(currentRow);
                    }
                }
                return list;
            }
        }
    }
}