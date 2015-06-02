using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;

namespace OpenNETCF.Configuration
{
    /// <summary>
    /// Provides access to configuration settings in a specified configuration section. 
    /// This class cannot be inherited.
    /// </summary>
    public static class ConfigurationSettings
    {
        private static string m_globalCodeBase = 
            OpenNETCF.Reflection.Assembly2.GetEntryAssembly ().GetName ().CodeBase;

        private static IDictionary<string, ConfigurationSettingsSystem> m_settings = new Dictionary<string, ConfigurationSettingsSystem> ();

        internal static bool SetConfigurationSystemInProgress
        {
            get
            {
                return m_settings[ m_globalCodeBase ].SetConfigurationSystemInProgress;
            }
        }

        /// <summary>
        /// Forces the settings provider to re-load the settings from the configuration file.
        /// </summary>
        /// <remarks></remarks>
        public static void Reload ()
        {
            foreach ( var s in m_settings.Values )
                s.Reload ();
        }

        /// <summary>
        /// Gets configuration settings in the configuration section.
        /// </summary>
        /// <value>The application settings.</value>
        public static NameValueCollection AppSettings
        {
            get
            {
                return GetAppSettings ();
            }
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <returns>The application settings.</returns>
        public static NameValueCollection GetAppSettings ()
        {
            return GetAppSettings ( m_globalCodeBase );
        }

        /// <summary>
        /// Gets the application settings for the specified code base.
        /// </summary>
        /// <param name="codeBase">The code base.</param>
        /// <returns>The application settings.</returns>
        public static NameValueCollection GetAppSettings ( string codeBase )
        {
            if (!m_settings.ContainsKey(codeBase))
            {
                var css = new ConfigurationSettingsSystem(codeBase);
                m_settings.Add(codeBase, css);
            }
            return ( m_settings.ContainsKey ( codeBase ) ) ?
                m_settings[ codeBase ].AppSettings : null;
        }

        /// <summary>
        /// Returns configuration settings for a user-defined configuration section.  
        /// </summary>
        /// <param name="sectionName">The configuration section to read.</param>
        /// <returns>The configuration settings for sectionName.</returns>
        public static object GetConfig ( string sectionName )
        {
            return GetConfig ( m_globalCodeBase, sectionName, null );
        }

        /// <summary>
        /// Returns configuration settings for a user-defined configuration section for the specified code base.
        /// </summary>
        /// <param name="codeBase">The code base.</param>
        /// <param name="sectionName">The configuration section to read.</param>
        /// <returns>The configuration settings for sectionName.</returns>
        public static object GetConfig ( string codeBase, string sectionName )
        {
            return GetConfig ( codeBase, sectionName, null );
        }

        /// <summary>
        /// Returns configuration settings for a user-defined configuration section. 
        /// </summary>
        /// <param name="sectionName">The configuration section to read.</param>
        /// <param name="configContext"></param>
        /// <returns></returns>
        public static object GetConfig ( string sectionName, object configContext )
        {
            return GetConfig ( m_globalCodeBase, sectionName, configContext );
        }

        /// <summary>
        /// Returns configuration settings for a user-defined configuration section for the specified code base.
        /// </summary>
        /// <param name="codeBase">The code base.</param>
        /// <param name="sectionName">The configuration section to read.</param>
        /// <param name="configContext">The configContext.</param>
        /// <returns>System.Object.</returns>
        public static object GetConfig ( string codeBase, string sectionName, object configContext )
        {
            if ( !m_settings.ContainsKey ( codeBase ) )
            {
                var css = new ConfigurationSettingsSystem ( codeBase );
                m_settings.Add ( codeBase, css );
            }

            return m_settings[ codeBase ].GetConfig ( sectionName, configContext );
        }

        class ConfigurationSettingsSystem
        {
            // The Configuration System
            private IConfigurationSystem m_configSystem = null;
            private string m_codeBase = null;
            private bool m_configurationInitialized = false;
            private Exception initError = null;

            internal ConfigurationSettingsSystem ( string codeBase )
            {
                this.m_codeBase = codeBase;
            }

            internal bool SetConfigurationSystemInProgress
            {
                get { return ( ( this.m_configSystem != null ) && ( this.m_configurationInitialized == false ) ); }
            }

            internal void Reload ()
            {
                this.m_configurationInitialized = false;
                this.m_configSystem = null;
            }

            internal NameValueCollection AppSettings
            {
                get
                {
                    var appSettings = ( ReadOnlyNameValueCollection ) GetConfig ( "appSettings" );

                    if ( appSettings == null )
                    {
                        appSettings = new ReadOnlyNameValueCollection ( StringComparer.OrdinalIgnoreCase );

                        appSettings.SetReadOnly ();
                    }

                    return appSettings;
                }
            }

            internal object GetConfig ( string sectionName )
            {
                return GetConfig ( sectionName, null );
            }

            internal object GetConfig ( string sectionName, object context )
            {
                if ( !this.m_configurationInitialized )
                {
                    lock ( this )
                    {
                        if ( this.m_configSystem == null && !this.SetConfigurationSystemInProgress )
                        {
                            SetConfigurationSystem ( new DefaultConfigurationSystem ( this.m_codeBase ) );
                        }
                    }
                }
                if ( initError != null )
                {
                    throw initError;
                }
                else
                {
                    return m_configSystem.GetConfig ( sectionName, context );
                }
            }

            void SetConfigurationSystem ( IConfigurationSystem configSystem )
            {
                lock ( this )
                {
                    if ( this.m_configSystem != null )
                    {
                        throw new InvalidOperationException ( "Config system already set" );
                    }

                    try
                    {
                        m_configSystem = configSystem;
                        m_configSystem.Init ();
                    }
                    catch ( Exception e )
                    {
                        initError = e;
                        throw;
                    }

                    m_configurationInitialized = true;
                }
            }
        }
    }
}
