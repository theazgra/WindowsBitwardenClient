namespace BitwardenNET.CliInterop
{
    internal class CliFlag
    {
        /// <summary>
        /// Flag name, will be used as --Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Flag value or null for basic flag.
        /// </summary>
        public object Value { get; private set; } = null;

        /// <summary>
        /// True if add hyphen/s.
        /// </summary>
        public bool IsFlag { get; private set; } = true;

        private CliFlag() { }

        internal static CliFlag Flag(char flag) => new CliFlag() { Name = flag.ToString() };
        internal static CliFlag Flag(string flag) => new CliFlag() { Name = flag };
        internal static CliFlag FlagWithValue(string flag, object value) => new CliFlag() { Name = flag, Value = value };
        internal static CliFlag StringValue(string value) => new CliFlag() { Value = value, IsFlag = false};


        /// <summary>
        /// Return the string representation of the flag. Can be used as a program argument.
        /// </summary>
        public override string ToString()
        {
            if (!IsFlag)
            {
                return (string)Value;
            }

            if (Name.Length > 1)
            {
                if (Value != null)
                {
                    return $"--{Name} {Value}";
                }
                else
                {
                    return $"--{Name}";
                }
            }
            else if (Name.Length == 1)
            {
                if (Value != null)
                {
                    return $"-{Name} {Value}";
                }
                else
                {
                    return $"-{Name}";
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
