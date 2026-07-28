using System;
using System.Buffers;
using System.Collections.Generic;

namespace srm.utils
{
#if NETSTANDARD2_0
    internal static class PolyFills
    {
        extension(string)
        {
            public static string Create<TState>(int length, TState state, SpanAction<char, TState> action)
            {
                string result = new string('\0', length);
                unsafe
                {
                    fixed (char* ptr = result)
                        action.Invoke(new Span<char>(ptr, length * sizeof(char)), state);
                }
                return result;
            }
        }

        public static bool TryPop<T>(this Stack<T> stack, [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out T? result)
        {
            if (stack.Count <= 0)
            {
                result = default;
                return false;
            }
            result = stack.Pop()!;
            return true;
        }

        public static bool TryGetValue<T>(this HashSet<T> set, T equalValue, out T actualValue)
        {
            if (!set.Contains(equalValue))
                goto Failed;
            IEqualityComparer<T> comparer = set.Comparer;
            foreach (T? item in set)
            {
                if (comparer.Equals(item, equalValue))
                {
                    actualValue = item;
                    return true;
                }
            }

        Failed:
            actualValue = default!;
            return false;
        }
    }
#endif
}

namespace System.Buffers
{
    internal delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
}
