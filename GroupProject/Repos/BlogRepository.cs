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
        private static AuthModel _database = AuthModel.Create();
        public BlogPost Insert(BlogPost blogPost)
        {
            //TODO: Make this NOT use 3 separate BlogPost objects. Could be condensed to 1 if we change the inputs, but that changes requirements in other parts.
            BlogPost newPost = _database.Posts.Create();
            newPost.DateAdded = DateTime.Now;
            newPost.LastEdited = DateTime.Now;
            newPost.Title = blogPost.Title;
            newPost.Message = blogPost.Message;
            BlogPost result = _database.Posts.Add(newPost);
            _database.SaveChanges();
            return result;
        }

        public IEnumerable<BlogPost> GetAllPosts()
        {
            //TODO: See if forcing enumeration is bad here. It probably is if we had a TON of posts. Also the sorting could be optimized by just reversing instead, probably. Could have other issues, but yeah.
            return _database.Posts.OrderByDescending(p => p.DateAdded).ToList();
        }
    }
}