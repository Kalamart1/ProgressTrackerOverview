﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MelonLoader;
using ProgressTrackerOverview; // The namespace of your mod class
// ...
[assembly: MelonInfo(typeof(ProgressTrackerOverview.MyMod), ProgressTrackerOverview.BuildInfo.ModName, ProgressTrackerOverview.BuildInfo.ModVersion, ProgressTrackerOverview.BuildInfo.Author)]
[assembly: VerifyLoaderVersion(0, 6, 5, true)]
[assembly: MelonGame("Buckethead Entertainment", "RUMBLE")]
[assembly: MelonColor(255, 195, 0, 255)]
[assembly: MelonAuthorColor(201, 20, 65, 255)]

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(ProgressTrackerOverview.BuildInfo.ModName)]
[assembly: AssemblyDescription(ProgressTrackerOverview.BuildInfo.Description)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(ProgressTrackerOverview.BuildInfo.Company)]
[assembly: AssemblyProduct(ProgressTrackerOverview.BuildInfo.ModName)]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("621d30a5-8fa1-4d87-9826-92c0149b033e")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(ProgressTrackerOverview.BuildInfo.ModVersion)]
[assembly: AssemblyFileVersion(ProgressTrackerOverview.BuildInfo.ModVersion)]