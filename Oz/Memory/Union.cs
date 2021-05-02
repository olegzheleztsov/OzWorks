using System.Runtime.InteropServices;

namespace Oz.Memory
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Union
    {
        [field: FieldOffset(0)]
        public byte Byte { get; set; }

        [field: FieldOffset(0)]
        public char Char { get; set; }

        [field: FieldOffset(0)]
        public float Float { get; set; }

        [field: FieldOffset(0)]
        public int Integer { get; set; }
    }
    
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class Place
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
    
    [StructLayout(LayoutKind.Explicit)]
    public struct NoCasting
    {
        [field: FieldOffset(0)]
        public Person Person { get; set; }

        [field: FieldOffset(0)]
        public Place Place { get; set; }
    }
}