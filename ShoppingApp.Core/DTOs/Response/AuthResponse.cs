using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core.DTOs.Response
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Token { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
