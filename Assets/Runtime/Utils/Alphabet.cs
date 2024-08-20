using System.Collections.Generic;
using UnityEngine;

namespace UniCore.Utils
{
    // From : https://gist.github.com/b-cancel/c516990b8b304d47188a7fa8be9a1ad9

    public static class Alphabet
    {
        public const char SPACE = ' ';
        public const char DOT = '.';
        public const char DASH = '-';
        public const char QUOTE = '\'';
        public const char EXCLAMATION = '!';
        public const char INTERROGATION = '?';
        public const char BIGDOT = '●';
        public const char UNDERSCORE = '_';
        public const char START = '*';

        public static readonly HashSet<char> Ponctuation = new()
        {
            ',', ';', '"', ':', '=', '+', '(', ')', '[', ']', '{', '}', '#', '@'
        };

        public static readonly Dictionary<KeyCode, char> KeyCodePairs = new()
        {
            { KeyCode.Space, SPACE },

            { KeyCode.A, 'a' },
            { KeyCode.B, 'b' },
            { KeyCode.C, 'c' },
            { KeyCode.D, 'd' },
            { KeyCode.E, 'e' },
            { KeyCode.F, 'f' },
            { KeyCode.G, 'g' },
            { KeyCode.H, 'h' },
            { KeyCode.I, 'i' },
            { KeyCode.J, 'j' },
            { KeyCode.K, 'k' },
            { KeyCode.L, 'l' },
            { KeyCode.M, 'm' },
            { KeyCode.N, 'n' },
            { KeyCode.O, 'o' },
            { KeyCode.P, 'p' },
            { KeyCode.Q, 'q' },
            { KeyCode.R, 'r' },
            { KeyCode.S, 's' },
            { KeyCode.T, 't' },
            { KeyCode.U, 'u' },
            { KeyCode.V, 'v' },
            { KeyCode.W, 'w' },
            { KeyCode.X, 'x' },
            { KeyCode.Y, 'y' },
            { KeyCode.Z, 'z' },

            //{ KeyCode.Quote, QUOTE },
            //{ KeyCode.Alpha4, QUOTE },

            //{ KeyCode.Minus, DASH },
            //{ KeyCode.Alpha6, DASH },
            //{ KeyCode.KeypadMinus, DASH },


            //{ KeyCode.Keypad1, '1' },
            //{ KeyCode.Keypad2, '2' },
            //{ KeyCode.Keypad3, '3' },
            //{ KeyCode.Keypad4, '4' },
            //{ KeyCode.Keypad5, '5' },
            //{ KeyCode.Keypad6, '6' },
            //{ KeyCode.Keypad7, '7' },
            //{ KeyCode.Keypad8, '8' },
            //{ KeyCode.Keypad9, '9' },
            //{ KeyCode.Keypad0, '0' },

            //{ KeyCode.Exclaim, '!' },
            //{ KeyCode.DoubleQuote, '"' },
            //{ KeyCode.Hash, '#' },
            //{ KeyCode.Dollar, '$' },
            //{ KeyCode.Ampersand, '&' },
            //{ KeyCode.LeftParen, '(' },
            //{ KeyCode.RightParen, ')' },
            //{ KeyCode.Asterisk, '*' },
            //{ KeyCode.Plus, '+' },
            //{ KeyCode.Comma, ',' },
            //{ KeyCode.Minus, '-' },
            //{ KeyCode.Period, '.' },
            //{ KeyCode.Slash, '/' },
            //{ KeyCode.Colon, ':' },
            //{ KeyCode.Semicolon, ';' },
            //{ KeyCode.Less, '<' },
            //{ KeyCode.Equals, '=' },
            //{ KeyCode.Greater, '>' },
            //{ KeyCode.Question, '?' },
            //{ KeyCode.At, '@' },
            //{ KeyCode.LeftBracket, '[' },
            //{ KeyCode.Backslash, '\\' },
            //{ KeyCode.RightBracket, ']' },
            //{ KeyCode.Caret, '^' },
            //{ KeyCode.Underscore, '_' },
            //{ KeyCode.BackQuote, '`' },
        };

        public static readonly Dictionary<char, KeyCode> CharPairs = new()
        {
            { SPACE, KeyCode.Space },

            { 'a', KeyCode.A },
            { 'b', KeyCode.B },
            { 'c', KeyCode.C },
            { 'd', KeyCode.D },
            { 'e', KeyCode.E },
            { 'f', KeyCode.F },
            { 'g', KeyCode.G },
            { 'h', KeyCode.H },
            { 'i', KeyCode.I },
            { 'j', KeyCode.J },
            { 'k', KeyCode.K },
            { 'l', KeyCode.L },
            { 'm', KeyCode.M },
            { 'n', KeyCode.N },
            { 'o', KeyCode.O },
            { 'p', KeyCode.P },
            { 'q', KeyCode.Q },
            { 'r', KeyCode.R },
            { 's', KeyCode.S },
            { 't', KeyCode.T },
            { 'u', KeyCode.U },
            { 'v', KeyCode.V },
            { 'w', KeyCode.W },
            { 'x', KeyCode.X },
            { 'y', KeyCode.Y },
            { 'z', KeyCode.Z },

            //{ QUOTE, KeyCode.Quote },
            //{ DASH, KeyCode.Minus },

            //{ '1', KeyCode.Keypad1 },
            //{ '2', KeyCode.Keypad2 },
            //{ '3', KeyCode.Keypad3 },
            //{ '4', KeyCode.Keypad4 },
            //{ '5', KeyCode.Keypad5 },
            //{ '6', KeyCode.Keypad6 },
            //{ '7', KeyCode.Keypad7 },
            //{ '8', KeyCode.Keypad8 },
            //{ '9', KeyCode.Keypad9 },
            //{ '0', KeyCode.Keypad0 },

            //{ '!', KeyCode.Exclaim },
            //{ '"', KeyCode.DoubleQuote },
            //{ '#', KeyCode.Hash },
            //{ '$', KeyCode.Dollar },
            //{ '&', KeyCode.Ampersand },
            //{ '(', KeyCode.LeftParen },
            //{ ')', KeyCode.RightParen },
            //{ '*', KeyCode.Asterisk },
            //{ '+', KeyCode.Plus },
            //{ ',', KeyCode.Comma },
            //{ '-', KeyCode.Minus },
            //{ '.', KeyCode.Period },
            //{ '/', KeyCode.Slash },
            //{ ':', KeyCode.Colon },
            //{ ';', KeyCode.Semicolon },
            //{ '<', KeyCode.Less },
            //{ '=', KeyCode.Equals },
            //{ '>', KeyCode.Greater },
            //{ '?', KeyCode.Question },
            //{ '@', KeyCode.At },
            //{ '[', KeyCode.LeftBracket },
            //{ '\\', KeyCode.Backslash },
            //{ ']', KeyCode.RightBracket },
            //{ '^', KeyCode.Caret },
            //{ '_', KeyCode.Underscore },
            //{ '`', KeyCode.BackQuote },
        };
    }
}
