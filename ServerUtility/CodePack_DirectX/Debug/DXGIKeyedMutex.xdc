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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.KeyedMutex" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgikeyedmutex.h" line="10">
<summary>
Represents a keyed mutex, which allows exclusive access to a shared resource 
that is used by multiple devices.
<para>(Also see DirectX SDK: IDXGIKeyedMutex)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.KeyedMutex.AcquireSync(System.UInt64,System.UInt32!System.Runtime.CompilerServices.IsLong)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgikeyedmutex.h" line="19">
<summary>
Using a key, acquires exclusive rendering access to a shared resource.
<para>(Also see DirectX SDK: IDXGIKeyedMutex::AcquireSync)</para>
</summary>
<param name="Key">A value that indicates which device to give access to. 
This method will succeed when the device that currently owns the surface calls
the KeyedMutex.ReleaseSync method using the same value. 
This value can be any UINT64 value.</param>
<param name="dwMilliseconds">The time-out interval, in milliseconds. 
This method will return if the interval elapses, and the keyed mutex has not 
been released  using the specified Key.           
If this value is set to zero, the AcquireSync method will test to see if 
the keyed mutex has been released and returns immediately.           
If this value is set to INFINITE, the time-out interval will never elapse.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.KeyedMutex.ReleaseSync(System.UInt64)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgikeyedmutex.h" line="35">
<summary>
Using a key, releases exclusive rendering access to a shared resource
<para>(Also see DirectX SDK: IDXGIKeyedMutex::ReleaseSync)</para>
</summary>
<param name="Key">A value that indicates which device to give access to. This method will succeed when the device that currently owns the surface calls the KeyedMutex.ReleaseSync method using the same value. This value can be any UINT64 value.</param>
</member>
</members>
</doc>