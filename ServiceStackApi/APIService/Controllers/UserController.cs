using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIService.Controllers
{
    public class UserController : ApiController
    {
        //htp://localhost:56223/api/user/GetUserList
        public string GetUserName(int id)
        {
            string name = string.Empty;
            switch (id)
            {
                case 1: name = "Asif";
                    break;
                 case 2: name = "Anwar";
                    break;
                case 3: name = "Tom";
                    break;
                default: name = "No Id";
                    break;
            }
            return name;
        }
        //htp://localhost:56223/api/user/GetUserList/1
        public List<string> GetUserList()
        {
            return new List<string> { "Asif","Anwar","Tom"};
        }
    }
}
