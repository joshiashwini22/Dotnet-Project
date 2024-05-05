﻿using BisleriumProject.Application.DTOs;
using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Entities;

namespace BisleriumProject.Application.Common.Interface.IServices
{
    public interface IBlogService
    {
        Task<List<BlogDTO>> GetAll();
        Task<List<BlogDTO>> GetBlogsByUserId(string id);
        Task<string> AddBlog(AddBlogDTO blog, List<string> errors);
        Task<string> DeleteBlog(int id,List<string> errors);
        Task<string> UpdateBlog(UpdateBlogDTO updateBlogDTO, List<string> errors);
        Task<BlogDTO> GetBlogById(int blogId);

    }
}
