using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.CliInterop
{
    internal class BitwardenCliCommandResult
    {
        public bool Success { get; set; }
        public int ExitCode { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
    }
}
