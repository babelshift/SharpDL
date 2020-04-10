using System;

namespace SharpDL
{
    [Flags]
    public enum InitializeType
    {
        Timer = 1,
        Audio = 16,
        Video = 32,
        Joystick = 512,
        Haptic = 4096,
        GameController = 8192,
        Events = 16384,
        Sensor = 32768,
        Everything = 62001,
        NoParachute = 1048576
    }
}