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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.View" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10view.h" line="11">
<summary>
A view interface specifies the parts of a resource the pipeline can access during rendering.
<para>(Also see DirectX SDK: ID3D10View)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.View.GetResource" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10view.h" line="19">
<summary>
Get the resource that is accessed through this view.
<para>(Also see DirectX SDK: ID3D10View::GetResource)</para>
</summary>
<returns>The resource that is accessed through this view.</returns>
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
</members>
</doc>