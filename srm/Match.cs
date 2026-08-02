// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.SRM
{
    public struct Match
    {
        internal static readonly Match NoMatch = new Match(-1, -1);

        public int Index { get; private set; }

        public int Length { get; private set; }

        public readonly bool Success => Index >= 0;

        public Match(int index, int length)
        {
            Index = index;
            Length = length;
        }

        public static bool operator ==(Match left, Match right)
            => left.Index == right.Index && left.Length == right.Length;

        public static bool operator !=(Match left, Match right) => !(left == right);

        public override readonly bool Equals(object obj) => obj is Match other && this == other;

        public override readonly int GetHashCode() => (Index, Length).GetHashCode();
    }
}
