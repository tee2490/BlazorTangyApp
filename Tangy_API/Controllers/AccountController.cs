﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tangy_Common;
using Tangy_Models;

namespace Tangy_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost] //ลงทะเบียนผู้ใช้
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
        {
            if (signUpRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            //สร้างออบเจคผู้ใช้
            var user = new ApplicationUser
            {
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                Name = signUpRequestDTO.Name,
                PhoneNumber = signUpRequestDTO.PhoneNumber,
                EmailConfirmed = true
            };

            //สร้างผู้ใช้
            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }

            //สร้าง role ให้ผู้ใช้
            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_Customer);
            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }

            return StatusCode(201); //รหัสสำเร็จ
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.UserName, signInRequestDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new SignInResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                //everything is valid and we need to login 

            }
            else
            {
                return Unauthorized(new SignInResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }

            return StatusCode(201);
        }

    }
}
