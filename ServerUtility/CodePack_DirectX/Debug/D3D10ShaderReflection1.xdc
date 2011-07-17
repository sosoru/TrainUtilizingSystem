<?xml version="1.0"?><doc>
<members>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection.h" line="13">
<summary>
A shader-reflection interface accesses shader information.
<para>(Also see DirectX SDK: ID3D10ShaderReflection)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection.GetConstantBufferByIndex(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection.h" line="21">
<summary>
Get a constant buffer by index.
<para>(Also see DirectX SDK: ID3D10ShaderReflection::GetConstantBufferByIndex)</para>
</summary>
<param name="index">Zero-based index.</param>
<returns>A shader reflection constant bufer.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection.GetConstantBufferByName(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection.h" line="29">
<summary>
Get a constant buffer by name.
<para>(Also see DirectX SDK: ID3D10ShaderReflection::GetConstantBufferByName)</para>
</summary>
<param name="name">The constant-buffer name.</param>
<returns>A shader reflection constant bufer.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection.h" line="37">
<summary>
Get a shader description.
<para>(Also see DirectX SDK: ID3D10ShaderReflection::GetDesc)</para>
</summary>
<returns>A shader description. See ShaderDescription.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection.GetInputParameterDescription(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection.h" line="47">
<summary>
Get an input-parameter description for a shader.
<para>(Also see DirectX SDK: ID3D10ShaderReflection::GetInputParameterDesc)</para>
</summary>
<param name="parameterIndex">A zero-based parameter index.</param>
<returns>A shader-input-signature description. See SignatureParameterDescription.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection.GetOutputParameterDescription(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection.h" line="55">
<summary>
Get an output-parameter description for a shader.
<para>(Also see DirectX SDK: ID3D10ShaderReflection::GetOutputParameterDesc)</para>
</summary>
<param name="parameterIndex">A zero-based parameter index.</param>
<returns>A shader-output-parameter description. See SignatureParameterDescription.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection.GetResourceBindingDescription(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection.h" line="63">
<summary>
Get a description of the resources bound to a shader.
<para>(Also see DirectX SDK: ID3D10ShaderReflection::GetResourceBindingDesc)</para>
</summary>
<param name="resourceIndex">A zero-based resource index.</param>
<returns>A input-binding description. See ShaderInputBindDescription.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="11">
<summary>
A shader-reflection interface accesses shader information.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.BitwiseInstructionCount" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="19">
<summary>
Gets the number of bitwise instructions.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::GetBitwiseInstructionCount)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.ConversionInstructionCount" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="28">
<summary>
Gets the number of Conversion instructions.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::GetConversionInstructionCount)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.GSInputPrimitive" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="37">
<summary>
Gets the geometry-shader input-primitive description.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::GetGSInputPrimitive)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.MovcInstructionCount" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="46">
<summary>
Gets the number of Movc instructions.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::GetMovcInstructionCount)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.MovInstructionCount" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="55">
<summary>
Gets the number of Mov instructions.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::GetMovInstructionCount)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.GetResourceBindingDescriptionByName(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="64">
<summary>
Gets a variable by name.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::GetResourceBindingDescByName)</para>
</summary>
<param name="name">A string containing the variable name.</param>
<returns>ShaderInputBindDescription object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.GetVariableByName(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="72">
<summary>
Gets a variable by name.
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::GetVariableByName)</para>
</summary>
<param name="name">A string containing the variable name.</param>
<returns>ShaderReflectionVariable object.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.IsLevel9Shader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="80">
<summary>
Is Level9 Shader
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::IsLevel9Shader)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderReflection1.IsSampleFrequencyShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderreflection1.h" line="89">
<summary>
TBD
<para>(Also see DirectX SDK: ID3D10ShaderReflection1::IsSampleFrequencyShader)</para>
</summary>
</member>
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
</members>
</doc>