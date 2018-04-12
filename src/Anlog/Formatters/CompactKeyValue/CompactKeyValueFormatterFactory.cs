using System.Globalization;
using Anlog.Factories;

namespace Anlog.Formatters.CompactKeyValue
{
    /// <summary>
    /// Factory for the <see cref="CompactKeyValueFormatter"/>.
    /// </summary>
    public static class CompactKeyValueFormatterFactory
    {
        /// <summary>
        /// Logger factory.
        /// </summary>
        internal static LogFormatter Factory => (minimumLevel, sink, path, name, number) =>
            new CompactKeyValueFormatter(minimumLevel, sink, path, name, number);

        /// <summary>
        /// Sets the log formatter as compact key/value pair.
        /// </summary>
        /// <param name="formatterFactory">Formatter factory.</param>
        /// <param name="culture">Culture to be used. The default is <see cref="CultureInfo.InvariantCulture"/>.</param>
        /// <param name="dateTimeFormat">Date/time log format. The default format is "yyyy-MM-dd HH:mm:ss.fff".</param>
        /// <returns>Logger factory.</returns>
        public static LoggerFactory CompactKeyValue(this LogFormatterFactory formatterFactory, 
            CultureInfo culture = null,
            string dateTimeFormat = null)
        {
            PrepareFormatter(culture, dateTimeFormat);
            formatterFactory.Formatter = Factory;
            return formatterFactory.Factory;
        }

        /// <summary>
        /// Prepares the formatter with singleton data.
        /// </summary>
        /// <param name="culture">Culture to be used.</param>
        /// <param name="dateTimeFormat">Date/time log format.</param>
        public static void PrepareFormatter(CultureInfo culture = null, string dateTimeFormat = null)
        {
            CompactKeyValueFormatter.Culture =  culture ?? CultureInfo.InvariantCulture;
            CompactKeyValueFormatter.DateTimeFormat = string.IsNullOrEmpty(dateTimeFormat) ? 
                "yyyy-MM-dd HH:mm:ss.fff" : dateTimeFormat;
            CompactKeyValueFormatter.Getters = TypeGettersUtils.GetDataContractGetters();
        }
    }
}
    