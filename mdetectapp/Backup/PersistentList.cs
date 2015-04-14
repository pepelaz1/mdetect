using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;


namespace MotionDetector
{
    public class PersistentList<T> : IList<T>, IDisposable
    {
        private const string FileExtension = "bin";


        private string _listDirectory;


        public PersistentList(string listDirectory)
        {
            _listDirectory = listDirectory;
            try
            {
                if (!Directory.Exists(_listDirectory))
                {
                    Directory.CreateDirectory(_listDirectory);
                }
            }
            catch { }
        }

        public PersistentList()
        {
            _listDirectory = "";
        }





        #region IList<T> Members

        public int IndexOf(T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Insert(int index, T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void RemoveAt(int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public T this[int index]
        {
            get
            {

                int count = 0;
                string[] files = new string[] { };
                try
                {
                    files = Directory.GetFiles(_listDirectory, "*." + FileExtension);
                    count = files.Length;
                }
                catch { }

                if (index < 0 || index >= count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                T item = (T) BinarySerializer.Deserialize(files[index]);
                return item;

            }
            set
            {
                throw new NotSupportedException();
            }
        }

        #endregion


        public long AddItem(T item)
        {
            long fileId = DateTime.Now.Ticks;
            string filename = _listDirectory + Path.DirectorySeparatorChar + fileId + "." + FileExtension;
            try
            {
                BinarySerializer.Serialize(filename, item);
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString());
            }
            return fileId;
        }

        public T GetItemById(long fileId)
        {
            string filename = _listDirectory + Path.DirectorySeparatorChar + fileId + "." + FileExtension;
            T item = (T)BinarySerializer.Deserialize(filename);
            return item;
        }


        #region ICollection<T> Members

        public void Add(T item)
        {

            try
            {
                BinarySerializer.Serialize(_listDirectory + Path.DirectorySeparatorChar + DateTime.Now.Ticks + "." + FileExtension, item);
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.ToString());
            }
        }


        public void Clear()
        {
            try
            {

                string[] files = Directory.GetFiles(_listDirectory);
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                //Directory.Delete(_listDirectory, true);
            }
            catch { }
        }

        public bool Contains(T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get
            {
                int count = 0;
                try
                {
                    string[] files = Directory.GetFiles(_listDirectory, "*." + FileExtension);
                    count = files.Length;
                }
                catch { }
                return count;
            }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(T item)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new PersistentListEnumerator<T>(_listDirectory);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PersistentListEnumerator<T>(_listDirectory);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                Directory.Delete(_listDirectory, true);
            }
            catch { }
        }

        #endregion







        public class PersistentListEnumerator<T> : IEnumerator<T>
        {
            private string _listDirectory;
            private string[] _fileList;
            private int _itemIndex;

            public PersistentListEnumerator(string listDirectory)
            {
                _listDirectory = listDirectory;
                Reset();
            }


            #region IEnumerator<T> Members

            public T Current
            {
                get
                {
                    if (_itemIndex < 0 || _itemIndex >= _fileList.Length)
                    {
                        throw new InvalidOperationException();
                    }
                    return (T)BinarySerializer.Deserialize(_fileList[_itemIndex]);
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {

            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            public bool MoveNext()
            {
                _itemIndex++;
                return _itemIndex < _fileList.Length;
            }

            public void Reset()
            {
                _itemIndex = -1;
                _fileList = new string[] { };

                try
                {
                    string[] files = Directory.GetFiles(_listDirectory, "*." + FileExtension);
                    _fileList = files;
                }
                catch { }
            }

            #endregion
        }
    }


}
