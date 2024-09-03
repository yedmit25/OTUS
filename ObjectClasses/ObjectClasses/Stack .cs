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
        { get
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

        public void Pop(out string item)
        {

            if (_list.Count > 0)
            {

                item = _list.First();

                _list.RemoveAt(0);
            } else
            {
                throw new Exception("Стек пустой");
            }
        } 
    


        private List<string> _list = new();

        public Stack (params string[] val)
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

