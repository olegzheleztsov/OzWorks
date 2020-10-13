using System;

namespace Oz.API.Logic
{
    public class Jug
    {
        public Jug(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; }

        public int CurrentAmount { get; private set; }

        public bool IsFull => IsFilledOn(Capacity);
        public bool IsEmpty => IsFilledOn(0);

        protected bool Equals(Jug other)
        {
            return Capacity == other.Capacity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Jug) obj);
        }

        public override int GetHashCode()
        {
            return Capacity;
        }

        public int FillFrom(Jug other)
        {
            var fillOn = Capacity - CurrentAmount;
            if (other.CurrentAmount <= fillOn)
            {
                SetCurrentAmount(CurrentAmount + other.CurrentAmount);
                return 0;
            }

            SetCurrentAmount(CurrentAmount + fillOn);
            return other.CurrentAmount - fillOn;
        }

        public bool IsFilledOn(int amount)
        {
            return CurrentAmount == amount;
        }

        public void SetCurrentAmount(int value)
        {
            CurrentAmount = value;
        }

        public bool ClearIfFull()
        {
            if (CurrentAmount < Capacity) return false;
            SetCurrentAmount(0);
            return true;
        }

        public void FillWhole()
        {
            CurrentAmount = Capacity;
        }

        public static bool operator <(Jug first, Jug second)
        {
            return first.Capacity < second.Capacity;
        }

        public static bool operator >(Jug first, Jug second)
        {
            return first.Capacity > second.Capacity;
        }

        public static bool operator ==(Jug first, Jug second)
        {
            if (Object.ReferenceEquals(first, null) || Object.ReferenceEquals(second, null)) return false;
            return first.Capacity == second.Capacity;
        }

        public static bool operator !=(Jug first, Jug second)
        {
            return !(first == second);
        }
    }
}