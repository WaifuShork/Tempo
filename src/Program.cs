using System;
using System.Threading;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using OpenTKRenderer.Windowing;

namespace OpenTKRenderer
{
    internal static class Program
    {
        private static void Main()
        {
            var windowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1280, 720),
                Title = "OpenTK"
            };

            using var window = new Window(GameWindowSettings.Default, windowSettings)
            {
                VSync = VSyncMode.Off
            };
            
            window.Run();
        }
    }
}