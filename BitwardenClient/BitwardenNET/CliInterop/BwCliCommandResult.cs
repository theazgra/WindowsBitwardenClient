using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.CliInterop
{
    internal class BwCliCommandResult
    {
        public int ReturnCode { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
    }
}
