using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace SmartB1t.Web.Extensions
{
    public static class TempDataExtensions
    {
        #region TempData Extensions

        /// <summary>
        /// Creates key values in the TempData to specify a status change of a model and it identifier.
        /// </summary>
        /// <typeparam name="T">The type of the Model to define the status.</typeparam>
        /// <typeparam name="I">The type of the model identifier.</typeparam>
        /// <param name="tempData">The the TempData to set up.</param>
        /// <param name="statusName">The name of the status of the model.</param>
        /// <param name="identifier">The identifier of the model.</param>
        private static void SetModelStatus<T, I>(this ITempDataDictionary tempData, string statusName, I identifier = default)
        {
            var statusKey = $"{statusName}{typeof(T).Name}";
            tempData[statusKey] = true;
            if (!identifier?.Equals(default(I)) == true)
            {
                var statusIdentifierKey = $"{statusKey}Id";
                tempData[statusIdentifierKey] = identifier;
            }
        }

        /// <summary>
        /// Reads the actual state of the model with of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the model to define the status.</typeparam>
        /// <typeparam name="I">The type of the model identifier.</typeparam>
        /// <param name="tempData">The TempData to read the data from.</param>
        /// <param name="statusName">The name of the status of the model.</param>
        /// <exception cref="Exception">Throws when the status of the model doesn's exists.</exception>
        /// <returns>The identifier of the model.</returns>
        private static I ReadModelStatus<T, I>(this ITempDataDictionary tempData, string statusName)
        {
            var statusKey = $"{statusName}{typeof(T).Name}";
            if (tempData.CheckValue(statusKey, true))
            {
                var statusIdentifierKey = $"{statusKey}Id";
                return tempData.CheckValue<I>(statusIdentifierKey) ? tempData.GetValue<I>(statusIdentifierKey) : default;
            }
            throw new Exception($"The provided model of type: {typeof(T).Name} have no status {statusName} setted in TempData.");
        }

        /// <summary>
        /// Set the status of model as 'Created' in the TempData.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="tempData">The TempData to be set up.</param>
        /// <param name="id">The identifier of the model.</param>
        public static void SetModelCreated<T, I>(this ITempDataDictionary tempData, I id)
            => tempData.SetModelStatus<T, I>(TempDataAlertModelState.Created, id);

        /// <summary>
        /// Reads if exists a 'Created' status for the specified Model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="tempData">The TempData to be set up.</param>
        /// <param name="id">The id of the <typeparamref name="T"/> model if exists it status, otherwise -1.</param>
        /// <returns>If the status exists returns the identifier of the Model, otherwise -1.</returns>
        public static bool WasModelCreated<T>(this ITempDataDictionary tempData, out Guid id)
        {
            try
            {
                id = tempData.ReadModelStatus<T, Guid>(TempDataAlertModelState.Created);
                return true;
            }
            catch (Exception)
            {
                id = new Guid();
                return false;
            }
        }

        /// <summary>
        /// Set the status of model as 'Updated' in the TempData.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="tempData">The TempData to be set up.</param>
        /// <param name="id">The identifier of the model.</param>
        public static void SetModelUpdated<T, I>(this ITempDataDictionary tempData, I id)
            => tempData.SetModelStatus<T, I>(TempDataAlertModelState.Updated, id);

        /// <summary>
        /// Reads if exists a 'Updated' status for the specified Model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="tempData">The TempData to be set up.</param>
        /// <param name="id">The id of the <typeparamref name="T"/> model if exists it status, otherwise -1.</param>
        /// <returns><see langword="true"/> if the status of the model exists.</returns>
        public static bool WasModelUpdated<T>(this ITempDataDictionary tempData, out Guid id)
        {
            try
            {
                id = tempData.ReadModelStatus<T, Guid>(TempDataAlertModelState.Updated);
                return true;
            }
            catch (Exception)
            {
                id = new Guid();
                return false;
            }
        }

        /// <summary>
        /// Set the status of model as 'Removed' in the TempData.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="tempData">The TempData to be set up.</param>
        /// <param name="id">The identifier of the model.</param>
        public static void SetModelRemoved<T>(this ITempDataDictionary tempData)
            => tempData.SetModelStatus<T, object>(TempDataAlertModelState.Removed, null);

        /// <summary>
        /// Reads if exists a 'Removed' status for the specified Model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="tempData">The TempData to be set up.</param>
        /// <returns>If the status exists returns the identifier of the Model, otherwise -1.</returns>
        public static bool WasModelRemoved<T>(this ITempDataDictionary tempData)
        {
            try
            {
                tempData.ReadModelStatus<T, object>(TempDataAlertModelState.Removed);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the TempData necessary key values to request an alert for: User Deactivation
        /// </summary>
        /// <param name="tempData">The TempData instance to be setted up.</param>
        /// <param name="userEmail">The email of the User deactivated.</param>
        public static void SetUserDeactivated(this ITempDataDictionary tempData, string userEmail)
        {
            tempData.SetValue(TempDataAlertConstants.UserDeactivated, true);
            tempData.SetValue(TempDataAlertConstants.UserDeactivatedEmail, userEmail);
        }

        /// <summary>
        /// Sets the TempData necessary key values to request an alert for: User Logged Out
        /// </summary>
        /// <param name="tempData">The TempData instance to be setted up.</param>
        public static void SetUserLoggedOut(this ITempDataDictionary tempData)
            => tempData.SetValue(TempDataAlertConstants.UserLoggedOut, true);

        /// <summary>
        /// Sets the TempData necessary key values to request an alert for: User Email Changed
        /// </summary>
        /// <param name="tempData">The TempData instance to be setted up.</param>
        public static void SetEmailChanged(this ITempDataDictionary tempData)
            => tempData.SetValue(TempDataAlertConstants.EmailChanged, true);

        /// <summary>
        /// Sets the TempData necessary key values to request an alert for: User Password Changed
        /// </summary>
        /// <param name="tempData">The TempData instance to be setted up.</param>
        public static void SetPasswordChanged(this ITempDataDictionary tempData)
            => tempData.SetValue(TempDataAlertConstants.PasswordChanged, true);

        #endregion
    }
}