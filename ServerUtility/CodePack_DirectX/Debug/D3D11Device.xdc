<?xml version="1.0"?><doc>
<members>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.LibraryLoader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\libraryloader.h" line="21">
<summary>
A private class supporting dll and functions loading.
Loaded dlls and functions are cached in a hash table.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceChild" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicechild.h" line="14">
<summary>
A device-child interface accesses data used by a device.
<para>(Also see DirectX SDK: ID3D11DeviceChild)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceChild.GetDevice" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicechild.h" line="22">
<summary>
Get the device that created this object.
<para>(Also see DirectX SDK: ID3D11DeviceChild::GetDevice)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11blendstate.h" line="10">
<summary>
This blend-state interface accesses blending state for the output-merger stage.
<para>(Also see DirectX SDK: ID3D11BlendState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11blendstate.h" line="18">
<summary>
Get the blend state description.
<para>(Also see DirectX SDK: ID3D11BlendState::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11resource.h" line="14">
<summary>
A resource interface provides common actions on all resources.
<para>(Also see DirectX SDK: ID3D11Resource)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource.EvictionPriority" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11resource.h" line="22">
<summary>
Gets or sets the eviction priority of a resource.
<para>(Also see DirectX SDK: ID3D11Resource::GetEvictionPriority, ID3D11Resource::SetEvictionPriority)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource.Type" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11resource.h" line="32">
<summary>
Get the type of the resource.
<para>(Also see DirectX SDK: ID3D11Resource::GetType)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource.GetDXGISurface" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11resource.h" line="41">
<summary>
Get associated DXGI suraface.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11buffer.h" line="10">
<summary>
Allows acccess to a buffer resource, which is unstructured memory. 
Buffers typically store vertex or index data.
<para>(Also see DirectX SDK: ID3D11Buffer)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11buffer.h" line="19">
<summary>
Get the properties of a buffer resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BufferDescription"/>.
<para>(Also see DirectX SDK: ID3D11Buffer::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classlinkage.h" line="11">
<summary>
This class encapsulates an HLSL dynamic linkage.
<para>(Also see DirectX SDK: ID3D11ClassLinkage)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage.CreateClassInstance(System.String,System.UInt32,System.UInt32,System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classlinkage.h" line="19">
<summary>
Initializes a class-instance object that represents an HLSL class instance.
<para>(Also see DirectX SDK: ID3D11ClassLinkage::CreateClassInstance)</para>
</summary>
<param name="classTypeName">The type name of a class to initialize.</param>
<param name="constantBufferOffset">Identifies the constant buffer that contains the class data.</param>
<param name="constantVectorOffset">The four-component vector offset from the start of the constant buffer where the class data will begin. Consequently, this is not a byte offset.</param>
<param name="textureOffset">The texture slot for the first texture; there may be multiple textures following the offset.</param>
<param name="samplerOffset">The sampler slot for the first sampler; there may be multiple samplers following the offset.</param>
<returns>A ClassInstance interface to initialize.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassLinkage.GetClassInstance(System.String,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classlinkage.h" line="31">
<summary>
Gets the class-instance object that represents the specified HLSL class.
<para>(Also see DirectX SDK: ID3D11ClassLinkage::GetClassInstance)</para>
</summary>
<param name="classInstanceName">The name of a class for which to get the class instance.</param>
<param name="instanceIndex">The index of the class instance.</param>
<returns>The ClassInstance to initialize.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshader.h" line="10">
<summary>
A compute-shader class manages an executable program (a compute shader) that controls the compute-shader stage.
<para>(Also see DirectX SDK: ID3D11ComputeShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Asynchronous" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11asynchronous.h" line="10">
<summary>
This class encapsulates methods for retrieving data from the GPU asynchronously.
<para>(Also see DirectX SDK: ID3D11Asynchronous)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Asynchronous.DataSize" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11asynchronous.h" line="18">
<summary>
Get the size of the data (in bytes) that is output when calling DeviceContext.GetData.
<para>(Also see DirectX SDK: ID3D11Asynchronous::GetDataSize)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DCounter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11counter.h" line="10">
<summary>
This class encapsulates methods for measuring GPU performance.
<para>(Also see DirectX SDK: ID3D11Counter)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DCounter.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11counter.h" line="18">
<summary>
Get the counter description.
<para>(Also see DirectX SDK: ID3D11Counter::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="46">
<summary>
The DeviceContext interface represents a device context which generates rendering commands.
<para>(Also see DirectX SDK: ID3D11DeviceContext)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.Begin(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Asynchronous)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="55">
<summary>
Mark the beginning of a series of commands.
<para>(Also see DirectX SDK: ID3D11DeviceContext::Begin)</para>
</summary>
<param name="asyncData">A Asynchronous object.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ClearDepthStencilView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView,&lt;unknown type&gt;,System.Single,System.Byte)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="62">
<summary>
Clears the depth-stencil resource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::ClearDepthStencilView)</para>
</summary>
<param name="depthStencilView">Pointer to the depth stencil to be cleared.</param>
<param name="clearFlags">Identify the type of data to clear (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClearFlag"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClearFlag"/>.</param>
<param name="depth">Clear the depth buffer with this value. This value will be clamped between 0 and 1.</param>
<param name="stencil">Clear the stencil buffer with this value.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ClearRenderTargetView(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView,Microsoft.WindowsAPICodePack.DirectX.DXGI.ColorRgba)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="72">
<summary>
Set all the elements in a render target to one value.
<para>(Also see DirectX SDK: ID3D11DeviceContext::ClearRenderTargetView)</para>
</summary>
<param name="renderTargetView">Pointer to the rendertarget.</param>
<param name="colorRgba">The color to fill the render target with.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ClearState" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="80">
<summary>
Restore all default settings.
<para>(Also see DirectX SDK: ID3D11DeviceContext::ClearState)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ClearUnorderedAccessViewFloat(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView,System.Single[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="86">
<summary>
Clears an unordered access resource with a float value.
<para>(Also see DirectX SDK: ID3D11DeviceContext::ClearUnorderedAccessViewFloat)</para>
</summary>
<param name="unorderedAccessView">Pointer to the Unordered Access View.</param>
<param name="values">A 4-component array that contains the values.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ClearUnorderedAccessViewUint(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView,System.UInt32[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="94">
<summary>
Clears an unordered access resource with bit-precise values.
<para>(Also see DirectX SDK: ID3D11DeviceContext::ClearUnorderedAccessViewUint)</para>
</summary>
<param name="unorderedAccessView">Pointer to the Unordered Access View.</param>
<param name="values">A 4-component array that contains the values.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.CopyResource(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="102">
<summary>
Copy the entire contents of the source resource to the destination resource using the GPU.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CopyResource)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
<param name="sourceResource">The source resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.CopySubresourceRegion(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,System.UInt32,System.UInt32,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Box)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="110">
<summary>
Copy a region from a source resource to a destination resource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CopySubresourceRegion)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
<param name="destinationSubresource">Destination subresource index.</param>
<param name="destinationX">The x offset between the source box location and the destination location.</param>
<param name="destinationY">The y offset between the source box location and the destination location. For a 1D subresource, this must be zero.</param>
<param name="destinationZ">The z offset between the source box location and the destination location. For a 1D or 2D subresource, this must be zero.</param>
<param name="sourceResource">The source resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
<param name="sourceSubresource">Source subresource index.</param>
<param name="sourceBox">A 3D box (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Box"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Box"/> that defines the source subresources that can be copied. The box must fit within the source resource.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.CopySubresourceRegion(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,System.UInt32,System.UInt32,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="124">
<summary>
Copy a region from a source resource to a destination resource.
Because the source box is not defined by this function, the entire source subresource is copied. 
<para>(Also see DirectX SDK: ID3D11DeviceContext::CopySubresourceRegion)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
<param name="destinationSubresource">Destination subresource index.</param>
<param name="destinationX">The x offset between the source box location and the destination location.</param>
<param name="destinationY">The y offset between the source box location and the destination location. For a 1D subresource, this must be zero.</param>
<param name="destinationZ">The z offset between the source box location and the destination location. For a 1D or 2D subresource, this must be zero.</param>
<param name="sourceResource">The source resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
<param name="sourceSubresource">Source subresource index.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.Dispatch(System.UInt32,System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="138">
<summary>
Execute a command list from a thread group.
<para>(Also see DirectX SDK: ID3D11DeviceContext::Dispatch)</para>
</summary>
<param name="threadGroupCountX">The index of the thread in the x direction.</param>
<param name="threadGroupCountY">The index of the thread in the y direction.</param>
<param name="threadGroupCountZ">The index of the thread in the z direction.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DispatchIndirect(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="147">
<summary>
Execute a command list to draw GPU-generated primitives over one of more thread groups.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DispatchIndirect)</para>
</summary>
<param name="bufferForArgs">A D3DBuffer, which must be loaded with data that matches the argument list for DeviceContext.Dispatch.</param>
<param name="alignedOffsetForArgs">A byte-aligned offset between the start of the buffer and the arguments.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.Draw(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="155">
<summary>
Draw non-indexed, non-instanced primitives.
<para>(Also see DirectX SDK: ID3D11DeviceContext::Draw)</para>
</summary>
<param name="vertexCount">Number of vertices to draw.</param>
<param name="startVertexLocation">Index of the first vertex, which is usually an offset in a vertex buffer; it could also be used as the first vertex id generated for a shader parameter marked with the SV_TargetId system-value semantic.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DrawAuto" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="163">
<summary>
Draw geometry of an unknown size.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DrawAuto)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DrawIndexed(System.UInt32,System.UInt32,System.Int32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="169">
<summary>
Draw indexed, non-instanced primitives.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DrawIndexed)</para>
</summary>
<param name="indexCount">Number of indices to draw.</param>
<param name="startIndexLocation">The location of the first index read by the GPU from the index buffer.</param>
<param name="baseVertexLocation">A value added to each index before reading a vertex from the vertex buffer.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DrawIndexedInstanced(System.UInt32,System.UInt32,System.UInt32,System.Int32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="178">
<summary>
Draw indexed, instanced primitives.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DrawIndexedInstanced)</para>
</summary>
<param name="IndexCountPerInstance">Number of indices read from the index buffer for each instance.</param>
<param name="InstanceCount">Number of instances to draw.</param>
<param name="StartIndexLocation">The location of the first index read by the GPU from the index buffer.</param>
<param name="BaseVertexLocation">A value added to each index before reading a vertex from the vertex buffer.</param>
<param name="StartInstanceLocation">A value added to each index before reading per-instance data from a vertex buffer.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DrawIndexedInstancedIndirect(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="189">
<summary>
Draw indexed, instanced, GPU-generated primitives.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DrawIndexedInstancedIndirect)</para>
</summary>
<param name="BufferForArgs">A Unknown Topic, which is a buffer containing the GPU generated primitives.</param>
<param name="AlignedByteOffsetForArgs">TBD</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DrawInstanced(System.UInt32,System.UInt32,System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="197">
<summary>
Draw non-indexed, instanced primitives.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DrawInstanced)</para>
</summary>
<param name="VertexCountPerInstance">Number of vertices to draw.</param>
<param name="InstanceCount">Number of instances to draw.</param>
<param name="StartVertexLocation">Index of the first vertex.</param>
<param name="StartInstanceLocation">A value added to each index before reading per-instance data from a vertex buffer.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DrawInstancedIndirect(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="207">
<summary>
TBD
<para>(Also see DirectX SDK: ID3D11DeviceContext::DrawInstancedIndirect)</para>
</summary>
<param name="BufferForArgs">TBD</param>
<param name="AlignedByteOffsetForArgs">TBD</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.End(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Asynchronous)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="215">
<summary>
Mark the end of a series of commands.
<para>(Also see DirectX SDK: ID3D11DeviceContext::End)</para>
</summary>
<param name="Async">A Asynchronous object.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ExecuteCommandList(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CommandList,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="222">
<summary>
Queues commands from a command list onto a device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::ExecuteCommandList)</para>
</summary>
<param name="commandList">A CommandList interface that encapsulates a command list.</param>
<param name="restoreContextState">A Boolean flag that determines whether the immediate context state is saved (prior) and restored (after) the execution of a command list. Use TRUE to indicate that the runtime needs to save and restore the state, which will cause lower performance. Use FALSE to indicate that no state shall be saved or restored, which causes the immediate context to  return to its default state after the command list executes.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.FinishCommandList(System.Boolean,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CommandList@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="230">
<summary>
Create a command list and record graphics commands into it.
<para>(Also see DirectX SDK: ID3D11DeviceContext::FinishCommandList)</para>
</summary>
<param name="restoreDeferredContextState">A Boolean flag that determines whether the immediate context state is saved (prior) and restored (after) the execution of a command list. Use TRUE to indicate that the runtime needs to save and restore the state, which will cause lower performance. Use FALSE to indicate that no state shall be saved or restored, which causes the immediate context to  return to its default state after the command list executes.</param>
<param name="outCommandList">Upon completion of the method, the passed CommandList interface pointer is initialized with the recorded command list information.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.Flush" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="238">
<summary>
Send queued-up commands in the command buffer to the GPU.
<para>(Also see DirectX SDK: ID3D11DeviceContext::Flush)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.GenerateMips(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="244">
<summary>
Generate mipmaps for the given shader resource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GenerateMips)</para>
</summary>
<param name="shaderResourceView">An ShaderResourceView interface that represents the shader resource.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ContextFlags" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="251">
<summary>
Gets the initialization flags associated with the current deferred context.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GetContextFlags)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.GetData(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Asynchronous,System.IntPtr,System.UInt32,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="260">
<summary>
Get data from the GPU asynchronously.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GetData)</para>
</summary>
<param name="asyncData">A Asynchronous object.</param>
<param name="data">Address of memory that will receive the data. If NULL, GetData will be used only to check status. The type of data output depends on the type of asynchronous object. See Remarks.</param>
<param name="dataSize">Size of the data to retrieve or 0. Must be 0 when pData is NULL.</param>
<param name="flags">Optional flags. Can be 0 or any combination of the flags enumerated by AsyncGetDataFlag.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.GetPredication(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate@,System.Boolean@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="270">
<summary>
Get the rendering predicate state.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GetPredication)</para>
</summary>
<param name="outPredicate">Address of a predicate (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate"/>. Value stored here will be NULL upon device creation.</param>
<param name="outPredicateValue">Address of a boolean to fill with the predicate comparison value. FALSE upon device creation.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.GetResourceMinLOD(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="278">
<summary>
Gets the minimum level-of-detail (LOD).
<para>(Also see DirectX SDK: ID3D11DeviceContext::GetResourceMinLOD)</para>
</summary>
<param name="Resource">A D3DResource which represents the resource.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.GetType" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="285">
<summary>
Gets the type of device context.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GetType)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.Map(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,&lt;unknown type&gt;,&lt;unknown type&gt;,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MappedSubresource)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="291">
<summary>
Get the data contained in a subresource, and deny the GPU access to that subresource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::Map)</para>
</summary>
<param name="resource">A D3DResource object.</param>
<param name="subresource">Index number of the subresource.</param>
<param name="mapType">Specifies the CPU's read and write permissions for a resource. For possible values, see Map.</param>
<param name="mapFlags">Flag that specifies what the CPU should do when the GPU is busy. This flag is optional.</param>
<param name="mappedResource">The mapped subresource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MappedSubresource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MappedSubresource"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.ResolveSubresource(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="302">
<summary>
Copy a multisampled resource into a non-multisampled resource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::ResolveSubresource)</para>
</summary>
<param name="destinationResource">Destination resource. Must be a created with the Default flag and be single-sampled. See D3DResource.</param>
<param name="destinationSubresource">A zero-based index, that identifies the destination subresource. Use D3D11CalcSubresource to calculate the index.</param>
<param name="sourceResource">Source resource. Must be multisampled.</param>
<param name="sourceSubresource">&gt;The source subresource of the source resource.</param>
<param name="format">A Format that indicates how the multisampled resource will be resolved to a single-sampled resource.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.SetPredication(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="313">
<summary>
Set a rendering predicate.
<para>(Also see DirectX SDK: ID3D11DeviceContext::SetPredication)</para>
</summary>
<param name="D3DPredicate">A predicate (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate"/>. A NULL value indicates "no" predication; in this case, the value of PredicateValue is irrelevent but will be preserved for DeviceContext.GetPredication.</param>
<param name="PredicateValue">If TRUE, rendering will be affected by when the predicate's conditions are met. If FALSE, rendering will be affected when the conditions are not met.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.SetResourceMinLOD(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.Single)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="321">
<summary>
Sets the minimum level-of-detail (LOD).
<para>(Also see DirectX SDK: ID3D11DeviceContext::SetResourceMinLOD)</para>
</summary>
<param name="Resource">A D3DResource which represents the resource.</param>
<param name="MinLOD">The level-of-detail, which ranges between 0 and 1.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.Unmap(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="329">
<summary>
Invalidate the resource and re-enable the GPU's access to that resource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::Unmap)</para>
</summary>
<param name="resource">A D3DResource object.</param>
<param name="subresource">A subresource to be unmapped.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.UpdateSubresource(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,System.IntPtr,System.UInt32,System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Box)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="337">
<summary>
The CPU copies data from memory to a subresource created in non-mappable memory.
<para>(Also see DirectX SDK: ID3D11DeviceContext::UpdateSubresource)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
<param name="destinationSubresource">A zero-based index, that identifies the destination subresource. See D3D11CalcSubresource for more details.</param>
<param name="destinationBox">A box that defines the portion of the destination subresource to copy the resource data into. Coordinates are in bytes for buffers and in texels for textures.  The dimensions of the source must fit the destination (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Box"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Box"/>.</param>
<param name="sourceData">The source data in memory.</param>
<param name="sourceRowPitch">The size of one row of the source data.</param>
<param name="sourceDepthPitch">The size of one depth slice of source data.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.UpdateSubresource(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource,System.UInt32,System.IntPtr,System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="349">
<summary>
The CPU copies data from memory to a subresource created in non-mappable memory.
A destination box is not defined by this method, so the data is written to the destination subresource with no offset.
<para>(Also see DirectX SDK: ID3D11DeviceContext::UpdateSubresource)</para>
</summary>
<param name="destinationResource">The destination resource (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DResource"/>.</param>
<param name="destinationSubresource">A zero-based index, that identifies the destination subresource. See D3D11CalcSubresource for more details.</param>
<param name="sourceData">The source data in memory.</param>
<param name="sourceRowPitch">The size of one row of the source data.</param>
<param name="sourceDepthPitch">The size of one depth slice of source data.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.CS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="361">
<summary>
Get the associated compute shader pipelines tage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.DS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="369">
<summary>
Get the associated domain shader pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.GS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="377">
<summary>
Get the associated geometry shader pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.HS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="385">
<summary>
Get the associated hull shader pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.IA" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="393">
<summary>
Get the associated input assembler pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.OM" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="401">
<summary>
Get the associated output merger pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.PS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="409">
<summary>
Get the associated pixel shader pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.RS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="417">
<summary>
Get the associated rasterizer pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.SO" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="425">
<summary>
Get the associated stream output pipeline stage object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DeviceContext.VS" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11devicecontext.h" line="433">
<summary>
Get the associated vertex shader pipeline stage object.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11depthstencilstate.h" line="10">
<summary>
A depth-stencil-state interface accesses depth-stencil state which sets up the depth-stencil test for the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DepthStencilState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11depthstencilstate.h" line="17">
<summary>
Get the depth-stencil state.
<para>(Also see DirectX SDK: ID3D11DepthStencilState::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.View" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11view.h" line="11">
<summary>
A view specifies the parts of a resource the pipeline can access during rendering.
<para>(Also see DirectX SDK: ID3D11View)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.View.GetResource" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11view.h" line="19">
<summary>
Get the resource that is accessed through this view.
<para>(Also see DirectX SDK: ID3D11View::GetResource)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11depthstencilview.h" line="10">
<summary>
A depth-stencil-view interface accesses a texture resource during depth-stencil testing.
<para>(Also see DirectX SDK: ID3D11DepthStencilView)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11depthstencilview.h" line="18">
<summary>
Get the depth-stencil view.
<para>(Also see DirectX SDK: ID3D11DepthStencilView::GetDesc)</para>
</summary>
<returns>A depth-stencil-view description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilViewDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshader.h" line="10">
<summary>
A domain-shader class manages an executable program (a domain shader) that controls the domain-shader stage.
<para>(Also see DirectX SDK: ID3D11DomainShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshader.h" line="10">
<summary>
A geometry-shader class manages an executable program (a geometry shader) that controls the geometry-shader stage.
<para>(Also see DirectX SDK: ID3D11GeometryShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshader.h" line="10">
<summary>
A hull-shader class manages an executable program (a hull shader) that controls the hull-shader stage.
<para>(Also see DirectX SDK: ID3D11HullShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputlayout.h" line="10">
<summary>
An input-layout interface accesses the input data for the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11InputLayout)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshader.h" line="10">
<summary>
A pixel-shader class manages an executable program (a pixel shader) that controls the pixel-shader stage.
<para>(Also see DirectX SDK: ID3D11PixelShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DQuery" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11query.h" line="10">
<summary>
A query interface queries information from the GPU.
<para>(Also see DirectX SDK: ID3D11Query)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DQuery.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11query.h" line="18">
<summary>
Get a query description.
<para>(Also see DirectX SDK: ID3D11Query::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DPredicate" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11predicate.h" line="10">
<summary>
A predicate interface determines whether geometry should be processed depending on the results of a previous draw call.
<para>(Also see DirectX SDK: ID3D11Predicate)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerstate.h" line="10">
<summary>
A rasterizer-state interface accesses rasterizer state for the rasterizer stage.
<para>(Also see DirectX SDK: ID3D11RasterizerState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerstate.h" line="18">
<summary>
Get the properties of a rasterizer-state object.
<para>(Also see DirectX SDK: ID3D11RasterizerState::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rendertargetview.h" line="10">
<summary>
A render-target-view interface identifies the render-target subresources that can be accessed during rendering.
<para>(Also see DirectX SDK: ID3D11RenderTargetView)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rendertargetview.h" line="18">
<summary>
Get the properties of a render target view.
<para>(Also see DirectX SDK: ID3D11RenderTargetView::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11samplerstate.h" line="10">
<summary>
A sampler-state interface accesses sampler state for a texture.
<para>(Also see DirectX SDK: ID3D11SamplerState)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11samplerstate.h" line="18">
<summary>
Get the sampler state.
<para>(Also see DirectX SDK: ID3D11SamplerState::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11shaderresourceview.h" line="10">
<summary>
A shader-resource-view interface specifies the subresources a shader can access during rendering. Examples of shader resources include a constant buffer, a texture buffer, a texture or a sampler.
<para>(Also see DirectX SDK: ID3D11ShaderResourceView)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11shaderresourceview.h" line="18">
<summary>
Get the shader resource view's description.
<para>(Also see DirectX SDK: ID3D11ShaderResourceView::GetDesc)</para>
</summary>
<returns>A ShaderResourceViewDescription structure to be filled with data about the shader resource view.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1D" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11texture1d.h" line="10">
<summary>
A 1D texture interface accesses texel data, which is structured memory.
<para>(Also see DirectX SDK: ID3D11Texture1D)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1D.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11texture1d.h" line="18">
<summary>
Get the properties of the texture resource.
<para>(Also see DirectX SDK: ID3D11Texture1D::GetDesc)</para>
</summary>
<returns>A resource description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture1DDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2D" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11texture2d.h" line="10">
<summary>
A 2D texture interface manages texel data, which is structured memory.
<para>(Also see DirectX SDK: ID3D11Texture2D)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture2D.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11texture2d.h" line="18">
<summary>
Get the properties of the texture resource.
<para>(Also see DirectX SDK: ID3D11Texture2D::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3D" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11texture3d.h" line="10">
<summary>
A 3D texture interface accesses texel data, which is structured memory.
<para>(Also see DirectX SDK: ID3D11Texture3D)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Texture3D.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11texture3d.h" line="18">
<summary>
Get the properties of the texture resource.
<para>(Also see DirectX SDK: ID3D11Texture3D::GetDesc)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11unorderedaccessview.h" line="11">
<summary>
A view interface specifies the parts of a resource the pipeline can access during rendering.
<para>(Also see DirectX SDK: ID3D11UnorderedAccessView)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11unorderedaccessview.h" line="19">
<summary>
Get a description of the resource.
<para>(Also see DirectX SDK: ID3D11UnorderedAccessView::GetDesc)</para>
</summary>
<returns>A resource description (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessViewDescription"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessViewDescription"/>.</returns>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshader.h" line="10">
<summary>
A vertex-shader class manages an executable program (a vertex shader) that controls the vertex-shader stage.
<para>(Also see DirectX SDK: ID3D11VertexShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="10">
<summary>
An information-queue interface stores, retrieves, and filters debug messages. The queue consists of a message queue, an optional storage filter stack, and a optional retrieval filter stack.
<para>(Also see DirectX SDK: ID3D11InfoQueue)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.AddApplicationMessage(&lt;unknown type&gt;,System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="18">
<summary>
Add a user-defined message to the message queue and send that message to debug output.
<para>(Also see DirectX SDK: ID3D11InfoQueue::AddApplicationMessage)</para>
</summary>
<param name="severity">Severity of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageSeverity"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageSeverity"/>.</param>
<param name="description">Message string.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.AddMessage(&lt;unknown type&gt;,&lt;unknown type&gt;,&lt;unknown type&gt;,System.String)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="26">
<summary>
Add a debug message to the message queue and send that message to debug output.
<para>(Also see DirectX SDK: ID3D11InfoQueue::AddMessage)</para>
</summary>
<param name="category">Category of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageCategory"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageCategory"/>.</param>
<param name="severity">Severity of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageSeverity"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageSeverity"/>.</param>
<param name="id">Unique identifier of a message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageId"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageId"/>.</param>
<param name="description">User-defined message.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.ClearStoredMessages" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="36">
<summary>
Clear all messages from the message queue.
<para>(Also see DirectX SDK: ID3D11InfoQueue::ClearStoredMessages)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.GetBreakOnCategory(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="42">
<summary>
Get a message category to break on when a message with that category passes through the storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetBreakOnCategory)</para>
</summary>
<param name="category">Message category to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageCategory"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageCategory"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.GetBreakOnId(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="49">
<summary>
Get a message identifier to break on when a message with that identifier passes through the storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetBreakOnID)</para>
</summary>
<param name="id">Message identifier to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageId"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageId"/>.</param>
<returns>
Whether this breaking condition is turned on or off (true for on, false for off).
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.GetBreakOnSeverity(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="59">
<summary>
Get a message severity level to break on when a message with that severity level passes through the storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetBreakOnSeverity)</para>
</summary>
<param name="severity">Message severity level to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageSeverity"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageSeverity"/>.</param>
<returns>
Whether this breaking condition is turned on or off (true for on, false for off).
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.GetMessage(System.UInt64)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="69">
<summary>
Get a message from the message queue.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetMessage)</para>
</summary>
<param name="messageIndex">Index into message queue after an optional retrieval filter has been applied. 
This can be between 0 and the number of messages in the message queue that pass through the retrieval filter (which can be obtained with GetNumStoredMessagesAllowedByRetrievalFilter(). 
0 is the message at the front of the message queue.</param>
<returns>Returned message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Message"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Message"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.TryGetMessage(System.UInt64,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Message@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="79">
<summary>
Try to get a message from the message queue.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetMessage)</para>
</summary>
<param name="messageIndex">Index into message queue after an optional retrieval filter has been applied. 
This can be between 0 and the number of messages in the message queue that pass through the retrieval filter 
(which can be obtained with GetNumStoredMessagesAllowedByRetrievalFilter(). 
Index 0 is the message at the front of the message queue. </param>        
<param name="message">Returned message (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Message"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Message"/>.</param>      
<returns>True if successful; false otherwise.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.MessageCountLimit" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="91">
<summary>
Get or set the maximum number of messages that can be added to the message queue.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetMessageCountLimit)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.MuteDebugOutput" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="101">
<summary>
Get or set a boolean that turns the debug output on or off.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetMuteDebugOutput)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.NumMessagesAllowedByStorageFilter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="112">
<summary>
Get the number of messages that were allowed to pass through a storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetNumMessagesAllowedByStorageFilter)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.NumMessagesDeniedByStorageFilter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="121">
<summary>
Get the number of messages that were denied passage through a storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetNumMessagesDeniedByStorageFilter)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.NumMessagesDiscardedByMessageCountLimit" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="130">
<summary>
Get the number of messages that were discarded due to the message count limit.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetNumMessagesDiscardedByMessageCountLimit)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.NumStoredMessages" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="139">
<summary>
Get the number of messages currently stored in the message queue.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetNumStoredMessages)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.NumStoredMessagesAllowedByRetrievalFilter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="148">
<summary>
Get the number of messages that are able to pass through a retrieval filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::GetNumStoredMessagesAllowedByRetrievalFilter)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.SetBreakOnCategory(&lt;unknown type&gt;,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="157">
<summary>
Set a message category to break on when a message with that category passes through the storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::SetBreakOnCategory)</para>
</summary>
<param name="category">Message category to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageCategory"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageCategory"/>.</param>
<param name="enable">Turns this breaking condition on or off (true for on, false for off).</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.SetBreakOnId(&lt;unknown type&gt;,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="165">
<summary>
Set a message identifier to break on when a message with that identifier passes through the storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::SetBreakOnID)</para>
</summary>
<param name="id">Message identifier to break on (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageId"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.MessageId"/>.</param>
<param name="enable">Turns this breaking condition on or off (true for on, false for off).</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InfoQueue.SetBreakOnSeverity(&lt;unknown type&gt;,System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11infoqueue.h" line="173">
<summary>
Set a message severity level to break on when a message with that severity level passes through the storage filter.
<para>(Also see DirectX SDK: ID3D11InfoQueue::SetBreakOnSeverity)</para>
</summary>
<param name="severity">A MessageSeverity, which represents a message severity level to break on.</param>
<param name="enable">Turns this breaking condition on or off (true for on, false for off).</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SwitchToRef" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11switchtoref.h" line="10">
<summary>
A swith-to-reference interface enables an application to switch between a hardware and software device.
<para>(Also see DirectX SDK: ID3D11SwitchToRef)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SwitchToRef.GetUseRef" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11switchtoref.h" line="18">
<summary>
Get a boolean value that indicates the type of device being used.
<para>(Also see DirectX SDK: ID3D10SwitchToRef::GetUseRef)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SwitchToRef.SetUseRef(System.Boolean)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11switchtoref.h" line="24">
<summary>
Switch between a hardware and a software device.
<para>(Also see DirectX SDK: ID3D10SwitchToRef::SetUseRef)</para>
</summary>
<param name="UseRef">A boolean value. Set this to TRUE to change to a software device, set this to FALSE to change to a hardware device.</param>
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
</members>
</doc>