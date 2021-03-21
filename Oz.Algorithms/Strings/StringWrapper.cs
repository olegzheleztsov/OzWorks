using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Oz.Algorithms.Strings
{
    public class StringWrapper : IEnumerable<char>
    {
        private readonly string _string;
        
        public StringWrapper(string @string)
        {
            _string = @string;
        }

        public StringWrapper(StringWrapper stringWrapper)
        {
            _string = stringWrapper._string;
        }

        public char this[int index] => _string[index - 1];

        public int Length => _string?.Length ?? 0;

        public static bool IsNullOrEmpty(StringWrapper stringWrapper)
            =>string.IsNullOrWhiteSpace(stringWrapper._string);

        public static StringWrapper Empty
            => new StringWrapper(string.Empty);

        public bool SubstringEquals(int sourceStartIndex, int sourceEndIndex, StringWrapper target,
            int targetStartIndex)
        {
            Debug.Assert(sourceStartIndex <= sourceEndIndex);
            var count = sourceEndIndex - sourceStartIndex + 1;
            Debug.Assert(targetStartIndex + count - 1 <= target[target.Length]);
            for (var i = 0; i < count; i++)
            {
                if (this[sourceStartIndex + i] != target[targetStartIndex + i])
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return _string.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
            => _string?.ToString() ?? string.Empty;

        public static StringWrapper operator +(StringWrapper stringWrapperLeft, StringWrapper stringWrapperRight)
            => new StringWrapper(stringWrapperLeft._string + stringWrapperRight._string);

        public static StringWrapper operator +(StringWrapper stringWrapperLeft, string stringRight)
            => new StringWrapper(stringWrapperLeft._string + stringRight);

        public static StringWrapper operator +(string stringLeft, StringWrapper stringWrapperRight)
            => new StringWrapper(stringLeft + stringWrapperRight._string);

        public static bool operator ==(StringWrapper stringWrapperLeft, StringWrapper stringWrapperRight)
        {
            if (object.ReferenceEquals(stringWrapperLeft, stringWrapperRight))
            {
                return true;
            }

            return stringWrapperLeft?.Equals(stringWrapperRight) ?? false;
        }

        public static bool operator !=(StringWrapper stringWrapperLeft, StringWrapper stringWrapperRight)
        {
            return !(stringWrapperLeft == stringWrapperRight);
        }
        
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj switch
            {
                null => false,
                StringWrapper other => _string.Equals(other._string),
                _ => false
            };
        }

        public override int GetHashCode()
        {
            return _string?.GetHashCode() ?? 0;
        }
        
        public static implicit operator StringWrapper(string text)
        {
            return new StringWrapper(text);
        }

        public StringWrapper Substring(int index, int length)
        {
            return _string.Substring(index - 1, length);
        }

        public StringWrapper Prefix(int length)
        {
            return new StringWrapper(_string.Substring(0, length));
        }

        public bool IsSuffix(StringWrapper other)
        {
            if (other.Length > Length)
            {
                return false;
            }
            
            for (int i = Length, k = other.Length; i > (Length - other.Length); i--, k--)
            {
                if (this[i] != other[k])
                {
                    return false;
                }
            }

            return true;
        }
    }
}