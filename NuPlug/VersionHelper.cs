using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using NEdifis.Attributes;
using NuGet;

namespace NuPlug
{
    [TestedBy(typeof(VersionHelper_Should))]
    public static class VersionHelper
    {
        internal static string BestMatch(this Version version, IEnumerable<string> folderNames)
        {
            return folderNames
                .OrderBy(f => VersionDistance(version, VersionUtility.ParseFrameworkName(f)?.Version))
                .FirstOrDefault();
        }


        internal static FrameworkName GetTargetFramework(Assembly assembly = null)
        {
            var safeAssembly = assembly ?? (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
            var a = safeAssembly.GetCustomAttributes(typeof(TargetFrameworkAttribute), false)
                .OfType<TargetFrameworkAttribute>().FirstOrDefault();
            return a != null ? new FrameworkName(a.FrameworkName) : null;
        }

        private static double VersionDistance(Version a, Version b)
        {
            if (a == null || b == null) return double.MaxValue;
            var dx = (a.Major - b.Major);
            var dy = (a.MajorRevision - b.MajorRevision);
            var dz = (a.Minor - b.Minor);
            var dw = (a.MinorRevision - b.MinorRevision);
            return 1000000 * dx * dx + 10000 * dy * dy + 100 * dz * dz + dw * dw;
        }
    }
}