using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("MDW")]
[assembly: AssemblyDescription("Markdown Wiki")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("MDW")]
[assembly: AssemblyProduct("Markdown Wiki")]
[assembly: AssemblyCopyright("Copyright © Paramesh Gunasekaran 2017 Markdown Wiki. All Rights Reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("881e89c6-3afd-4d5a-89c1-7989c2312d85")]
[assembly: AssemblyMetadata("Email", "root@localhost")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

public static class AssemblyInfo
{
    public static string Version
    {
        get
        {
            string result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            result = fvi.FileVersion;

            return result;
        }
    }

    public static string Build
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            result = new FileInfo(assembly.Location).LastWriteTime.ToString();

            return result;
        }
    }

    public static string Copyright
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);

            if (attributes.Length > 0)
                result = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

            return result;
        }
    }

    public static string Email { get { return AssemblyMetadata("Email"); } }

    public static string Hash
    {
        get
        {
            var result = string.Empty;

            var buffer = File.ReadAllBytes(Assembly.GetExecutingAssembly().Location);

            result = MDW.Tools.Hash.SHA256(buffer);

            return result;
        }
    }

    private static string AssemblyMetadata(string key)
    {
        var result = string.Empty;

        var assembly = Assembly.GetExecutingAssembly();

        var attributes = assembly.GetCustomAttributes(typeof(AssemblyMetadataAttribute), true);

        if (attributes.Length > 0)
        {
            foreach (var i in attributes)
            {
                var meta = i as AssemblyMetadataAttribute;

                if (meta != null && meta.Key.Equals(key))
                    result = meta.Value;
            }
        }

        return result;
    }
}