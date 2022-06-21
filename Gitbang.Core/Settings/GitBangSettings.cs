using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Gitbang.Core.Settings
{
    public class GitBangSettings
    {
		readonly XElement root;

        GitBangSettings()
		{
			this.root = new XElement("Gitbang");
		}

        GitBangSettings(XElement root)
		{
			this.root = root;
		}

		public XElement this[XName section]
		{
			get
			{
				return root.Element(section) ?? new XElement(section);
			}
		}

		/// <summary>
		/// Loads the settings file from disk.
		/// </summary>
		/// <returns>
		/// An instance used to access the loaded settings.
		/// </returns>
		public static GitBangSettings Load()
		{
			using (new MutexProtector(ConfigFileMutex))
			{
				try
				{
					var doc = LoadWithoutCheckingCharacters(GetConfigFile());
					return new GitBangSettings(doc.Root);
				}
				catch (IOException)
				{
					return new GitBangSettings();
				}
				catch (XmlException)
				{
					return new GitBangSettings();
				}
			}
		}

		static XDocument LoadWithoutCheckingCharacters(string fileName)
		{
			return XDocument.Load(fileName, LoadOptions.None);
		}

		/// <summary>
		/// Saves a setting section.
		/// </summary>
		public static void SaveSettings(XElement section)
		{
			Update(
				delegate (XElement root) {
					XElement existingElement = root.Element(section.Name);
					if (existingElement != null)
						existingElement.ReplaceWith(section);
					else
						root.Add(section);
				});
		}

		/// <summary>
		/// Updates the saved settings.
		/// We always reload the file on updates to ensure we aren't overwriting unrelated changes performed
		/// by another Gitbang instance.
		/// </summary>
		public static void Update(Action<XElement> action)
		{
			using (new MutexProtector(ConfigFileMutex))
			{
				string config = GetConfigFile();
				XDocument doc;
				try
				{
					doc = LoadWithoutCheckingCharacters(config);
				}
				catch (IOException)
				{
					// ensure the directory exists
					Directory.CreateDirectory(Path.GetDirectoryName(config));
					doc = new XDocument(new XElement("Gitbang"));
				}
				catch (XmlException)
				{
					doc = new XDocument(new XElement("Gitbang"));
				}
				doc.Root.SetAttributeValue("version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
				action(doc.Root);
				doc.Save(config, SaveOptions.None);
			}
		}

		static string GetConfigFile()
		{
			string localPath = Path.Combine(AppContext.BaseDirectory, "Gitbang.xml");
			if (File.Exists(localPath))
				return localPath;
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Gitbang.xml");
		}

		const string ConfigFileMutex = "355BE18F-FE69-437F-B8D2-7F085D975457";

		/// <summary>
		/// Helper class for serializing access to the config file when multiple Gitbang instances are running.
		/// </summary>
		sealed class MutexProtector : IDisposable
		{
			readonly Mutex mutex;

			public MutexProtector(string name)
			{
                this.mutex = new Mutex(true, name, out var createdNew);
				
                if (!createdNew)
				{
					try
					{
						mutex.WaitOne();
					}
					catch (AbandonedMutexException)
					{
					}
				}
			}

			public void Dispose()
			{
				mutex.ReleaseMutex();
				mutex.Dispose();
			}
		}
	}
}
