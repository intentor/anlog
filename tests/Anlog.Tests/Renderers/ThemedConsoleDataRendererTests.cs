using System;
using System.Collections.Generic;
using System.Globalization;
using Anlog.Renderers;
using Anlog.Renderers.ConsoleThemes;
using Xunit;
using static Anlog.Renderers.ThemedConsoleDataRenderer;

namespace Anlog.Tests.Renderers
{
    /// <summary>
    /// Tests for <see cref="ThemedConsoleDataRenderer"/>
    /// </summary>
    public class ThemedConsoleDataRendererTests
    {
        /// <summary>
        /// Console theme used in tests.
        /// </summary>
        private static readonly IConsoleTheme ConsoleTheme = new DefaultConsoleTheme();
        
        /// <summary>
        /// Object to test.
        /// </summary>
        private readonly ThemedConsoleDataRenderer renderer;

        public ThemedConsoleDataRendererTests()
        {
            renderer = new ThemedConsoleDataRenderer(ConsoleTheme);
        }

        [Fact]
        public void WhenRenderDate_RenderData()
        {
            var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var rendering = renderer.RenderDate(date).Render();
            
            Assert.Equal(string.Concat(ConsoleTheme.DefaultColor, date, ResetColor), rendering);
        }
        
        /// <summary>
        /// Render level test data.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetRenderLevelTestData()
        {
            return new List<object[]> {
                new object[] { LogLevel.Debug, ConsoleTheme.LevelDebugColor },
                new object[] { LogLevel.Info, ConsoleTheme.LevelInfoColor },
                new object[] { LogLevel.Warn, ConsoleTheme.LevelWarnColor },
                new object[] { LogLevel.Error, ConsoleTheme.LevelErrorColor }
            };
        }

        [Theory]
        [MemberData(nameof(GetRenderLevelTestData))]
        public void WhenRenderLevel_RenderData(LogLevel level, string color)
        {
            var rendering = renderer.RenderLevel(level, level.ToString()).Render();
            
            Assert.Equal(string.Concat(color, level, ResetColor), rendering);
        }

        [Fact]
        public void WhenRenderKey_RenderData()
        {
            var key = Guid.NewGuid().ToString();
            var rendering = renderer.RenderKey(key).Render();
            
            Assert.Equal(string.Concat(ConsoleTheme.KeyColor, key, ResetColor), rendering);
        }

        [Fact]
        public void WhenRenderValue_RenderData()
        {
            var value = Guid.NewGuid().ToString();
            var rendering = renderer.RenderValue(value).Render();
            
            Assert.Equal(string.Concat(ConsoleTheme.ValueColor, value, ResetColor), rendering);
        }

        [Fact]
        public void WhenRenderException_RenderData()
        {
            var value = Guid.NewGuid().ToString();
            var rendering = renderer.RenderException(value).Render();
            
            Assert.Equal(string.Concat(ConsoleTheme.ExceptionColor, value, ResetColor), rendering);
        }

        [Fact]
        public void WhenRenderInvariant_RenderData()
        {
            var invariant = Guid.NewGuid().ToString();
            var rendering = renderer.RenderInvariant(invariant).Render();
            
            Assert.Equal(invariant, rendering);
        }

        [Fact]
        public void WhenRemoveLastCharacter_RemoveIt()
        {
            var invariant = Guid.NewGuid().ToString();
            var rendering = renderer.RenderInvariant(invariant).RemoveLastCharacter().Render();
            
            Assert.Equal(invariant.Substring(0, invariant.Length - 1), rendering);
        }
    }
}