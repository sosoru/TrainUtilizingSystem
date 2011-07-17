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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionVariable" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectionvariable.h" line="13">
<summary>
This shader-reflection interface provides access to a variable. This class does not inherit from anything, but does declare the following methods:
<para>(Also see DirectX SDK: ID3D10ShaderReflectionVariable)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionVariable.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectionvariable.h" line="21">
<summary>
Get a shader-variable description.
<para>(Also see DirectX SDK: ID3D10ShaderReflectionVariable::GetDesc)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionVariable.Type" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectionvariable.h" line="30">
<summary>
Get a shader-variable type.
<para>(Also see DirectX SDK: ID3D10ShaderReflectionVariable::GetType)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionType" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectiontype.h" line="11">
<summary>
This shader-reflection interface provides access to variable type. This class does not inherit from anything, but does declare the following methods:
<para>(Also see DirectX SDK: ID3D10ShaderReflectionType)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionType.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectiontype.h" line="19">
<summary>
Get the description of a shader-reflection-variable type.
<para>(Also see DirectX SDK: ID3D10ShaderReflectionType::GetDesc)</para>
</summary>
<returns>A shader-type description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderTypeDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderTypeDescription"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionType.GetMemberTypeByIndex(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectiontype.h" line="29">
<summary>
Get a shader-reflection-variable type by index.
<para>(Also see DirectX SDK: ID3D10ShaderReflectionType::GetMemberTypeByIndex)</para>
</summary>
<param name="index">Zero-based index.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionType.GetMemberTypeByName(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectiontype.h" line="36">
<summary>
Get a shader-reflection-variable type by name.
<para>(Also see DirectX SDK: ID3D10ShaderReflectionType::GetMemberTypeByName)</para>
</summary>
<param name="name">Member name.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflectionType.GetMemberTypeName(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflectiontype.h" line="43">
<summary>
Get a shader-reflection-variable type.
<para>(Also see DirectX SDK: ID3D10ShaderReflectionType::GetMemberTypeName)</para>
</summary>
<param name="index">Zero-based index.</param>
</member>
</members>
</doc>