using UnityEngine;

namespace Lang.ChainLetterJam
{
    public static class LetterBoxStates
    {
        public struct State
        {
            public Color Color;
            public bool IsSnagged;
            public bool IsWin;
            public bool IsFwoop;
            public bool IsUI;
            public bool IsDestroyed;
        }

        public static State Default { get; } = new State
        {
            Color = Color.white,
        };

        public static State Want { get; } = new State
        {
            Color = Color.red,
        };

        public static State Snagged { get; } = new State
        {
            Color = Color.cyan,
            IsSnagged = true
        };

        public static State Win { get; } = new State
        {
            Color = Color.yellow,
            IsWin = true,
            IsUI = true
        };

        public static State Fwoop { get; } = new State
        {
            Color = Color.gray,
            IsFwoop = true
        };

        public static State Menu { get; } = new State
        {
            Color = Color.cyan,
            IsUI = true
        };

        public static State Destroyed { get; } = new State
        {
            Color = Color.black,
            IsDestroyed = true
        };
    }
}
