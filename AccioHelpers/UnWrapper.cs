using CliWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.AccioHelpers
{
    public class UnWrapper
    {
        private  string WrappingK { get; set; } = "nope";

       
     

        public async Task<string> TrigProcAsync()
        {
            await Task.Run(async () =>
            {
                string spath = AccioEasyHelpers.MeExistanceLocation().Substring(0, AccioEasyHelpers.MeExistanceLocation().Length - ("Spring.exe").Length);

                try
                {

                    await Cli.Wrap("powershell.exe")
                     
                     .WithArguments(new[] { $@"& '{spath}process\Coffen.exe'" + " " + "'sys$1234'" })

                     .WithStandardOutputPipe(PipeTarget.ToDelegate(HandleLinesForMimRunning))

                     .WithStandardErrorPipe(PipeTarget.ToDelegate(Console.WriteLine))

                     .ExecuteAsync();
                }
                catch (OperationCanceledException)
                {
                    // Command was canceled
                    Console.WriteLine("The operation was canceled.");
                }


                
            });
            return WrappingK;

        }
        private void HandleLinesForMimRunning(string outLine)
        {
            if (!String.IsNullOrEmpty(outLine))
            {
                Console.WriteLine(outLine);

                WrappingK = outLine;
            }

        }
    }
}
