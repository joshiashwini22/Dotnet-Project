﻿using BisleriumProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumProject.Application.Common.Interface.IServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<string> DeleteUser(string id, List<string> errors);
        Task<string> UpdateUser(UpdateDTO updateUserDTO, List<string> errors);
        Task<UserDTO> GetUserById(string userId);
        Task<string> GetUserNameById(string userId);
        Task<string> UpdateUserDetails(UpdateUserDTO updateUserDTO, List<string> errors);
    }
}
