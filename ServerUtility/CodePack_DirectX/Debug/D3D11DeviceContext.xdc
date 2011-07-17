<?xml version="1.0"?><doc>
<members>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshader.h" line="10">
<summary>
A compute-shader class manages an executable program (a compute shader) that controls the compute-shader stage.
<para>(Also see DirectX SDK: ID3D11ComputeShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classinstance.h" line="11">
<summary>
This class encapsulates an HLSL class.
<para>(Also see DirectX SDK: ID3D11ClassInstance)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance.GetClassLinkage" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classinstance.h" line="19">
<summary>
Gets the ClassLinkage object associated with the current HLSL class.
<para>(Also see DirectX SDK: ID3D11ClassInstance::GetClassLinkage)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance.Description" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classinstance.h" line="25">
<summary>
Gets a description of the current HLSL class.
<para>(Also see DirectX SDK: ID3D11ClassInstance::GetDesc)</para>
</summary>
<returns>A ClassInstanceDescription structure that describes the current HLSL class.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance.InstanceName" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classinstance.h" line="35">
<summary>
Gets the instance name of the current HLSL class.
<para>(Also see DirectX SDK: ID3D11ClassInstance::GetInstanceName)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance.TypeName" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11classinstance.h" line="44">
<summary>
Gets the type of the current HLSL class.
<para>(Also see DirectX SDK: ID3D11ClassInstance::GetTypeName)</para>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshader.h" line="10">
<summary>
A domain-shader class manages an executable program (a domain shader) that controls the domain-shader stage.
<para>(Also see DirectX SDK: ID3D11DomainShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CommandList" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11commandlist.h" line="10">
<summary>
The CommandList interface encapsulates a collection of graphics commands for play back.
<para>(Also see DirectX SDK: ID3D11CommandList)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.CommandList.ContextFlags" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11commandlist.h" line="18">
<summary>
Gets the initialization flags associated with the deferred context that created the command list.
The context flag is reserved for future use and is always 0.
<para>(Also see DirectX SDK: ID3D11CommandList::GetContextFlags)</para>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshader.h" line="10">
<summary>
A pixel-shader class manages an executable program (a pixel shader) that controls the pixel-shader stage.
<para>(Also see DirectX SDK: ID3D11PixelShader)</para>
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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshader.h" line="10">
<summary>
A vertex-shader class manages an executable program (a vertex shader) that controls the vertex-shader stage.
<para>(Also see DirectX SDK: ID3D11VertexShader)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pipelinestage.h" line="11">
<summary>
A pipeline stage. base class for all pipline stage related classes.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="8">
<summary>
Compute Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="14">
<summary>
Get the constant buffers used by the compute-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>A collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="23">
<summary>
Get an array of sampler state objects from the compute-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.GetShader(System.UInt32,System.Collections.ObjectModel.ReadOnlyCollection`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance}@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="32">
<summary>
Get the compute shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSGetShader)</para>
</summary>
<param name="numClassInstances">The number of class-instances to retrieve.</param>
<param name="outClassInstances">Returns a collection of class instances (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>.</param>
<returns>A Compute shader object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="42">
<summary>
Get the compute shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSGetShader)</para>
</summary>
<returns>A Compute shader object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="49">
<summary>
Get the compute-shader resources.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to get from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>A collection of shader resource view objects to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.GetUnorderedAccessViews(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="58">
<summary>
Gets an array of views for an unordered resource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSGetUnorderedAccessViews)</para>
</summary>
<param name="startSlot">Index of the first element in the zero-based array to return (ranges from 0 to D3D11_PS_CS_UAV_REGISTER_COUNT - 1).</param>
<param name="numUAVs">Number of views to get (ranges from 0 to D3D11_PS_CS_UAV_REGISTER_COUNT - startSlot).</param>
<returns>A collection of Unorderd Access Views (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView"/> to get.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="67">
<summary>
Set the constant buffers used by the compute-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the zero-based array to begin setting constant buffers to (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">A collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="75">
<summary>
Set an array of sampler states to the compute-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="83">
<summary>
Set a compute shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSSetShader)</para>
</summary>
<param name="computeShader">A compute shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader"/>. Passing in NULL disables the shader for this pipeline stage.</param>
<param name="classInstances">A collection of class-instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>. Each interface used by a shader must have a corresponding class instance or the shader will get disabled. 
Set to NULL if the shader does not use any interfaces.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="92">
<summary>
Set a compute shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSSetShader)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="98">
<summary>
Bind an array of shader resources to the compute-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ComputeShaderPipelineStage.SetUnorderedAccessViews(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView},System.UInt32[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11computeshaderpipelinestage.h" line="106">
<summary>
Sets an array of views for an unordered resource.
<para>(Also see DirectX SDK: ID3D11DeviceContext::CSSetUnorderedAccessViews)</para>
</summary>
<param name="startSlot">Index of the first element in the zero-based array to begin setting.</param>
<param name="unorderedAccessViews">A collection of Unordered Access Views (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView"/> to be set by the method.</param>
<param name="initialCounts">Number of objects in the array.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="8">
<summary>
Domain Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="14">
<summary>
Get the constant buffers used by the domain-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>Collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="23">
<summary>
Get an array of sampler state objects from the domain-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.GetShader(System.UInt32,System.Collections.ObjectModel.ReadOnlyCollection`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance}@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="32">
<summary>
Get the domain shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSGetShader)</para>
</summary>
<param name="outClassInstances">A collection of class instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>.</param>
<param name="numClassInstances">The number of class-instance elements requested.</param>
<returns>A domain shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="41">
<summary>
Get the domain shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSGetShader)</para>
</summary>
<returns>A domain shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="48">
<summary>
Get the domain-shader resources.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to get from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>A collection of shader resource view objects to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="57">
<summary>
Set the constant buffers used by the domain-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the zero-based array to begin setting constant buffers to (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="65">
<summary>
Set an array of sampler states to the domain-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="73">
<summary>
Set a domain shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSSetShader)</para>
</summary>
<param name="domainShader">A domain shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
<param name="classInstances">A collection of class-instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>. 
Each interface used by a shader must have a corresponding class instance or the shader will get disabled. 
Set to null if the shader does not use any interfaces.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="84">
<summary>
Set a domain shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSSetShader)</para>
</summary>
<param name="domainShader">A domain shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DomainShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11domainshaderpipelinestage.h" line="92">
<summary>
Bind an array of shader resources to the domain-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::DSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="8">
<summary>
Geometry Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="14">
<summary>
Get the constant buffers used by the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>A collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="23">
<summary>
Get an array of sampler state objects from the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.GetShader(System.UInt32,System.Collections.ObjectModel.ReadOnlyCollection`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance}@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="32">
<summary>
Get the geometry shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSGetShader)</para>
</summary>
<param name="classInstances">A collection of class instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>.</param>
<param name="numClassInstances">The number of class-instance elements requested.</param>
<returns>A geometry shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="42">
<summary>
Get the geometry shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSGetShader)</para>
</summary>
<returns>A geometry shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="49">
<summary>
Get the geometry shader resources.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to request from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>Collection of shader resource view objects to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="58">
<summary>
Set the constant buffers used by the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting constant buffers to (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="66">
<summary>
Set an array of sampler states to the geometry shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="74">
<summary>
Set a geometry shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSSetShader)</para>
</summary>
<param name="geometryShader">A geometry shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>. Passing in NULL disables the shader for this pipeline stage.</param>
<param name="classInstances">A collection of class-instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>. Each interface used by a shader must have a corresponding class instance or the shader will get disabled. Set to NULL if the shader does not use any interfaces.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="82">
<summary>
Set a geometry shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSSetShader)</para>
</summary>
<param name="geometryShader">A geometry shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>. Passing in NULL disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11geometryshaderpipelinestage.h" line="89">
<summary>
Bind an array of shader resources to the geometry shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::GSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="8">
<summary>
Hull Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="14">
<summary>
Get the constant buffers used by the hull-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>Collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="23">
<summary>
Get an array of sampler state objects from the hull-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.GetShader(System.UInt32,System.Collections.ObjectModel.ReadOnlyCollection`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance}@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="32">
<summary>
Get the hull shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSGetShader)</para>
</summary>
<param name="numClassInstances">The number of class-instance elements requested.</param>
<param name="classInstances">A collection of class instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>.</param>
<returns>A hull shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="42">
<summary>
Get the hull shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSGetShader)</para>
</summary>
<returns>A hull shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.GeometryShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="49">
<summary>
Get the hull-shader resources.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to get from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>Collection of shader resource view objects to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="58">
<summary>
Set the constant buffers used by the hull-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting constant buffers to (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="66">
<summary>
Set an array of sampler states to the hull-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the zero-based array to begin setting samplers to (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="74">
<summary>
Set a hull shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSSetShader)</para>
</summary>
<param name="Shader">A hull shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
<param name="classInstances">A collection of class-instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>. 
Each interface used by a shader must have a corresponding class instance or the shader will get disabled. Set to null if the shader does not use any interfaces.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="85">
<summary>
Set a hull shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSSetShader)</para>
</summary>
<param name="Shader">A hull shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.HullShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11hullshaderpipelinestage.h" line="93">
<summary>
Bind an array of shader resources to the hull-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::HSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device. 
Up to a maximum of 128 slots are available for shader resources(ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="8">
<summary>
InputAssembler pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.GetIndexBuffer(&lt;unknown type&gt;@,System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="14">
<summary>
Get the index buffer that is bound to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IAGetIndexBuffer)</para>
</summary>
<param name="outFormat">The format of the data in the index buffer (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.DXGI.Format"/>. These formats provide the size and type of the data in the buffer. The only formats allowed for index buffer data are 16-bit (Format_R16_UINT) and 32-bit (Format_R32_UINT) integers.</param>
<param name="outOffset">Offset (in bytes) from the start of the index buffer, to the first index to use.</param>
<returns>An index buffer returned by the method (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.GetInputLayout" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="23">
<summary>
Get the input-layout object that is bound to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IAGetInputLayout)</para>
</summary>
<returns>The input-layout object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>, which describes the input buffers that will be read by the IA stage.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.GetPrimitiveTopology" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="30">
<summary>
Get information about the primitive type, and data order that describes input data for the input assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IAGetPrimitiveTopology)</para>
</summary>
<returns>The type of primitive, and ordering of the primitive data (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PrimitiveTopology"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PrimitiveTopology"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.GetVertexBuffers(System.UInt32,System.UInt32,System.UInt32[]@,System.UInt32[]@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="37">
<summary>
Get the vertex buffers bound to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IAGetVertexBuffers)</para>
</summary>
<param name="startSlot">The input slot of the first vertex buffer to get. The first vertex buffer is explicitly bound to the start slot; this causes each additional vertex buffer in the array to be implicitly bound to each subsequent input slot. There are 16 input slots.</param>
<param name="numBuffers">The number of vertex buffers to get starting at the offset. The number of buffers (plus the starting slot) cannot exceed the total number of IA-stage input slots.</param>
<param name="strides">A collection of stride values returned by the method; one stride value for each buffer in the vertex-buffer array. Each stride value is the size (in bytes) of the elements that are to be used from that vertex buffer.</param>
<param name="offsets">A collection of offset values returned by the method; one offset value for each buffer in the vertex-buffer array. Each offset is the number of bytes between the first element of a vertex buffer and the first element that will be used.</param>
<returns>A collection of vertex buffers returned by the method (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.SetIndexBuffer(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer,&lt;unknown type&gt;,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="48">
<summary>
Bind an index buffer to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IASetIndexBuffer)</para>
</summary>
<param name="indexBuffer">A D3DBuffer object, that contains indices. The index buffer must have been created with the indexBuffer flag.</param>
<param name="format">A Format that specifies the format of the data in the index buffer. The only formats allowed for index buffer data are 16-bit (Format_R16_UINT) and 32-bit (Format_R32_UINT) integers.</param>
<param name="offset">Offset (in bytes) from the start of the index buffer to the first index to use.</param>
<remarks>Calling this method using a buffer that is currently bound for writing (i.e. bound to the stream output pipeline stage) will effectively 
bind null instead because a buffer cannot be bound as both an input and an output at the same time.</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.SetInputLayout(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="59">
<summary>
Bind an input-layout object to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IASetInputLayout)</para>
</summary>
<param name="inputLayout">The input-layout object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputLayout"/>, which describes the input buffers that will be read by the IA stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.SetPrimitiveTopology(&lt;unknown type&gt;)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="66">
<summary>
Bind information about the primitive type, and data order that describes input data for the input assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IASetPrimitiveTopology)</para>
</summary>
<param name="topology">The type of primitive and ordering of the primitive data (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PrimitiveTopology"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PrimitiveTopology"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.InputAssemblerPipelineStage.SetVertexBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer},System.UInt32[],System.UInt32[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11inputassemblerpipelinestage.h" line="73">
<summary>
Bind an array of vertex buffers to the input-assembler stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::IASetVertexBuffers)</para>
</summary>
<param name="startSlot">The first input slot for binding. The first vertex buffer is explicitly bound to the start slot; this causes each additional vertex buffer in the array to be implicitly bound to each subsequent input slot. There are 16 input slots (ranges from 0 to D3D11_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="vertexBuffers">A collection of vertex buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>. 
The number of buffers (plus the starting slot) cannot exceed the total number of IA-stage input slots (ranges from 0 to D3D11_IA_VERTEX_INPUT_RESOURCE_SLOT_COUNT - startSlot).
The vertex buffers must have been created with the VertexBuffer flag.</param>
<param name="strides">An array of stride values; one stride value for each buffer in the vertex-buffer array. Each stride is the size (in bytes) of the elements that are to be used from that vertex buffer.</param>
<param name="offsets">An array of offset values; one offset value for each buffer in the vertex-buffer array. Each offset is the number of bytes between the first element of a vertex buffer and the first element that will be used.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="8">
<summary>
OutputMerger pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetBlendState(Microsoft.WindowsAPICodePack.DirectX.DXGI.ColorRgba@,System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="15">
<summary>
Get the blend state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetBlendState)</para>
</summary>
<param name="blendFactor">Array of blend factors containing four entries; one for each RGBA component.</param>
<param name="sampleMask">A sample mask.</param>
<returns>A blend-state object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetBlendState" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="24">
<summary>
Get the blend state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetBlendState)</para>
</summary>
<returns>A blend-state object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetDepthStencilState(System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="31">
<summary>
Gets the depth-stencil state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetDepthStencilState)</para>
</summary>
<param name="stencilRef">The stencil reference value used in the depth-stencil test.</param>
<returns>A depth-stencil state object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/> to be filled with information from the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetDepthStencilState" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="39">
<summary>
Gets the depth-stencil state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetDepthStencilState)</para>
</summary>
<returns>A depth-stencil state object (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/> to be filled with information from the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetRenderTargets(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="46">
<summary>
Get pointers to the resources bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetRenderTargets)</para>
</summary>
<param name="numViews">Number of render targets to retrieve.</param>
<returns>A collection of ID3D11RenderTargetViews which represent render target views.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetRenderTargets(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="54">
<summary>
Get the resources bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetRenderTargets)</para>
</summary>
<param name="numViews">Number of render targets to retrieve.</param>
<param name="depthStencilView">Retrieves a depth-stencil view.</param>
<returns>A collection of ID3D11RenderTargetViews which represent render target views.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetDepthStencilView" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="63">
<summary>
Get the depth stencil view bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetRenderTargets)</para>
</summary>
<returns>The depth-stencil view bound to the output-merger stage.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetRenderTargetsAndUnorderedAccessViews(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView@,System.UInt32,System.UInt32,System.Collections.ObjectModel.ReadOnlyCollection`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView}@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="70">
<summary>
Get the resources bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetRenderTargetsAndUnorderedAccessViews)</para>
</summary>
<param name="numViews">The number of render-target views to retrieve.</param>
<param name="outDepthStencilView">Retrieves the depth-stencil view. </param>
<param name="UAVstartSlot">Start Unordered Access View Slot.</param>
<param name="numUAVs">Number of Unordered Access Views</param>
<param name="unorderedAccessViews">A collection of Unordered Access Views.</param>
<returns>A collection of render-target views. </returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.GetUnorderedAccessViews(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="82">
<summary>
Get the resources bound to the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMGetRenderTargetsAndUnorderedAccessViews)</para>
</summary>
<param name="UAVstartSlot">Start Unordered Access View Slot.</param>
<param name="numUAVs">Number of Unordered Access Views</param>
<returns>A collection of Unordered Access Views.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.SetBlendState(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState,Microsoft.WindowsAPICodePack.DirectX.DXGI.ColorRgba,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="91">
<summary>
Set the blend state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMSetBlendState)</para>
</summary>
<param name="blendState">A blend-state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.BlendState"/>. Passing in NULL implies a default blend state. See remarks for further details.</param>
<param name="blendFactor">Four components array of blend factors, one for each RGBA component. This requires a blend state object that specifies the BlendFactor option.</param>
<param name="sampleMask">32-bit sample coverage. The default value is 0xffffffff.</param>
<remarks>
<para>
Passing in null for the blend-state interface indicates to the runtime to set a default blending state. 
See the DX SDK (ID3D11Device::OMSetBlendState) for more details about the default blending state.
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
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.SetDepthStencilState(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="113">
<summary>
Sets the depth-stencil state of the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMSetDepthStencilState)</para>
</summary>
<param name="depthStencilState">A depth-stencil state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilState"/> to bind to the device. Set this to NULL to use the default state listed in DepthStencilDescription.</param>
<param name="stencilRef">Reference value to perform against when doing a depth-stencil test.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.SetRenderTargets(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView},Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="121">
<summary>
Bind one or more render targets atomically and the depth-stencil buffer to the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMSetRenderTargets)</para>
</summary>
<param name="renderTargetViews">A collection of render targets (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView"/> to bind to the device (ranges between 0 and D3D11_SIMULTANEOUS_RENDER_TARGET_COUNT). 
If this parameter is null, no render targets are bound.</param>
<param name="depthStencilView">A depth-stencil view (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView"/> to bind to the device. 
If this parameter is null, the depth-stencil state is not bound.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.SetRenderTargets(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="132">
<summary>
Bind one or more render targets atomically and the depth-stencil buffer to the output-merger stage.
The depth-stencil state is not bound.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMSetRenderTargets)</para>
</summary>
<param name="renderTargetViews">A collection of render targets (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView"/> to bind to the device (ranges between 0 and D3D11_SIMULTANEOUS_RENDER_TARGET_COUNT). 
If this parameter is null, no render targets are bound.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.OutputMergerPipelineStage.SetRenderTargetsAndUnorderedAccessViews(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RenderTargetView},Microsoft.WindowsAPICodePack.DirectX.Direct3D11.DepthStencilView,System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.UnorderedAccessView},System.UInt32[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11outputmergerpipelinestage.h" line="141">
<summary>
Bind resources to the output-merger stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::OMSetRenderTargetsAndUnorderedAccessViews)</para>
</summary>
<param name="renderTargetViews">A collection of ID3D11RenderTargetViews, which represent render-target views. Specify NULL to set none.</param>
<param name="depthStencilView">A DepthStencilView, which represents a depth-stencil view. Specify NULL to set none.</param>
<param name="UAVstartSlot">Index into a zero-based array to begin setting unordered access views (ranges from 0 to D3D11_PS_CS_UAV_REGISTER_COUNT - 1).</param>
<param name="unorderedAccessViews">A collection of ID3D11UnorderedAccessViews, which represent unordered access views.</param>
<param name="UAVInitialCounts">An array The number of unordered access views to set.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="8">
<summary>
Pixel Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="14">
<summary>
Get the constant buffers used by the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>Collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="23">
<summary>
Get an array of sampler states from the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>Collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/> to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.GetShader(System.UInt32,System.Collections.ObjectModel.ReadOnlyCollection`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance}@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="32">
<summary>
Get the pixel shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSGetShader)</para>
</summary>
<param name="numClassInstances">The number of class-instance elements in the array.</param>
<param name="classInstances">A collection of class instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>.</param>
<returns>A pixel shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="41">
<summary>
Get the pixel shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSGetShader)</para>
</summary>
<returns>A pixel shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="48">
<summary>
Get the pixel shader resources.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to get from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>Collection of shader resource view objects to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="57">
<summary>
Set the constant buffers used by the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting constant buffers to (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="65">
<summary>
Set an array of sampler states to the pixel shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="73">
<summary>
Sets a pixel shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSSetShader)</para>
</summary>
<param name="pixelShader">A pixel shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
<param name="classInstances">A collection of class-instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>. 
Each interface used by a shader must have a corresponding class instance or the shader will get disabled. 
Set to null if the shader does not use any interfaces.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="85">
<summary>
Sets a pixel shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSSetShader)</para>
</summary>
<param name="pixelShader">A pixel shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PixelShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pixelshaderpipelinestage.h" line="93">
<summary>
Bind an array of shader resources to the pixel shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::PSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerpipelinestage.h" line="8">
<summary>
Rasterizer pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerPipelineStage.GetScissorRects(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerpipelinestage.h" line="15">
<summary>
Get the collection of scissor rectangles bound to the rasterizer stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::RSGetScissorRects)</para>
</summary>
<returns>A collection of scissor rectangles (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>.
If maximumNumberOfRects is greater than the number of scissor rects currently bound, 
then the collection will be trimmed to contain only the currently bound. 
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerPipelineStage.GetState" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerpipelinestage.h" line="25">
<summary>
Get the rasterizer state from the rasterizer stage of the pipeline.
<para>(Also see DirectX SDK: ID3D11DeviceContext::RSGetState)</para>
</summary>
<returns>A rasterizer-state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState"/> to fill with information from the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerPipelineStage.GetViewports(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerpipelinestage.h" line="32">
<summary>
Get the array of viewports bound to the rasterizer stage
<para>(Also see DirectX SDK: ID3D11DeviceContext::RSGetViewports)</para>
</summary>
<param name="maxNumberOfViewports">The input specifies the maximum number of viewports (ranges from 0 to D3D11_VIEWPORT_AND_SCISSORRECT_OBJECT_COUNT_PER_PIPELINE) to retrieve, the output contains the actual number of viewports returned.</param>
<returns>A collection of Viewport structures that are bound to the device. 
If the number of viewports (in maxNumberOfViewports) is greater than the actual number of viewports currently bound, 
then the collection will be trimmed to contain only the currently bound. 
See the structure page for details about how the viewport size is dependent on the device feature level 
which has changed between Direct3D 11 and Direct3D 10.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerPipelineStage.SetScissorRects(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerpipelinestage.h" line="44">
<summary>
Bind an array of scissor rectangles to the rasterizer stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::RSSetScissorRects)</para>
</summary>
<param name="rects">A collection of scissor rectangles (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D.D3dRect"/>.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerPipelineStage.SetState(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerpipelinestage.h" line="51">
<summary>
Set the rasterizer state for the rasterizer stage of the pipeline.
<para>(Also see DirectX SDK: ID3D11DeviceContext::RSSetState)</para>
</summary>
<param name="rasterizerState">A rasterizer-state interface (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerState"/> to bind to the pipeline.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.RasterizerPipelineStage.SetViewports(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.Viewport})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11rasterizerpipelinestage.h" line="58">
<summary>
Bind an array of viewports to the rasterizer stage of the pipeline.
<para>(Also see DirectX SDK: ID3D11DeviceContext::RSSetViewports)</para>
</summary>
<param name="viewports">A collection of Viewport structures to bind to the device. 
See the SDK for details about how the viewport size is dependent on the device feature 
level which has changed between Direct3D 11 and Direct3D 10.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.StreamOutputPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11streamoutputpipelinestage.h" line="8">
<summary>
StreamOutput pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.StreamOutputPipelineStage.GetTargets(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11streamoutputpipelinestage.h" line="15">
<summary>
Get the target output buffers for the stream-output stage of the pipeline.
<para>(Also see DirectX SDK: ID3D11DeviceContext::SOGetTargets)</para>
</summary>
<param name="numBuffers">Number of buffers to get.</param>
<returns>A collection of output buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to be retrieved from the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.StreamOutputPipelineStage.SetTargets(System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer},System.UInt32[])" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11streamoutputpipelinestage.h" line="23">
<summary>
Set the target output buffers for the stream-output stage of the pipeline.
<para>(Also see DirectX SDK: ID3D11DeviceContext::SOSetTargets)</para>
</summary>
<param name="targets">The collection of output buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to bind to the device. 
The buffers must have been created with the <b>StreamOutput</b> flag.
A maximum of four output buffers can be set. If less than four are defined by the call, the remaining buffer slots are set to NULL.</param>
<param name="offsets">Array of offsets to the output buffers from ppBuffers, 
one offset for each buffer. The offset values must be in bytes.</param>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="8">
<summary>
Vertex Shader pipeline stage. 
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.GetConstantBuffers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="15">
<summary>
Get the constant buffers used by the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSGetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin retrieving constant buffers from (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="numBuffers">Number of buffers to retrieve (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - startSlot).</param>
<returns>Collection of constant buffer objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.GetSamplers(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="24">
<summary>
Get an array of sampler states from the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSGetSamplers)</para>
</summary>
<param name="startSlot">Index into a zero-based array to begin getting samplers from (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="numSamplers">Number of samplers to get from a device context. Each pipeline stage has a total of 16 sampler slots available (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - startSlot).</param>
<returns>Collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/> to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.GetShader(System.UInt32,System.Collections.ObjectModel.ReadOnlyCollection`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance}@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="33">
<summary>
Get the vertex shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSGetShader)</para>
</summary>
<param name="numClassInstances">The number of class-instance elements requested.</param>
<param name="classInstances">A collection of class instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>.</param>
<returns>A vertex shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.GetShader" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="42">
<summary>
Get the vertex shader currently set on the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSGetShader)</para>
</summary>
<returns>A vertex shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/> to be returned by the method.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.GetShaderResources(System.UInt32,System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="49">
<summary>
Get the vertex shader resources.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSGetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin getting shader resources from (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="numViews">The number of resources to get from the device. Up to a maximum of 128 slots are available for shader resources (ranges from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
<returns>Collection of shader resource views to be returned by the device.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.SetConstantBuffers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="58">
<summary>
Set the constant buffers used by the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSSetConstantBuffers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting constant buffers to (ranges from 0 to D3D11_COMMONSHADER_CONSTANT_BUFFER_API_SLOT_COUNT - 1).</param>
<param name="constantBuffers">Collection of constant buffers (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.D3DBuffer"/> being given to the device.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.SetSamplers(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="66">
<summary>
Set an array of sampler states to the vertex shader pipeline stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSSetSamplers)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting samplers to (ranges from 0 to D3D11_COMMONSHADER_SAMPLER_SLOT_COUNT - 1).</param>
<param name="samplers">A collection of sampler-state objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.SamplerState"/>. See Remarks.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="74">
<summary>
Set a vertex shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSSetShader)</para>
</summary>
<param name="vertexShader">A vertex shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
<param name="classInstances">A collection of class-instance objects (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ClassInstance"/>. 
Each interface used by a shader must have a corresponding class instance or the shader will get disabled. 
Set this parameter to null if the shader does not use any interfaces.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.SetShader(Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="85">
<summary>
Set a vertex shader to the device.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSSetShader)</para>
</summary>
<param name="vertexShader">A vertex shader (see <see cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/>)<seealso cref="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShader"/>. 
Passing in null disables the shader for this pipeline stage.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.VertexShaderPipelineStage.SetShaderResources(System.UInt32,System.Collections.Generic.IEnumerable`1{Microsoft.WindowsAPICodePack.DirectX.Direct3D11.ShaderResourceView})" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11vertexshaderpipelinestage.h" line="93">
<summary>
Bind an array of shader resources to the vertex-shader stage.
<para>(Also see DirectX SDK: ID3D11DeviceContext::VSSetShaderResources)</para>
</summary>
<param name="startSlot">Index into the device's zero-based array to begin setting shader resources to (range is from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - 1).</param>
<param name="shaderResourceViews">Collection of shader resource view objects to set to the device.
Up to a maximum of 128 slots are available for shader resources (range is from 0 to D3D11_COMMONSHADER_INPUT_RESOURCE_SLOT_COUNT - startSlot).</param>
</member>
</members>
</doc>