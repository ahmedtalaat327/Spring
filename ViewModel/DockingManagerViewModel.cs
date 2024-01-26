
using Oracle.ManagedDataAccess.Client;
using Spring.AccioHelpers;
using Spring.Data;
using Spring.ViewModel.Base;
using Spring.ViewModel.Command;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Spring.ViewModel
{
    public class DockingManagerViewModel : BaseViewModel
    {
        #region Fields
        private bool _firstLoad = true;
        #endregion
        #region Properties
        /// <summary>
        /// flag to detrmine wheter the window is visible or not
        /// </summary>
        public bool WindowVisible { get; set; } = false;
        /// <summary>
        /// Logged user currently
        /// </summary>
        public User loggedUser { get; set; } = new User() { FullName = "xxx.xxx.xx" };
        /// <summary>
        /// Name on the account button
        /// </summary>
        public string NameBannser { get { return loggedUser.FullName; } }
        /// <summary>
        /// Detiremines whether progress bar is shown or not also determines if loading happens or not.
        /// </summary>
        public bool Loading { get; set; }
        /// <summary>
        /// the current object opened and closed depend on params will be loaded onLoad only
        /// </summary>
        public OracleConnection MyAppOnlyObjctConn { get; set; }

        #endregion
        public DockingManagerViewModel()
        {
            Loading = false;

        }
        /// <summary>
        /// check current tunnle conn
        /// </summary>
        /// <returns></returns>
        public async Task<OracleConnection> ReadyMyDatabaseConn()
        {
                if(_firstLoad)
                MyAppOnlyObjctConn = await GetOracleConnection(false);

                return  MyAppOnlyObjctConn;
            

        }
        /// <summary>
        /// Get the oracle Connection
        /// </summary>
        /// <param name="closeOrNot">Flag to close the connection or not before using in other commands statements</param>
        /// <returns></returns>
        private Task<OracleConnection> GetOracleConnection(bool closeOrNot)
        {
            return Task.Run(() =>
            {
                _firstLoad = false;
                return AccioEasyHelpers.ReadParamsThenConnectToDB(closeOrNot);
            });
        }

    }
}

