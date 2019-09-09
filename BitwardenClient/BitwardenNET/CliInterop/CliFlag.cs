namespace BitwardenNET.CliInterop
{
    internal class CliFlag
    {
        /// <summary>
        /// Flag name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Flag value.
        /// </summary>
        public object Value { get; set; } = null;

        public CliFlag(string flagName)
        {
            Name = flagName;
        }
        public CliFlag(string flagName, object flagValue)
        {
            Name = flagName;
            Value = flagValue;
        }

        public override string ToString()
        {
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
