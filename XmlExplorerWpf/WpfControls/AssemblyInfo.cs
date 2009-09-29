using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WpfControls
{
	public class AssemblyInfo
	{
		private static AssemblyInfo _default;

		public string Title { get; private set; }
		public Version Version { get; private set; }
		public string Description { get; private set; }
		public string Company { get; private set; }
		public string Copyright { get; private set; }
		public string Product { get; private set; }

		public static AssemblyInfo Default
		{
			get
			{
				if (_default == null)
					_default = new AssemblyInfo();

				return _default;
			}
		}

		public AssemblyInfo()
			: this(Assembly.GetEntryAssembly())
		{
		}

		public AssemblyInfo(Assembly assembly)
		{
			this.Title = GetTitle(assembly);
			this.Version = GetVersion(assembly);
			this.Description = GetDescription(assembly);
			this.Company = GetCompany(assembly);
			this.Product = GetProduct(assembly);
			this.Copyright = GetCopyright(assembly);
		}

		public static string GetTitle(Assembly assembly)
		{
			var attribute = GetFirstAttribute<AssemblyTitleAttribute>(assembly);

			// If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
			if (attribute == null || string.IsNullOrEmpty(attribute.Title))
				return System.IO.Path.GetFileNameWithoutExtension(assembly.CodeBase);

			return attribute.Title;
		}

		public static Version GetVersion(Assembly assembly)
		{
			return assembly.GetName().Version;
		}

		public static string GetDescription(Assembly assembly)
		{
			var attribute = GetFirstAttribute<AssemblyDescriptionAttribute>(assembly);

			if (attribute == null)
				return null;

			return attribute.Description;
		}

		public static string GetProduct(Assembly assembly)
		{
			var attribute = GetFirstAttribute<AssemblyProductAttribute>(assembly);

			if (attribute == null)
				return null;

			return attribute.Product;
		}

		public static string GetCopyright(Assembly assembly)
		{
			var attribute = GetFirstAttribute<AssemblyCopyrightAttribute>(assembly);

			if (attribute == null)
				return null;

			return attribute.Copyright;
		}

		public static string GetCompany(Assembly assembly)
		{
			var attribute = GetFirstAttribute<AssemblyCompanyAttribute>(assembly);

			if (attribute == null)
				return null;

			return attribute.Company;
		}

		public static TAttribute GetFirstAttribute<TAttribute>(Assembly assembly)
		{
			// Get all attributes on this assembly
			object[] attributes = assembly.GetCustomAttributes(typeof(TAttribute), false);

			// If there aren't any attributes, return null
			if (attributes.Length == 0)
				return default(TAttribute);

			return (TAttribute)attributes[0];
		}

	}
}