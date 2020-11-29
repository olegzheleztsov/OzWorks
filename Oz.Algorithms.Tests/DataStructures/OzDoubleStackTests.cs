using System;
using Oz.Algorithms.DataStructures;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures
{
    public class OzDoubleStackTests
    {

        [Fact]
        public void Should_Correctly_Push_Pop()
        {
            var stack = new OzDoubleStack<int>(5);
            stack.PushLeft(1);
            stack.PushLeft(2);
            stack.PushRight(3);
            stack.PushRight(4);
            stack.PushLeft(5);
            
            Assert.True(stack.IsFull);
            var right = stack.PopRight();
            Assert.Equal(4, right);
            Assert.Equal(5, stack.PopLeft());
            Assert.Equal(2, stack.PopLeft());
            Assert.Equal(1, stack.PopLeft());
            Assert.Equal(3, stack.PopRight());
            Assert.True(stack.IsLeftEmpty);
            Assert.True(stack.IsRightEmpty);
        }

        [Fact]
        public void Should_Correctly_Behavior_On_Left_Stack()
        {
            var stack = new OzDoubleStack<int>(5);
            stack.PushLeft(1);
            stack.PushLeft(2);
            stack.PushLeft(3);
            stack.PushLeft(4);
            stack.PushLeft(5);
            Assert.True(stack.IsFull);
            
        }

        [Fact]
        public void Should_Correctly_Behavior_On_Right_Stack()
        {
            var stack = new OzDoubleStack<int>(5);
            stack.PushRight(1);
            stack.PushRight(2);
            stack.PushRight(3);
            stack.PushRight(4);
            stack.PushRight(5);
            Assert.True(stack.IsFull);
        }

        [Fact]
        public void Should_Throw_On_Overflow()
        {
            var stack = new OzDoubleStack<int>(3);
            stack.PushLeft(1);
            stack.PushRight(2);
            stack.PushRight(3);
            Assert.Throws<IndexOutOfRangeException>(() => stack.PushLeft(4));
        }
        
        
    }
}