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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="22">
<summary>
Implements methods for generating DXGI objects (which handle fullscreen transitions).
<para>(Also see DirectX SDK: IDXGIFactory)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory.CreateSwapChain(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DDevice,Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="31">
<summary>
Creates a swap chain.
<para>(Also see DirectX SDK: IDXGIFactory::CreateSwapChain)</para>
</summary>
<param name="device">The device that will write 2D images to the swap chain.</param>
<param name="description">The swap-chain description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription"/>. This parameter cannot be NULL.</param>
<return>The swap chain created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain"/>.</return>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory.CreateSwapChain(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DDevice,Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="40">
<summary>
Creates a swap chain.
<para>(Also see DirectX SDK: IDXGIFactory::CreateSwapChain)</para>
</summary>
<param name="device">The device that will write 2D images to the swap chain.</param>
<param name="description">The swap-chain description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChainDescription"/>. This parameter cannot be NULL.</param>
<return>The swap chain created (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain"/>.</return>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory.GetAdapters" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="49">
<summary>
Enumerates the adapters (video cards).
<para>(Also see DirectX SDK: IDXGIFactory::EnumAdapters)</para>
</summary>
<returns>A readonly collection of Adapter objects.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory.GetAdapter(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="56">
<summary>
Get an adapter (video cards).
<para>(Also see DirectX SDK: IDXGIFactory::EnumAdapters)</para>
</summary>
<param name="index">The index of the adapter requested.</param>
<returns>An Adapter object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory.GetWindowAssociation" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="64">
<summary>
Get the window through which the user controls the transition to and from fullscreen.
<para>(Also see DirectX SDK: IDXGIFactory::GetWindowAssociation)</para>
</summary>
<return>A window handle.</return>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory.MakeWindowAssociation(System.IntPtr,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="71">
<summary>
Allows DXGI to monitor an application's message queue for the alt-enter key sequence (which causes the application to switch from windowed to fullscreen or vice versa).
<para>(Also see DirectX SDK: IDXGIFactory::MakeWindowAssociation)</para>
</summary>
<param name="WindowHandle">The handle of the window that is to be monitored. This parameter can be NULL; but only if the flags are also 0.</param>
<param name="Flags">One or more of the MakeWindowAssociation flags.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory.CreateFactory" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory.h" line="79">
<summary>
Creates a DXGI 1.0 factory that generates objects used to enumerate and specify video graphics settings.
<para>(Also see DirectX SDK: CreateDXGIFactory() Function.)</para>
</summary>
<returns>DXGIFactory Object.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory1" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory1.h" line="13">
<summary>
The DXGIFactory1 interface implements methods for generating DXGI objects.
<para>(Also see DirectX SDK: IDXGIFactory1)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory1.GetAdapters1" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory1.h" line="21">
<summary>
Enumerates both local and remote adapters (video cards).
<para>(Also see DirectX SDK: IDXGIFactory1::EnumAdapters1)</para>
</summary>
<returns>A read-only collection of Adapter objects.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory1.GetAdapter1(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory1.h" line="28">
<summary>
Get a local or remote adapter (video cards).
<para>(Also see DirectX SDK: IDXGIFactory1::EnumAdapters1)</para>
</summary>
<param name="index">The index of the adapter requested.</param>
<returns>An Adapter1 object.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory1.IsCurrent" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory1.h" line="36">
<summary>
Informs application the possible need to re-enumerate adapters -- new adapter(s) have become available, current adapter(s) become unavailable.
Called by Direct3D 10.1 Command Remoting applications to handle Remote Desktop Services session transitions.
<para>(Also see DirectX SDK: IDXGIFactory1::IsCurrent)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIFactory1.CreateFactory1" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgifactory1.h" line="46">
<summary>
Creates a DXGI 1.1 factory that generates objects used to enumerate and specify video graphics settings.
<para>(Also see DirectX SDK: CreateDXGIFactory1() Function.)</para>
</summary>
<returns>DXGIFactory1 Object.</returns>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter1" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter1.h" line="10">
<summary>
The Adapter1 interface represents a display sub-system (including one or more GPU's, DACs and video memory).
<para>(Also see DirectX SDK: IDXGIAdapter1)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.Adapter1.Description1" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiadapter1.h" line="18">
<summary>
Gets a DXGI 1.1 description of a local or remote adapter (or video card).
<para>(Also see DirectX SDK: IDXGIAdapter1::GetDesc1)</para>
</summary>
<returns>An AdapterDescription1 object.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.LibraryLoader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\libraryloader.h" line="21">
<summary>
A private class supporting dll and functions loading.
Loaded dlls and functions are cached in a hash table.
</summary>
</member>
</members>
</doc>