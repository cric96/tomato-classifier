using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Helper
{
    public class Option<T> : IEnumerable<T>
    {
        private readonly List<T> elem;
        private Option()
        {
            this.elem = new List<T>();
        }
        private Option(T elem) : this()
        {
            if (elem != null)
            {
                this.elem.Add(elem);
            }
        }
        public static Option<T> Some(T elem)
        {
            return new Option<T>(elem);
        }

        public static Option<T> None()
        {
            return new Option<T>();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return this.elem.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.elem.GetEnumerator();
        }

        public bool Empty
        {
            get => elem.Count == 0;
        }

        public T Get
        {
            get => elem[0];
        }

        public T GetOrElse(T other)
        {
            if(Empty)
            {
                return other;
            } else
            {
                return Get;
            }
        }

        public void IfPresent(Action<T> logic)
        {
            if(!Empty)
            {
                logic.Invoke(Get);
            }
        }
    }
}
