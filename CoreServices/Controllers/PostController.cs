using System;
using System.Threading.Tasks;
using CoreServices.Models;
using CoreServices.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostRepository postRepository;
        public PostController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        [HttpGet]
        [Route("GetCategories")]
        public IActionResult GetCategories()
        {
            try
            {
                var categories =  postRepository.GetCategories();
                if (categories == null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("GetPosts")]
        public IActionResult GetPosts()
        {
            try
            {
                var posts = postRepository.GetPosts();
                if (posts == null)
                {
                    return NotFound();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetPost")]
        public IActionResult GetPost(int? postId)
        {
            if (postId == null)
            {
                return BadRequest();
            }

            try
            {
                var post = postRepository.GetPost(postId);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddPost")]
        public IActionResult AddPost([FromBody]Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = postRepository.AddPost(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost]
        [Route("DeletePost")]
        public IActionResult DeletePost(int? postId)
        {
            int result = 0;

            if (postId == null)
            {
                return BadRequest();
            }

            try
            {
                result = postRepository.DeletePost(postId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPost]
        [Route("UpdatePost")]
        public IActionResult UpdatePost([FromBody]Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    postRepository.UpdatePost(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}