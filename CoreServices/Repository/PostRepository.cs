using CoreServices.Models;
using CoreServices.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
    public class PostRepository : IPostRepository
    {
        BlogDBContext db;
        public PostRepository(BlogDBContext _db)
        {
            db = _db;
        }

        public List<Category> GetCategories()
        {
            try
            {
                if (db != null)
                {
                    return  db.Category.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        public List<PostViewModel> GetPosts()
        {
            if (db != null)
            {
                return  (from p in db.Post
                              from c in db.Category
                              where p.CategoryId == c.Id
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  Title = p.Title,
                                  Description = p.Description,
                                  CategoryId = p.CategoryId,
                                  CategoryName = c.Name,
                                  CreatedDate = p.CreatedDate
                              }).ToList();
            }

            return null;
        }

        public PostViewModel GetPost(int? postId)
        {
            if (db != null)
            {
                return  (from p in db.Post
                              from c in db.Category
                              where p.PostId == postId
                              select new PostViewModel
                              {
                                  PostId = p.PostId,
                                  Title = p.Title,
                                  Description = p.Description,
                                  CategoryId = p.CategoryId,
                                  CategoryName = c.Name,
                                  CreatedDate = p.CreatedDate
                              }).FirstOrDefault();
            }

            return null;
        }

        public int AddPost(Post post)
        {
            if (db != null)
            {
                 db.Post.Add(post);
                 db.SaveChanges();

                return post.PostId;
            }

            return 0;
        }

        public int DeletePost(int? postId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var post =  db.Post.FirstOrDefault(x => x.PostId == postId);

                if (post != null)
                {
                    //Delete that post
                    db.Post.Remove(post);

                    //Commit the transaction
                    result =  db.SaveChanges();
                }
                return result;
            }

            return result;
        }


        public void UpdatePost(Post post)
        {
            if (db != null)
            {
                //Delete that post
                db.Post.Update(post);

                //Commit the transaction
                 db.SaveChanges();
            }
        }
    }
}

