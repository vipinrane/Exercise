// ***********************************************************************
// Assembly         : DytrexEndirePro.Shared
// Author           : Alexander Burghardt
// Created          : 04-08-2014
//
// Last Modified By : Alexander Burghardt
// Last Modified On : 04-08-2014
// ***********************************************************************
// <copyright file="StaticHelper.cs" company="DYTREX Ltd.">
//     Copyright (c) DYTREX Ltd.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using OpenNETCF.Configuration;

namespace TestAppSettingReading
{
    /// <summary>
    /// Shared common helper methods.
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// Gets the app setting for the specified key in the given settings namespace.
        /// </summary>
        /// <typeparam name="TEnumKeyType">The type of the enum that represents the key.</typeparam>
        /// <param name="settingsNamespace">The settings namespace. Can be null if no namespace is required.</param>
        /// <param name="key">The key for which the value is requested.</param>
        /// <returns>The value which is associated to the specified key or null if nothing was found.</returns>
        public static string GetAppSetting<TEnumKeyType> ( string settingsNamespace, TEnumKeyType key )
            where TEnumKeyType : struct
        {
            return GetAppSetting ( null, settingsNamespace, key.ToString () );
        }

        /// <summary>
        /// Gets the app setting for the specified key in the given settings namespace.
        /// </summary>
        /// <param name="settingsNamespace">The settings namespace. Can be null if no namespace is required.</param>
        /// <param name="key">The key for which the value is requested.</param>
        /// <returns>The value which is associated to the specified key or null if nothing was found.</returns>
        /// <exception cref="System.ArgumentNullException">The key cannot be a null reference (Nothing in Visual Basic).</exception>
        public static string GetAppSetting ( string settingsNamespace, string key )
        {
            return GetAppSetting ( null, settingsNamespace, key );
        }

        /// <summary>
        /// Gets the app setting for the specified key in the given settings namespace. It will be searched in a configuration specified by the given code base.
        /// </summary>
        /// <typeparam name="TEnumKeyType">The type of the enum that represents the key.</typeparam>
        /// <param name="codeBase">The code base for the configuration or null if the global configuration should be used.</param>
        /// <param name="settingsNamespace">The settings namespace. Can be null if no namespace is required.</param>
        /// <param name="key">The key for which the value is requested.</param>
        /// <returns>The value which is associated to the specified key or null if nothing was found.</returns>
        public static string GetAppSetting<TEnumKeyType> ( string codeBase, string settingsNamespace, TEnumKeyType key )
            where TEnumKeyType : struct
        {
            return GetAppSetting ( codeBase, settingsNamespace, key.ToString () );
        }

        /// <summary>
        /// Gets the app setting for the specified key in the given settings namespace. It will be searched in a configuration specified by the given code base.
        /// </summary>
        /// <typeparam name="TEnumKeyType">The type of the enum that represents the key.</typeparam>
        /// <param name="codeBase">The code base for the configuration or null if the global configuration should be used.</param>
        /// <param name="settingsNamespace">The settings namespace.</param>
        /// <param name="key">The key for which the value is requested.</param>
        /// <returns>The value which is associated to the specified key or null if nothing was found.</returns>
        public static string GetAppSetting<TEnumKeyType> ( string codeBase, TEnumKeyType settingsNamespace, TEnumKeyType key )
            where TEnumKeyType : struct
        {
            return GetAppSetting ( codeBase, settingsNamespace.ToString (), key.ToString () );
        }

        /// <summary>
        /// Gets the app setting for the specified key in the given settings namespace. It will be searched in a configuration specified by the given code base.
        /// </summary>
        /// <param name="codeBase">The code base for the configuration or null if the global configuration should be used.</param>
        /// <param name="settingsNamespace">The settings namespace. Can be null if no namespace is required.</param>
        /// <param name="key">The key for which the value is requested.</param>
        /// <returns>The value which is associated to the specified key or null if nothing was found.</returns>
        /// <exception cref="System.ArgumentNullException">The key cannot be a null reference (Nothing in Visual Basic).</exception>
        public static string GetAppSetting ( string codeBase, string settingsNamespace, string key )
        {
            if ( key == null ) throw new ArgumentNullException ( "key" );

            var appSettingsName = ( ( settingsNamespace.IsNullOrEmpty () ) ? "" : settingsNamespace + "." ) + key;

            //return ( codeBase == null ) ?
            //    ConfigurationSettings.AppSettings.Get ( appSettingsName ) :
            //    ConfigurationSettings.GetAppSettings ( codeBase ).Get ( appSettingsName );

            var obj = ConfigurationSettings.AppSettings.Get(appSettingsName);
            return string.Empty;
        }
    }
}
