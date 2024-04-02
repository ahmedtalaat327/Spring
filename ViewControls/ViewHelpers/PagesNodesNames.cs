
/////////////////////////////////////////////by dev.@ahmedtalaat327 on github/////////////////////////////
/////////////////THIS CLASS MADE FOR CONTROLE ALL VIEWs WITH SMALL OPTIONS AND LITTLE EFFORTS/////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////


using Spring.Pages;
using Spring.Pages.ChartsPages;
using System.Collections.Generic;



namespace Spring.Helpers.Controls
{
    public static class PagesNodesNames
    {
        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
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
        //Change or Terminate Current User button
        public static string ChangeorTerminateCurrentUserPageButtonTitle { get; set; } = "Change or Terminate Current User";
        public static string ChangeorTerminateCurrentUserPrimaryPageButtonName { get; set; } = "changeorterminatecurrentuser";
        #endregion



        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
        #region ALL NAMES TITLES FOR RIGHT MENU OPTIONS [NODES}
        //=>users nodes
        public static string UsersFirstButtonTitle { get; set; } = "Print out current records";
        public static string UsersSecondButtonTitle { get; set; } = "Refresh users table";
        //=>adduser nodes
        public static string AddUserFirstButtonTitle { get; set; } = "Print out user details";
        public static string AddUserSecondButtonTitle { get; set; } = "Proceed to add new record";
        public static string AddUserThirdButtonTitle { get; set; } = "Generate QR image";
        //=>usercard nodes
        public static string UserCardFirstButtonTitle { get; set; } = "Print out user card";
        public static string UserCardSecondButtonTitle { get; set; } = "Find a User";
        //=>changeorterminatecurrentuser nodes
        public static string ChangeorTerminateCurrentUserFirstButtonTitle { get; set; } = "Update these values";
        public static string ChangeorTerminateCurrentUserSecondButtonTitle { get; set; } = "Terminate current user";

        #endregion


        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
        #region Lists FOR BOTH PRIM & FRIENDLY TITLES {LEFT} & FRIENDLY FOR {RIGHT}
        public static List<string> ALLLEFTPRIMARYTITLES { get; set; } = new List<string>() {
            UsersPrimaryPageButtonName , AddUserPrimaryPageButtonName , UserCardPrimaryPageButtonName , ChangeorTerminateCurrentUserPrimaryPageButtonName
        };

        public static List<List<string>> ALLRIGHTTITLES { get; set; } = new List<List<string>>() {
            new List<string>(){UsersFirstButtonTitle, UsersSecondButtonTitle},
            new List<string>(){AddUserFirstButtonTitle, AddUserSecondButtonTitle,AddUserThirdButtonTitle},
            new List<string>(){UserCardFirstButtonTitle, UserCardSecondButtonTitle},
            new List<string>(){ ChangeorTerminateCurrentUserFirstButtonTitle, ChangeorTerminateCurrentUserSecondButtonTitle},
        };
        ///-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-icons
        public static List<List<int[]>> RIGHTOPTIONSICONS { get; set; } = new List<List<int[]>>()
        {
              new List<int[]>{ new int[] { 27 }, new int[] { 6 } },
              new List<int[]>{ new int[] { 27 },  new int[] { 34 }, new int[] { 64 }},
              new List<int[]>{ new int[] { 27 }, new int[] { 41 } },
              new List<int[]>{ new int[] { 35 }, new int[] { 56 } },
        };
        #endregion
        #region PageConrrols List
        public static List<BasePage> PagesViewsControls { get; set; } = new List<BasePage>{
            new UsersPage(),new AddUserPage(),new UserCardPage() , new BasePage()
        };
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
            if (primaryname == PagesNodesNames.ChangeorTerminateCurrentUserPrimaryPageButtonName)
            {
                return PagesNodesNames.ChangeorTerminateCurrentUserPageButtonTitle;
            }

            else
            {
                return "no_name";
            }
        }



        
        
    }
}
