// Developer Express Code Central Example:
// How to obtain labels from a datasource and handle deleted labels
// 
// This example elaborates the approach illustrated in example
// http://www.devexpress.com/scid=E2028. Labels for appointments are obtained from
// a data source. When deleting, the label is not actually removed but its color
// and caption are changed to identify a "deleted label" entity which should be
// treated specifically. An appointment labeled with the "deleted label" could not
// be created or edited (moved, resized).
// Additionally, this example demonstrates
// how to hide "deleted" labels when editing or creating a new appointment, as well
// as hide them in the appointment context menu.
// This approach can help you to
// implement more complex scenario.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2087

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DS12188")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("DS12188")]
[assembly: AssemblyCopyright("Copyright ©  2010")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("976ad08e-16b0-40de-892a-a39ae8a8cc13")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
