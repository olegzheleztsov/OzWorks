// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace clrcs_frm
{
    [Serializable]
    public sealed class Singleton : ISerializable
    {
        private static readonly Singleton _theOneObject = new Singleton();

        public string Name = "Oleg";
        public DateTime Date = DateTime.Now;

        private Singleton() { }

        public static Singleton GetSingleton() => _theOneObject;

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) =>
            info.SetType(typeof(SingletonSerializationHelper));

        [Serializable]
        private sealed class SingletonSerializationHelper : IObjectReference
        {
            public object GetRealObject(StreamingContext context) => Singleton.GetSingleton();
        }
    }
}
