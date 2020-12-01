using System;
using System.Collections.Generic;
using System.Linq;

namespace GoLive.Saturn.Data.Entities
{
    public struct Ref<T> : IEquatable<Ref<T>> where T : Entity
    {
        public Ref(string refId)
        {
            _refId = refId;
            Item = null;
        }

        public Ref(T item)
        {
            Item = item;
            _refId = null;

            if (item != null && !String.IsNullOrWhiteSpace(item.Id))
            {
                _refId = item.Id;
            }
        }

        public static implicit operator string(Ref<T> item)
        {
            return item.Id;
        }

        public static implicit operator T(Ref<T> item)
        {
            return item.Item;
        }

        public static implicit operator Ref<T>(T item)
        {
            return new Ref<T>() { Item = item };
        }

        public static implicit operator Ref<T>(string item)
        {
            return new Ref<T>(item);
        }

        private string _refId;

        public T Item { get; set; }

        public string Id
        {
            get
            {
                if (Item != null)
                {
                    _refId = Item.Id;
                    return Item.Id;
                }

                return _refId;
            }
            set => _refId = value;
        }

        public override string ToString()
        {
            return Id;
        }

        public void Fetch(IQueryable<T> items)
        {
            var id = _refId;
            Item = items.FirstOrDefault(f => f.Id == id);
        }

        public void Fetch(IList<T> items)
        {
            var id = _refId;
            Item = items.FirstOrDefault(f => f.Id == id);
        }

        public void Fetch(Func<string, T> expr)
        {
            this.Item = expr.Invoke(this.Id);
        }

        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;

                if (generic == cur)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }
            return false;
        }


        public string Type => typeof(T).FullName;

        public bool Equals(Ref<T> other)
        {
            return string.Equals(_refId, other._refId);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ref<T>)obj);
        }

        public override int GetHashCode()
        {
            return (_refId != null ? _refId.GetHashCode() : 0);
        }

        public static bool operator ==(Ref<T> left, Ref<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Ref<T> left, Ref<T> right)
        {
            return !Equals(left, right);
        }
    }
}