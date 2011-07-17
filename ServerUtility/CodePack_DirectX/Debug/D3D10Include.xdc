<?xml version="1.0"?><doc>
<members>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectObject" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directobject.h" line="7">
<summary>
Base for classes supporting an internal interface that is not an IUnknown
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectObject.NativeObject" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directobject.h" line="31">
<summary>
Get the internal native pointer for the wrapped native object
</summary>
<returns>
A pointer to the wrapped native interfac.
</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Include" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10include.h" line="11">
<summary>
An include interface allows an application to create user-overridable methods for opening and closing files when loading an effect from memory. This class does not inherit from anything, but does declare the following methods:
<para>(Also see DirectX SDK: ID3D10Include)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Include.Close(System.IntPtr)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10include.h" line="19">
<summary>
A user-implemented method for closing a shader #include file.
<para>(Also see DirectX SDK: ID3D10Include::Close)</para>
</summary>
<param name="data">Pointer to the returned buffer that contains the include directives. This is the pointer that was returned by the corresponding Include.Open call.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Include.Open(&lt;unknown type&gt;,System.String,System.IntPtr,System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10include.h" line="26">
<summary>
A user-implemented method for opening and reading the contents of a shader #include file.
<para>(Also see DirectX SDK: ID3D10Include::Open)</para>
</summary>
<param name="includeType">The location of the #include file. See IncludeType.</param>
<param name="fileName">Name of the #include file.</param>
<param name="parentData">Pointer to the container that includes the #include file.</param>
<param name="bytes">Number of bytes returned.</param>
<returns>Pointer to the returned buffer that contains the include directives. This pointer remains valid until Include.Close() is called.</returns>
</member>
</members>
</doc>