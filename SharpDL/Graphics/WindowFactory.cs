using System;
using Microsoft.Extensions.Logging;

namespace SharpDL.Graphics
{
    public class WindowFactory : IWindowFactory
    {
        private readonly ILogger<WindowFactory> logger;
        private readonly ILogger<Window> loggerWindow;

        public WindowFactory(
            ILogger<WindowFactory> logger = null, 
            ILogger<Window> loggerWindow = null)
        {
            this.logger = logger;
            this.loggerWindow = loggerWindow;
        }

        public IWindow CreateWindow(string title)
        {
            return CreateWindow(title, 100, 100, 1280, 720, WindowFlags.Shown);
        }

        public IWindow CreateWindow(string title, int x, int y)
        {
            return CreateWindow(title, x, y, 1280, 720, WindowFlags.Shown);
        }

        public IWindow CreateWindow(string title, int x, int y, int width, int height)
        {
            return CreateWindow(title, x, y, width, height, WindowFlags.Shown);
        }

        public IWindow CreateWindow(string title, int x, int y, int width, int height, WindowFlags flags)
        {
            try
            {
                var window = new Window(title, x, y, width, height, flags, loggerWindow);
                logger?.LogTrace($"Window created. Title = {window.Title}, X = {window.X}, Y = {window.Y}, Width = {window.Width}, Height = {window.Height}, Handle = {window.Handle}.");
                return window;
            }
            catch(Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}