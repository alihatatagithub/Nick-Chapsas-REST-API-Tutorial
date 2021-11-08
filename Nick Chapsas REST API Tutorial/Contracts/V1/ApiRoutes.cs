using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        
        public static class Posts
        {
            public const string GetAll = Base + "/" + "post";
            public const  string Update = Base + "/post/{postId}";  //Last Modify
            public const  string Delete = Base + "/post/{postId}";
            public const  string Get = Base + "/post/{postId}";
            public const  string Create = Base + "/post";

        }

        public static class Identity
        {
            public const string Login = Base + "/Identity/Login";
            public const string Register = Base + "/Identity/Register";

        }


    }
}
