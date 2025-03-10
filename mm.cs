using System;
using System.Runtime.InteropServices;
using System.Threading;

public class MM
{
    // Import required Windows API functions
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    // Structure to hold cursor position
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Random Mouse Mover Started!");
        Console.WriteLine("Press Ctrl+C to exit...");
        Console.WriteLine("Moving mouse every 10 seconds...");

        Random random = new Random();

        // Main program loop
        while (true)
        {
            // Get current mouse position
            POINT currentPosition;
            GetCursorPos(out currentPosition);
            
            // Generate random offsets (between -10 and 10 pixels)
            int offsetX = random.Next(-10, 11);
            int offsetY = random.Next(-10, 11);
            
            // Calculate new position
            int newX = currentPosition.X + offsetX;
            int newY = currentPosition.Y + offsetY;
            
            // Move the mouse
            SetCursorPos(newX, newY);
            
            Console.WriteLine($"Moved mouse from ({currentPosition.X}, {currentPosition.Y}) " +
                              $"to ({newX}, {newY}) by offset ({offsetX}, {offsetY})");
            
            // Wait for 10 seconds
            Thread.Sleep(10000);
        }
    }
}
