using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Helpers.Controls
{
    public static class PagesNodesNames
    {

        #region ALL NAMES TITLES FOR LEFT MENU OPTIONS [NODES]
        //users main button 
        public static string UsersPageButtonTitle { get; set; } = "Users";
        public static string UsersPrimaryPageButtonName { get; set; } = "users";
        //adduser main button
        public static string AddUserPageButtonTitle { get; set; } = "Add New User";
        public static string AddUserPrimaryPageButtonName { get; set; } = "adduser";
        //usercard main button
        public static string UserCardPageButtonTitle { get; set; } = "User Card";
        public static string UserCardPrimaryPageButtonName { get; set; } = "usercard";
        #endregion






        /// <summary>
        /// return real name depend on some code name
        /// </summary>
        /// <param name="primaryname">code name</param>
        /// <returns></returns>
        public static string BringFriendlyName(string primaryname)
        {
            if (primaryname == PagesNodesNames.UsersPrimaryPageButtonName)
            {
                return PagesNodesNames.UsersPageButtonTitle;
            }

            if (primaryname == PagesNodesNames.AddUserPrimaryPageButtonName)
            {
                return PagesNodesNames.AddUserPageButtonTitle;
            }

            if (primaryname == PagesNodesNames.UserCardPrimaryPageButtonName)
            {
                return PagesNodesNames.UserCardPageButtonTitle;
            }

            else
            {
                return "no_name";
            }
        }



        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
        #region ALL NAMES TITLES FOR RIGHT MENU OPTIONS [NODES}
        //=>users nodes
        public static string UsersFirstButtonTitle { get; set; } = "Print out current records";
        public static string UsersSecondButtonTitle { get; set; } = "Refresh users table";
        //=>adduser nodes
        public static string AddUserFirstButtonTitle { get; set; } = "Print out user details";
        public static string AddUserSecondButtonTitle { get; set; } = "Proceed to add new record";
        //=>usercard nodes
        public static string UserCardFirstButtonTitle { get; set; } = "Print out user card";
        public static string UserCardSecondButtonTitle { get; set; } = "Find a User";
        #endregion
        
    }
}
