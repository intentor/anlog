using System.Text;
using Anlog.Renderers.ConsoleThemes;

namespace Anlog.Renderers
{
    /// <summary>
    /// Writes logs to a string builder adding console colors to data.
    /// <para/>
    /// It acts as a decorator to the formatter in use.
    /// </summary>
    public class ThemedConsoleDataRenderer : IDataRenderer
    {
        /// <summary>
        /// Ansi code to reset a color.
        /// </summary>
        public const string ResetColor = "\x1b[0m";
        
        /// <summary>
        /// Internal string builder.
        /// </summary>
        private StringBuilder builder = new StringBuilder();
        
        /// <summary>
        /// Console theme.
        /// </summary>
        private IConsoleTheme theme;

        /// <summary>
        /// Initializes a new instance of see <see cref="ThemedConsoleDataRenderer"/>.
        /// </summary>
        public ThemedConsoleDataRenderer(IConsoleTheme theme)
        {
            this.theme = theme;
        }
        
        /// <inheritdoc />
        public IDataRenderer RenderDate(string formattedDate)
        {
            builder.Append(theme.DefaultColor);
            builder.Append(formattedDate);
            builder.Append(ResetColor);
            
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderLevel(LogLevel level, string formattedLevel)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    builder.Append(theme.LevelDebugColor);
                    break;
                
                case LogLevel.Info:
                    builder.Append(theme.LevelInfoColor);
                    break;
                
                case LogLevel.Warn:
                    builder.Append(theme.LevelWarnColor);
                    break;
                
                case LogLevel.Error:
                    builder.Append(theme.LevelErrorColor);
                    break;
            }
            
            builder.Append(formattedLevel);
            builder.Append(ResetColor);
            
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                builder.Append(theme.KeyColor);
                builder.Append(key);
                builder.Append(ResetColor);
            }
            
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                builder.Append(theme.ValueColor);
                builder.Append(value);
                builder.Append(ResetColor);
            }
            
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderException(string value)
        {
            builder.Append(theme.ExceptionColor);
            builder.Append(value);
            builder.Append(ResetColor);
            return this;
        }

        /// <inheritdoc />
        public IDataRenderer RenderInvariant(string invariant)
        {
            builder.Append(invariant);
            return this;
        }
        
        /// <inheritdoc />
        public IDataRenderer RemoveLastCharacter()
        {
            builder.Length--;
            return this;
        }

        /// <inheritdoc />
        public string Render()
        {
            return builder.ToString();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Render();
        }
    }
}