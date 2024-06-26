﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using otpsystemback.Interfaces;
using otpsystemback.Models;

namespace otpsystemback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private IUserValidator userValidator;
        public UserController(IUserService userService, IUserValidator userValidator)
        {
            this.userService = userService;
            this.userValidator = userValidator;

        }

        [HttpGet("GeneratePass")]
        public IActionResult GeneratePass()
        {
            string password = this.userService.GeneratePass();
            if (password != null)
            {
                string passToken = this.userService.GenerateToken(password);
                return Ok(new { passToken, DateTime.Now });
            }
            return BadRequest("Error generating password");
        }

        [HttpPost("ValidatePass")]
        public IActionResult ValidatePass([FromBody] string password)
        {
            return this.userValidator.ValidateOTP(password);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserRegisterModel user)
        {
            var response = this.userValidator.ValidateUserEmail(user.Email);
            if (!String.IsNullOrEmpty(response))
            {
                return BadRequest(response);
            }
            if (!this.userValidator.CheckIfEmailExist(user.Email))
            {
                this.userService.AddUser(user);
                return this.userService.GetUserToken(user.Email);
            }
            return BadRequest("User already exists");

        }

        [HttpPost("CheckIfUserExist")]
        public IActionResult GetVerifiedUser([FromBody] LoginModel loginModel)
        {
            if (loginModel.Email != null && loginModel.Password != null)
                return userService.verifyUser(loginModel.Email, loginModel.Password);
            return BadRequest("Sorry, your email or password is incorrect.");
        }

        [HttpGet("GetId/{email}")]
        public IActionResult GetUserId([FromRoute] string email)
        {
            if (this.userValidator.CheckIfEmailExist(email))
            {
                var userId = this.userService.GetUserId(email);
                if(userId != null)
                {
                    return Ok(userId);
                }
            }
            return BadRequest("Something went wrong while retrieving your id");
        }

        [HttpGet("VerifyUserAlreadyRegistered/{userId}")]
        public IActionResult verifyUserRegistered([FromRoute] int userId)
        {
            return Ok();
        }

    }
}
