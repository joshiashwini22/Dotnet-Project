using BisleriumProject.Application.Common.Interface.IServices;
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

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

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

        [HttpGet("get-by-user")]
        public async Task<IActionResult> GetBlogsByUserId(string userId)
        {
            try
            {
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

        //[Authorize] // Optionally require authorization for this endpoint
        [HttpPost("add")]
        public async Task<IActionResult> AddBlog([FromForm] AddBlogDTO blogDTO)
        {
            var errors = new List<string>();
            try
            {
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


        //[Authorize] // Optionally require authorization for this endpoint
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var errors = new List<string>();
            try
            {
                var response = _blogService.DeleteBlog(id, errors);
                if (errors.Count > 0) // If there are validation errors or blog not found, return BadRequest
                {
                    return BadRequest(new { errors });
                }
                return Ok(new { message = response });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message); // Handle internal errors
            }
        }
    }
}
