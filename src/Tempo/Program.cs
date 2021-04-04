using System;
using System.Threading;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Tempo.Windowing;

namespace Tempo
{
    internal static class Program
    {
        private static void Main()
        {
            var windowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(1280, 720),
                Title = "Tempo"
            };

            using var window = new Window(GameWindowSettings.Default, windowSettings)
            {
                VSync = VSyncMode.Off
            };
            
            window.Run();
        }
    }
}