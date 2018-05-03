using System;
using System.Globalization;
using Anlog.Renderers;
using Xunit;

namespace Anlog.Tests.Renderers
{
    /// <summary>
    /// Tests for <see cref="DefaultDataRenderer"/>
    /// </summary>
    public class DefaultDataRendererTests
    {
        /// <summary>
        /// Object to test.
        /// </summary>
        private readonly DefaultDataRenderer renderer;

        public DefaultDataRendererTests()
        {
            renderer = new DefaultDataRenderer();
        }

        [Fact]
        public void WhenRenderDate_RenderData()
        {
            var date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var rendering = renderer.RenderDate(date).Render();
            
            Assert.Equal(date, rendering);
        }

        [Fact]
        public void WhenRenderLevel_RenderData()
        {
            var level = LogLevel.Info;
            var rendering = renderer.RenderLevel(level, level.ToString()).Render();
            
            Assert.Equal(level.ToString(), rendering);
        }

        [Fact]
        public void WhenRenderKey_RenderData()
        {
            var key = Guid.NewGuid().ToString();
            var rendering = renderer.RenderKey(key).Render();
            
            Assert.Equal(key, rendering);
        }

        [Fact]
        public void WhenRenderValue_RenderData()
        {
            var value = Guid.NewGuid().ToString();
            var rendering = renderer.RenderValue(value).Render();
            
            Assert.Equal(value, rendering);
        }

        [Fact]
        public void WhenRenderException_RenderData()
        {
            var value = Guid.NewGuid().ToString();
            var rendering = renderer.RenderException(value).Render();
            
            Assert.Equal(value, rendering);
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