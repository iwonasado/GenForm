using System.Collections;
using System.Collections.Generic;

namespace Informedica.GenForm.Settings.Environments
{
    public class GenFormEnvironmentCollection: ICollection<GenFormEnvironment>
    {
        private readonly ICollection<GenFormEnvironment> _genFormEnvironments = new List<GenFormEnvironment>();
        private EnvironmentCollection _environments;

        public GenFormEnvironmentCollection(EnvironmentCollection environments)
        {
            _environments = environments;
        }

        public GenFormEnvironmentCollection() {}


        private void AddEnvironment(GenFormEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(environment.Database))
                throw new GenFormEnvironmentException("Database connection string cannot be empty");
            _genFormEnvironments.Add(environment);
        }


        #region Implementation of ICollection<GenFormEnvironment>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<GenFormEnvironment> GetEnumerator()
        {
            return _genFormEnvironments.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void Add(GenFormEnvironment item)
        {
            AddEnvironment(item);
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(GenFormEnvironment item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(GenFormEnvironment[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(GenFormEnvironment item)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion
    }
}