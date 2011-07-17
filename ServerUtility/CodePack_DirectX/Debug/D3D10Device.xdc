<?xml version="1.0"?><doc>
<members>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.LibraryLoader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\libraryloader.h" line="21">
<summary>
A private class supporting dll and functions loading.
Loaded dlls and functions are cached in a hash table.
</summary>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilView" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10depthstencilview.h" line="10">
<summary>
A depth-stencil-view interface accesses a texture resource during depth-stencil testing.
<para>(Also see DirectX SDK: ID3D10DepthStencilView)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilView.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10depthstencilview.h" line="18">
<summary>
Get the depth-stencil view description.
<para>(Also see DirectX SDK: ID3D10DepthStencilView::GetDesc)</para>
</summary>
<returns>A depth-stencil-view description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilViewDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rendertargetview.h" line="10">
<summary>
A render-target-view interface identifies the render-target subresources that can be accessed during rendering.
<para>(Also see DirectX SDK: ID3D10RenderTargetView)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rendertargetview.h" line="18">
<summary>
Get the properties of a render target view.
<para>(Also see DirectX SDK: ID3D10RenderTargetView::GetDesc)</para>
</summary>
<returns>The description of a render target view (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetViewDescription"/>.</returns>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10blendstate.h" line="10">
<summary>
This blend-state interface accesses blending state for a Direct3D 10.0 device for the output-merger stage.
<para>(Also see DirectX SDK: ID3D10BlendState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10blendstate.h" line="18">
<summary>
Get the blend state description.
<para>(Also see DirectX SDK: ID3D10BlendState::GetDesc)</para>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10depthstencilstate.h" line="10">
<summary>
A depth-stencil-state interface accesses depth-stencil state which sets up the depth-stencil test for the output-merger stage.
<para>(Also see DirectX SDK: ID3D10DepthStencilState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10depthstencilstate.h" line="18">
<summary>
Get the depth-stencil state.
<para>(Also see DirectX SDK: ID3D10DepthStencilState::GetDesc)</para>
</summary>
<returns>The depth-stencil state (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshader.h" line="10">
<summary>
A geometry-shader interface manages an executable program (a geometry shader) that controls the geometry-shader stage.
<para>(Also see DirectX SDK: ID3D10GeometryShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputlayout.h" line="10">
<summary>
An input-layout interface accesses the input data for the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10InputLayout)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshader.h" line="10">
<summary>
A pixel-shader interface manages an executable program (a pixel shader) that controls the pixel-shader stage.
<para>(Also see DirectX SDK: ID3D10PixelShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerstate.h" line="10">
<summary>
A rasterizer-state interface accesses rasterizer state for the rasterizer stage.
<para>(Also see DirectX SDK: ID3D10RasterizerState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerstate.h" line="18">
<summary>
Get the properties of a rasterizer-state object.
<para>(Also see DirectX SDK: ID3D10RasterizerState::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10samplerstate.h" line="10">
<summary>
A sampler-state interface accesses sampler state for a texture.
<para>(Also see DirectX SDK: ID3D10SamplerState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10samplerstate.h" line="18">
<summary>
Get the sampler state.
<para>(Also see DirectX SDK: ID3D10SamplerState::GetDesc)</para>
</summary>
<returns>The sampler state (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceView" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderresourceview.h" line="10">
<summary>
A shader-resource-view interface specifies the subresources a shader can access during rendering. Examples of shader resources include a constant buffer, a texture buffer, a texture or a sampler.
<para>(Also see DirectX SDK: ID3D10ShaderResourceView)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceView.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10shaderresourceview.h" line="18">
<summary>
Get the shader resource view's description.
<para>(Also see DirectX SDK: ID3D10ShaderResourceView::GetDesc)</para>
</summary>
<returns>A ShaderResourceViewDescription structure to be filled with data about the shader resource view.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture1d.h" line="10">
<summary>
A 1D texture interface accesses texel data, which is structured memory.
<para>(Also see DirectX SDK: ID3D10Texture1D)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture1d.h" line="18">
<summary>
Get the properties of the texture resource.
<para>(Also see DirectX SDK: ID3D10Texture1D::GetDesc)</para>
</summary>
<returns>A resource description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1DDescription"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D.Map(System.UInt32,&lt;unknown type&gt;,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture1d.h" line="28">
<summary>
Get the data contained in a subresource, and deny the GPU access to that subresource.
<para>(Also see DirectX SDK: ID3D10Texture1D::Map)</para>
</summary>
<param name="subresourceIndex">Index number of the subresource. See D3D10CalcSubresource for more details.</param>
<param name="type">Specifies the CPU's read and write permissions for a resource. For possible values, see Map.</param>
<param name="flags">Flag that specifies what the CPU should do when the GPU is busy. This flag is optional.</param>
<returns>Pointer to the texture resource data.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture1D.Unmap(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture1d.h" line="38">
<summary>
Invalidate the resource that was retrieved by Texture1D.Map, and re-enable the GPU's access to that resource.
<para>(Also see DirectX SDK: ID3D10Texture1D::Unmap)</para>
</summary>
<param name="subresourceIndex">Subresource to be unmapped. See D3D10CalcSubresource for more details.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture2d.h" line="10">
<summary>
A 2D texture interface manages texel data, which is structured memory.
<para>(Also see DirectX SDK: ID3D10Texture2D)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture2d.h" line="18">
<summary>
Get the properties of the texture resource.
<para>(Also see DirectX SDK: ID3D10Texture2D::GetDesc)</para>
</summary>
<returns>A resource description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2DDescription"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D.Map(System.UInt32,&lt;unknown type&gt;,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture2d.h" line="28">
<summary>
Get the data contained in a subresource, and deny the GPU access to that subresource.
<para>(Also see DirectX SDK: ID3D10Texture2D::Map)</para>
</summary>
<param name="subresourceIndex">Index number of the subresource. See D3D10CalcSubresource for more details.</param>
<param name="type">Specifies the CPU's read and write permissions for a resource. For possible values, see Map.</param>
<param name="flags">Flag that specifies what the CPU should do when the GPU is busy. This flag is optional.</param>
<returns>Pointer to the texture resource data.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture2D.Unmap(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture2d.h" line="38">
<summary>
Invalidate the pointer to the resource that was retrieved by Texture2D.Map, and re-enable GPU access to the resource.
<para>(Also see DirectX SDK: ID3D10Texture2D::Unmap)</para>
</summary>
<param name="subresourceIndex">Subresource to be unmapped. See D3D10CalcSubresource for more details.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture3d.h" line="10">
<summary>
A 3D texture interface accesses texel data, which is structured memory.
<para>(Also see DirectX SDK: ID3D10Texture3D)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture3d.h" line="18">
<summary>
Get the properties of the texture resource.
<para>(Also see DirectX SDK: ID3D10Texture3D::GetDesc)</para>
</summary>
<returns>A resource description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3DDescription"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D.Map(System.UInt32,&lt;unknown type&gt;,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture3d.h" line="28">
<summary>
Get the data contained in a subresource, and deny the GPU access to that subresource.
<para>(Also see DirectX SDK: ID3D10Texture3D::Map)</para>
</summary>
<param name="subresourceIndex">Index number of the subresource. See D3D10CalcSubresource for more details.</param>
<param name="type">Specifies the CPU's read and write permissions for a resource. For possible values, see Map.</param>
<param name="flags">Flag that specifies what the CPU should do when the GPU is busy. This flag is optional.</param>
<returns>Pointer to the texture resource data.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Texture3D.Unmap(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10texture3d.h" line="38">
<summary>
Invalidate the pointer to the resource retrieved by Texture3D.Map, and re-enable the GPU's access to the resource.
<para>(Also see DirectX SDK: ID3D10Texture3D::Unmap)</para>
</summary>
<param name="subresourceIndex">Subresource to be unmapped. See D3D10CalcSubresource for more details.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshader.h" line="10">
<summary>
A vertex-shader interface manages an executable program (a vertex shader) that controls the vertex-shader stage.
<para>(Also see DirectX SDK: ID3D10VertexShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SwitchToRef" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10switchtoref.h" line="12">
<summary>
A swith-to-reference interface (see the switch-to-reference layer) enables an application to switch between a hardware and software device.
<para>(Also see DirectX SDK: ID3D10SwitchToRef)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SwitchToRef.UseRef" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10switchtoref.h" line="20">
<summary>
Get or set a boolean value that indicates the type of device being used.
Set this to True to change to a software device, set this to False to change to a hardware device.
<para>(Also see DirectX SDK: ID3D10SwitchToRef::GetUseRef)</para>
<para>(Also see DirectX SDK: ID3D10SwitchToRef::SetUseRef)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pipelinestage.h" line="11">
<summary>
A pipeline stage. base class for all pipline stage related classes.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="8">
<summary>
Geometry Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="14">
<summary>
Get the constant buffers used by the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::GSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>A collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="23">
<summary>
Get an array of sampler state objects from the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::GSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="32">
<summary>
Get the geometry shader currently set on the device.
<para>(Also see DirectX SDK: ID3D10Device::GSGetShader)</para>
</summary>
<returns>A geometry shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="39">
<summary>
Get the geometry shader resources.
<para>(Also see DirectX SDK: ID3D10Device::GSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to request from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>Collection of shader resource view objects to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="48">
<summary>
Set the constant buffers used by the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::GSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting constant buffers to (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="56">
<summary>
Set an array of sampler states to the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::GSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="64">
<summary>
Set a geometry shader to the device.
<para>(Also see DirectX SDK: ID3D10Device::GSSetShader)</para>
</summary>
<param name="geometryShader">A geometry shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShader"/>. Passing in NULL disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.GeometryShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10geometryshaderpipelinestage.h" line="71">
<summary>
Bind an array of shader resources to the geometry shader stage.
<para>(Also see DirectX SDK: ID3D10Device::GSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="8">
<summary>
InputAssembler pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.GetIndexBuffer(&lt;unknown type&gt;@,System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="14">
<summary>
Get the index buffer that is bound to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IAGetIndexBuffer)</para>
</summary>
<param name="outFormat">The format of the data in the index buffer (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>. These formats provide the size and type of the data in the buffer. The only formats allowed for index buffer data are 16-bit (Format_R16_UINT) and 32-bit (Format_R32_UINT) integers.</param>
<param name="outOffset">Offset (in bytes) from the start of the index buffer, to the first index to use.</param>
<returns>An index buffer returned by the method (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.GetInputLayout" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="23">
<summary>
Get the input-layout object that is bound to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IAGetInputLayout)</para>
</summary>
<returns>The input-layout object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>, which describes the input buffers that will be read by the IA stage.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.GetPrimitiveTopology" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="30">
<summary>
Get information about the primitive type, and data order that describes input data for the input assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IAGetPrimitiveTopology)</para>
</summary>
<returns>The type of primitive, and ordering of the primitive data (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PrimitiveTopology"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PrimitiveTopology"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.GetVertexBuffers(System.UInt32,System.UInt32,System.UInt32[]@,System.UInt32[]@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="37">
<summary>
Get the vertex buffers bound to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IAGetVertexBuffers)</para>
</summary>
<param name="startSlot">The input slot of the first vertex buffer to get. The first vertex buffer is explicitly bound to the start slot; this causes each additional vertex buffer in the array to be implicitly bound to each subsequent input slot. There are 16 input slots.</param>
<param name="numBuffers">The number of vertex buffers to get starting at the offset. The number of buffers (plus the starting slot) cannot exceed the total number of IA-stage input slots.</param>
<param name="strides">A collection of stride values returned by the method; one stride value for each buffer in the vertex-buffer array. Each stride value is the size (in bytes) of the elements that are to be used from that vertex buffer.</param>
<param name="offsets">A collection of offset values returned by the method; one offset value for each buffer in the vertex-buffer array. Each offset is the number of bytes between the first element of a vertex buffer and the first element that will be used.</param>
<returns>A collection of vertex buffers returned by the method (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.SetIndexBuffer(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer,&lt;unknown type&gt;,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="48">
<summary>
Bind an index buffer to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IASetIndexBuffer)</para>
</summary>
<param name="indexBuffer">A D3DBuffer object, that contains indices. The index buffer must have been created with the indexBuffer flag.</param>
<param name="format">A Format that specifies the format of the data in the index buffer. The only formats allowed for index buffer data are 16-bit (Format_R16_UINT) and 32-bit (Format_R32_UINT) integers.</param>
<param name="offset">Offset (in bytes) from the start of the index buffer to the first index to use.</param>
<remarks>Calling this method using a buffer that is currently bound for writing (i.e. bound to the stream output pipeline stage) will effectively 
bind null instead because a buffer cannot be bound as both an input and an output at the same time.</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.SetInputLayout(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="59">
<summary>
Bind an input-layout object to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IASetInputLayout)</para>
</summary>
<param name="inputLayout">The input-layout object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputLayout"/>, which describes the input buffers that will be read by the IA stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.SetPrimitiveTopology(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="66">
<summary>
Bind information about the primitive type, and data order that describes input data for the input assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IASetPrimitiveTopology)</para>
</summary>
<param name="topology">The type of primitive and ordering of the primitive data (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PrimitiveTopology"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PrimitiveTopology"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InputAssemblerPipelineStage.SetVertexBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer},System.UInt32[],System.UInt32[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10inputassemblerpipelinestage.h" line="73">
<summary>
Bind an array of vertex buffers to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D10Device::IASetVertexBuffers)</para>
</summary>
<param name="startSlot">The first input slot for binding. The first vertex buffer is explicitly bound to the start slot; this causes each additional vertex buffer in the array to be implicitly bound to each subsequent input slot. There are 16 input slots (ranges from 0 to D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="vertexBuffers">A collection of vertex buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>. 
The number of buffers (plus the starting slot) cannot exceed the total number of IA-stage input slots (ranges from 0 to D3D10_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT - startSlot).
The vertex buffers must have been created with the VertexBuffer flag.</param>
<param name="strides">An array of stride values; one stride value for each buffer in the vertex-buffer array. Each stride is the size (in bytes) of the elements that are to be used from that vertex buffer.</param>
<param name="offsets">An array of offset values; one offset value for each buffer in the vertex-buffer array. Each offset is the number of bytes between the first element of a vertex buffer and the first element that will be used.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="8">
<summary>
OutputMerger pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.GetBlendState(Microsoft.WindowsAPICodePack.DirectX.DXGI.ColorRgba@,System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="15">
<summary>
Get the blend state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMGetBlendState)</para>
</summary>
<param name="blendFactor">Array of blend factors containing four entries; one for each RGBA component.</param>
<param name="sampleMask">A sample mask.</param>
<returns>A blend-state object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.GetDepthStencilState(System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="24">
<summary>
Gets the depth-stencil state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMGetDepthStencilState)</para>
</summary>
<param name="stencilRef">The stencil reference value used in the depth-stencil test.</param>
<returns>A depth-stencil state object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilState"/> to be filled with information from the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.GetRenderTargets(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="32">
<summary>
Get pointers to the resources bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMGetRenderTargets)</para>
</summary>
<param name="numViews">Number of render targets to retrieve.</param>
<returns>A collection of ID3D10RenderTargetViews which represent render target views.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.GetRenderTargets(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilView@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="40">
<summary>
Get the resources bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMGetRenderTargets)</para>
</summary>
<param name="numViews">Number of render targets to retrieve.</param>
<param name="outDepthStencilView">Retrieves a depth-stencil view.</param>
<returns>A collection of ID3D10RenderTargetViews which represent render target views.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.GetDepthStencilView" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="49">
<summary>
Get the depth stencil view bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMGetRenderTargets)</para>
</summary>
<returns>The depth-stencil view bound to the output-merger stage.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.SetBlendState(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendState,Microsoft.WindowsAPICodePack.DirectX.DXGI.ColorRgba,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="56">
<summary>
Set the blend state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMSetBlendState)</para>
</summary>
<param name="blendState">A blend-state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.BlendState"/>. Passing in NULL implies a default blend state. See remarks for further details.</param>
<param name="blendFactor">Four components array of blend factors, one for each RGBA component. This requires a blend state object that specifies the BlendFactor option.</param>
<param name="sampleMask">32-bit sample coverage. The default value is 0xffffffff.</param>
<remarks>
<para>
Passing in null for the blend-state interface indicates to the runtime to set a default blending state. 
See the DX SDK (ID3D10Device::OMSetBlendState) for more details about the default blending state.
</para>
<para>
A sample mask determines which samples get updated in all the active render targets. 
The mapping of bits in a sample mask to samples in a multisample render target is 
the responsibility of an individual application. 
A sample mask is always applied; it is independent of whether multisampling is enabled, 
and does not depend on whether an application uses multisample render targets.
</para>
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.SetDepthStencilState(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilState,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="78">
<summary>
Sets the depth-stencil state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMSetDepthStencilState)</para>
</summary>
<param name="depthStencilState">A depth-stencil state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilState"/> to bind to the device. Set this to NULL to use the default state listed in DepthStencilDescription.</param>
<param name="stencilRef">Reference value to perform against when doing a depth-stencil test.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.SetRenderTargets(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView},Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilView)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="86">
<summary>
Bind one or more render targets atomically and the depth-stencil buffer to the output-merger stage.
<para>(Also see DirectX SDK: ID3D10Device::OMSetRenderTargets)</para>
</summary>
<param name="renderTargetViews">A collection of render targets (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView"/> to bind to the device (ranges between 0 and D3D10_SIMULTANEOUS_RENDER_TARGET_COUNT). 
If this parameter is null, no render targets are bound.</param>
<param name="depthStencilView">A depth-stencil view (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.DepthStencilView"/> to bind to the device. 
If this parameter is null, the depth-stencil state is not bound.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.OutputMergerPipelineStage.SetRenderTargets(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10outputmergerpipelinestage.h" line="96">
<summary>
Bind one or more render targets atomically and the depth-stencil buffer to the output-merger stage.
The depth-stencil state is not bound.
<para>(Also see DirectX SDK: ID3D10Device::OMSetRenderTargets)</para>
</summary>
<param name="renderTargetViews">A collection of render targets (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RenderTargetView"/> to bind to the device (ranges between 0 and D3D10_SIMULTANEOUS_RENDER_TARGET_COUNT). 
If this parameter is null, no render targets are bound.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="8">
<summary>
Pixel Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="14">
<summary>
Get the constant buffers used by the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::PSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>Collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="23">
<summary>
Get an array of sampler states from the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::PSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>Collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/> to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="32">
<summary>
Get the pixel shader currently set on the device.
<para>(Also see DirectX SDK: ID3D10Device::PSGetShader)</para>
</summary>
<returns>A pixel shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="39">
<summary>
Get the pixel shader resources.
<para>(Also see DirectX SDK: ID3D10Device::PSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to get from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>Collection of shader resource view objects to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="48">
<summary>
Set the constant buffers used by the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::PSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting constant buffers to (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="56">
<summary>
Set an array of sampler states to the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::PSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="64">
<summary>
Sets a pixel shader to the device.
<para>(Also see DirectX SDK: ID3D10Device::PSSetShader)</para>
</summary>
<param name="pixelShader">A pixel shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.PixelShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10pixelshaderpipelinestage.h" line="72">
<summary>
Bind an array of shader resources to the pixel shader stage.
<para>(Also see DirectX SDK: ID3D10Device::PSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerpipelinestage.h" line="8">
<summary>
Rasterizer pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerPipelineStage.GetScissorRects(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerpipelinestage.h" line="15">
<summary>
Get the collection of scissor rectangles bound to the rasterizer stage.
<para>(Also see DirectX SDK: ID3D10Device::RSGetScissorRects)</para>
</summary>
<returns>A collection of scissor rectangles (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>.
If maximumNumberOfRects is greater than the number of scissor rects currently bound, 
then the collection will be trimmed to contain only the currently bound. 
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerPipelineStage.GetState" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerpipelinestage.h" line="25">
<summary>
Get the rasterizer state from the rasterizer stage of the pipeline.
<para>(Also see DirectX SDK: ID3D10Device::RSGetState)</para>
</summary>
<returns>A rasterizer-state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerState"/> to fill with information from the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerPipelineStage.GetViewports(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerpipelinestage.h" line="32">
<summary>
Get the array of viewports bound to the rasterizer stage
<para>(Also see DirectX SDK: ID3D10Device::RSGetViewports)</para>
</summary>
<param name="maxNumberOfViewports">The input specifies the maximum number of viewports (ranges from 0 to D3D10_VIEWPORT_AND_SCISSORRECT_OBJECT_COUNT_PER_PIPELINE) to retrieve, the output contains the actual number of viewports returned.</param>
<returns>A collection of Viewport structures that are bound to the device. 
If the number of viewports (in maxNumberOfViewports) is greater than the actual number of viewports currently bound, 
then the collection will be trimmed to contain only the currently bound. 
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerPipelineStage.SetScissorRects(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerpipelinestage.h" line="43">
<summary>
Bind an array of scissor rectangles to the rasterizer stage.
<para>(Also see DirectX SDK: ID3D10Device::RSSetScissorRects)</para>
</summary>
<param name="rects">A collection of scissor rectangles (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerPipelineStage.SetState(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerState)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerpipelinestage.h" line="50">
<summary>
Set the rasterizer state for the rasterizer stage of the pipeline.
<para>(Also see DirectX SDK: ID3D10Device::RSSetState)</para>
</summary>
<param name="rasterizerState">A rasterizer-state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerState"/> to bind to the pipeline.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.RasterizerPipelineStage.SetViewports(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Viewport})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10rasterizerpipelinestage.h" line="57">
<summary>
Bind an array of viewports to the rasterizer stage of the pipeline.
<para>(Also see DirectX SDK: ID3D10Device::RSSetViewports)</para>
</summary>
<param name="viewports">A collection of Viewport structures to bind to the device.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.StreamOutputPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10streamoutputpipelinestage.h" line="8">
<summary>
StreamOutput pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.StreamOutputPipelineStage.GetTargets(System.UInt32,System.UInt32[]@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10streamoutputpipelinestage.h" line="15">
<summary>
Get the target output buffers for the stream-output stage of the pipeline.
<para>(Also see DirectX SDK: ID3D10Device::SOGetTargets)</para>
</summary>
<param name="numBuffers">Number of buffers to get.</param>
<param name="offsets">Output array of offsets to the output buffers from ppBuffers, one offset for each buffer. The offset values are in bytes.</param>
<returns>A collection of output buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> to be retrieved from the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.StreamOutputPipelineStage.SetTargets(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer},System.UInt32[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10streamoutputpipelinestage.h" line="24">
<summary>
Set the target output buffers for the stream-output stage of the pipeline.
<para>(Also see DirectX SDK: ID3D10Device::SOSetTargets)</para>
</summary>
<param name="targets">The collection of output buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> to bind to the device. 
The buffers must have been created with the <b>StreamOutput</b> flag.
A maximum of four output buffers can be set. If less than four are defined by the call, the remaining buffer slots are set to NULL.</param>
<param name="offsets">Array of offsets to the output buffers from ppBuffers, 
one offset for each buffer. The offset values must be in bytes.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="8">
<summary>
Vertex Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="15">
<summary>
Get the constant buffers used by the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::VSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>Collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="24">
<summary>
Get an array of sampler states from the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::VSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>Collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/> to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="33">
<summary>
Get the vertex shader currently set on the device.
<para>(Also see DirectX SDK: ID3D10Device::VSGetShader)</para>
</summary>
<returns>A vertex shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="40">
<summary>
Get the vertex shader resources.
<para>(Also see DirectX SDK: ID3D10Device::VSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to get from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>Collection of shader resource views to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="49">
<summary>
Set the constant buffers used by the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::VSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting constant buffers to (ranges from 0 to D3D10_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="57">
<summary>
Set an array of sampler states to the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D10Device::VSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D10_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="65">
<summary>
Set a vertex shader to the device.
<para>(Also see DirectX SDK: ID3D10Device::VSSetShader)</para>
</summary>
<param name="vertexShader">A vertex shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.VertexShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D10.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10vertexshaderpipelinestage.h" line="73">
<summary>
Bind an array of shader resources to the vertex-shader stage.
<para>(Also see DirectX SDK: ID3D10Device::VSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (range is from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.
Up to a maximum of 128 slots are available for shader resources (range is from 0 to D3D10_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="17">
<summary>
An Effect interface manages a set of state objects, resources and shaders for implementing a rendering effect.
<para>(Also see DirectX SDK: ID3D10Effect)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetConstantBufferByIndex(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="25">
<summary>
Get a constant buffer by index.
<para>(Also see DirectX SDK: ID3D10Effect::GetConstantBufferByIndex)</para>
</summary>
<param name="index">A zero-based index.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetConstantBufferByName(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="32">
<summary>
Get a constant buffer by name.
<para>(Also see DirectX SDK: ID3D10Effect::GetConstantBufferByName)</para>
</summary>
<param name="Name">The constant-buffer name.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="39">
<summary>
Get an effect description.
<para>(Also see DirectX SDK: ID3D10Effect::GetDesc)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="48">
<summary>
Get the device that created the effect as 10.0 device.
<para>(Also see DirectX SDK: ID3D10Effect::GetDevice)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetTechniqueByIndex(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="54">
<summary>
Get a technique by index.
<para>(Also see DirectX SDK: ID3D10Effect::GetTechniqueByIndex)</para>
</summary>
<param name="index">A zero-based index.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetTechniqueByName(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="61">
<summary>
Get a technique by name.
<para>(Also see DirectX SDK: ID3D10Effect::GetTechniqueByName)</para>
</summary>
<param name="Name">The name of the technique.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetVariableByIndex(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="68">
<summary>
Get a variable by index.
<para>(Also see DirectX SDK: ID3D10Effect::GetVariableByIndex)</para>
</summary>
<param name="index">A zero-based index.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetVariableByName(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="75">
<summary>
Get a variable by name.
<para>(Also see DirectX SDK: ID3D10Effect::GetVariableByName)</para>
</summary>
<param name="Name">The variable name.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.GetVariableBySemantic(System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="82">
<summary>
Get a variable by semantic.
<para>(Also see DirectX SDK: ID3D10Effect::GetVariableBySemantic)</para>
</summary>
<param name="Semantic">The semantic name.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.IsOptimized" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="89">
<summary>
Test an effect to see if the reflection metadata has been removed from memory.
<para>(Also see DirectX SDK: ID3D10Effect::IsOptimized)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.IsPool" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="98">
<summary>
Test an effect to see if it is part of a memory pool.
<para>(Also see DirectX SDK: ID3D10Effect::IsPool)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.IsValid" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="107">
<summary>
Test an effect to see if it contains valid syntax.
<para>(Also see DirectX SDK: ID3D10Effect::IsValid)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Effect.Optimize" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10effect.h" line="116">
<summary>
Minimize the amount of memory required for an effect.
<para>(Also see DirectX SDK: ID3D10Effect::Optimize)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Asynchronous" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10asynchronous.h" line="10">
<summary>
This class encapsulates methods for retrieving data from the GPU asynchronously.
<para>(Also see DirectX SDK: ID3D10Asynchronous)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Asynchronous.Begin" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10asynchronous.h" line="18">
<summary>
Starts the collection of GPU data.
<para>(Also see DirectX SDK: ID3D10Asynchronous::Begin)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Asynchronous.End" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10asynchronous.h" line="24">
<summary>
Ends the collection of GPU data.
<para>(Also see DirectX SDK: ID3D10Asynchronous::End)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Asynchronous.GetData(System.IntPtr,System.UInt32,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10asynchronous.h" line="30">
<summary>
Get data from the GPU asynchronously.
<para>(Also see DirectX SDK: ID3D10Asynchronous::GetData)</para>
</summary>
<param name="data">Address of memory that will receive the data. If NULL, GetData will be used only to check status. The type of data output depends on the type of asynchronous object. See Remarks.</param>
<param name="dataSize">Size of the data to retrieve or 0. This value can be obtained with Asynchronous.GetDataSize. Must be 0 when pData is NULL.</param>
<param name="flags">Optional flags. Can be 0 or any combination of the flags enumerated by AsyncGetDataFlag.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Asynchronous.GetDataSize" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10asynchronous.h" line="38">
<summary>
Get the size of the data (in bytes) that is output when calling Asynchronous.GetData.
<para>(Also see DirectX SDK: ID3D10Asynchronous::GetDataSize)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DCounter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10counter.h" line="10">
<summary>
This class encapsulates methods for measuring GPU performance.
<para>(Also see DirectX SDK: ID3D10Counter)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DCounter.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10counter.h" line="18">
<summary>
Get a counter description.
<para>(Also see DirectX SDK: ID3D10Counter::GetDesc)</para>
</summary>
<returns>A counter description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.CounterDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DQuery" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10query.h" line="10">
<summary>
A query interface queries information from the GPU.
<para>(Also see DirectX SDK: ID3D10Query)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DQuery.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10query.h" line="18">
<summary>
Get a query description.
<para>(Also see DirectX SDK: ID3D10Query::GetDesc)</para>
</summary>
<returns>A query description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.QueryDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.D3DPredicate" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10predicate.h" line="10">
<summary>
A predicate interface determines whether geometry should be processed depending on the results of a previous draw call.
<para>(Also see DirectX SDK: ID3D10Predicate)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="11">
<summary>
An information-queue interface stores, retrieves, and filters debug messages. The queue consists of a message queue, an optional storage filter stack, and a optional retrieval filter stack.
<para>(Also see DirectX SDK: ID3D10InfoQueue)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.AddApplicationMessage(&lt;unknown type&gt;,System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="19">
<summary>
Add a user-defined message to the message queue and send that message to debug output.
<para>(Also see DirectX SDK: ID3D10InfoQueue::AddApplicationMessage)</para>
</summary>
<param name="severity">Severity of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>.</param>
<param name="description">Message string.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.AddMessage(&lt;unknown type&gt;,&lt;unknown type&gt;,&lt;unknown type&gt;,System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="27">
<summary>
Add a Direct3D 10 debug message to the message queue and send that message to debug output.
<para>(Also see DirectX SDK: ID3D10InfoQueue::AddMessage)</para>
</summary>
<param name="category">Category of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageCategory"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageCategory"/>.</param>
<param name="severity">Severity of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>.</param>
<param name="id">Unique identifier of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageId"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageId"/>.</param>
<param name="description">User-defined message.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.ClearStoredMessages" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="37">
<summary>
Clear all messages from the message queue.
<para>(Also see DirectX SDK: ID3D10InfoQueue::ClearStoredMessages)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.GetBreakOnCategory(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="43">
<summary>
Get a message category to break on when a message with that category passes through the storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetBreakOnCategory)</para>
</summary>
<param name="category">Message category to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageCategory"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageCategory"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.GetBreakOnId(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="50">
<summary>
Get a message identifier to break on when a message with that identifier passes through the storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetBreakOnID)</para>
</summary>
<param name="id">Message identifier to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageId"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageId"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.GetBreakOnSeverity(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="57">
<summary>
Get a message severity level to break on when a message with that severity level passes through the storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetBreakOnSeverity)</para>
</summary>
<param name="severity">Message severity level to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.GetMessage(System.UInt64)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="64">
<summary>
Get a message from the message queue.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetMessage)</para>
</summary>
<param name="messageIndex">Index into message queue after an optional retrieval filter has been applied. 
This can be between 0 and the number of messages in the message queue that pass through the retrieval filter (which can be obtained with GetNumStoredMessagesAllowedByRetrievalFilter(). 
0 is the message at the front of the message queue.</param>
<returns>Returned message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Message"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Message"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.TryGetMessage(System.UInt64,Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Message@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="74">
<summary>
Try to get a message from the message queue.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetMessage)</para>
</summary>
<param name="messageIndex">Index into message queue after an optional retrieval filter has been applied. 
This can be between 0 and the number of messages in the message queue that pass through the retrieval filter 
(which can be obtained with NumStoredMessagesAllowedByRetrievalFilter property. 
Index 0 is the message at the front of the message queue. </param>        
<param name="message">Returned message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Message"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.Message"/>.</param>      
<returns>True if successful; false otherwise.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.MessageCountLimit" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="86">
<summary>
Get or set the maximum number of messages that can be added to the message queue.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetMessageCountLimit)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.MuteDebugOutput" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="96">
<summary>
Get or set a boolean that turns the debug output on or off.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetMuteDebugOutput)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.NumMessagesAllowedByStorageFilter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="106">
<summary>
Get the number of messages that were allowed to pass through a storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetNumMessagesAllowedByStorageFilter)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.NumMessagesDeniedByStorageFilter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="115">
<summary>
Get the number of messages that were denied passage through a storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetNumMessagesDeniedByStorageFilter)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.NumMessagesDiscardedByMessageCountLimit" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="124">
<summary>
Get the number of messages that were discarded due to the message count limit.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetNumMessagesDiscardedByMessageCountLimit)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.NumStoredMessages" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="134">
<summary>
Get the number of messages currently stored in the message queue.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetNumStoredMessages)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.NumStoredMessagesAllowedByRetrievalFilter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="143">
<summary>
Get the number of messages that are able to pass through a retrieval filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::GetNumStoredMessagesAllowedByRetrievalFilter)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.SetBreakOnCategory(&lt;unknown type&gt;,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="152">
<summary>
Set a message category to break on when a message with that category passes through the storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::SetBreakOnCategory)</para>
</summary>
<param name="category">Message category to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageCategory"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageCategory"/>.</param>
<param name="enable">Turns this breaking condition on or off (true for on, false for off).</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.SetBreakOnId(&lt;unknown type&gt;,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="160">
<summary>
Set a message identifier to break on when a message with that identifier passes through the storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::SetBreakOnID)</para>
</summary>
<param name="id">Message identifier to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageId"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageId"/>.</param>
<param name="enable">Turns this breaking condition on or off (true for on, false for off).</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.InfoQueue.SetBreakOnSeverity(&lt;unknown type&gt;,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d10\d3d10infoqueue.h" line="168">
<summary>
Set a message severity level to break on when a message with that severity level passes through the storage filter.
<para>(Also see DirectX SDK: ID3D10InfoQueue::SetBreakOnSeverity)</para>
</summary>
<param name="severity">Message severity level to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D10.MessageSeverity"/>.</param>
<param name="enable">Turns this breaking condition on or off (true for on, false for off).</param>
</member>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="11">
<summary>
An SwapChain interface implements one or more surfaces for storing rendered data before presenting it to an output.
<para>(Also see DirectX SDK: IDXGISwapChain)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.GetBuffer``1(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="19">
<summary>
Access one of the swap-chain back buffers.
<para>(Also see DirectX SDK: IDXGISwapChain::GetBuffer)</para>
</summary>
<param name="bufferIndex">A zero-based buffer index. 
If the swap effect is not Sequential, this method only has access to the first buffer; for this case, set the index to zero.
</param>
<typeparam name="T">The type of the buffer object. Must inherit from <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DirectUnknown"/>.</typeparam>
<returns>The back-buffer object.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.GetContainingOutput" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="31">
<summary>
Get the output (the display monitor) that contains the majority of the client area of the target window.
<para>(Also see DirectX SDK: IDXGISwapChain::GetContainingOutput)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="37">
<summary>
Get a description of the swap chain.
<para>(Also see DirectX SDK: IDXGISwapChain::GetDesc)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.GetFrameStatistics" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="46">
<summary>
Get performance statistics about the last render frame.
<para>(Also see DirectX SDK: IDXGISwapChain::GetFrameStatistics)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.GetFullScreenState(Microsoft.WindowsAPICodePack.DirectX.DXGI.Output@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="52">
<summary>
Get the state associated with full-screen mode.
<para>(Also see DirectX SDK: IDXGISwapChain::GetFullscreenState)</para>
</summary>
<param name="target">returns tshe output target (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output"/> when the mode is full screen; otherwise null will be returned.</param>
<returns>A boolean whose value is either:TRUE if the swap chain is in full-screen modeFALSE if the swap chain is in windowed mode</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.IsFullScreen" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="60">
<summary>
Sets or get the swap chain in full screen.
DXGI will choose the output based on the swap-chain's device and the output window's placement.
<para>(Also see DirectX SDK: IDXGISwapChain::GetFullscreenState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.LastPresentCount" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="71">
<summary>
Get the number of times SwapChain.Present has been called.
<para>(Also see DirectX SDK: IDXGISwapChain::GetLastPresentCount)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.Present(System.UInt32,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="80">
<summary>
Present a rendered image to the user.
This method can throw exceptions if the Swap Chain is unable to present. 
TryPresent() method should be used instead when exceptions can impact performance.
<para>(Also see DirectX SDK: IDXGISwapChain::Present)</para>
</summary>
<param name="syncInterval">If the update region straddles more than one output (each represented by Output), the synchronization will be performed to the output that contains the largest subrectangle of the target window's client area.</param>
<param name="flags">An integer value that contains swap-chain presentation options (see <see cref="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.Present(System.UInt32,&lt;unknown type&gt;)"/>)<seealso cref="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.Present(System.UInt32,&lt;unknown type&gt;)"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.TryPresent(System.UInt32,&lt;unknown type&gt;,Microsoft.WindowsAPICodePack.DirectX.ErrorCode@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="90">
<summary>
Try to present a rendered image to the user.
No exceptions will be thrown by this method.
<para>(Also see DirectX SDK: IDXGISwapChain::Present)</para>
</summary>
<param name="syncInterval">If the update region straddles more than one output (each represented by Output), the synchronization will be performed to the output that contains the largest subrectangle of the target window's client area.</param>
<param name="flags">An integer value that contains swap-chain presentation options (see <see cref="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.Present(System.UInt32,&lt;unknown type&gt;)"/>)<seealso cref="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.Present(System.UInt32,&lt;unknown type&gt;)"/>.</param>
<param name="error">An error code indicating Present error if unsuccessful.</param>
<returns>False if unsuccessful; True otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.ResizeBuffers(System.UInt32,System.UInt32,System.UInt32,&lt;unknown type&gt;,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="101">
<summary>
Change the swap chain's back buffer size, format, and number of buffers. This should be called when the application window is resized.
<para>(Also see DirectX SDK: IDXGISwapChain::ResizeBuffers)</para>
</summary>
<param name="bufferCount">The number of buffers in the swap chain (including all back and front buffers). This can be different from the number of buffers the swap chain was created with.</param>
<param name="width">New width of the back buffer. If 0 is specified the width of the client area of the target window will be used.</param>
<param name="height">New height of the back buffer. If 0 is specified the height of the client area of the target window will be used.</param>
<param name="newFormat">The new format of the back buffer.</param>
<param name="flags">Flags that indicate how the swap chain will function.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.TryResizeBuffers(System.UInt32,System.UInt32,System.UInt32,&lt;unknown type&gt;,&lt;unknown type&gt;,Microsoft.WindowsAPICodePack.DirectX.ErrorCode@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="112">
<summary>
Change the swap chain's back buffer size, format, and number of buffers. This should be called when the application window is resized.
This method will not throw exceptions, but will return a bool indicating success or failure.
The errorCode output value can also be used to track the error type.
<para>(Also see DirectX SDK: IDXGISwapChain::ResizeBuffers)</para>
</summary>
<param name="bufferCount">The number of buffers in the swap chain (including all back and front buffers). This can be different from the number of buffers the swap chain was created with.</param>
<param name="width">New width of the back buffer. If 0 is specified the width of the client area of the target window will be used.</param>
<param name="height">New height of the back buffer. If 0 is specified the height of the client area of the target window will be used.</param>
<param name="newFormat">The new format of the back buffer.</param>
<param name="flags">Flags that indicate how the swap chain will function.</param>
<param name="errorCode">Returned error code.</param>
<returns>True if successful, false otherwise.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.ResizeTarget(Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="127">
<summary>
Resize the output target.
<para>(Also see DirectX SDK: IDXGISwapChain::ResizeTarget)</para>
</summary>
<param name="newTargetParameters">The mode description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.ModeDescription"/>, which specifies the new width, height, format and refresh rate of the target. 
If the format is UNKNOWN, the existing format will be used. Using UNKNOWN is only recommended when the swap chain is in full-screen mode as this method is not thread safe.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DXGI.SwapChain.SetFullScreenState(System.Boolean,Microsoft.WindowsAPICodePack.DirectX.DXGI.Output)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgiswapchain.h" line="135">
<summary>
Set the display mode to windowed or full-screen.
<para>(Also see DirectX SDK: IDXGISwapChain::SetFullscreenState)</para>
</summary>
<param name="isFullScreen">Use True for full-screen, False for windowed.</param>
<param name="target">If the current display mode is full-screen, this parameter must be the output target (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Output"/> that contains the swap chain; 
otherwise, this parameter is ignored. If you set this parameter to Null, DXGI will choose the output based on the swap-chain's device and the output window's placement.</param>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIDevice1" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevice1.h" line="10">
<summary>
Implements a derived class for DXGI 1.1 objects that produce image data.
<para>(Also see DirectX SDK: IDXGIDevice1)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DXGI.DXGIDevice1.MaximumFrameLatency" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\dxgi\dxgidevice1.h" line="18">
<summary>
Gets or sets the number of frames that the system is allowed to queue for rendering.
<para>(Also see DirectX SDK: IDXGIDevice1::GetMaximumFrameLatency, IDXGIDevice1::SetMaximumFrameLatency)</para>
</summary>
</member>
</members>
</doc>