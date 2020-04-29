using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireFighting.Tool
{
    [Serializable]
    public class HashObject : System.Collections.IDictionary
    {
        private System.Collections.IDictionary dicData;
        //private IDictionary<string, object> DicData 
        //{
        //    get { return dicData; }
        //    set { dicData = value; }
        //}

        public HashObject() 
        {
            dicData = new Dictionary<string, object>();
        }
        public HashObject(string[] keys,object[] values)
            : this() 
        {
            if (keys.Length != values.Length)
                throw new Exception("传入键的数量与值的数量不一致！");

            for (int i = 0; i < keys.Length; i++) 
            {
                dicData.Add(keys[i], values[i]);
            }

        }

        public void Add(object key, object value)
        {
            dicData.Add(key, value);
        }

        public void Clear()
        {
            dicData.Clear();
        }

        public bool Contains(object key)
        {
            bool IsExists = false;
            foreach (string curKey in dicData.Keys) 
            {
                if (curKey == key) { 
                    IsExists = true;
                    break;
                }
            }
            return IsExists;
        }

        public System.Collections.IDictionaryEnumerator GetEnumerator()
        {
            return dicData.GetEnumerator();
        }

        public bool IsFixedSize
        {
            get { return dicData.IsFixedSize; }
        }

        public bool IsReadOnly
        {
            get { return dicData.IsReadOnly; }
        }

        public System.Collections.ICollection Keys
        {
            get { return dicData.Keys; }
        }

        public void Remove(object key)
        {
            dicData.Remove(key);
        }

        public System.Collections.ICollection Values
        {
            get { return dicData.Values; }
        }

        public object this[object key]
        {
            get
            {
                return dicData[key];
            }
            set
            {
                dicData[key] = value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            dicData.CopyTo(array, index);
        }

        public int Count
        {
            get { return dicData.Count; }
        }

        public bool IsSynchronized
        {
            get { return dicData.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return dicData.SyncRoot; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dicData.GetEnumerator();
        }

        #region 自定义Method
        /// <summary>
        /// 根据类型取值（这里是泛型转换）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValue<T>(object key, object defaultValue = null)
        {
            object value = dicData[key];
            if (value == null)
            {
                value = defaultValue;
            }
            return (T)Convert.ChangeType(value,typeof(T));
        }
        #endregion
    }
}