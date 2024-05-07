﻿using BisleriumProject.Application.Common.Interface.IServices;
using BisleriumProject.Application.DTOs;
using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BisleriumProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private const long MaxFileSizeInBytes = 3 * 1024 * 1024; // 3 Megabytes in bytes


        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var blogs = await _blogService.GetAll();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); // Handle internal server errors
            }
        }

        [AllowAnonymous]
        [HttpGet("get-by-id/{blogId}")]
        public async Task<IActionResult> GetBlogById(int blogId) // Get blog by ID
        {
            try
            {
                var blogDTO = await _blogService.GetBlogById(blogId); // Call the service method
                return Ok(blogDTO); // Return the blog DTO
            }
            catch (KeyNotFoundException ex) // Handle case where the blog doesn't exist
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex) // Handle unexpected errors
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("get-by-user")]
        public async Task<IActionResult> GetBlogsByUserId()
        {
            try
            {
                var userId = User.FindFirst("userId")?.Value; 

                var blogDTOs = await _blogService.GetBlogsByUserId(userId);
                return Ok(blogDTOs);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Handle cases where the user or blogs aren't found
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Authorize] 
        [HttpPost("add")]
        public async Task<IActionResult> AddBlog([FromForm] AddBlogDTO blogDTO)
        {
            var errors = new List<string>();
            try
            {
                // Retrieve the user ID from the JWT claim
                var userId = User.FindFirst("userId")?.Value; // Adjust based on your JWT claims

                if (string.IsNullOrEmpty(userId)) // If UserId is not found in the token
                {
                    return Unauthorized(new { message = "User ID not found in token." });
                }

                blogDTO.UserId = userId;

                // Check if there's an image and validate its size
                if (blogDTO.Image != null && blogDTO.Image.Length > MaxFileSizeInBytes)
                {
                    return BadRequest(new { message = "Image size exceeds the 3 MB limit." });
                }

                var response = await _blogService.AddBlog(blogDTO, errors); // Ensure awaiting asynchronous operation

                if (errors.Count > 0) // If there are validation errors
                {
                    return BadRequest(new { errors }); // Return 400 Bad Request with errors
                }

                return Ok(new { message = response }); // Return success response
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message }); // Return 500 Internal Server Error
            }
        }


        [Authorize]
        [HttpPut("update-blog")]
        public async Task<IActionResult> UpdateBlog([FromForm] UpdateBlogDTO updateBlogDTO)
        {
            try
            {
                var userId = User.FindFirst("userId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User ID not found in token." });
                }

                var blog = await _blogService.GetBlogById(updateBlogDTO.BlogId);

                if (blog.UserId != userId) // Only allow the blog author to update
                {
                    return StatusCode(403, new { message = "Only the blog author can update this post." });
                }
                // Check the image file size during update
                if (updateBlogDTO.Image != null && updateBlogDTO.Image.Length > MaxFileSizeInBytes)
                {
                    return BadRequest(new { message = "Image size exceeds the 3 MB limit." });
                }
                var errors = new List<string>();
                var response = await _blogService.UpdateBlog(updateBlogDTO, errors);

                if (errors.Count > 0)
                {
                    return BadRequest(new { errors });
                }

                return Ok(new { message = response });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("delete/{blogId}")]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var errors = new List<string>();
            try
            {
                var userId = User.FindFirst("userId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User ID not found in token." });
                }

                var blog = await _blogService.GetBlogById(blogId);

                if (blog.UserId != userId) // Only allow the blog author to delete
                {
                    return StatusCode(403, new { message = "Only the blog author can delete this post." });
                }

                var response = await _blogService.DeleteBlog(blogId, errors);

                if (errors.Count > 0)
                {
                    return BadRequest(new { errors });
                }

                return Ok(new { message = response });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }
    }
}
