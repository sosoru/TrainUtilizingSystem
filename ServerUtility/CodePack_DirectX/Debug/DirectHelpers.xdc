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
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectHelpers" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directhelpers.h" line="12">
<summary>
This class provides a set of helper methods that can be used to extend
the API, and wrap external natives interfaces.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectHelpers.GetExceptionForHResult(System.Int32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directhelpers.h" line="19">
<summary>
Return an exception for a given HResult
</summary>
<param name="hResult">The HResult value</param>
<returns>The equivalent exception</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectHelpers.GetExceptionForErrorCode(Microsoft.WindowsAPICodePack.DirectX.ErrorCode)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directhelpers.h" line="26">
<summary>
Return an exception for a given ErrorCode
</summary>
<param name="errorCode">The ErrorCode value</param>
<returns>The equivalent exception</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectHelpers.ThrowExceptionForErrorCode(Microsoft.WindowsAPICodePack.DirectX.ErrorCode)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directhelpers.h" line="33">
<summary>
Throw an exception for a given ErrorCode. 
No exception will be thrown if the errorCode is Success.
</summary>
<param name="errorCode">The ErrorCode value</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectHelpers.ThrowExceptionForHResult(System.Int32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directhelpers.h" line="40">
<summary>
Throw an exception for a given HResult
No exception will be thrown if the errorCode is Success.
</summary>
<param name="hResult">The HResult value</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectHelpers.CreateIUnknown``1(System.IntPtr)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directhelpers.h" line="47">
<summary>
Create a wrapper for a given native IUnknown interface pointer.
This method will not increase the ref count for the wrapped native
interface. However, when this class is disposed, the native interface
will have Release() called.
This method is mainly intended to wrap interfaces that inherit from IUnknown.
</summary>
<param name="nativeIUnknownPointer">The native pointer to the IUnknown interface.</param>
<typeparam name="T">The type of the IUnknown wrapper to create. Must inherit from <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DirectUnknown"/>.</typeparam>
<returns>A DirectUnknown wrapper.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectHelpers.CreateInterface``1(System.IntPtr)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directhelpers.h" line="60">
<summary>
Create a wrapper for a given native interface pointer that does not have an IUnknown.
This method will not increase the ref count for the wrapped native
interface. Also, when this class is disposed, the native interface
will not be deleted or released.
This method is mainly intended to wrap interfaces that do not inherit from IUnknown.
</summary>
<typeparam name="T">The type of the object wrapper to create. Must inherit from <see cref="T:Microsoft.WindowsAPICodePack.DirectX.DirectObject"/>.</typeparam>
<param name="nativeInterfacePointer">The native pointer to the interface.</param>
<returns>A DirectObject wrapper.</returns>
</member>
</members>
</doc>