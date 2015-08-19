using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStackAPi
{
    public class UserRequestDTO
    {
        public int Id { get; set; }
    }
    public class UserResponseDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public class UserListRequestDTO
    {
        public int Id { get; set; }
    }
    public class UserListDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}