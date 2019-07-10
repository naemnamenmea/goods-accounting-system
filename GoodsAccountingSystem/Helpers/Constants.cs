﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Helpers
{
    public static class Constants
    {
        public static string GOODS = "Goods";
        public static string ACCOUNT = "Account";
        public static string USERS = "Users";
        public static string ADMIN = "Admin";

        public static string CREATE = "Create";
        public static string DELETE = "Delete";
        public static string EDIT = "Edit";

        public static string MODAL_HOLDER = "modalHolder";
        public static string FORM_TO_SUBMIT_ID = "formToSubmitId";

        public static string MODAL_ID = "modalId";

        public static Dictionary<string, string> Method =
            new Dictionary<string, string>()
            {
                { CREATE, "post" },
                { EDIT, "put" },
                { DELETE, "delete" }
            };
    }
}
