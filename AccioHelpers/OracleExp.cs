using CliWrap;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spring.AccioHelpers
{
    public class OracleExp
    {
        /// <summary>
        /// private fields
        /// </summary>
        private string userName, password, dbInstace, urlToSave;
        private string urlToProc = null;
        /// <summary>
        /// constructor to assign all fields
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pass"></param>
        /// <param name="instance"></param>
        /// <param name="url"></param>
        public OracleExp(string username,string pass, string instance, string url) { 
        
            this.userName = username;
            this.password = pass;
            this.dbInstace = instance;
            this.urlToSave = url;

        }
        /// <summary>
        /// Start task related to load all params then put the command.
        /// </summary>
        /// <returns></returns>
        public async Task StartProc()
        {
            urlToProc = AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring for Server.exe").Length) ;
            //var xxx = urlToProc;
            await Task.Run(async () =>
            {

                var cts = new CancellationTokenSource();
                
                var stdOutBuffer = new StringBuilder();
                var stdErrBuffer = new StringBuilder();
                try
                {
                    await Cli.Wrap("powershell.exe")
                        .WithArguments(new[] { $@"& '{urlToProc}process\OracleExportDB.exe'" + " " + userName + " " + password + " " + dbInstace + " " + urlToSave })
                         
                        .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
                        .WithStandardErrorPipe(PipeTarget.ToDelegate(Console.WriteLine))
                        .ExecuteAsync();
                }
                catch (OperationCanceledException)
                {
                    // Command was canceled
                    cts.Cancel();
                }
                 
             
              

            });
        }
        
    }
}
