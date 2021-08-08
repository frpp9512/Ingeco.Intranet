using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartB1t.Web.Extensions
{
    public static class GenericDictionariesExtensions
    {
        #region Dictionaries Generic Extensions

        /// <summary>
        /// Checks if the provided key id the <typeparamref name="D"/> dictionary exists and it is of the specified <typeparamref name="T"/> type.
        /// </summary>
        /// <typeparam name="D">The type of the <typeparamref name="D"/> dictionary to check the key from.</typeparam>
        /// <typeparam name="T">The type of the value that should contain the value.</typeparam>
        /// <param name="dictionary">The dictionary to check the key value from.</param>
        /// <param name="key">The key to check the value.</param>
        /// <returns><see langword="true"/> if exists a value with the provided key an it is of type <typeparamref name="T"/>.</returns>
        public static bool CheckValue<D, T>(this D dictionary, string key)
            where D : IDictionary<string, object>
            => dictionary[key] != null && dictionary[key] is T;

        /// <summary>
        /// Checks if the provided key id the ViewDataDictionary exists and it is of the specified <typeparamref name="T"/> type.
        /// </summary>
        /// <typeparam name="T">The type of the value that should contain the value.</typeparam>
        /// <param name="viewData">The dictionary to check the key value from.</param>
        /// <param name="key">The key to check the value.</param>
        /// <returns><see langword="true"/> if exists a value with the provided key an it is of type <typeparamref name="T"/>.</returns>
        public static bool CheckValue<T>(this ViewDataDictionary viewData, string key)
            => CheckValue<ViewDataDictionary, T>(viewData, key);

        /// <summary>
        /// Checks if the provided key id the ViewDataDictionary exists and it is of the specified <typeparamref name="T"/> type.
        /// </summary>
        /// <typeparam name="T">The type of the value that should contain the value.</typeparam>
        /// <param name="viewData">The dictionary to check the key value from.</param>
        /// <param name="key">The key to check the value.</param>
        /// <returns><see langword="true"/> if exists a value with the provided key an it is of type <typeparamref name="T"/>.</returns>
        public static bool CheckValue<T>(this ITempDataDictionary tempData, string key)
            => CheckValue<ITempDataDictionary, T>(tempData, key);

        /// <summary>
        /// Checks if the provided key in the <typeparamref name="D"/> dictionary exists and contains the specified <typeparamref name="T"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the value that contains the key.</typeparam>
        /// <param name="tempData">The <typeparamref name="D"/> dictionary that contains the key to check the value from.</param>
        /// <param name="key">The key that should contain the specified <typeparamref name="T"/> value.</param>
        /// <param name="valueToCheck">The value to check if exists in the provided key.</param>
        /// <returns><see langword="true"/> if the the provided <typeparamref name="T"/> value exists in the key of <see cref="ITempDataDictionary"/></returns>
        public static bool CheckValue<D, T>(this D dictionary, string key, T valueToCheck)
            where D : IDictionary<string, object>
            => dictionary[key] != null && dictionary[key] is T t && t.Equals(valueToCheck);

        /// <summary>
        /// Gets a value stored in the provided key from the <typeparamref name="D"/> dictionary specified.
        /// </summary>
        /// <typeparam name="T">The type of the value stored in the key.</typeparam>
        /// <param name="dictionary">The <typeparamref name="D"/> dictionary that have the key that contains the value.</param>
        /// <param name="key">The key that contains the value</param>
        /// <param name="defaultValue">The default value to obtain in case that the value doesn't exist.</param>
        /// <returns>The value stored in the key.</returns>
        public static T GetValue<D, T>(this D dictionary, string key, T defaultValue = default)
            where D : IDictionary<string, object>
            => dictionary[key] != null ? (dictionary[key] is T ? (T)Convert.ChangeType(dictionary[key], typeof(T)) : defaultValue) : defaultValue;

        /// <summary>
        /// Gets a value stored in the provided key from the <see cref="ViewDataDictionary"/> specified.
        /// </summary>
        /// <typeparam name="T">The type of the value stored in the key.</typeparam>
        /// <param name="dictionary">The <see cref="ViewDataDictionary"/> that have the key that contains the value.</param>
        /// <param name="key">The key that contains the value</param>
        /// <param name="defaultValue">The default value to obtain in case that the value doesn't exist.</param>
        /// <returns>The value stored in the key.</returns>
        public static T GetValue<T>(this ViewDataDictionary viewData, string key, T defaultValue = default)
            => viewData[key] != null ? (viewData[key] is T ? (T)Convert.ChangeType(viewData[key], typeof(T)) : defaultValue) : defaultValue;

        /// <summary>
        /// Gets a value stored in the provided key from the <see cref="ITempDataDictionary"/> specified.
        /// </summary>
        /// <typeparam name="T">The type of the value stored in the key.</typeparam>
        /// <param name="dictionary">The <see cref="ITempDataDictionary"/> that have the key that contains the value.</param>
        /// <param name="key">The key that contains the value</param>
        /// <param name="defaultValue">The default value to obtain in case that the value doesn't exist.</param>
        /// <returns>The value stored in the key.</returns>
        public static T GetValue<T>(this ITempDataDictionary tempData, string key, T defaultValue = default)
            => tempData[key] != null ? (tempData[key] is T ? (T)Convert.ChangeType(tempData[key], typeof(T)) : defaultValue) : defaultValue;

        /// <summary>
        /// Sets a value in the <typeparamref name="D"/> dictionary with the spcified key.
        /// </summary>
        /// <typeparam name="D">The type of the dictionary to add the value to.</typeparam>
        /// <typeparam name="T">The type of value to add.</typeparam>
        /// <param name="dictionary">The <typeparamref name="D"/> dictionary which the value will be added to.</param>
        /// <param name="key">The key where the value will be added.</param>
        /// <param name="value">The value to be added.</param>
        public static void SetValue<D, T>(this D dictionary, string key, T value)
            where D : IDictionary<string, object> => dictionary.Add(key, value);

        #endregion

    }
}
