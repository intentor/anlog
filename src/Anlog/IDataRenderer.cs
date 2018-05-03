namespace Anlog
{
    /// <summary>
    /// Provides rendering of data in the log.
    /// <para/>
    /// Renderers are stateful and should be instantiated per log writing.
    /// </summary>
    public interface IDataRenderer
    {
        /// <summary>
        /// Renders a formatted date/time value.
        /// </summary>
        /// <param name="formattedDate">Formatted date/time value.</param>
        /// <returns>Current renderer, for chaining.</returns>
        IDataRenderer RenderDate(string formattedDate);

        /// <summary>
        /// Renders a formatted date/time value.
        /// </summary>
        /// <param name="level">Level being rendered.</param>
        /// <param name="formattedLevel">Formatted level name.</param>
        /// <returns>Current renderer, for chaining.</returns>
        IDataRenderer RenderLevel(LogLevel level, string formattedLevel);

        /// <summary>
        /// Renders a log key.
        /// </summary>
        /// <param name="key">Log key.</param>
        /// <returns>Current renderer, for chaining.</returns>
        IDataRenderer RenderKey(string key);

        /// <summary>
        /// Renders a log value.
        /// </summary>
        /// <param name="value">Log Value.</param>
        /// <returns>Current renderer, for chaining.</returns>
        IDataRenderer RenderValue(string value);

        /// <summary>
        /// Renders an invariant data.
        /// </summary>
        /// <param name="invariant">Invariant data.</param>
        /// <returns>Current renderer, for chaining.</returns>
        IDataRenderer RenderInvariant(string invariant);

        /// <summary>
        /// Removes the last character added for rendering.
        /// <para/>
        /// It's useful when adding e.g. commas on lists, where sometimes the last added character should not be a comma.
        /// </summary>
        /// <returns>Current renderer, for chaining.</returns>
        IDataRenderer RemoveLastCharacter();

        /// <summary>
        /// Renders the log.
        /// </summary>
        /// <returns>Rendered log.</returns>
        string Render();
    }
}