using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ObjectClasses
{
    public class Stack : IEnumerable
    {
        public int Size
        {
            get
            {
                return _list.Count;
            }
        }

        public string Top
        {
            get
            {
                return _list.FirstOrDefault();
            }
        }

        public void Add(string val)
        { _list.Add(val); }

        public string Pop()
        {
            string item;
            if (_list.Count > 0)
            {

                item = _list.Last();

                _list.Remove(item);
            }
            else
            {
                throw new Exception("Стек пустой");
            }
            return item;
        }

        public static Stack Concat(params Stack[] stacks)
        {
            var _Stack = new Stack();

            int i = 0;


            foreach (var item in stacks)
            {
                int _size = item.Size;

                for (i = 0; i < _size; i++)
                {
                    _Stack.Add(item.Pop());
                }

            }


            return _Stack;

        }


        private List<string> _list = new();

        public Stack(params string[] val)
        {
            foreach (string s in val)
            {
                _list.Add(s);
            }
        }


        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }

}

