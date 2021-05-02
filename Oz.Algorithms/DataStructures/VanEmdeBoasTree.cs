#region

using System;
using System.Diagnostics;

#endregion

namespace Oz.Algorithms.DataStructures
{
    public class VanEmdeBoasTree
    {
        private readonly VanEmdeBoasNode _root;

        public VanEmdeBoasTree(int size)
        {
            if (!Util.IsPowerOf2(size))
            {
                throw new ArgumentException($"Size of tree should be power of 2. Actual size: {size}");
            }

            _root = new VanEmdeBoasNode(size);
        }

        public override string ToString()
        {
            return _root.ToString();
        }


        public int? Minimum()
        {
            return _NodeMinimum(_root);
        }

        public int? Maximum()
        {
            return _NodeMaximum(_root);
        }

        public bool IsMember(int key)
        {
            return _IsMember(_root, key);
        }

        public int? Successor(int key)
        {
            return _Successor(_root, key);
        }

        public int? Predecessor(int key)
        {
            return _Predecessor(_root, key);
        }

        public void Insert(int key)
        {
            _Insert(_root, key);
        }

        public void Delete(int key)
        {
            _Delete(_root, key);
        }


        private static int? _NodeMinimum(VanEmdeBoasNode node)
        {
            return node.Minimum;
        }

        private static int? _NodeMaximum(VanEmdeBoasNode node)
        {
            return node.Maximum;
        }

        private static bool _IsMember(VanEmdeBoasNode node, int key)
        {
            if (key == _NodeMinimum(node) || key == _NodeMaximum(node))
            {
                return true;
            }

            return node.Size != 2 && _IsMember(node.Cluster[node.High(key)], node.Low(key));
        }

        private static int? _Successor(VanEmdeBoasNode node, int key)
        {
            if (node.Size == 2)
            {
                if (key == 0 && node.Maximum == 1)
                {
                    return 1;
                }

                return null;
            }

            if (node.Minimum != null && key < node.Minimum)
            {
                return node.Minimum;
            }

            var maxLow = _NodeMaximum(node.Cluster[node.High(key)]);
            if (maxLow != null && node.Low(key) < maxLow)
            {
                var offset = _Successor(node.Cluster[node.High(key)], node.Low(key));
                if (offset != null)
                {
                    return node.Index(node.High(key), offset.Value);
                }

                return null;
            }

            var succCluster = _Successor(node.Summary, node.High(key));
            if (succCluster == null)
            {
                return null;
            }

            var offset2 = _NodeMinimum(node.Cluster[succCluster.Value]);
            if (offset2 == null)
            {
                return null;
            }

            return node.Index(succCluster.Value, offset2.Value);
        }

        private static int? _Predecessor(VanEmdeBoasNode node, int key)
        {
            if (node.Size == 2)
            {
                if (key == 1 && node.Minimum == 0)
                {
                    return 0;
                }

                return null;
            }

            if (node.Maximum != null && key > node.Maximum)
            {
                return node.Maximum;
            }

            var minLow = _NodeMinimum(node.Cluster[node.High(key)]);
            if (minLow != null && node.Low(key) > minLow)
            {
                var offset = _Predecessor(node.Cluster[node.High(key)], node.Low(key));
                if (offset == null)
                {
                    return null;
                }

                return node.Index(node.High(key), offset.Value);
            }

            var predCluster = _Predecessor(node.Summary, node.High(key));
            if (predCluster == null)
            {
                if (node.Minimum != null && key > node.Minimum)
                {
                    return node.Minimum;
                }

                return null;
            }

            var offset2 = _NodeMaximum(node.Cluster[predCluster.Value]);
            if (offset2 == null)
            {
                return null;
            }

            return node.Index(predCluster.Value, offset2.Value);
        }

        private static void _EmptyInsert(VanEmdeBoasNode node, int key)
        {
            node.Minimum = key;
            node.Maximum = key;
        }

        private static void _Insert(VanEmdeBoasNode node, int key)
        {
            if (node.Minimum == null)
            {
                _EmptyInsert(node, key);
            }
            else
            {
                if (key < node.Minimum)
                {
                    var temp = node.Minimum.Value;
                    node.Minimum = key;
                    key = temp;
                }

                if (node.Size > 2)
                {
                    if (_NodeMinimum(node.Cluster[node.High(key)]) == null)
                    {
                        _Insert(node.Summary, node.High(key));
                        _EmptyInsert(node.Cluster[node.High(key)], node.Low(key));
                    }
                    else
                    {
                        _Insert(node.Cluster[node.High(key)], node.Low(key));
                    }
                }

                if (key > node.Maximum)
                {
                    node.Maximum = key;
                }
            }
        }

        private static void _Delete(VanEmdeBoasNode node, int key)
        {
            if (node.Minimum == node.Maximum)
            {
                node.Minimum = null;
                node.Maximum = null;
            }
            else if (node.Size == 2)
            {
                node.Minimum = key == 0 ? 1 : 0;

                node.Maximum = node.Minimum;
            }
            else
            {
                if (key == node.Minimum)
                {
                    var firstCluster = _NodeMinimum(node.Summary);

                    Debug.Assert(firstCluster != null, nameof(firstCluster) + " != null");
                    // ReSharper disable once PossibleInvalidOperationException
                    var nodeMinimum = _NodeMinimum(node.Cluster[firstCluster.Value]) ??
                                      throw new InvalidOperationException(
                                          $"{nameof(_NodeMinimum)} with param: {firstCluster.Value}");
                    key = node.Index(firstCluster.Value, nodeMinimum);
                    node.Minimum = key;
                }

                _Delete(node.Cluster[node.High(key)], node.Low(key));
                if (_NodeMinimum(node.Cluster[node.High(key)]) == null)
                {
                    _Delete(node.Summary, node.High(key));
                    if (key == node.Maximum)
                    {
                        var summaryMax = _NodeMaximum(node.Summary);
                        if (summaryMax == null)
                        {
                            node.Maximum = node.Minimum;
                        }
                        else
                        {
                            var nodeMaximum = _NodeMaximum(node.Cluster[summaryMax.Value])
                                              ?? throw new InvalidOperationException(
                                                  $"{nameof(_NodeMaximum)} with param: {summaryMax.Value}");
                            node.Maximum = node.Index(summaryMax.Value,
                                // ReSharper disable once PossibleInvalidOperationException
                                nodeMaximum);
                        }
                    }
                }
                else
                {
                    if (key == node.Maximum)
                    {
                        // ReSharper disable once PossibleInvalidOperationException
                        var nodeMaximum = _NodeMaximum(node.Cluster[node.High(key)])
                                          ?? throw new InvalidOperationException(
                                              $"{nameof(_NodeMaximum)} with param: {node.High(key)}");
                        node.Maximum = node.Index(node.High(key), nodeMaximum);
                    }
                }
            }
        }
    }
}