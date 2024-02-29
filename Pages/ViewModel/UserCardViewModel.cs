using Spring.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Pages.ViewModel
{
    public class UserCardViewModel : BaseViewModel
    {
        #region Members

        #endregion
        #region Properties
        /// <summary>
        /// corp-logo property
        /// </summary>
        public Image CorporationLogo { get; set; }  
        /// <summary>
        /// user photo property
        /// </summary>
        public Image UserPicture { get; set; }
        /// <summary>
        /// Name in card
        /// </summary>
        public string EmpName { get; set; }
        /// <summary>
        /// department name
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// iputed id
        /// </summary>
        public int IdOfCardUser { get; set; } = 221;
        /// <summary>
        /// checker for id
        /// </summary>
        public bool IdCheckerVisiblity { get; set; } = true;

        #endregion
        #region Commands

        #endregion

        #region Constructor
        public UserCardViewModel() {

        }
        #endregion
        #region Methods
        #endregion

    }
}
