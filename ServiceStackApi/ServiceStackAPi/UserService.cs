using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStackAPi
{
    public class UserService:Service
    {
        //htp://localhost:56012/json/oneway/UserRequestDTO/1
        //htp://localhost:56012/xml/oneway/UserRequestDTO/1
        public object Get(UserRequestDTO req)
        {
            UserResponseDTO _resp;
            switch (req.Id)
            {
                case 1: _resp=new UserResponseDTO{Name="Asif",Age=22, City="Sydney"};
                    break;
                 case 2: _resp=new UserResponseDTO{Name="Peter",Age=44, City="USA"};
                    break;
                case 3: _resp=new UserResponseDTO{Name="Tom",Age=32, City="Delhi"};
                    break;
                default: _resp=new UserResponseDTO{Name="Ram",Age=12, City="Mumbai"};
                    break;
            }
            return _resp;
        }

        //htp://localhost:56012/json/oneway/UserListRequestDTO
        //htp://localhost:56012/xml/oneway/UserListRequestDTO
        public object Get(UserListRequestDTO req)
        {
            List<UserListDTO> _user = new List<UserListDTO>();
            _user.Add(new UserListDTO { Name = "Asif", Age = 22, City = "Sydney" });
            _user.Add(new UserListDTO { Name = "Sam", Age = 12, City = "Delhi" });
            _user.Add(new UserListDTO { Name = "Ram", Age = 42, City = "Mumbai" });
            return _user;
        }
    }
}