using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            #region API Client
            var apiclient = new JsonServiceClient("http://localhost:56223/api/user/");
            var apireconResponse = apiclient.Get(string.Format("GetUserList"));
            var apireconResponseText = apireconResponse.ReadToEnd(); 
            #endregion

            #region ServiceStack Client
            var ssclient = new JsonServiceClient("http://localhost:56012/json/oneway/");
            var ssreconResponse = ssclient.Get(string.Format("UserListRequestDTO"));
            var ssreconResponseText = ssreconResponse.ReadToEnd();
            #endregion
        }
    }
}
