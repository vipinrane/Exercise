using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("OpenNETCF.IoC")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OpenNETCF Consulting, LLC")]
[assembly: AssemblyProduct("OpenNETCF.IoC")]
[assembly: AssemblyCopyright("Public Domain")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("56681ced-172e-46f5-be28-89e047c6b3a8")]

// Below attribute is to suppress FxCop warning "CA2232 : Microsoft.Usage : Add STAThreadAttribute to assembly"
// as Device app does not support STA thread.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2232:MarkWindowsFormsEntryPointsWithStaThread")]

//[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("OpenNETCF.IoC.Unit.Test")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo ( "OpenNETCF.IoC.UI, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b1adf5e34457e50deceb6a61dcd41b33fe1ee9c4e7da543612947ff0ac84b850782e594a77d368e52f113a17450113416d835c79a7ce21d6848931c523e7b1709cc9b725da0b8053398f5ad51c2999108f48b518d29d6fbd4349297584389a2d00435af7af231c9a2277023c2187890ac6e63e8a6d8f9c76ec3682ba21805bcf" )]