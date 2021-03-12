using UnityEngine;

namespace Lang.ChainLetterJam
{
    public static class LetterBoxStates
    {
        static Color WarmYellow = new Color(0.94901960784f, 0.61568627451f, 0.1725490196f);

        public struct State
        {
            public Color Color;
            public bool IsSnagged;
            public bool IsWin;
            public bool IsFwoop;
            public bool IsUI;
            public bool IsDestroyed;
            public float Glow;
        }

        public static State Default { get; } = new State
        {
            Color = new Color(0.5f, 0.5f, 0.5f),
            Glow = 0.6f
        };

        public static State Want { get; } = new State
        {
            Color = WarmYellow,
            Glow = 3f
        };

        public static State Snagged { get; } = new State
        {
            Color = Color.white,
            IsSnagged = true,
            Glow = 2f
        };

        public static State Win { get; } = new State
        {
            Color = WarmYellow,
            IsWin = true,
            IsUI = true,
            Glow = 4f

        };

        public static State Fwoop { get; } = new State
        {
            Color = Color.gray,
            IsFwoop = true,
            Glow = 0.6f
        };

        public static State Menu { get; } = new State
        {
            Color = WarmYellow,
            IsUI = true,
            Glow = 2.5f
        };

        public static State Destroyed { get; } = new State
        {
            Color = new Color(0.2f, 0.2f, 0.2f),
            IsDestroyed = true,
            Glow = 0.6f
        };
    }
}
