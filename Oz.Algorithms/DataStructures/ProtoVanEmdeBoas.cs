using System;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class ProtoVanEmdeBoas
    {
        public ProtoVanEmdeBoas(int size)
        {
            if (size != 2 && !IsSize2K(size))
            {
                throw new ArgumentException(
                    $"Allowed Sizes: {string.Join(" ", AvailableSizes)}, Real Size: {size}");
            }

            Size = size;
            switch (Size)
            {
                case > 2:
                {
                    var childSize = (int) Math.Sqrt(Size);
                    Summary = new ProtoVanEmdeBoas(childSize);
                    Cluster = new ProtoVanEmdeBoas[childSize];
                    for (var i = 0; i < Cluster.Length; i++)
                    {
                        Cluster[i] = new ProtoVanEmdeBoas(childSize);
                    }

                    break;
                }
                case 2:
                    Keys = new byte[2];
                    break;
                default:
                    throw new ArgumentException(nameof(Size));
            }
        }

        public int Size { get; }

        private ProtoVanEmdeBoas Summary { get; }
        private ProtoVanEmdeBoas[] Cluster { get; }

        private byte[] Keys { get; }

        public bool IsLeaf => Keys != null;

        private uint[] AvailableSizes
        {
            get
            {
                const uint one = 1U;
                var sizes = new List<uint>();
                for (var bitCount = 1; Math.Pow(2, bitCount) < 32; bitCount++)
                {
                    var powerOf2In2K = one << (int) Math.Pow(2, bitCount);
                    sizes.Add(powerOf2In2K);
                }

                return sizes.ToArray();
            }
        }

        private bool IsSize2K(int size)
        {
            const uint one = 1U;
            var found = false;
            for (var bitCount = 1; Math.Pow(2, bitCount) < 32; bitCount++)
            {
                var powerOf2In2K = one << (int) Math.Pow(2, bitCount);
                if ((size & powerOf2In2K) == size)
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        public int KeyHigh(int key)
        {
            return (int) Math.Floor(key / Math.Sqrt(Size));
        }

        public int KeyLow(int key)
        {
            return key % (int) Math.Sqrt(Size);
        }

        public int Index(int x, int y)
        {
            return x * (int) Math.Sqrt(Size) + y;
        }

        public bool IsMember(int key)
        {
            return _IsMember(this, key);
        }

        public int? Minimum()
        {
            return _Minimum(this);
        }

        public int? Maximum()
        {
            return _Maximum(this);
        }

        public void Insert(int key)
        {
            _Insert(this, key);
        }

        public int? Successor(int key)
        {
            return _Successor(this, key);
        }

        public int? Predecessor(int key)
        {
            return _Predecessor(this, key);
        }

        public void Delete(int key)
        {
            _Delete(this, key);
        }

        private static bool _IsMember(ProtoVanEmdeBoas proto, int key)
        {
            if (proto.Size == 2)
            {
                return proto.Keys[key] != 0;
            }

            return _IsMember(proto.Cluster[proto.KeyHigh(key)], proto.KeyLow(key));
        }


        private static void _Delete(ProtoVanEmdeBoas proto, int key)
        {
            if (proto.Size == 2)
            {
                proto.Keys[key] = 0;
            }
            else
            {
                _Delete(proto.Cluster[proto.KeyHigh(key)], proto.KeyLow(key));
                var inCluster = false;

                var sqrtOfSize = (int) Math.Sqrt(proto.Size);
                var lowBorder = 0;
                var highBorder = sqrtOfSize;
                var keyHigh = proto.KeyHigh(key);

                for (var i = lowBorder; i < highBorder; i++)
                {
                    if (_IsMember(proto.Cluster[keyHigh], i))
                    {
                        inCluster = true;
                        break;
                    }
                }

                if (!inCluster)
                {
                    _Delete(proto.Summary, proto.KeyHigh(key));
                }
            }
        }

        private static int? _Predecessor(ProtoVanEmdeBoas proto, int key)
        {
            if (proto.Size == 2)
            {
                if (key == 1 && proto.Keys[0] == 1)
                {
                    return 0;
                }

                return null;
            }

            var offset = _Predecessor(proto.Cluster[proto.KeyHigh(key)], proto.KeyLow(key));
            if (offset.HasValue)
            {
                return proto.Index(proto.KeyHigh(key), offset.Value);
            }

            var predCluster = _Predecessor(proto.Summary, proto.KeyHigh(key));
            if (!predCluster.HasValue)
            {
                return null;
            }

            var maxKey = _Maximum(proto.Cluster[predCluster.Value]);
            if (!maxKey.HasValue)
            {
                return null;
            }

            return proto.Index(predCluster.Value, maxKey.Value);
        }

        private static int? _Successor(ProtoVanEmdeBoas proto, int key)
        {
            if (proto.Size == 2)
            {
                if (key == 0 && proto.Keys[1] == 1)
                {
                    return 1;
                }

                return null;
            }

            var offset = _Successor(proto.Cluster[proto.KeyHigh(key)], proto.KeyLow(key));
            if (offset.HasValue)
            {
                return proto.Index(proto.KeyHigh(key), offset.Value);
            }

            var successorCluster = _Successor(proto.Summary, proto.KeyHigh(key));
            if (!successorCluster.HasValue)
            {
                return null;
            }

            offset = _Minimum(proto.Cluster[successorCluster.Value]);
            if (!offset.HasValue)
            {
                return null;
            }

            return proto.Index(successorCluster.Value, offset.Value);
        }

        private static void _Insert(ProtoVanEmdeBoas proto, int key)
        {
            if (proto.Size == 2)
            {
                proto.Keys[key] = 1;
            }
            else
            {
                _Insert(proto.Cluster[proto.KeyHigh(key)], proto.KeyLow(key));
                _Insert(proto.Summary, proto.KeyHigh(key));
            }
        }

        private static int? _Maximum(ProtoVanEmdeBoas proto)
        {
            if (proto.Size == 2)
            {
                if (proto.Keys[1] == 1)
                {
                    return 1;
                }

                if (proto.Keys[0] == 1)
                {
                    return 0;
                }

                return null;
            }

            var maxCluster = _Maximum(proto.Summary);
            if (!maxCluster.HasValue)
            {
                return null;
            }

            var offset = _Maximum(proto.Cluster[maxCluster.Value]);
            if (!offset.HasValue)
            {
                return null;
            }

            return proto.Index(maxCluster.Value, offset.Value);
        }

        private static int? _Minimum(ProtoVanEmdeBoas proto)
        {
            if (proto.Size == 2)
            {
                if (proto.Keys[0] == 1)
                {
                    return 0;
                }

                if (proto.Keys[1] == 1)
                {
                    return 1;
                }

                return null;
            }

            var minCluster = _Minimum(proto.Summary);
            if (minCluster == null)
            {
                return null;
            }

            var offset = _Minimum(proto.Cluster[minCluster.Value]);
            if (!offset.HasValue)
            {
                return null;
            }

            return proto.Index(minCluster.Value, offset.Value);
        }
    }
}