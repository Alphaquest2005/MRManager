using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Data
{
    public class Option<T>:IEnumerable<T> 
    {
        private readonly T[] data;

        private Option(T[] data)
        {
            this.data = data;
        } 
        public static Option<T> Create(T element)
        {
            return new Option<T>(new T[] {element});
        }
        public static Option<T> CreateEmpty()
        {
            return new Option<T>(new T[0]);
        }

        public T Item
        {
            get
            {
                var i = this.data.First();
                return i;
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
