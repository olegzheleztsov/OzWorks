using System.Linq;
using Oz.Algorithms.DataStructures;
using Xunit;
using FluentAssertions;

namespace Oz.Algorithms.Tests.DataStructures
{
    public class OzSingleLinkedListTests
    {
        private OzSingleLinkedList<char> CreateListWithLoop()
        {
            var list = new OzSingleLinkedList<char>();
            list.InsertLast('a');
            list.InsertLast('b');
            list.InsertLast('c');
            list.InsertLast('d');
            list.Search(c => c == 'd').Next = list.HeadNode;
            return list;
        }
        
        [Fact]
        public void Should_Correctly_Count_In_List_With_Loop()
        {
            var list = CreateListWithLoop();
            list.Count.Should().Be(4);
        }

        [Fact]
        public void Should_Correctly_Find_Circle()
        {
            var list = CreateListWithLoop();
            list.GetStartCircleNode().Data.Should().Be('a');
        }

        [Fact]
        public void Should_Correctly_Find_Circle_On_Two_Elem_List()
        {
            var list = new OzSingleLinkedList<char>();
            list.InsertLastRange(new char[]{'a', 'b'});
            list.Search(c => c == 'b').Next = list.Search(c => c == 'a');
            list.GetStartCircleNode().Data.Should().Be('a');
            list.Count.Should().Be(2);
        }

        [Fact]
        public void Should_Correctly_Initialize_List()
        {
            var list = new OzSingleLinkedList<int>();

            list.Count.Should().Be(0);
            list.HeadValue.Should().Be(default(int));
            list.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void Should_Search_Correctly()
        {
            var list = PrepareList();
            var result = list.Search(data => data.Value == 2);
            
            result.Should().NotBeNull();
            result.Data.Value.Should().Be(2);

            var result2 = list.Search(data => data.Value == 100);
            result2.Should().BeNull();
        }

        [Fact]
        public void Should_Delete_Correctly()
        {
            var list = PrepareList();
            list.Count.Should().Be(3);
            list.Delete(data => data.Value == 1);
            list.Search(data => data.Value == 1).Should().BeNull();
            list.Count.Should().Be(2);
            list.Delete(data => data.Value == 2);
            list.Count.Should().Be(1);
            list.HeadValue.Value.Should().Be(3);
            list.Delete(data => data.Value == 3);
            list.Count.Should().Be(0);
            list.IsEmpty.Should().BeTrue();
            list.Delete(data => data.Value == 100);
        }

        [Fact]
        public void Should_Delete_Head_Correctly()
        {
            var list = PrepareList();

            list.HeadValue.Value.Should().Be(1);
            list.DeleteHead();
            list.HeadValue.Value.Should().Be(2);
            list.Count.Should().Be(2);
        }

        [Fact]
        public void Should_Correctly_Insert_First()
        {
            var list = PrepareList();
            list.InsertFirst(new SampleData(10));
            list.HeadValue.Value.Should().Be(10);
            list.Count.Should().Be(4);
        }

        [Fact]
        public void Should_Correctly_Insert_Last()
        {
            var list = PrepareList();
            list.InsertLast(new SampleData(10));
            list.Skip(3).Take(1).First().Value.Should().Be(10);
        }

        [Fact]
        public void Should_Correctly_Delete_Element_By_Ref()
        {
            var list = PrepareList();
            list.Delete(list.Search(data => data.Value == 2));
            list.Count.Should().Be(2);
            list.Delete(list.Search(data => data.Value == 1));
            list.Count.Should().Be(1);
            list.Delete(list.Search(data => data.Value == 3));
            list.IsEmpty.Should().BeTrue();
        }

        private OzSingleLinkedList<SampleData> PrepareList()
        {
            var list = new OzSingleLinkedList<SampleData>();
            list.InsertLast(new SampleData(1));
            list.InsertLast(new SampleData(2));
            list.InsertLast(new SampleData(3));
            return list;
        }


        public class SampleData
        {
            public int Value { get; }

            public SampleData(int value) => Value = value;
            
            
        }
    }
}