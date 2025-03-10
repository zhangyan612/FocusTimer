using System;
using System.Runtime.InteropServices;
using System.Threading;

class MouseMover
{
    // Import required Windows API functions
    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    // For simulating actual mouse input (more effective for preventing screen locks)
    [DllImport("user32.dll")]
    static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    // Mouse event constants
    private const int MOUSEEVENTF_MOVE = 0x0001;

    // Structure to hold cursor position
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Enhanced Random Mouse Mover Started!");
        Console.WriteLine("Press Ctrl+C to exit...");
        Console.WriteLine("Moving mouse at random intervals between 5-30 seconds...");

        Random random = new Random();

        // Main program loop
        while (true)
        {
            // Get current mouse position
            POINT currentPosition;
            GetCursorPos(out currentPosition);
            
            // Generate random offsets (between -15 and 15 pixels)
            int offsetX = random.Next(-15, 16);
            int offsetY = random.Next(-15, 16);
            
            // Calculate new position
            int newX = currentPosition.X + offsetX;
            int newY = currentPosition.Y + offsetY;
            
            // Move the mouse using both methods for better screen lock prevention
            
            // Method 1: Direct position setting
            SetCursorPos(newX, newY);
            
            // Method 2: Simulate actual mouse movement
            // This is more likely to be detected as user activity by the OS
            mouse_event(MOUSEEVENTF_MOVE, offsetX, offsetY, 0, 0);
            
            // Get the current time for logging
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            
            Console.WriteLine($"[{currentTime}] Moved mouse from ({currentPosition.X}, {currentPosition.Y}) " +
                              $"to ({newX}, {newY}) by offset ({offsetX}, {offsetY})");
            
            // Generate random wait time between 5 and 30 seconds
            int waitTime = random.Next(5, 31) * 1000;
            
            Console.WriteLine($"Waiting for {waitTime/1000} seconds before next movement...");
            
            // Wait for the random interval
            Thread.Sleep(waitTime);
        }
    }
}
