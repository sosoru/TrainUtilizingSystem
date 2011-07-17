<?xml version="1.0"?><doc>
<members>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DeviceChild" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10devicechild.h" line="15">
<summary>
A device-child interface accesses data used by a device.
<para>(Also see DirectX SDK: ID3D10DeviceChild)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DeviceChild.GetDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10devicechild.h" line="23">
<summary>
Get the device that created this object.
<para>(Also see DirectX SDK: ID3D10DeviceChild::GetDevice)</para>
</summary>
<returns>A D3DDevice 10.0 object</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10resource.h" line="14">
<summary>
A resource interface provides common actions on all resources.
<para>(Also see DirectX SDK: ID3D10Resource)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource.EvictionPriority" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10resource.h" line="22">
<summary>
Get or set the eviction priority of a resource.
<para>(Also see DirectX SDK: ID3D10Resource::GetEvictionPriority)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource.Type" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10resource.h" line="32">
<summary>
Get the type of the resource.
<para>(Also see DirectX SDK: ID3D10Resource::GetType)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DResource.GetDXGISurface" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10resource.h" line="41">
<summary>
Get associated DXGI suraface. If none is associated - throw an InvalidCastException.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10buffer.h" line="10">
<summary>
A buffer interface accesses a buffer resource, which is unstructured memory. Buffers typically store vertex or index data.
<para>(Also see DirectX SDK: ID3D10Buffer)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10buffer.h" line="18">
<summary>
Get the properties of a buffer resource.
<para>(Also see DirectX SDK: ID3D10Buffer::GetDesc)</para>
</summary>
<returns>A resource description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BufferDescription"/> filled in by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer.Map(&lt;unknown type&gt;,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10buffer.h" line="28">
<summary>
Get the data contained in the resource and deny GPU access to the resource.
<para>(Also see DirectX SDK: ID3D10Buffer::Map)</para>
</summary>
<param name="type">Flag that specifies the CPU's permissions for the reading and writing of a resource. For possible values, see Map.</param>
<param name="flags">Flag that specifies what the CPU should do when the GPU is busy (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MapFlag"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MapFlag"/>. This flag is optional.</param>
<returns>Pointer to the buffer resource data.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer.Unmap" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10buffer.h" line="37">
<summary>
Invalidate the pointer to the resource retrieved by D3DBuffer.Map and reenable GPU access to the resource.
<para>(Also see DirectX SDK: ID3D10Buffer::Unmap)</para>
</summary>
</member>
</members>
</doc>