// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using FluentAssertions;
using Leetcode.Models;
using Leetcode.Solutions;
using System.Collections.Generic;
using Xunit;

namespace Oz.Tests.LeetCode.Solutions;

public class SolutionTests
{
    [Fact]
    public void Should_Find_Alphabet_Board_Path()
    {
        var sut = new _1138();
        sut.AlphabetBoardPath("leet").Should().Be("DDR!UURRR!!DDD!");
        sut.AlphabetBoardPath("code").Should().Be("RR!DDRR!UUL!R!");
        sut.AlphabetBoardPath("z").Should().Be("DDDDD!");
    }

    [Fact]
    public void Should_Return_Ranks()
    {
        var sut = new _1331();
        int[] arr1 =
        {
            40, 10, 20, 30
        };
        sut.ArrayRankTransform(arr1).Should().BeEquivalentTo(new[]
        {
            4, 1, 2, 3
        });

        int[] arr2 =
        {
            100, 100, 100
        };
        sut.ArrayRankTransform(arr2).Should().BeEquivalentTo(new[]
        {
            1, 1, 1
        });

        int[] arr3 =
        {
            37, 12, 28, 9, 100, 56, 80, 5, 12
        };
        sut.ArrayRankTransform(arr3).Should().BeEquivalentTo(new[]
        {
            5, 3, 4, 2, 8, 6, 7, 1, 3
        });
    }

    [Fact]
    public void Should_Find_Max_Result()
    {
        int[] nums =
        {
            1, -1, -2, 4, -7, 3
        };
        var k = 2;
        var obj = new _1696();
        obj.MaxResult(nums, k).Should().Be(7);

        nums = new[]
        {
            10, -5, -2, 4, 0, 3
        };
        k = 3;
        obj.MaxResult(nums, k).Should().Be(17);

        nums = new[]
        {
            1, -5, -20, 4, -1, 3, -6, -3
        };
        k = 2;
        obj.MaxResult(nums, k).Should().Be(0);
    }

    [Fact]
    public void Should_Correctly_Rearrange_Array()
    {
        var sut = new _2149();
        sut.RearrangeArray(new[]
        {
            3, 1, -2, -5, 2, -4
        }).Should().BeEquivalentTo(new[]
        {
            3, -2, 1, -5, 2, -4
        });

        sut.RearrangeArray(new[]
        {
            -1, 1
        }).Should().BeEquivalentTo(new[]
        {
            1, -1
        });
    }

    [Fact]
    public void Should_Find_Diameter_Of_Binary_Tree()
    {
        var n1 = new TreeNode(1);
        var n2 = new TreeNode(2);
        var n3 = new TreeNode(3);
        var n4 = new TreeNode(4);
        var n5 = new TreeNode(5);
        n1.left = n2;
        n1.right = n3;
        n2.left = n4;
        n2.right = n5;

        var sut = new _543();
        sut.DiameterOfBinaryTree(n1).Should().Be(3);
    }

    [Fact]
    public void Should_Correctly_Find_Array_Max()
    {
        var sut = new _1131();
        sut.MaxAbsValExpr(new[]
        {
            1, 2, 3, 4
        }, new[]
        {
            -1, 4, 5, 6
        }).Should().Be(13);

        sut.MaxAbsValExpr(new[]
        {
            1, -2, -5, 0, 10
        }, new[]
        {
            0, -2, -1, -7, -4
        }).Should().Be(20);
    }

    [Fact]
    public void Should_377_Correct()
    {
        int[] nums =
        {
            1, 2, 3
        };
        var target = 4;
        var sut = new _377();
        sut.CombinationSum4(nums, target).Should().Be(7);

        nums = new[]
        {
            9
        };
        target = 3;
        sut.CombinationSum4(nums, target).Should().Be(0);
    }

    [Fact]
    public void Should_279_Correct()
    {
        var n = 12;
        var sut = new _279();
        sut.NumSquares(12).Should().Be(3);

        sut.NumSquares(13).Should().Be(2);
    }

    [Fact]
    public void Should_110_Be_Correct()
    {
        var n3 = new TreeNode(3);
        var n9 = new TreeNode(9);
        var n20 = new TreeNode(20);
        var n15 = new TreeNode(15);
        var n7 = new TreeNode(7);
        n20.left = n15;
        n20.right = n7;
        n3.left = n9;
        n3.right = n20;
        var sut = new _110();
        sut.IsBalanced(n3).Should().BeTrue();

        var n1 = new TreeNode(1);
        var n2 = new TreeNode(2);
        var n22 = new TreeNode(2);
        n3 = new TreeNode(3);
        var n33 = new TreeNode(3);
        var n4 = new TreeNode(4);
        var n44 = new TreeNode(4);

        n33.left = n44;
        n33.right = n4;
        n22.left = n33;
        n22.right = n3;
        n1.left = n22;
        n1.right = n2;

        sut.IsBalanced(n1).Should().BeFalse();
    }

    [Fact]
    public void Should_459_Correct()
    {
        _459 sut = new _459();
        sut.RepeatedSubstringPattern("abab").Should().BeTrue();
        sut.RepeatedSubstringPattern("aba").Should().BeFalse();
        sut.RepeatedSubstringPattern("abcabcabcabc").Should().BeTrue();
    }

    [Fact]
    public void Should_739_Correct()
    {
        int[] temps =
        {
            73, 74, 75, 71, 69, 72, 76, 73
        };
        var sut = new _739();
        sut.DailyTemperatures(temps).Should().BeEquivalentTo(new int[]
        {
            1, 1, 4, 2, 1, 1, 0, 0
        });

        temps = new int[]
        {
            30, 40, 50, 60
        };
        sut.DailyTemperatures(temps).Should().BeEquivalentTo(new[]
        {
            1, 1, 1, 0
        });

        temps = new[]
        {
            30, 60, 90
        };
        sut.DailyTemperatures(temps).Should().BeEquivalentTo(new[]
        {
            1, 1, 0
        });
    }

    [Fact]
    public void Should_1630_Correct()
    {
        int[] nums =
        {
            4, 6, 5, 9, 3, 7
        };
        int[] l =
        {
            0, 0, 2
        };
        int[] r =
        {
            2, 3, 5
        };
        var sut = new _1630();
        sut.CheckArithmeticSubarrays(nums, l, r).Should().BeEquivalentTo(new List<bool>()
        {
            true,
            false,
            true
        });

        nums = new int[]
        {
            -12, -9, -3, -12, -6, 15, 20, -25, -20, -15, -10
        };
        l = new int[]
        {
            0, 1, 6, 4, 8, 7
        };
        r = new int[]
        {
            4, 4, 9, 7, 9, 10
        };
        bool[] expected =
        {
            false, true, false, false, true, true
        };
        sut.CheckArithmeticSubarrays(nums, l, r).Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Should_429_Correct()
    {
        _429.Node n1 = new _429.Node(1);
        _429.Node n3 = new _429.Node(3);
        _429.Node n2 = new _429.Node(2);
        _429.Node n4 = new _429.Node(4);
        _429.Node n5 = new _429.Node(5);
        _429.Node n6 = new _429.Node(6);
        n1.children = new List<_429.Node>()
        {
            n3,
            n2,
            n4
        };
        n3.children = new List<_429.Node>()
        {
            n5,
            n6
        };
        var sut = new _429();
        var result = _429.LevelOrder(n1);
        result[0].Should().BeEquivalentTo(new List<int>()
        {
            1
        });
        result[1].Should().BeEquivalentTo(new List<int>()
        {
            3,
            2,
            4
        });
        result[2].Should().BeEquivalentTo(new List<int>()
        {
            5,
            6
        });

        Dictionary<int, _429.Node> nodeDict = new Dictionary<int, _429.Node>();
        for (int i = 1; i <= 14; i++)
        {
            nodeDict[i] = new _429.Node(i);
        }

        nodeDict[1].children = new List<_429.Node>()
        {
            nodeDict[2],
            nodeDict[3],
            nodeDict[4],
            nodeDict[5]
        };
        nodeDict[3].children = new List<_429.Node>()
        {
            nodeDict[6],
            nodeDict[7]
        };
        nodeDict[4].children = new List<_429.Node>()
        {
            nodeDict[8]
        };
        nodeDict[5].children = new List<_429.Node>()
        {
            nodeDict[9],
            nodeDict[10]
        };
        nodeDict[7].children = new List<_429.Node>()
        {
            nodeDict[11]
        };
        nodeDict[8].children = new List<_429.Node>()
        {
            nodeDict[12]
        };
        nodeDict[9].children = new List<_429.Node>()
        {
            nodeDict[13]
        };
        nodeDict[11].children = new List<_429.Node>()
        {
            nodeDict[14]
        };

        result = _429.LevelOrder(nodeDict[1]);

        result[0].Should().BeEquivalentTo(new[]
        {
            1
        });
        result[1].Should().BeEquivalentTo(new[]
        {
            2, 3, 4, 5
        });
        result[2].Should().BeEquivalentTo(new[]
        {
            6, 7, 8, 9, 10
        });
        result[3].Should().BeEquivalentTo(new[]
        {
            11, 12, 13
        });
        result[4].Should().BeEquivalentTo(new[]
        {
            14
        });
    }

    [Fact]
    public void Should_503_Correct()
    {
        int[] nums = new int[]
        {
            1, 2, 1
        };
        _503 sut = new _503();
        sut.NextGreaterElements(nums).Should().BeEquivalentTo(new int[]
        {
            2, -1, 2
        });
    }
}