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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.Direct3D11.PipelineStage" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\direct3d11\d3d11pipelinestage.h" line="11">
<summary>
A pipeline stage. base class for all pipline stage related classes.
</summary>
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
</members>
</doc>