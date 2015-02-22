// Guids.cs
// MUST match guids.h
using System;

namespace Sqline.VSPackage
{
    static class GuidList
    {
        public const string guidSqlinePkgString = "0c69ad0f-767e-4797-81f8-dc9948461076";
        public const string guidSqlineCmdSetString = "6b9005d7-bbbd-4e48-bb88-78ced0925cee";

				public static readonly Guid guidSqlineCmdSet = new Guid(guidSqlineCmdSetString);
				public static readonly Guid guidSqlinePkg = new Guid(guidSqlinePkgString);
    };
}