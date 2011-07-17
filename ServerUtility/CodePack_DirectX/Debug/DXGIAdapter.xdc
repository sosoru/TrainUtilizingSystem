<?xml version="1.0"?><doc>
<members>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIObject" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiobject.h" line="11">
<summary>
An DXGIObject is the base for all DXGI classes. 
<para>(Also see DirectX SDK: IDXGIObject)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIObject.GetParent``1" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiobject.h" line="20">
<summary>
Gets the parent of this object.
<para>(Also see DirectX SDK: IDXGIObject::GetParent)</para>
</summary>
<typeparam name="T">The type of the parent object requested. 
This type has to be a DXGIObject or a subtype.</typeparam>
<returns>The parent object. Or null if this object does not have a parent.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter.h" line="11">
<summary>
The  Adapter interface represents a display sub-system (including one or more GPU's, DACs and video memory).
<para>(Also see DirectX SDK: IDXGIAdapter)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter.CheckDeviceSupport(Microsoft.WindowsAPICodePack.DirectX.Direct3D.DeviceType,System.Int32@,System.Int32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter.h" line="19">
<summary>
Checks to see if a device interface for a graphics component is supported by the system.
<para>(Also see DirectX SDK: IDXGIAdapter::CheckInterfaceSupport)</para>
</summary>
<param name="deviceType"> The device support checked.</param>
<param name="UMDVersionMajor">An out parameter containing the user mode driver version of InterfaceName high 32 bit value. Returns 0 if interface is not supported.</param>
<param name="UMDVersionMinor">An out parameter containing the user mode driver version of InterfaceName low 32 bit value. Returns 0 if interface is not supported.</param>
<returns>True if the device is supported by the system; false otherwise</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter.CheckDeviceSupport(Microsoft.WindowsAPICodePack.DirectX.Direct3D.DeviceType)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter.h" line="29">
<summary>
Checks to see if a device interface for a graphics component is supported by the system.
<para>(Also see DirectX SDK: IDXGIAdapter::CheckInterfaceSupport)</para>
</summary>
<param name="deviceType"> The device support checked.</param>
<returns>True if the device is supported by the system; false otherwise</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter.GetOutputs" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter.h" line="37">
<summary>
Get a read-only collection of all adapter (video card) outputs available.
<para>(Also see DirectX SDK: IDXGIAdapter::EnumOutputs)</para>
</summary>
<returns>A readonly collection of Output object</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter.GetOutput(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter.h" line="44">
<summary>
Get an adapter (video card) output.
<para>(Also see DirectX SDK: IDXGIAdapter::EnumOutputs)</para>
</summary>
<param name="index">The index of the output requested.</param>
<returns>An Output object</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter.h" line="52">
<summary>
Gets a DXGI 1.0 description of an adapter (or video card).
<para>(Also see DirectX SDK: IDXGIAdapter::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="22">
<summary>
An Output interface represents an adapter output (such as a monitor).
<para>(Also see DirectX SDK: IDXGIOutput)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.FindClosestMatchingMode(Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="30">
<summary>
Find the display mode that most closely matches the requested display mode 
for a given Direct3D Device.
<para>(Also see DirectX SDK: IDXGIOutput::FindClosestMatchingMode)</para>
</summary>
<param name="modeToMatch">The desired display mode (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>. Members of ModeDescription can be unspecified indicating no preference for that member.  A value of 0 for Width or Height indicates the value is unspecified.  If either Width or Height are 0 both must be 0.  A numerator and denominator of 0 in RefreshRate indicate it is unspecified. Other members of ModeDescription have enumeration values indicating the member is unspecified.  If pConnectedDevice is null, Format cannot be UNKNOWN.</param>
<param name="concernedDevice">The Direct3D device object. If this parameter is NULL, only modes whose format matches that of pModeToMatch will         be returned; otherwise, only those formats that are supported for scan-out by the device are returned.</param>
<returns>The mode that most closely matches ModeToMatch.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.FindClosestMatchingMode(Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="40">
<summary>
Find the display mode that most closely matches the requested display mode 
for a given Direct3D Device.
<para>(Also see DirectX SDK: IDXGIOutput::FindClosestMatchingMode)</para>
</summary>
<param name="modeToMatch">The desired display mode (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>. Members of ModeDescription can be unspecified indicating no preference for that member.  A value of 0 for Width or Height indicates the value is unspecified.  If either Width or Height are 0 both must be 0.  A numerator and denominator of 0 in RefreshRate indicate it is unspecified. Other members of ModeDescription have enumeration values indicating the member is unspecified.  If pConnectedDevice is null, Format cannot be UNKNOWN.</param>
<param name="concernedDevice">The Direct3D device object. If this parameter is NULL, only modes whose format matches that of pModeToMatch will         be returned; otherwise, only those formats that are supported for scan-out by the device are returned.</param>
<returns>The mode that most closely matches ModeToMatch.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.FindClosestMatchingMode(Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="50">
<summary>
Find the display mode that most closely matches the requested display mode.
<para>(Also see DirectX SDK: IDXGIOutput::FindClosestMatchingMode)</para>
</summary>
<param name="modeToMatch">The desired display mode (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>. Members of ModeDescription can be unspecified indicating no preference for that member.  A value of 0 for Width or Height indicates the value is unspecified.  If either Width or Height are 0 both must be 0.  A numerator and denominator of 0 in RefreshRate indicate it is unspecified. Other members of ModeDescription have enumeration values indicating the member is unspecified. Format cannot be UNKNOWN.</param>
<returns>The mode that most closely matches ModeToMatch.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="58">
<summary>
Get a description of the output.
<para>(Also see DirectX SDK: IDXGIOutput::GetDesc)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.GetDisplayModeList(&lt;unknown type&gt;,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="67">
<summary>
Gets the display modes that match the requested format and other input options.
<para>(Also see DirectX SDK: IDXGIOutput::GetDisplayModeList)</para>
</summary>
<param name="colorFormat">The color format (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>.</param>
<param name="flags">Options for modes to include (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.EnumModes"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.EnumModes"/>. EnumModes.Scaling needs to be specified to expose the display modes that require scaling.  Centered modes, requiring no scaling and corresponding directly to the display output, are enumerated by default.</param>
<returns>An aray of display modes (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/> or null if no modes were found.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.GetNumberOfDisplayModes(&lt;unknown type&gt;,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="76">
<summary>
Gets the display modes that match the requested format and other input options.
<para>(Also see DirectX SDK: IDXGIOutput::GetDisplayModeList)</para>
</summary>
<param name="colorFormat">The color format (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>.</param>
<param name="flags">Options for modes to include (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.EnumModes"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.EnumModes"/>. EnumModes.Scaling needs to be specified to expose the display modes that require scaling.  Centered modes, requiring no scaling and corresponding directly to the display output, are enumerated by default.</param>
<returns>The number of display modes (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>) that matches the format and options.<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/></returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.GetDisplaySurfaceData(Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="85">
<summary>
Get a copy of the current display surface.
<para>(Also see DirectX SDK: IDXGIOutput::GetDisplaySurfaceData)</para>
</summary>
<remarks>
GetDisplaySurfaceData can only be called when an output is in full-screen mode. 
If the method succeeds, the destination surface will be filled and its reference count incremented. 
</remarks>
<param name="Destination">A destination surface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.GetRenderedFrameStatistics" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="96">
<summary>
Get statistics about recently rendered frames.
<para>(Also see DirectX SDK: IDXGIOutput::GetFrameStatistics)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.GetGammaControl" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="102">
<summary>
Gets the gamma control settings.
<para>(Also see DirectX SDK: IDXGIOutput::GetGammaControl)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.SetGammaControl(Microsoft.WindowsAPICodePack.DirectX.DXGI.GammaControl)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="108">
<summary>
Sets the gamma control settings.
<para>(Also see DirectX SDK: IDXGIOutput::SetGammaControl)</para>
</summary>
<param name="gammaControl">Gamma control settings.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.GetGammaControlCapabilities" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="115">
<summary>
Get a description of the gamma-control capabilities.
<para>(Also see DirectX SDK: IDXGIOutput::GetGammaControlCapabilities)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.SetDisplaySurface(Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="121">
<summary>
Change the display mode.
<para>(Also see DirectX SDK: IDXGIOutput::SetDisplaySurface)</para>
</summary>
<param name="ScanoutSurface">A surface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface"/> used for rendering an image to the screen. The surface must have been created with as a back buffer (DXGI_USAGE_BACKBUFFER).</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.ReleaseOwnership" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="128">
<summary>
Release ownership of the output.
<para>(Also see DirectX SDK: IDXGIOutput::ReleaseOwnership)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.TakeOwnership(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="134">
<summary>
Take ownership of an output.
<para>(Also see DirectX SDK: IDXGIOutput::TakeOwnership)</para>
</summary>
<param name="device">A Direct3D 10 Device.</param>
<param name="exclusive">Set to TRUE to enable other threads or applications to take ownership of the device; otherwise set to FALSE.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.TakeOwnership(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="142">
<summary>
Take ownership of an output.
<para>(Also see DirectX SDK: IDXGIOutput::TakeOwnership)</para>
</summary>
<param name="device">A Direct3D 11 Device.</param>
<param name="exclusive">Set to TRUE to enable other threads or applications to take ownership of the device; otherwise set to FALSE.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.TakeOwnership(Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIDevice,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="150">
<summary>
Take ownership of an output.
<para>(Also see DirectX SDK: IDXGIOutput::TakeOwnership)</para>
</summary>
<param name="device">A DXGI Device.</param>
<param name="exclusive">Set to TRUE to enable other threads or applications to take ownership of the device; otherwise set to FALSE.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output.WaitForVBlank" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgioutput.h" line="158">
<summary>
Halt a thread until the next vertical blank occurs.
<para>(Also see DirectX SDK: IDXGIOutput::WaitForVBlank)</para>
</summary>
</member>
</members>
</doc>