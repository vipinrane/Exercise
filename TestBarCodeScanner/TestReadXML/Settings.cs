using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace TestReadXML
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class Settings
	{
		private static NameValueCollection m_settings;
		private static string m_settingsPath;
        
		static Settings()
		{
			m_settingsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			m_settingsPath += @"\Settings.xml";
			
			if(!File.Exists(m_settingsPath))
				throw new FileNotFoundException(m_settingsPath + " could not be found.");

			System.Xml.XmlDocument xdoc = new XmlDocument();
			xdoc.Load(m_settingsPath);
			XmlElement root = xdoc.DocumentElement;
			System.Xml.XmlNodeList nodeList = root.ChildNodes.Item(0).ChildNodes;

			// Add settings to the NameValueCollection.
			m_settings = new NameValueCollection();
			m_settings.Add("ServerIP", nodeList.Item(0).Attributes["value"].Value);
			m_settings.Add("UserName", nodeList.Item(1).Attributes["value"].Value);
			m_settings.Add("Password", nodeList.Item(2).Attributes["value"].Value);
			m_settings.Add("PhoneNumber", nodeList.Item(3).Attributes["value"].Value);
			m_settings.Add("TimeOut", nodeList.Item(4).Attributes["value"].Value);
			m_settings.Add("LastTransmit", nodeList.Item(5).Attributes["value"].Value);
			m_settings.Add("DatabasePath", nodeList.Item(6).Attributes["value"].Value);
		}

		public static void Update()
		{
			XmlTextWriter tw = new XmlTextWriter(m_settingsPath, System.Text.UTF8Encoding.UTF8);
			tw.WriteStartDocument();
			tw.WriteStartElement("configuration");
			tw.WriteStartElement("appSettings");

			for(int i=0; i<m_settings.Count; ++i)
			{
				tw.WriteStartElement("add");
				tw.WriteStartAttribute("key", string.Empty);
				tw.WriteRaw(m_settings.GetKey(i));
				tw.WriteEndAttribute();

				tw.WriteStartAttribute("value", string.Empty);
				tw.WriteRaw(m_settings.Get(i));
				tw.WriteEndAttribute();
				tw.WriteEndElement();
			}

			tw.WriteEndElement();
			tw.WriteEndElement();

			tw.Close();
		}

		public static string ServerIP
		{
			get { return m_settings.Get("ServerIP"); }
			set { m_settings.Set("ServerIP", value); }
		}

		public static string UserName
		{
			get { return m_settings.Get("UserName"); }
			set { m_settings.Set("UserName", value); }
		}

		public static string Password
		{
			get { return m_settings.Get("Password"); }
			set { m_settings.Set("Password", value); }
		}

		public static string PhoneNumber
		{
			get { return m_settings.Get("PhoneNumber"); }
			set { m_settings.Set("PhoneNumber", value); }
		}

		public static string TimeOut
		{
			get { return m_settings.Get("TimeOut"); }
			set { m_settings.Set("TimeOut", value); }
		}

		public static string LastTransmit
		{
			get { return m_settings.Get("LastTransmit"); }
			set { m_settings.Set("LastTransmit", value); }
		}

		public static string DatabasePath
		{
			get { return m_settings.Get("DatabasePath"); }
			set { m_settings.Set("DatabasePath", value); }
		}
	}
}
