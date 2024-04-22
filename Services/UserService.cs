using otpsystemback.Interfaces;
using System.Security.Cryptography;
using otpsystemback.Data.Helpers;
using otpsystemback.Models;
using otpsystemback.Models.ModelToEntity;
using otpsystemback.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace otpsystemback.Services
{
    public class UserService : IUserService
    {
        private readonly char[] punctuations = Characters.Punctuations;
        private readonly IRegisterUserModelToEntity registerUserModelToEntity;
        private readonly IUserRepository userRepository;
        private readonly JwtSettings jwtSettings;
        public UserService(IRegisterUserModelToEntity registerUserModelToEntity, IUserRepository userRepository, JwtSettings jwtSettings)
        {
            this.registerUserModelToEntity = registerUserModelToEntity;
            this.userRepository = userRepository;
            this.jwtSettings = jwtSettings;
        }
        public string GeneratePass()
        {
            var r = new Random();
            int length = r.Next(8, 20);
            int numberOfNonAlphanumericCharacters = r.Next(1, 4);
            string password = this.Generate(length, numberOfNonAlphanumericCharacters);
            return password;
            
        }

        public string GenerateToken(string password)
        {
            var Token = JwtHelpers.JwtHelpers.GetTokenKey(new UserToken()
            {
                Password = password,
                Id = 0,
            }, jwtSettings);
            if (Token.Token != null)
                return Token.Token;
            return "";
        }

        public void AddUser(UserRegisterModel registeredUser)
        {
            User user = this.registerUserModelToEntity.RegisterModelToUser(registeredUser);
            var salt = GetRandomSalt();
            user.Salt = salt;
            user.PasswordHash = HashPassword(user.PasswordHash, salt);
            this.userRepository.Create(user);
        }

        public IActionResult GetUserToken(string email)
        {
            User? currentUser = userRepository.Get().FirstOrDefault(x => x.Email == email);
            if (currentUser == null)
                return new BadRequestObjectResult("Problem appeared while register. Please try again.");
            var Token = JwtHelpers.JwtHelpers.GetTokenKey(new UserToken()
            {
                Email = currentUser.Email,
                Id = currentUser.Id,
            }, jwtSettings);
            if (Token.Token != null)
                return new OkObjectResult(Token.Token);
            return new BadRequestObjectResult("Problem appeared while register. Please try again.");
        }

        public IActionResult verifyUser(string email, string password)
        {
            User? currentUser = userRepository.Get().FirstOrDefault(x => x.Email == email);
            if (currentUser != null)
            {
                var userHashPassword = getHashedPassword(currentUser, password);
                if (currentUser.PasswordHash.Equals(userHashPassword))
                {
                    var Token = JwtHelpers.JwtHelpers.GetTokenKey(new UserToken()
                    {
                        Email = currentUser.Email,
                        Id = currentUser.Id,
                    }, jwtSettings);
                    if (Token.Token != null)
                        return new OkObjectResult(Token.Token);
                }
                return new BadRequestObjectResult("Sorry, your email or password is incorrect.");
            }
            return new BadRequestObjectResult("Sorry, your email or password is incorrect.");
        }

        public int GetUserId(string email)
        {
            return userRepository.Get().First(x => x.Email == email).Id;
        }

        private string getHashedPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, user.Salt);
        }
        private string Generate(int length, int numberOfNonAlphanumericCharacters)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];

                rng.GetBytes(byteBuffer);

                var count = 0;
                var characterBuffer = new char[length];

                for (var iter = 0; iter < length; iter++)
                {
                    var i = byteBuffer[iter] % 87;

                    if (i < 10)
                    {
                        characterBuffer[iter] = (char)('0' + i);
                    }
                    else if (i < 36)
                    {
                        characterBuffer[iter] = (char)('A' + i - 10);
                    }
                    else if (i < 62)
                    {
                        characterBuffer[iter] = (char)('a' + i - 36);
                    }
                    else
                    {
                        characterBuffer[iter] = punctuations[i - 62];
                        count++;
                    }
                }

                if (count >= numberOfNonAlphanumericCharacters)
                {
                    return new string(characterBuffer);
                }

                int j;
                var rand = new Random();

                for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
                {
                    int k;
                    do
                    {
                        k = rand.Next(0, length);
                    }
                    while (!char.IsLetterOrDigit(characterBuffer[k]));

                    characterBuffer[k] = punctuations[rand.Next(0, punctuations.Length)];
                }

                return new string(characterBuffer);
            }
        }
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        private static string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

    }
}
