using BisleriumProject.Application.Common.Interface.IRepositories;
using BisleriumProject.Application.Common.Interface.IServices;
using BisleriumProject.Application.DTOs;
using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Entities;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BisleriumProject.Infrastructures.Services
{
    public class BlogService : IBlogService
    {
        private Cloudinary _cloudinary;
        private CloudinarySettings _cloudinarySettings;
        private Account _account;
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;

        public BlogService(IBlogRepository blogsRepository, UserManager<User> userManager, IOptions<CloudinarySettings> cloudinarySettingsOptions)
        {
            _cloudinarySettings = cloudinarySettingsOptions.Value;
            _account = new Account(
                _cloudinarySettings.CloudName,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret);
            _cloudinary = new Cloudinary(_account);
            _blogRepository = blogsRepository;
            _userManager = userManager;
        }

        public async Task<List<BlogDTO>> GetAll()
        {
            // Retrieve all blogs associated with the userId
            var blogs = await _blogRepository.GetAll(null); // Get all blogs


            var blogDTOs = new List<BlogDTO>();

            foreach (var blog in blogs)
            {
                var user = await _userManager.FindByIdAsync(blog.UserId); // Use await to avoid blocking

                var blogDTO = new BlogDTO
                {
                    Description = blog.Description,
                    CreatedDate = blog.CreatedDate,
                    IsEdited = blog.IsEdited,
                    Category = blog.Category,
                    Image = blog.Image.ToString(),
                    Title = blog.Title,
                    UserId = user.Id  // Ensure UserId is passed to the DTO
                };

                blogDTOs.Add(blogDTO);
            }

            return blogDTOs.OrderByDescending(r => r.CreatedDate).ToList(); // Order by CreatedDate
        }
        
        public async Task<List<BlogDTO>> GetBlogsByUserId(string userId)
        {
            // Retrieve all blogs associated with the userId
            var blogs = await _blogRepository.GetAll(null); // Get all blogs

            // Filter blogs by UserId
            var userBlogs = blogs.Where(blog => blog.UserId == userId).ToList();

            var blogDTOs = new List<BlogDTO>();

            foreach (var blog in userBlogs)
            {
                var user = await _userManager.FindByIdAsync(blog.UserId); // Use await to avoid blocking

                var blogDTO = new BlogDTO
                {
                    Description = blog.Description,
                    CreatedDate = blog.CreatedDate,
                    IsEdited = blog.IsEdited,
                    Category = blog.Category,
                    Image = blog.Image.ToString(),
                    Title = blog.Title,
                    UserId = user.Id  // Ensure UserId is passed to the DTO
                };

                blogDTOs.Add(blogDTO);
            }

            return blogDTOs.OrderByDescending(r => r.CreatedDate).ToList(); // Order by CreatedDate
        }

        public async Task<string> AddBlog(AddBlogDTO blogDTO, List<string> errors)
        {
            // Upload the image to Cloudinary
            var imageId = UploadImageToCloudinary(blogDTO.Image, "Blogs/Images");
            if (imageId == Guid.Empty)
            {
                errors.Add("Image upload failed.");
                return null; // Return null to indicate failure
            }

            // Validate UserId before adding the blog
            var user = await _userManager.FindByIdAsync(blogDTO.UserId);
            if (user == null)
            {
                errors.Add("User not found.");
                return "Blog addition failed.";
            }

            // Create a new Blog object
            var newBlog = new Blog
            {
                Description = blogDTO.Description,
                IsEdited = false,
                Category = blogDTO.Category,
                CreatedDate = DateTime.UtcNow,
                Image = imageId,
                Title = blogDTO.Title,
                UserId = blogDTO.UserId // Ensure valid UserId
            };

            // Add the blog to the repository and commit changes
            await _blogRepository.Add(newBlog); // Add to repository
            await _blogRepository.SaveChangesAsync(); // Commit changes

            return "Blog added successfully.";
        }


        public async Task<string> DeleteBlog(int blogId, List<string> errors)
        {
            var blog = await _blogRepository.GetById(blogId);

            if (blog == null)
            {
                errors.Add("Blog not found.");
                return "Blog deletion failed.";
            }

            await _blogRepository.Delete(blog);
            await _blogRepository.SaveChangesAsync(); // Ensure SaveChangesAsync() is called

            return "Blog deleted successfully.";
        }

        private Guid UploadImageToCloudinary(IFormFile file, string folder)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = folder,
                    PublicId = Guid.NewGuid().ToString(),
                    Transformation = new Transformation().FetchFormat("auto")
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                if (uploadResult.Error != null)
                {
                    return Guid.Empty;
                }

                var publicIdParts = uploadResult.PublicId.Split('/');
                var guidPart = publicIdParts.Last();

                return Guid.Parse(guidPart);
            }
        }


    }
}
