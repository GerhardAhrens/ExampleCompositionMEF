namespace ExampleCompositionMEF
{
    /// <summary>
    /// The contract interface.
    /// Both, main application and modules are working against this interface.
    /// </summary>
    public interface ICalculator
    {
        string Version { get; }

        string Description {get;}

        /// <summary>
        /// Calculates a value out of two given decimal values.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns></returns>
        decimal Calculate(decimal value1, decimal value2);
    }
}
