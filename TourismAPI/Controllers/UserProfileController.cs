using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Tourism.Repository.Contracts;
using Tourism.Repository.DTO;
using Tourism.Repository.Models;

namespace TourismAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly ILogger<UserProfileController> _logger;
        public UserProfileController(IRepositoryWrapper repository, IMapper mapper, ILogger<UserProfileController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] CreateUserProfileDto user)
        {
            try
            {
                if (user is null)
                {
                    _logger.LogError("CreateUser - UserProfile object sent from client is null.");
                    return BadRequest("CreateUser - UserProfile object is null");
                }
                //Checks whether the passed object has all the required fields
                if (!ModelState.IsValid)
                {
                    _logger.LogError("CreateUser - " + ModelState + " Invalid UserProfile object sent from client.");
                    return UnprocessableEntity(ModelState);
                }

                //Check if user already created
                _logger.LogInformation($"Check if userName already created inside CreateUser - {user.UserName}.");
                Task<UserProfile> dupUser = _repository.User.GetUser(user.UserName);
                if(dupUser.Result != null)
                {
                    _logger.LogError($"Duplicate userName inside CreateUser action - {user.UserName}.");
                    return StatusCode(500, $"Duplicate userName - Username <strong> {user.UserName} </strong> already created.");
                }

                //Mapping CreateUserProfileDto to UserProfile object
                UserProfile userprofile = _mapper.Map<UserProfile>(user);
                userprofile.CreatedDate = DateTime.Now;
                userprofile.UpdatedBy = userprofile.UpdatedBy;
                userprofile.UpdatedDate = DateTime.Now;

                //EncryptPassword
                string strPwd = Utility.EncryptPassword(user.Password);

                userprofile.Password = strPwd ;

                _logger.LogInformation("Calling CreateUser method.");
                Task<bool> flag = _repository.User.CreateUser(userprofile);
                //Checks if flag is true
                if (flag.Result)
                {
                    _logger.LogInformation($"User created successfully");
                    return Ok();
                }
                else
                {
                    _logger.LogError($"Something went wrong inside CreateUser action.");
                    return StatusCode(500, "Internal server error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>
        /// Get GetAllUsers 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Calling GetAllUsers to get user profile data from DB.");
                IEnumerable<UserProfile> user = await _repository.User.GetAllUsers();

                //Check if Branches list has any data
                if (!user.Any())
                {
                    _logger.LogError($"GetAllUsers - no userprofile data found");
                    return NotFound("GetAllUsers - no userprofile data found");
                }

                IEnumerable<UserProfileDto> userResult = _mapper.Map<IEnumerable<UserProfileDto>>(user);

                _logger.LogInformation($"UserProfile data returned successfully");
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllUsers - Something went wrong inside GetAllUsers action: {ex}");
                return StatusCode(500, "GetAllUsers - Internal server error");
            }
        }

        private async Task<byte[]> EncryptPasswordAsync(string password, string passphrase)
        {
            using Aes aes = Aes.Create();
            aes.Key = DeriveKeyFromPassword(passphrase);
            //aes.IV = IV;
            using MemoryStream output = new();
            using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);
            await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(password));
            await cryptoStream.FlushFinalBlockAsync();
            return output.ToArray();
        }

        private byte[] DeriveKeyFromPassword(string passphrase)
        {
            var emptySalt = Array.Empty<byte>();
            var iterations = 1000;
            var desiredKeyLength = 16; // 16 bytes equal 128 bits.
            var hashMethod = HashAlgorithmName.SHA384;
            return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(passphrase),
                                             emptySalt,
                                             iterations,
                                             hashMethod,
                                             desiredKeyLength);
        }

        //public async Task<string> DecryptPasswordAsync(byte[] encrypted, string passphrase)
        //{
        //    using Aes aes = Aes.Create();
        //    aes.Key = DeriveKeyFromPassword(passphrase);
        //    //aes.IV = IV;
        //    using MemoryStream input = new(encrypted);
        //    using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);
        //    using MemoryStream output = new();
        //    await cryptoStream.CopyToAsync(output);
        //    return Encoding.Unicode.GetString(output.ToArray());
        //}

        private string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
