using CoreServices.Models;
using CoreServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
   public interface IPostRepository
    {
        List<Category> GetCategories();

        List<PostViewModel> GetPosts();

        PostViewModel GetPost(int? postId);

        int AddPost(Post post);

        int DeletePost(int? postId);

        void UpdatePost(Post post);
    }
}
