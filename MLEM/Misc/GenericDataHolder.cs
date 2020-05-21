using System;
using System.Collections.Generic;

namespace MLEM.Misc {
    /// <summary>
    /// Represents an object that can hold generic key-value based data.
    /// A lot of MLEM components extend this class to allow for users to add additional data to them easily.
    /// </summary>
    public class GenericDataHolder {

        private Dictionary<string, object> data;

        /// <summary>
        /// Store a piece of generic data on this object.
        /// </summary>
        /// <param name="key">The key to store the data by</param>
        /// <param name="data">The data to store in the object</param>
        public void SetData(string key, object data) {
            if (data == default) {
                if (this.data != null)
                    this.data.Remove(key);
            } else {
                if (this.data == null)
                    this.data = new Dictionary<string, object>();
                this.data[key] = data;
            }
        }

        /// <summary>
        /// Returns a piece of generic data of the given type on this object.
        /// </summary>
        /// <param name="key">The key that the data is stored by</param>
        /// <typeparam name="T">The type of the data stored</typeparam>
        /// <returns>The data, or default if it doesn't exist</returns>
        public T GetData<T>(string key) {
            if (this.data != null && this.data.TryGetValue(key, out var val) && val is T t)
                return t;
            return default;
        }

        /// <summary>
        /// Returns all of the generic data that this object stores.
        /// </summary>
        /// <returns>The generic data on this object</returns>
        public IReadOnlyCollection<string> GetDataKeys() {
            if (this.data == null)
                return Array.Empty<string>();
            return this.data.Keys;
        }

    }
}