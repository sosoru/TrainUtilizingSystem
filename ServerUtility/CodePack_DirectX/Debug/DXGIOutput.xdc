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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DeviceSubObject" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevicesubobject.h" line="19">
<summary>
Inherited from objects that are tied to the device so that they can retrieve it.
<para>(Also see DirectX SDK: IDXGIDeviceSubObject)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DeviceSubObject.GetDirect3D10Device" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevicesubobject.h" line="28">
<summary>
Retrieves the device as Direct3D 10 device.
<para>(Also see DirectX SDK: IDXGIDeviceSubObject::GetDevice)</para>
</summary>
<returns>A Direct3D 10 Device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DeviceSubObject.GetDirect3D10Device1" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevicesubobject.h" line="35">
<summary>
Retrieves the device as Direct3D 10.1 device.
<para>(Also see DirectX SDK: IDXGIDeviceSubObject::GetDevice)</para>
</summary>
<returns>A Direct3D 10.1 Device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DeviceSubObject.GetDirect3D11Device" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevicesubobject.h" line="42">
<summary>
Retrieves the device as  Direct3D 11 device.
<para>(Also see DirectX SDK: IDXGIDeviceSubObject::GetDevice)</para>
</summary>
<returns>A Direct3D 11 Device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DeviceSubObject.GetDXGIDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevicesubobject.h" line="49">
<summary>
Retrieves the device as DXGI device.
<para>(Also see DirectX SDK: IDXGIDeviceSubObject::GetDevice)</para>
</summary>
<returns>A DXGI Device.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgisurface.h" line="10">
<summary>
An Surface interface implements methods for image-data objects.
<para>(Also see DirectX SDK: IDXGISurface)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgisurface.h" line="18">
<summary>
Get a description of the surface.
<para>(Also see DirectX SDK: IDXGISurface::GetDesc)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface.Map(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgisurface.h" line="27">
<summary>
Get the data contained in the surface, and deny GPU access to the surface.
<para>(Also see DirectX SDK: IDXGISurface::Map)</para>
</summary>
<param name="flags">CPU read-write flags. These flags can be combined with a logical OR.</param>
<returns>The surface data (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.MappedRect"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.MappedRect"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface.Unmap" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgisurface.h" line="35">
<summary>
Invalidate the pointer to the surface retrieved by Surface.Map and re-enable GPU access to the resource.
<para>(Also see DirectX SDK: IDXGISurface::Unmap)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.Surface.FromNativeSurface(System.IntPtr)" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgisurface.h" line="41">
<summary>
Create a new Surface instance from the provided IDXGISurface pointer.
This method will AddRef the pointer and assume ownership of the new reference.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIDevice" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevice.h" line="14">
<summary>
Implements a derived class for DXGI objects that produce image data.
<para>(Also see DirectX SDK: IDXGIDevice)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIDevice.GetAdapter" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevice.h" line="23">
<summary>
Returns the adapter for the specified device.
<para>(Also see DirectX SDK: IDXGIDevice::GetAdapter)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIDevice.GPUThreadPriority" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevice.h" line="29">
<summary>
Sets or Gets the GPU thread priority.
<para>(Also see DirectX SDK: IDXGIDevice::GetGPUThreadPriority and IDXGIDevice::SetGPUThreadPriority)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIDevice.QueryResourceResidency(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIResource})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevice.h" line="39">
<summary>
Gets the residency status of a colleciton of resources.
<para>Note: This method should not be called every frame as it incurs a non-trivial amount of overhead.</para>
<para>(Also see DirectX SDK: IDXGIDevice::QueryResourceResidency)</para>
</summary>
<param name="resources">A collection or array of DXGIResource interfaces.</param>
<returns>An array of residency flags. Each element describes the residency status for corresponding element in
the resources argument.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="50">
<summary>
The device interface represents a virtual Direct3D 11 adapter; it is used to perform rendering and create resources.
<para>To create a D3DDevice instance, use one of the static factory method overloads: CreateDevice() or CreateDeviceAndSwapChain().</para>
<para>(Also see DirectX SDK: ID3D11Device)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckCounter(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterDescription,&lt;unknown type&gt;@,System.UInt32@,System.String@,System.String@,System.String@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="60">
<summary>
Get the type, name, units of measure, and a description of an existing counter.
<para>(Also see DirectX SDK: ID3D11Device::CheckCounter)</para>
</summary>
<param name="counterDescription">A counter description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterDescription"/>. Specifies which counter information is to be retrieved about.</param>
<param name="type">The data type of a counter (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterType"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterType"/>. Specifies the data type of the counter being retrieved.</param>
<param name="numActiveCounters">The number of hardware counters that are needed for this counter type to be created. All instances of the same counter type use the same hardware counters.</param>
<param name="name">String to be filled with a brief name for the counter. May be NULL if the application is not interested in the name of the counter.</param>
<param name="units">Name of the units a counter measures, provided the memory the pointer points to has enough room to hold the string. Can be NULL. The returned string will always be in English.</param>
<param name="descriptionString">A description of the counter, provided the memory the pointer points to has enough room to hold the string. Can be NULL. The returned string will always be in English.</param>
<returns>True if successful; false otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckCounterInformation" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="73">
<summary>
Get a counter's information.
<para>(Also see DirectX SDK: ID3D11Device::CheckCounterInfo)</para>
</summary>
<returns>The counter information.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckThreadingSupport(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.FeatureDataThreading@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="80">
<summary>
Gets information about the features that are supported by the current graphics driver.
<para>(Also see DirectX SDK: ID3D11Device::CheckFeatureSupport)</para>
</summary>
<param name="outFeatureData">Data object describing the feature if supported. 
If feature is not supported this parameter will not be valid.</param>
<returns>True if feature is supported. Otherwise, returns false.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckFeatureDataDoubles(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.FeatureDataDoubles@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="89">
<summary>
Gets information about the features that are supported by the current graphics driver.
<para>(Also see DirectX SDK: ID3D11Device::CheckFeatureSupport)</para>
</summary>
<param name="outFeatureData">Data object describing the feature if supported. 
If feature is not supported this parameter will not be valid.</param>
<returns>True if feature is supported. Otherwise, returns false.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckFeatureDataFormatSupport(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.FeatureDataFormatSupport@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="98">
<summary>
Gets information about the features that are supported by the current graphics driver.
<para>(Also see DirectX SDK: ID3D11Device::CheckFeatureSupport)</para>
</summary>
<param name="featureData">Data object describing the feature if supported.</param>
<returns>True if feature is supported. Otherwise, returns false.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckFeatureDataFormatSupport2(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.FeatureDataFormatSupport2@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="106">
<summary>
Gets information about the features that are supported by the current graphics driver.
<para>(Also see DirectX SDK: ID3D11Device::CheckFeatureSupport)</para>
</summary>
<param name="featureData">Data object describing the feature if supported.</param>
<returns>True if feature is supported. Otherwise, returns false.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckFeatureDataD3D10XHardwareOptions(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.FeatureDataD3D10XHardwareOptions@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="114">
<summary>
Gets information about the features that are supported by the current graphics driver.
<para>(Also see DirectX SDK: ID3D11Device::CheckFeatureSupport)</para>
</summary>
<param name="outFeatureData">Data object describing the feature if supported. 
If feature is not supported this parameter will be null.</param>
<returns>True if feature is supported. Otherwise, returns false.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.GetFormatSupport(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="123">
<summary>
Get the support of a given format on the installed video device.
<para>(Also see DirectX SDK: ID3D11Device::CheckFormatSupport)</para>
</summary>
<param name="format">A Format enumeration that describes a format for which to check for support.</param>
<returns>A FormatSupport enumeration values describing how the specified format is supported on the installed device. The values are ORed together.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckFormatSupport(&lt;unknown type&gt;,&lt;unknown type&gt;@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="131">
<summary>
Get the support of a given format on the installed video device.
<para>(Also see DirectX SDK: ID3D11Device::CheckFormatSupport)</para>
</summary>
<param name="format">A Format enumeration that describes a format for which to check for support.</param>
<param name="formatSupport">The type of support for the given format. 
This value is undefined if the retun value is false.</param>
<returns>True if successful; false otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.GetMultisampleQualityLevels(&lt;unknown type&gt;,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="141">
<summary>
Get the number of quality levels available during multisampling.
<para>(Also see DirectX SDK: ID3D11Device::CheckMultisampleQualityLevels)</para>
</summary>
<param name="format">The texture format. See Format.</param>
<param name="sampleCount">The number of samples during multisampling.</param>
<returns>Number of quality levels supported by the adapter.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CheckMultisampleQualityLevels(&lt;unknown type&gt;,System.UInt32,System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="150">
<summary>
Get the number of quality levels available during multisampling.
<para>(Also see DirectX SDK: ID3D11Device::CheckMultisampleQualityLevels)</para>
</summary>
<param name="format">The texture format. See Format.</param>
<param name="sampleCount">The number of samples during multisampling.</param>
<param name="multisampleQualityLevels">An our parameter containing the number of quality levels supported by the adapter.
This value is undefined if the retun value is false.</param>
<returns>True if successful; false otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateBlendState(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="161">
<summary>
Create a blend-state object that encapsules blend state for the output-merger stage.
<para>(Also see DirectX SDK: ID3D11Device::CreateBlendState)</para>
</summary>
<param name="description">A blend-state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendDescription"/>.</param>
<returns>The blend-state object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateBuffer(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="169">
<summary>
Create a buffer (vertex buffer, index buffer, or shader-constant buffer).
<para>(Also see DirectX SDK: ID3D11Device::CreateBuffer)</para>
</summary>
<param name="description">A buffer description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription"/>.</param>
<param name="InitialData">Initialization data (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>; Cannot be emmpty or null if the usage flag is Immutable).</param>
<returns>The buffer created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateBuffer(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="178">
<summary>
Create a buffer (vertex buffer, index buffer, or shader-constant buffer).
This method does not include initialization data. 
Use CreateBuffer(BufferDescription, SubresourceData) if you need to include initialization data.
<para>(Also see DirectX SDK: ID3D11Device::CreateBuffer)</para>
</summary>
<param name="description">A buffer description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription"/>.</param>
<returns>The buffer created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateClassLinkage" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="188">
<summary>
TBD
<para>(Also see DirectX SDK: ID3D11Device::CreateClassLinkage)</para>
</summary>
<returns>A class-linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateComputeShader(System.IntPtr,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="195">
<summary>
Create a compute shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateComputeShader)</para>
</summary>
<param name="shaderBytecode">A compiled shader.</param>
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<param name="classLinkage">A ClassLinkage, which represents  class linkage interface; the value can be null.</param>
<returns>A ComputeShader object</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateComputeShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="205">
<summary>
Create a compute shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateComputeShader)</para>
</summary>
<param name="shaderBytecode">A compiled shader.</param>
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<returns>A ComputeShader object</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateComputeShader(System.IO.Stream,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="214">
<summary>
Create a compute shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateComputeShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<param name="classLinkage">A ClassLinkage, which represents  class linkage interface; the value can be null.</param>
<returns>A ComputeShader object</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateComputeShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="223">
<summary>
Create a compute shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateComputeShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<returns>A ComputeShader object</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateCounter(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="231">
<summary>
Create a counter object for measuring GPU performance.
<para>(Also see DirectX SDK: ID3D11Device::CreateCounter)</para>
</summary>
<param name="description">A counter description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CounterDescription"/>.</param>
<returns>A counter (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DCounter"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DCounter"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDeferredContext(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="239">
<summary>
Creates a deferred context for play back of command lists.
<para>(Also see DirectX SDK: ID3D11Device::CreateDeferredContext)</para>
</summary>
<param name="contextFlags">Reserved for future use. Pass 0.</param>
<returns>A DeviceContext object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDepthStencilState(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="247">
<summary>
Create a depth-stencil state object that encapsulates depth-stencil test information for the output-merger stage.
<para>(Also see DirectX SDK: ID3D11Device::CreateDepthStencilState)</para>
</summary>
<param name="description">A depth-stencil state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilDescription"/>.</param>
<returns>A depth-stencil state object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDepthStencilView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilViewDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="255">
<summary>
Create a depth-stencil view for accessing resource data.
<para>(Also see DirectX SDK: ID3D11Device::CreateDepthStencilView)</para>
</summary>
<param name="resource">The resource that will serve as the depth-stencil surface. This resource must have been created with the DepthStencil flag.</param>
<param name="description">A depth-stencil-view description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilViewDescription"/>.</param>
<returns>A DepthStencilView object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDepthStencilView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="264">
<summary>
Create a depth-stencil view for accessing resource data
that accesses mipmap level 0 of the entire resource (using the format the resource was created with).
<para>(Also see DirectX SDK: ID3D11Device::CreateDepthStencilView)</para>
</summary>
<param name="resource">The resource that will serve as the depth-stencil surface. This resource must have been created with the DepthStencil flag.</param>
<returns>A DepthStencilView object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDomainShader(System.IntPtr,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="273">
<summary>
Create a domain shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateDomainShader)</para>
</summary>
<param name="shaderBytecode">A compiled shader.</param>        
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A DomainShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDomainShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="283">
<summary>
Create a domain shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateDomainShader)</para>
</summary>
<param name="shaderBytecode">A compiled shader.</param>        
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<returns>A DomainShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDomainShader(System.IO.Stream,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="292">
<summary>
Create a domain shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateDomainShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A DomainShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDomainShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="301">
<summary>
Create a domain shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateDomainShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<returns>A DomainShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShader(System.IntPtr,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="309">
<summary>
Create a geometry shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A GeometryShader object. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="319">
<summary>
Create a geometry shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<returns>A GeometryShader object. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShaderWithStreamOutput(System.IntPtr,System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.StreamOutputDeclarationEntry},System.UInt32[],System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="328">
<summary>
Create a geometry shader that can write to streaming output buffers.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShaderWithStreamOutput)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">The length in bytes of compiled shader.</param>
<param name="streamOutputDeclarations">A StreamOutputDeclarationEntry collection. ( ranges from 0 to D3D11_SO_STREAM_COUNT * D3D11_SO_OUTPUT_COMPONENT_COUNT ).</param>
<param name="bufferStrides">An array of buffer strides; each stride is the size of an element for that buffer (ranges from 0 to D3D11_SO_BUFFER_SLOT_COUNT).</param>
<param name="rasterizedStream">The index number of the stream to be sent to the rasterizer stage (ranges from 0 to D3D11_SO_STREAM_COUNT). Set to D3D11_SO_NO_RASTERIZED_STREAM if no stream is to be rasterized.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A GeometryShader interface, representing the geometry shader that was created. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShaderWithStreamOutput(System.IntPtr,System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.StreamOutputDeclarationEntry},System.UInt32[],System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="341">
<summary>
Create a geometry shader that can write to streaming output buffers.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShaderWithStreamOutput)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">The length in bytes of compiled shader.</param>
<param name="streamOutputDeclarations">A StreamOutputDeclarationEntry collection. ( ranges from 0 to D3D11_SO_STREAM_COUNT * D3D11_SO_OUTPUT_COMPONENT_COUNT ).</param>
<param name="bufferStrides">An array of buffer strides; each stride is the size of an element for that buffer (ranges from 0 to D3D11_SO_BUFFER_SLOT_COUNT).</param>
<param name="rasterizedStream">The index number of the stream to be sent to the rasterizer stage (ranges from 0 to D3D11_SO_STREAM_COUNT). Set to D3D11_SO_NO_RASTERIZED_STREAM if no stream is to be rasterized.</param>
<returns>A GeometryShader interface, representing the geometry shader that was created. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShader(System.IO.Stream,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="353">
<summary>
Create a geometry shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A GeometryShader object. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="362">
<summary>
Create a geometry shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<returns>A GeometryShader object. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShaderWithStreamOutput(System.IO.Stream,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.StreamOutputDeclarationEntry},System.UInt32[],System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="370">
<summary>
Create a geometry shader that can write to streaming output buffers.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShaderWithStreamOutput)</para>
</summary>
<param name="shaderStream">Stream to load compiled shader bytes from.</param>
<param name="streamOutputDeclarations">A StreamOutputDeclarationEntry collection. ( ranges from 0 to D3D11_SO_STREAM_COUNT * D3D11_SO_OUTPUT_COMPONENT_COUNT ).</param>
<param name="bufferStrides">An array of buffer strides; each stride is the size of an element for that buffer (ranges from 0 to D3D11_SO_BUFFER_SLOT_COUNT).</param>
<param name="rasterizedStream">The index number of the stream to be sent to the rasterizer stage (ranges from 0 to D3D11_SO_STREAM_COUNT). Set to D3D11_SO_NO_RASTERIZED_STREAM if no stream is to be rasterized.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A GeometryShader interface, representing the geometry shader that was created. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateGeometryShaderWithStreamOutput(System.IO.Stream,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.StreamOutputDeclarationEntry},System.UInt32[],System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="382">
<summary>
Create a geometry shader that can write to streaming output buffers.
<para>(Also see DirectX SDK: ID3D11Device::CreateGeometryShaderWithStreamOutput)</para>
</summary>
<param name="shaderStream">Stream to load compiled shader bytes from.</param>
<param name="streamOutputDeclarations">A StreamOutputDeclarationEntry collection. ( ranges from 0 to D3D11_SO_STREAM_COUNT * D3D11_SO_OUTPUT_COMPONENT_COUNT ).</param>
<param name="bufferStrides">An array of buffer strides; each stride is the size of an element for that buffer (ranges from 0 to D3D11_SO_BUFFER_SLOT_COUNT).</param>
<param name="rasterizedStream">The index number of the stream to be sent to the rasterizer stage (ranges from 0 to D3D11_SO_STREAM_COUNT). Set to D3D11_SO_NO_RASTERIZED_STREAM if no stream is to be rasterized.</param>
<returns>A GeometryShader interface, representing the geometry shader that was created. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateHullShader(System.IntPtr,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="393">
<summary>
Create a hull shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateHullShader)</para>
</summary>
<param name="shaderBytecode">A compiled shader.</param>        
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A hull-shader object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateHullShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="403">
<summary>
Create a hull shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateHullShader)</para>
</summary>
<param name="shaderBytecode">A compiled shader.</param>        
<param name="shaderBytecodeLength">Size of compiled shader code in bytes.</param>
<returns>A hull-shader object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateHullShader(System.IO.Stream,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="413">
<summary>
Create a hull shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateHullShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<param name="classLinkage">A class linkage object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A hull-shader object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateHullShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="422">
<summary>
Create a hull shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateHullShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<returns>A hull-shader object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateInputLayout(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputElementDescription},System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="430">
<summary>
Create an input-layout object to describe the input-buffer data for the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11Device::CreateInputLayout)</para>
</summary>
<param name="inputElementDescriptions">A collection of the input-assembler stage input data types; each type is described by an element description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputElementDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputElementDescription"/>.</param>
<param name="shaderBytecodeWithInputSignature">The compiled shader.  The compiled shader code contains a input signature which is validated against the array of elements.</param>        
<param name="shaderBytecodeWithInputSignatureSize">Size of compiled shader code in bytes.</param>
<returns>The input-layout object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateInputLayout(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputElementDescription},System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="441">
<summary>
Create an input-layout object to describe the input-buffer data for the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11Device::CreateInputLayout)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<param name="inputElementDescriptions">A collection of the input-assembler stage input data types; each type is described by an element description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputElementDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputElementDescription"/>.</param>
<returns>The input-layout object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreatePixelShader(System.IntPtr,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="450">
<summary>
Create a pixel shader.
<para>(Also see DirectX SDK: ID3D11Device::CreatePixelShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">The compiled shader.  The compiled shader code contains a input signature which is validated against the array of elements.</param>        
<param name="classLinkage">(optional) A class linkage interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A PixelShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreatePixelShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="460">
<summary>
Create a pixel shader.
<para>(Also see DirectX SDK: ID3D11Device::CreatePixelShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">The compiled shader.  The compiled shader code contains a input signature which is validated against the array of elements.</param>        
<returns>A PixelShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreatePixelShader(System.IO.Stream,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="470">
<summary>
Create a pixel shader.
<para>(Also see DirectX SDK: ID3D11Device::CreatePixelShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<param name="classLinkage">(optional) A class linkage interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A PixelShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreatePixelShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="479">
<summary>
Create a pixel shader.
<para>(Also see DirectX SDK: ID3D11Device::CreatePixelShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<returns>A PixelShader object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreatePredicate(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.QueryDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="487">
<summary>
Creates a predicate.
<para>(Also see DirectX SDK: ID3D11Device::CreatePredicate)</para>
</summary>
<param name="predicateDescription">A query description where the type of query must be a StreamOutputOverflowPredicate or OcclusionPredicate (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.QueryDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.QueryDescription"/>.</param>
<returns>A predicate object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateQuery(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.QueryDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="495">
<summary>
This class encapsulates methods for querying information from the GPU.
<para>(Also see DirectX SDK: ID3D11Device::CreateQuery)</para>
</summary>
<param name="description">A query description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.QueryDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.QueryDescription"/>.</param>
<returns>The query object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DQuery"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DQuery"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateRasterizerState(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="503">
<summary>
Create a rasterizer state object that tells the rasterizer stage how to behave.
<para>(Also see DirectX SDK: ID3D11Device::CreateRasterizerState)</para>
</summary>
<param name="description">A rasterizer state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerDescription"/>.</param>
<returns>A rasterizer state object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateRenderTargetView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetViewDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="511">
<summary>
Create a render-target view for accessing resource data.
<para>(Also see DirectX SDK: ID3D11Device::CreateRenderTargetView)</para>
</summary>
<param name="resource">A D3DResource which represents a render target. This resource must have been created with the RenderTarget flag.</param>
<param name="description">A RenderTargetViewDescription which represents a render-target-view description. Set this parameter to NULL to create a view that accesses all of the subresources in mipmap level 0.</param>
<returns>A RenderTargetView. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateRenderTargetView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="520">
<summary>
Create a render-target view for accessing resource data in mipmap level 0.
<para>(Also see DirectX SDK: ID3D11Device::CreateRenderTargetView)</para>
</summary>
<param name="resource">A D3DResource which represents a render target. This resource must have been created with the RenderTarget flag.</param>
<returns>A RenderTargetView. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateSamplerState(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="528">
<summary>
Create a sampler-state object that encapsulates sampling information for a texture.
<para>(Also see DirectX SDK: ID3D11Device::CreateSamplerState)</para>
</summary>
<param name="description">A sampler state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerDescription"/>.</param>
<returns>The sampler state object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateShaderResourceView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceViewDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="536">
<summary>
Create a shader-resource view for accessing data in a resource.
<para>(Also see DirectX SDK: ID3D11Device::CreateShaderResourceView)</para>
</summary>
<param name="resource">The resource that will serve as input to a shader. This resource must have been created with the ShaderResource flag.</param>
<param name="description">A shader-resource-view description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceViewDescription"/>. Set this parameter to NULL to create a view that accesses the entire resource (using the format the resource was created with).</param>
<returns>A ShaderResourceView. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateShaderResourceView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="545">
<summary>
Create a shader-resource view for accessing data in a resource. It accesses the entire resource (using the format the resource was created with).
<para>(Also see DirectX SDK: ID3D11Device::CreateShaderResourceView)</para>
</summary>
<param name="resource">The resource that will serve as input to a shader. This resource must have been created with the ShaderResource flag.</param>
<returns>A ShaderResourceView. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateTexture1D(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="553">
<summary>
Create an array of 1D textures.
<para>(Also see DirectX SDK: ID3D11Device::CreateTexture1D)</para>
</summary>
<param name="description">A 1D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<param name="initialData">Subresource descriptions (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>; one for each subresource. Applications may not specify NULL for initialData when creating IMMUTABLE resources (see Usage). If the resource is multisampled, pInitialData must be NULL because multisampled resources cannot be initialized with data when they are created.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateTexture2D(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2DDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="562">
<summary>
Create an array of 2D textures.
<para>(Also see DirectX SDK: ID3D11Device::CreateTexture2D)</para>
</summary>
<param name="description">A 2D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<param name="initialData">Subresource descriptions (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>; one for each subresource. Applications may not specify NULL for pInitialData when creating IMMUTABLE resources (see Usage). If the resource is multisampled, pInitialData must be NULL because multisampled resources cannot be initialized with data when they are created.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateTexture3D(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3DDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="571">
<summary>
Create a single 3D texture.
<para>(Also see DirectX SDK: ID3D11Device::CreateTexture3D)</para>
</summary>
<param name="description">A 3D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<param name="initialData">Subresource descriptions (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SubresourceData"/>; one for each subresource. Applications may not specify NULL for pInitialData when creating IMMUTABLE resources (see Usage). If the resource is multisampled, pInitialData must be NULL because multisampled resources cannot be initialized with data when they are created.</param>
<returns>Address of the created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateTexture1D(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="580">
<summary>
Create an array of 1D textures.
<para>(Also see DirectX SDK: ID3D11Device::CreateTexture1D)</para>
</summary>
<param name="description">A 1D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<returns>Address of the created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateTexture2D(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2DDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="588">
<summary>
Create an array of 2D textures.
<para>(Also see DirectX SDK: ID3D11Device::CreateTexture2D)</para>
</summary>
<param name="description">A 2D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateTexture3D(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3DDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="596">
<summary>
Create a single 3D texture.
<para>(Also see DirectX SDK: ID3D11Device::CreateTexture3D)</para>
</summary>
<param name="description">A 3D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<returns>Address of the created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateUnorderedAccessView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessViewDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="604">
<summary>
Create a view for accessing an unordered access resource.
<para>(Also see DirectX SDK: ID3D11Device::CreateUnorderedAccessView)</para>
</summary>
<param name="resource">An D3DResource that represents a resources that will be serve as an input to a shader.</param>
<param name="description">An UnorderedAccessViewDescription that represents a shader-resource-view description.</param>
<returns>An UnorderedAccessView object that represents an unordered access view. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateUnorderedAccessView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="613">
<summary>
Create a view for accessing an unordered access resource.
The view created accesses the entire resource (using the format the resource was created with).
<para>(Also see DirectX SDK: ID3D11Device::CreateUnorderedAccessView)</para>
</summary>
<param name="resource">An D3DResource that represents a resources that will be serve as an input to a shader.</param>
<returns>An UnorderedAccessView object that represents an unordered access view. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateVertexShader(System.IntPtr,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="622">
<summary>
Create a vertex-shader object from a compiled shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateVertexShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">The compiled shader length in bytes.  The compiled shader code contains a input signature which is validated against the array of elements.</param>        
<param name="classLinkage">  A class linkage interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A VertexShader object. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateVertexShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="632">
<summary>
Create a vertex-shader object from a compiled shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateVertexShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader length in bytes.</param>
<param name="shaderBytecodeLength">The compiled shader.  The compiled shader code contains a input signature which is validated against the array of elements.</param>        
<returns>A VertexShader object. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateVertexShader(System.IO.Stream,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="641">
<summary>
Create a vertex-shader object from a compiled shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateVertexShader)</para>
</summary>
<param name="stream">Stream to load compiled shader bytes from.</param>
<param name="classLinkage">  A class linkage interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage"/>; the value can be null.</param>
<returns>A VertexShader object. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateVertexShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="650">
<summary>
Create a vertex-shader object from a compiled shader.
<para>(Also see DirectX SDK: ID3D11Device::CreateVertexShader)</para>
</summary>
<returns>A VertexShader object. </returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreationFlags" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="657">
<summary>
Get the flags used during the call to create the device.
<para>(Also see DirectX SDK: ID3D11Device::GetCreationFlags)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.DeviceRemovedReason" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="666">
<summary>
Get the reason why the device was removed.
<para>(Also see DirectX SDK: ID3D11Device::GetDeviceRemovedReason)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.ExceptionMode" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="675">
<summary>
Gets or Sets the exception-mode flags.
<para>(Also see DirectX SDK: ID3D11Device::GetExceptionMode, ID3D11Device::SetExceptionMode)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.GetImmediateContext" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="685">
<summary>
Gets an immediate context which can record command lists.
<para>(Also see DirectX SDK: ID3D11Device::GetImmediateContext)</para>
</summary>
<returns>A DeviceContext object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.GetInfoQueue" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="692">
<summary>
Gets an information queue object that can retrieve, store and filter debug messages.
</summary>
<returns>An InfoQueue (information queue) object.</returns>
<remarks>
Can only be obtained if the device was created using <see cref="F:&lt;unknown type&gt;.Debug"/> flag. Otherwise, throws an exception.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.GetSwitchToRef" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="701">
<summary>
Gets a switch-to-reference object that enables an application to switch between a hardware and software device.
</summary>
<returns>A SwitchToRef (switch-to-reference) object.</returns>
<remarks>
Can only be obtained if the device was created using CreateDeviceFlag.SwitchToRef flag. Otherwise, throws an exception.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.OpenSharedResource``1(System.IntPtr)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="710">
<summary>
Give a device access to a shared resource created on a different device.
<para>(Also see DirectX SDK: ID3D11Device::OpenSharedResource)</para>
</summary>
<param name="resource">The resource handle.</param>
<typeparam name="T">The type of this shared resource. Must be <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIObject"/></typeparam>
<returns>The requested resource using the given type.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.DeviceFeatureLevel" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="720">
<summary>
Gets the feature level of the hardware device.
<para>(Also see DirectX SDK: ID3D10Device1::GetFeatureLevel)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.GetDXGIDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="729">
<summary>
Queries this device as a DXGI Device object.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDeviceAndSwapChain(System.IntPtr,Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="735">
<summary>
Create a Direct3D 11.0 device and a swap chain using the default hardware adapter 
and the most common settings.
<para>(Also see DirectX SDK: D3D11CreateDeviceAndSwapChain() function)</para>
</summary>
<param name="windowHandle">The window handle to the output window.</param>
<param name="swapChain">The created SwapChain object.</param>
<returns>The created Direct3D 11.0 Device</returns>
<remarks>
If DirectX SDK environment variable is found, and the build is a Debug one,
this method will attempt to use <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CreateDeviceFlag"/>.Debug flag. This should allow 
using the InfoQueue object.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDeviceAndSwapChain(Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter,&lt;unknown type&gt;,System.String,&lt;unknown type&gt;,Microsoft.WindowsAPICodePack.DirectX.Direct3D.FeatureLevel[],Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription,Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="750">
<summary>
Create a Direct3D 11.0 device and a swap chain.
<para>(Also see DirectX SDK: D3D11CreateDeviceAndSwapChain() function)</para>
</summary>
<param name="adapter">An Adapter object. Can be null.</param>
<param name="driverType">The type of driver for the device.</param>
<param name="softwareRasterizerLibrary">A path to the DLL that implements a software rasterizer. Must be NULL if driverType is non-software.</param>
<param name="flags">Device creation flags that enable API layers. These flags can be bitwise OR'd together.</param>
<param name="featureLevels">An array of FeatureLevels, which determine the order of feature levels to attempt to create. 
If set to null, the following array of feature levels will be used: 
<code>
   {
       FeatureLevel_11_0,
       FeatureLevel_10_1,
       FeatureLevel_10_0,
       FeatureLevel_9_3,
       FeatureLevel_9_2,
       FeatureLevel_9_1,
   };
</code>
</param>
<param name="swapChainDescription">Description of the swap chain.</param>
<param name="swapChain">The created SwapChain object.</param>
<returns>The created Direct3D 11.0 Device</returns>
<remarks>By default, all Direct3D 11 calls are handled in a thread-safe way. 
By creating a single-threaded device (using <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CreateDeviceFlag"/>.SingleThreaded), 
you disable thread-safe calling.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="787">
<summary>
Create a Direct3D 11.0 device using the default hardware adapter and the most common settings.
<para>(Also see DirectX SDK: D3D11CreateDevice() function)</para>
</summary>
<returns>The created Direct3D 11.0 Device</returns>
<remarks>
If DirectX SDK environment variable is found, and the build is a Debug one,
this method will attempt to use <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CreateDeviceFlag"/>.Debug flag. This should allow 
using the InfoQueue object.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice.CreateDevice(Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter,&lt;unknown type&gt;,System.String,&lt;unknown type&gt;,Microsoft.WindowsAPICodePack.DirectX.Direct3D.FeatureLevel[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11device.h" line="799">
<summary>
Create a Direct3D 11.0 device. 
<para>(Also see DirectX SDK: D3D11CreateDevice() function)</para>
</summary>
<param name="adapter">An Adapter object. Can be null.</param>
<param name="driverType">The type of driver for the device.</param>
<param name="softwareRasterizerLibrary">A path for a DLL that implements a software rasterizer. Must be NULL if driverType is non-software.</param>
<param name="flags">Device creation flags that enable API layers. These flags can be bitwise OR'd together.</param>
<param name="featureLevels">An array of FeatureLevels, which determine the order of feature levels to attempt to create. 
If set to null, the following array of feature levels will be used: 
<code>
   {
       FeatureLevel_11_0,
       FeatureLevel_10_1,
       FeatureLevel_10_0,
       FeatureLevel_9_3,
       FeatureLevel_9_2,
       FeatureLevel_9_1,
   };
</code>
</param>
<returns>The created Direct3D 11.0 Device</returns>
<remarks>By default, all Direct3D 11 calls are handled in a thread-safe way. 
By creating a single-threaded device (using <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CreateDeviceFlag"/>.SingleThreaded), 
you disable thread-safe calling.
</remarks>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.EffectPool" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effectpool.h" line="13">
<summary>
A pool interface represents a common memory space (or pool) for sharing variables between effects.
<para>(Also see DirectX SDK: ID3D10EffectPool)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.EffectPool.AsEffect" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effectpool.h" line="21">
<summary>
Get the effect that created the effect pool.
<para>(Also see DirectX SDK: ID3D10EffectPool::AsEffect)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="50">
<summary>
The device interface represents a virtual adapter for Direct3D 10.0; it is used to perform rendering and create Direct3D resources.
<para>To create a D3DDevice instance, use one of the static factory method overloads: CreateDevice() or CreateDeviceAndSwapChain().</para>
<para>(Also see DirectX SDK: ID3D10Device)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CheckCounter(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription,&lt;unknown type&gt;@,System.UInt32@,System.String@,System.String@,System.String@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="59">
<summary>
Get the type, name, units of measure, and a description of an existing counter.
<para>(Also see DirectX SDK: ID3D10Device::CheckCounter)</para>
</summary>
<param name="counterDescription">A counter description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription"/>. Specifies which counter information is to be retrieved about.</param>
<param name="type">The data type of a counter (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterType"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterType"/>. Specifies the data type of the counter being retrieved.</param>
<param name="numActiveCounters">The number of hardware counters that are needed for this counter type to be created. All instances of the same counter type use the same hardware counters.</param>
<param name="name">String to be filled with a brief name for the counter. May be NULL if the application is not interested in the name of the counter.</param>
<param name="units">Name of the units a counter measures, provided the memory the pointer points to has enough room to hold the string. Can be NULL. The returned string will always be in English.</param>
<param name="descriptionString">A description of the counter, provided the memory the pointer points to has enough room to hold the string. Can be NULL. The returned string will always be in English.</param>
<returns>True if successful; false otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CheckCounterInformation" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="72">
<summary>
Get a counter's information.
<para>(Also see DirectX SDK: ID3D10Device::CheckCounterInfo)</para>
</summary>
<returns>The counter information.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GetFormatSupport(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="79">
<summary>
Get the support of a given format on the installed video device.
<para>(Also see DirectX SDK: ID3D10Device::CheckFormatSupport)</para>
</summary>
<param name="format">A Format enumeration that describes a format for which to check for support.</param>
<returns>A FormatSupport enumeration values describing how the specified format is supported on the installed device. The values are ORed together.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CheckFormatSupport(&lt;unknown type&gt;,&lt;unknown type&gt;@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="87">
<summary>
Get the support of a given format on the installed video device.
<para>(Also see DirectX SDK: ID3D10Device::CheckFormatSupport)</para>
</summary>
<param name="format">A Format enumeration that describes a format for which to check for support.</param>
<param name="formatSupport">The type of support for the given format. 
This value is undefined if the retun value is false.</param>
<returns>True if successful; false otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GetMultisampleQualityLevels(&lt;unknown type&gt;,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="98">
<summary>
Get the number of quality levels available during multisampling.
<para>(Also see DirectX SDK: ID3D10Device::CheckMultisampleQualityLevels)</para>
</summary>
<param name="format">The texture format. See Format.</param>
<param name="sampleCount">The number of samples during multisampling.</param>
<returns>Number of quality levels supported by the adapter.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CheckMultisampleQualityLevels(&lt;unknown type&gt;,System.UInt32,System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="107">
<summary>
Get the number of quality levels available during multisampling.
<para>(Also see DirectX SDK: ID3D10Device::CheckMultisampleQualityLevels)</para>
</summary>
<param name="format">The texture format. See Format.</param>
<param name="sampleCount">The number of samples during multisampling.</param>
<param name="multisampleQualityLevels">An our parameter containing the number of quality levels supported by the adapter.
This value is undefined if the retun value is false.</param>
<returns>True if successful; false otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.ClearDepthStencilView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilView,&lt;unknown type&gt;,System.Single,System.Byte)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="118">
<summary>
Clears the depth-stencil resource.
<para>(Also see DirectX SDK: ID3D10Device::ClearDepthStencilView)</para>
</summary>
<param name="depthStencilView">Pointer to the depth stencil to be cleared.</param>
<param name="flags">Which parts of the buffer to clear. See ClearFlag.</param>
<param name="depth">Clear the depth buffer with this value. This value will be clamped between 0 and 1.</param>
<param name="stencil">Clear the stencil buffer with this value.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.ClearRenderTargetView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView,Microsoft.WindowsAPICodePack.DirectX.DXGI.ColorRgba)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="128">
<summary>
Set all the elements in a render target to one value.
<para>(Also see DirectX SDK: ID3D10DeviceContext::ClearRenderTargetView)</para>
</summary>
<param name="renderTargetView">Pointer to the rendertarget.</param>
<param name="colorRgba">The color to fill the render target with.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.ClearState" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="136">
<summary>
Restore all default device settings; return the device to the state it was in when it was created. This will set all set all input/output resource slots, shaders, input layouts, predications, scissor rectangles, depth-stencil state, rasterizer state, blend state, sampler state, and viewports to NULL. The primitive topology will be set to UNDEFINED.
<para>(Also see DirectX SDK: ID3D10Device::ClearState)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CopyResource(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="142">
<summary>
Copy the entire contents of the source resource to the destination resource using the GPU.
<para>(Also see DirectX SDK: ID3D10Device::CopyResource)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>.</param>
<param name="sourceResource">The source resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CopySubresourceRegion(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32,System.UInt32,System.UInt32,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Box)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="150">
<summary>
Copy a region from a source resource to a destination resource.
<para>(Also see DirectX SDK: ID3D10Device::CopySubresourceRegion)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>.</param>
<param name="destinationSubresource">Destination subresource index.</param>
<param name="destinationX">The x coordinate of the upper left corner of the destination region.</param>
<param name="destinationY">The y coordinate of the upper left corner of the destination region.</param>
<param name="destinationZ">The z coordinate of the upper left corner of the destination region. For a 1D or 2D subresource, this must be zero.</param>
<param name="sourceResource">The source resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>.</param>
<param name="sourceSubresourceIndex">Source subresource index.</param>
<param name="sourceBox">A 3D box (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Box"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Box"/> that defines the source subresources that can be copied. The box must fit within the source resource.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CopySubresourceRegion(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32,System.UInt32,System.UInt32,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="164">
<summary>
Copy a region from a source resource to a destination resource.
The entire source subresource is copied.
<para>(Also see DirectX SDK: ID3D10Device::CopySubresourceRegion)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>.</param>
<param name="destinationSubresource">Destination subresource index.</param>
<param name="destinationX">The x coordinate of the upper left corner of the destination region.</param>
<param name="destinationY">The y coordinate of the upper left corner of the destination region.</param>
<param name="destinationZ">The z coordinate of the upper left corner of the destination region. For a 1D or 2D subresource, this must be zero.</param>
<param name="sourceResource">The source resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource"/>.</param>
<param name="sourceSubresourceIndex">Source subresource index.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateBlendState(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="178">
<summary>
Create a blend-state object that encapsules blend state for the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::CreateBlendState)</para>
</summary>
<param name="blendStateDescription">A blend-state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendDescription"/>.</param>
<returns>The blend-state object created (see BlendState Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateCounter(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="186">
<summary>
Create a counter object for measuring GPU performance.
<para>(Also see DirectX SDK: ID3D10Device::CreateCounter)</para>
</summary>
<param name="description">A counter description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription"/>.</param>
<returns>A counter (see D3DCounter).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateBuffer(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="194">
<summary>
Create a buffer (vertex buffer, index buffer, or shader-constant buffer).
<para>(Also see DirectX SDK: ID3D10Device::CreateBuffer)</para>
</summary>
<param name="description">A buffer description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription"/>.</param>
<param name="initialData">Pointer to the initialization data (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>; use NULL to allocate space only.</param>
<returns>The buffer created (see D3DBuffer Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateBuffer(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="203">
<summary>
Create a buffer (vertex buffer, index buffer, or shader-constant buffer) with no initial data.
<para>(Also see DirectX SDK: ID3D10Device::CreateBuffer)</para>
</summary>
<param name="description">A buffer description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription"/>.</param>
<returns>The buffer created (see D3DBuffer Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateDepthStencilState(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="211">
<summary>
Create a depth-stencil state object that encapsulates depth-stencil test information for the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::CreateDepthStencilState)</para>
</summary>
<param name="description">A depth-stencil state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilDescription"/>.</param>
<returns>The depth-stencil state object created (see DepthStencilState Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateDepthStencilView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilViewDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="219">
<summary>
Create a depth-stencil view for accessing resource data.
<para>(Also see DirectX SDK: ID3D10Device::CreateDepthStencilView)</para>
</summary>
<param name="resource">The resource that will serve as the depth-stencil surface. This resource must have been created with the DepthStencil flag.</param>
<param name="description">A depth-stencil-view description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilViewDescription"/>. </param>
<returns>A DepthStencilView.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateDepthStencilView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="228">
<summary>
Create a depth-stencil view for accessing resource data.
This method creates a view that accesses mipmap level 0 of the entire resource (using the format the resource was created with).
<para>(Also see DirectX SDK: ID3D10Device::CreateDepthStencilView)</para>
</summary>
<param name="resource">The resource that will serve as the depth-stencil surface. This resource must have been created with the DepthStencil flag.</param>
<returns>A DepthStencilView.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateGeometryShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="236">
<summary>
Create a geometry shader.
<para>(Also see DirectX SDK: ID3D10Device::CreateGeometryShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader. To get this object see Getting a A Compiled Shader in DX SDK.</param>
<param name="shaderBytecodeLength">Size of the compiled geometry shader.</param>
<returns>A GeometryShader Object.  If this is NULL, all other parameters will be validated, and if all parameters pass validation this API will return S_FALSE instead of S_OK.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateGeometryShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="245">
<summary>
Create a geometry shader.
<para>(Also see DirectX SDK: ID3D10Device::CreateGeometryShader)</para>
</summary>
<param name="shaderByteCodeStream">The stream to read the compiled shader shader data from. To get this object see Getting a A Compiled Shader in DX SDK.</param>
<returns>A GeometryShader Object.  If this is NULL, all other parameters will be validated, and if all parameters pass validation this API will return S_FALSE instead of S_OK.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateGeometryShaderWithStreamOutput(System.IntPtr,System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.StreamOutputDeclarationEntry},System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="253">
<summary>
Create a geometry shader that can write to streaming output buffers.
<para>(Also see DirectX SDK: ID3D10Device::CreateGeometryShaderWithStreamOutput)</para>
</summary>
<param name="shaderBytecode">The compiled shader.</param>
<param name="shaderBytecodeLength">The length in bytes of compiled shader.</param>
<param name="streamOutputDeclarations">A StreamOutputDeclarationEntry collection. ( ranges from 0 to D3D11_SO_STREAM_COUNT * D3D11_SO_OUTPUT_COMPONENT_COUNT ).</param>
<param name="outputStreamStride">The size of pStreamOutputDecl. This parameter is only used when the output slot is 0 for all entries in streamOutputDeclarations.</param>
<returns>A GeometryShader interface, representing the geometry shader that was created. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateGeometryShaderWithStreamOutput(System.IO.Stream,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.StreamOutputDeclarationEntry},System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="264">
<summary>
Create a geometry shader that can write to streaming output buffers.
<para>(Also see DirectX SDK: ID3D10Device::CreateGeometryShaderWithStreamOutput)</para>
</summary>
<param name="shaderByteCodeStream">The stream to read the compiled shader shader data from. To get this object see Getting a A Compiled Shader in DX SDK.</param>
<param name="streamOutputDeclarations">A StreamOutputDeclarationEntry collection. ( ranges from 0 to D3D11_SO_STREAM_COUNT * D3D11_SO_OUTPUT_COMPONENT_COUNT ).</param>
<param name="outputStreamStride">The size of pStreamOutputDecl. This parameter is only used when the output slot is 0 for all entries in streamOutputDeclarations.</param>
<returns>A GeometryShader interface, representing the geometry shader that was created. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateInputLayout(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputElementDescription},System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="274">
<summary>
Create an input-layout object to describe the input-buffer data for the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::CreateInputLayout)</para>
</summary>
<param name="inputElementDescriptions">A collection of the input-assembler stage input data types; each type is described by an element description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputElementDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputElementDescription"/>.</param>
<param name="shaderBytecodeWithInputSignature">The compiled shader.  The compiled shader code contains a input signature which is validated against the array of elements.</param>        
<param name="shaderBytecodeWithInputSignatureSize">The length in bytes of compiled shader.</param>        
<returns>The input-layout object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>. To validate the other input parameters.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateInputLayout(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputElementDescription},System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="284">
<summary>
Create an input-layout object to describe the input-buffer data for the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::CreateInputLayout)</para>
</summary>
<param name="inputElementDescriptions">A collection of the input-assembler stage input data types; each type is described by an element description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputElementDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputElementDescription"/>.</param>
<param name="shaderBytecodeWithInputSignatureStream">The compiled shader stream.  The compiled shader code contains a input signature which is validated against the array of elements.</param>        
<returns>The input-layout object created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>. To validate the other input parameters.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreatePredicate(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="293">
<summary>
Creates a predicate.
<para>(Also see DirectX SDK: ID3D10Device::CreatePredicate)</para>
</summary>
<param name="predicateDescription">A query description where the type of query must be a StreamOutputOverflowPredicate or OcclusionPredicate (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription"/>.</param>
<returns>A predicate (see D3DPredicate Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateQuery(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="301">
<summary>
This class encapsulates methods for querying information from the GPU.
<para>(Also see DirectX SDK: ID3D10Device::CreateQuery)</para>
</summary>
<param name="description">A query description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription"/>.</param>
<returns>The query object created (see D3DQuery Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreatePixelShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="309">
<summary>
Create a pixel shader.
<para>(Also see DirectX SDK: ID3D10Device::CreatePixelShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader. To get this object see Getting a A Compiled Shader.</param>
<param name="shaderBytecodeLength">Size of the compiled pixel shader.</param>
<returns>A PixelShader Object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreatePixelShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="318">
<summary>
Create a pixel shader.
<para>(Also see DirectX SDK: ID3D10Device::CreatePixelShader)</para>
</summary>
<param name="shaderBytecodeStream">The compiled shader stream. To get this object see Getting a A Compiled Shader.</param>
<returns>A PixelShader Object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateRasterizerState(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="326">
<summary>
Create a rasterizer state object that tells the rasterizer stage how to behave.
<para>(Also see DirectX SDK: ID3D10Device::CreateRasterizerState)</para>
</summary>
<param name="description">A rasterizer state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerDescription"/>.</param>
<returns>The rasterizer state object created (see RasterizerState Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateRenderTargetView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetViewDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="334">
<summary>
Create a render-target view for accessing resource data.
<para>(Also see DirectX SDK: ID3D10Device::CreateRenderTargetView)</para>
</summary>
<param name="resource">Pointer to the resource that will serve as the render target. This resource must have been created with the RenderTarget flag.</param>
<param name="description">A render-target-view description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetViewDescription"/>. Set this parameter to NULL to create a view that accesses mipmap level 0 of the entire resource (using the format the resource was created with).</param>
<returns>A RenderTargetView.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateRenderTargetView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="343">
<summary>
Create a render-target view for accessing resource data in mipmap level 0 of the entire resource (using the format the resource was created with).
<para>(Also see DirectX SDK: ID3D10Device::CreateRenderTargetView)</para>
</summary>
<param name="resource">Pointer to the resource that will serve as the render target. This resource must have been created with the RenderTarget flag.</param>
<returns>A RenderTargetView.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateSamplerState(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="351">
<summary>
Create a sampler-state object that encapsulates sampling information for a texture.
<para>(Also see DirectX SDK: ID3D10Device::CreateSamplerState)</para>
</summary>
<param name="description">A sampler state description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerDescription"/>.</param>
<returns>The sampler state object created (see SamplerState Object).</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateShaderResourceView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceViewDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="359">
<summary>
Create a shader-resource view for accessing data in a resource.
<para>(Also see DirectX SDK: ID3D10Device::CreateShaderResourceView)</para>
</summary>
<param name="resource">Pointer to the resource that will serve as input to a shader. This resource must have been created with the ShaderResource flag.</param>
<param name="description">A shader-resource-view description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceViewDescription"/>. Set this parameter to NULL to create a view that accesses the entire resource (using the format the resource was created with).</param>
<returns>A ShaderResourceView.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateShaderResourceView(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="368">
<summary>
Create a shader-resource view for accessing data in a resource. It accesses the entire resource (using the format the resource was created with).
<para>(Also see DirectX SDK: ID3D10Device::CreateShaderResourceView)</para>
</summary>
<param name="resource">Pointer to the resource that will serve as input to a shader. This resource must have been created with the ShaderResource flag.</param>
<returns>A ShaderResourceView.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateTexture1D(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="376">
<summary>
Create an array of 1D textures.
<para>(Also see DirectX SDK: ID3D10Device::CreateTexture1D)</para>
</summary>
<param name="description">A 1D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<param name="initialData">Subresource descriptions (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>; one for each subresource. Applications may not specify NULL for initialData when creating IMMUTABLE resources (see Usage). If the resource is multisampled, pInitialData must be NULL because multisampled resources cannot be initialized with data when they are created.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateTexture2D(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="385">
<summary>
Create an array of 2D textures.
<para>(Also see DirectX SDK: ID3D10Device::CreateTexture2D)</para>
</summary>
<param name="description">A 2D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<param name="initialData">Subresource descriptions (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>; one for each subresource. Applications may not specify NULL for pInitialData when creating IMMUTABLE resources (see Usage). If the resource is multisampled, pInitialData must be NULL because multisampled resources cannot be initialized with data when they are created.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateTexture3D(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="394">
<summary>
Create a single 3D texture.
<para>(Also see DirectX SDK: ID3D10Device::CreateTexture3D)</para>
</summary>
<param name="description">A 3D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<param name="initialData">Subresource descriptions (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SubresourceData"/>; one for each subresource. Applications may not specify NULL for pInitialData when creating IMMUTABLE resources (see Usage). If the resource is multisampled, pInitialData must be NULL because multisampled resources cannot be initialized with data when they are created.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateTexture1D(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="403">
<summary>
Create an array of 1D textures.
<para>(Also see DirectX SDK: ID3D10Device::CreateTexture1D)</para>
</summary>
<param name="description">A 1D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateTexture2D(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="411">
<summary>
Create an array of 2D textures.
<para>(Also see DirectX SDK: ID3D10Device::CreateTexture2D)</para>
</summary>
<param name="description">A 2D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateTexture3D(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="419">
<summary>
Create a single 3D texture.
<para>(Also see DirectX SDK: ID3D10Device::CreateTexture3D)</para>
</summary>
<param name="description">A 3D texture description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription"/>. To create a typeless resource that can be interpreted at runtime into different, compatible formats, specify a typeless format in the texture description. To generate mipmap levels automatically, set the number of mipmap levels to 0.</param>
<returns>The created texture (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D"/>. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateVertexShader(System.IntPtr,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="427">
<summary>
Create a vertex-shader object from a compiled shader.
<para>(Also see DirectX SDK: ID3D10Device::CreateVertexShader)</para>
</summary>
<param name="shaderBytecode">The compiled shader. To get this object see Getting a A Compiled Shader.</param>
<param name="shaderBytecodeLength">Size of the compiled vertex shader.</param>
<returns>A VertexShader Object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateVertexShader(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="436">
<summary>
Create a vertex-shader object from a compiled shader.
<para>(Also see DirectX SDK: ID3D10Device::CreateVertexShader)</para>
</summary>
<param name="shaderBytecodeStream">The compiled shader stream. To get this object see Getting a A Compiled Shader.</param>
<returns>A VertexShader Object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.Draw(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="444">
<summary>
Draw non-indexed, non-instanced primitives.
<para>(Also see DirectX SDK: ID3D10Device::Draw)</para>
</summary>
<param name="vertexCount">Number of vertices to draw.</param>
<param name="startVertexLocation">Index of the first vertex, which is usually an offset in a vertex buffer; it could also be used as the first vertex id generated for a shader parameter marked with the SV_TargetId system-value semantic.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.DrawAuto" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="452">
<summary>
Draw geometry of an unknown size that was created by the geometry shader stage.
<para>(Also see DirectX SDK: ID3D10Device::DrawAuto)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.DrawIndexed(System.UInt32,System.UInt32,System.Int32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="458">
<summary>
Draw indexed, non-instanced primitives.
<para>(Also see DirectX SDK: ID3D10Device::DrawIndexed)</para>
</summary>
<param name="indexCount">Number of indices to draw.</param>
<param name="startIndexLocation">Index of the first index to use when accesssing the vertex buffer; begin at startIndexLocation to index vertices from the vertex buffer.</param>
<param name="baseVertexLocation">Offset from the start of the vertex buffer to the first vertex.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.DrawIndexedInstanced(System.UInt32,System.UInt32,System.UInt32,System.Int32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="467">
<summary>
Draw indexed, instanced primitives.
<para>(Also see DirectX SDK: ID3D10Device::DrawIndexedInstanced)</para>
</summary>
<param name="indexCountPerInstance">Size of the index buffer used in each instance.</param>
<param name="instanceCount">Number of instances to draw.</param>
<param name="startIndexLocation">Index of the first index.</param>
<param name="baseVertexLocation">Index of the first vertex. The index is signed, which allows a negative index. If the negative index plus the index value from the index buffer are less than 0, the result is undefined.</param>
<param name="startInstanceLocation">Index of the first instance.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.DrawInstanced(System.UInt32,System.UInt32,System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="478">
<summary>
Draw non-indexed, instanced primitives.
<para>(Also see DirectX SDK: ID3D10Device::DrawInstanced)</para>
</summary>
<param name="vertexCountPerInstance">Number of vertices to draw.</param>
<param name="instanceCount">Number of instances to draw.</param>
<param name="startVertexLocation">Index of the first vertex.</param>
<param name="startInstanceLocation">Index of the first instance.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.Flush" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="488">
<summary>
Send queued-up commands in the command buffer to the GPU.
<para>(Also see DirectX SDK: ID3D10Device::Flush)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GenerateMips(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceView)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="494">
<summary>
Generate mipmaps for the given shader resource.
<para>(Also see DirectX SDK: ID3D10Device::GenerateMips)</para>
</summary>
<param name="shaderResourceView">An ShaderResourceView Object. The mipmaps will be generated for this shader resource.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreationFlags" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="501">
<summary>
Get the flags used during the call to create the device.
<para>(Also see DirectX SDK: ID3D10Device::GetCreationFlags)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.DeviceRemovedReason" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="510">
<summary>
Get the reason why the device was removed.
<para>(Also see DirectX SDK: ID3D10Device::GetDeviceRemovedReason)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.ExceptionMode" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="519">
<summary>
Gets or Sets the exception-mode flags.
<para>(Also see DirectX SDK: ID3D10Device::GetExceptionMode, ID3D10Device::SetExceptionMode)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GetPredication(System.Boolean@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="529">
<summary>
Get the rendering predicate state.
<para>(Also see DirectX SDK: ID3D10Device::GetPredication)</para>
</summary>
<param name="outPredicateValue">A boolean to fill with the predicate comparison value. FALSE upon device creation.</param>
<returns>A predicate (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DPredicate"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DPredicate"/>. Value stored here will be null upon device creation.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.OpenSharedResource``1(System.IntPtr)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="537">
<summary>
Give a device access to a shared resource created on a different device.
<para>(Also see DirectX SDK: ID3D10Device::OpenSharedResource)</para>
</summary>
<param name="resource">The resource handle.</param>
<typeparam name="T">The type of this shared resource. Must be <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIObject"/></typeparam>
<returns>The requested resource using the given type.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.ResolveSubresource(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="547">
<summary>
Copy a multisampled resource into a non-multisampled resource. This API is most useful when re-using the resulting rendertarget of one render pass as an input to a second render pass.
<para>(Also see DirectX SDK: ID3D10Device::ResolveSubresource)</para>
</summary>
<param name="destinationResource">Destination resource. Must be a created with the Default flag and be single-sampled. See D3DResource.</param>
<param name="destinationSubresource">A zero-based index, that identifies the destination subresource. See D3D10CalcSubresource for more details.</param>
<param name="sourceResource">Source resource. Must be multisampled.</param>
<param name="sourceSubresource">The source subresource of the source resource.</param>
<param name="format">Format that indicates how the multisampled resource will be resolved to a single-sampled resource.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.SetPredication(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DPredicate,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="558">
<summary>
Set a rendering predicate.
<para>(Also see DirectX SDK: ID3D10Device::SetPredication)</para>
</summary>
<param name="predicate">A predicate (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DPredicate"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DPredicate"/>. A NULL value indicates "no" predication; in this case, the value of PredicateValue is irrelevent but will be preserved for Device.GetPredication.</param>
<param name="predicateValue">If TRUE, rendering will be affected by when the predicate's conditions are met. If FALSE, rendering will be affected when the conditions are not met.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.UpdateSubresource(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32,System.IntPtr,System.UInt32,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Box)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="566">
<summary>
The CPU copies data from memory to a subresource created in non-mappable memory.
<para>(Also see DirectX SDK: ID3D10Device::UpdateSubresource)</para>
</summary>
<param name="destinationResource">The destination resource (see D3DResource Object).</param>
<param name="destinationSubresource">A zero-based index, that identifies the destination subresource. See D3D10CalcSubresource for more details.</param>
<param name="destinationBox">A box that defines the portion of the destination subresource to copy the resource data into. Coordinates are in bytes for buffers and in texels for textures. The dimensions of the source must fit the destination (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Box"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Box"/>.</param>
<param name="sourceData">The source data in memory.</param>
<param name="sourceRowPitch">The size of one row of the source data.</param>
<param name="sourceDepthPitch">The size of one depth slice of source data.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.UpdateSubresource(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource,System.UInt32,System.IntPtr,System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="578">
<summary>
The CPU copies data from memory to a subresource created in non-mappable memory.
Because no destination box is defined, the data is written to the destination subresource with no offset.
<para>(Also see DirectX SDK: ID3D10Device::UpdateSubresource)</para>
</summary>
<param name="destinationResource">The destination resource (see D3DResource Object).</param>
<param name="destinationSubresource">A zero-based index, that identifies the destination subresource. See D3D10CalcSubresource for more details.</param>
<param name="sourceData">The source data in memory.</param>
<param name="sourceRowPitch">The size of one row of the source data.</param>
<param name="sourceDepthPitch">The size of one depth slice of source data.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateEffectFromCompiledBinary(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="590">
<summary>
Loads a precompiled effect from a file.
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory() function)</para>
</summary>
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory)</para>
<param name="path">The path to the file that contains the compiled effect.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateEffectFromCompiledBinary(System.IO.BinaryReader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="598">
<summary>
Loads a precompiled effect from a binary data stream.
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory() function)</para>
</summary>
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory)</para>
<param name="binaryEffect">The binary data stream.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateEffectFromCompiledBinary(System.IO.BinaryReader,System.Int32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.EffectPool)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="606">
<summary>
Loads a precompiled effect from a binary data stream.
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory() function)</para>
</summary>
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory)</para>
<param name="binaryEffect">The binary data stream.</param>
<param name="effectFlags">Effect compile options</param>
<param name="effectPool">A memory space for effect variables shared across effects.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateEffectFromCompiledBinary(System.IO.Stream)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="616">
<summary>
Loads a precompiled effect from a binary data stream.
</summary>
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory)</para>
<param name="inputStream">The input data stream.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateEffectFromCompiledBinary(System.IO.Stream,System.Int32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.EffectPool)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="623">
<summary>
Loads a precompiled effect from a binary data stream.
</summary>
<para>(Also see DirectX SDK: D3D10CreateEffectFromMemory)</para>
<param name="inputStream">The input data stream.</param>
<param name="effectFlags">Effect compile options</param>
<param name="effectPool">A memory space for effect variables shared across effects.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="632">
<summary>
Get the associated geometry shader pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.IA" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="640">
<summary>
Get the associated input assembler pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.OM" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="648">
<summary>
Get the associated output merger pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.PS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="656">
<summary>
Get the associated pixel shader pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.RS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="664">
<summary>
Get the associated rasterizer pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.SO" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="672">
<summary>
Get the associated stream output pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.VS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="680">
<summary>
Get the associated vertex shader pipeline stage object.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GetInfoQueue" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="688">
<summary>
Gets an information queue object that can retrieve, store and filter debug messages.
</summary>
<returns>An InfoQueue (information queue) object.</returns>
<remarks>
Can only be obtained if the device was created using <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CreateDeviceFlag"/>.Debug flag. Otherwise, throws an exception.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GetSwitchToRef" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="697">
<summary>
Gets a switch-to-reference object that enables an application to switch between a hardware and software device.
</summary>
<returns>A SwitchToRef (switch-to-reference) object.</returns>
<remarks>
Can only be obtained if the device was created using <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CreateDeviceFlag"/>.SwitchToRef flag. Otherwise, throws an exception.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.GetDXGIDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="706">
<summary>
Queries the this device object as a DXGI Device object
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateDeviceAndSwapChain(System.IntPtr,Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="711">
<summary>
Create a Direct3D 10.0 device and a swap chain using the default hardware adapter 
and the most common settings.
<para>(Also see DirectX SDK: D3D10CreateDeviceAndSwapChain() function)</para>
</summary>
<param name="windowHandle">The window handle to the output window.</param>
<param name="swapChain">The created SwapChain object.</param>
<returns>The created Direct3D 10.0 Device</returns>
<remarks>
If DirectX SDK environment variable is found, and the build is a Debug one,
this method will attempt to use <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CreateDeviceFlag"/>.Debug flag. This should allow 
using the InfoQueue object.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateDeviceAndSwapChain(Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter,&lt;unknown type&gt;,System.String,&lt;unknown type&gt;,Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription,Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="726">
<summary>
Create a Direct3D 10.0 device and a swap chain.
<para>(Also see DirectX SDK: D3D10CreateDeviceAndSwapChain() function)</para>
</summary>
<param name="adapter">An Adapter object. Can be null.</param>
<param name="driverType">The type of driver for the device.</param>
<param name="softwareRasterizerLibrary">A path the DLL that implements a software rasterizer. Must be NULL if driverType is non-software.</param>
<param name="flags">Device creation flags that enable API layers. These flags can be bitwise OR'd together.</param>
<param name="swapChainDescription">Description of the swap chain.</param>
<param name="swapChain">The created SwapChain object.</param>
<returns>The created Direct3D 10.0 Device</returns>
<remarks>
By default, all Direct3D 10 calls are handled in a thread-safe way. 
By creating a single-threaded device (using <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CreateDeviceFlag"/>.SingleThreaded, 
you disable thread-safe calling.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="750">
<summary>
Create a Direct3D 10.0 device using the default hardware adapter and the most common settings.
<para>(Also see DirectX SDK: D3D10CreateDevice() function)</para>
</summary>
<returns>The created Direct3D 10.0 Device</returns>
<remarks>
If DirectX SDK environment variable is found, and the build is a Debug one,
this method will attempt to use <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CreateDeviceFlag"/>.Debug flag. This should allow 
using the InfoQueue object.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice.CreateDevice(Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter,&lt;unknown type&gt;,System.String,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10device.h" line="762">
<summary>
Create a Direct3D 10.0 device. 
<para>(Also see DirectX SDK: D3D10CreateDevice() function)</para>
</summary>
<param name="adapter">An Adapter object. Can be null.</param>
<param name="driverType">The type of driver for the device.</param>
<param name="softwareRasterizerLibrary">A path the DLL that implements a software rasterizer. Must be NULL if driverType is non-software.</param>
<param name="flags">Device creation flags that enable API layers. These flags can be bitwise OR'd together.</param>
<returns>The created Direct3D 10.0 Device</returns>
<remarks>
By default, all Direct3D 10 calls are handled in a thread-safe way. 
By creating a single-threaded device (using <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CreateDeviceFlag"/>.SingleThreaded, 
you disable thread-safe calling.
</remarks>
</member>
</members>
</doc>