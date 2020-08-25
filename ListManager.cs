using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{
    public class ListManager<T>
    {
        private List<T> m_list;

        /// <summary>
        /// Constructor
        /// </summary>
        public ListManager()
        {
            m_list = new List<T>();
        }

        /// <summary>
        /// Resets the list
        /// </summary>
        public void Reset()
        {
            m_list.Count();
        }

        /// <summary>
        /// Returns the length of the List
        /// </summary>
        public int Count { get { return this.m_list.Count(); } }

        public bool Add(T aType)
        {
            //TODO
            m_list.Add(aType);

            return true;
        }

        /// <summary>
        /// Adds every item in a string array to the list
        /// </summary>
        /// <param name="aType"></param>
        /// <returns></returns>
        public bool AddAsList(T[] aType)
        {
            for (var i = 0; i < aType.Length; i++)
            {
                this.Add(aType[i]);
            }
            return true;
        }

        public bool AddAsList(List<T> list)
        {
            this.m_list.AddRange(list);
            return true;
        }

        /// <summary>
        /// Changes the item at specified index
        /// </summary>
        /// <param name="aType"></param>
        /// <param name="anIndex"></param>
        /// <returns></returns>
        public bool ChangeAt(T aType, int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                m_list[anIndex] = aType;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates submited index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool CheckIndex(int index)
        {
            if (index >= 0 && index < Count)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Removes all items in the list
        /// </summary>
        public void DeleteAll()
        {
            m_list.Clear();
        }

        /// <summary>
        /// Removes item at specified index
        /// </summary>
        /// <param name="anIndex"></param>
        /// <returns></returns>
        public bool DeleteAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                m_list.RemoveAt(anIndex);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the element att specified index
        /// </summary>
        /// <param name="anIndex"></param>
        /// <returns></returns>
        public T GetAt(int anIndex)
        {
            if (CheckIndex(anIndex))
                return m_list[anIndex];
            else
                return default;
        }

        /// <summary>
        /// Returns the list as a string array
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            string[] strArray = new string[Count];
            for (int i = 0; i < Count; i++) strArray[i] = m_list[i].ToString();
            return strArray;
        }

        /// <summary>
        /// Returns the list as a string list
        /// </summary>
        /// <returns></returns>
        public List<string> ToStringList()
        {
            var list = ((IEnumerable<T>)m_list).OfType<object>();
            List<string> strings = list.Select(x => x.ToString()).ToList();

            return strings;
        }

        protected List<T> GetList()
        {
            return this.m_list;
        }
    }
}
