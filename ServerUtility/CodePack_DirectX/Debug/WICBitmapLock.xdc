<?xml version="1.0"?><doc>
<members>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapSize" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="9">
<summary>
Describes size of the bitmap.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapSize.Width" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="15">
<summary>
The width of the bitmap in pixels.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapSize.Height" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="20">
<summary>
The height of the bitmap in pixels.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapSize.#ctor(System.UInt32,System.UInt32)" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="25">
<summary>
Constructor
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapResolution" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="37">
<summary>
Describes dots per inch resolution of the bitmap.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapResolution.DpiX" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="43">
<summary>
The horizontal resolution of the bitmap.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapResolution.DpiY" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="48">
<summary>
The vertical resolution of the bitmap.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapResolution.#ctor(System.Double,System.Double)" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="53">
<summary>
Constructor
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapRectangle" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="63">
<summary>
Describes a rectangle of bitmap data.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapRectangle.X" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="69">
<summary>
The horizontal component of the top left corner of the rectangle.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapRectangle.Y" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="74">
<summary>
The vertical component of the top left corner of the rectangle.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapRectangle.Width" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="79">
<summary>
The width of the rectangle.
</summary>
</member>
<member name="F:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapRectangle.Height" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="84">
<summary>
The height of the rectangle.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.BitmapRectangle.#ctor(System.Int32,System.Int32,System.Int32,System.Int32)" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicstructs.h" line="89">
<summary>
Constructor.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.WICBitmapLock" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicbitmaplock.h" line="12">
<summary>
Defines methods that add the concept of writeability and static in-memory representations 
of bitmaps to WICBitmapSource. 
<para>(Also see IWICBitmap interface)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.WICBitmapLock.GetDataPointer(System.UInt32@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicbitmaplock.h" line="21">
<summary>
Gets the pointer to the top left pixel in the locked rectangle.
</summary>
<param name="bufferSize">The size of the buffer.</param>
<returns>
A pointer to the top left pixel in the locked rectangle.
</returns>
<remarks>
The pointer provided by this method should not be used outside of the lifetime of the lock itself.
GetDataPointer is not available in multi-threaded apartment applications.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.WICBitmapLock.CopyData" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicbitmaplock.h" line="34">
<summary>
Retrieves a copy of the buffer data.
</summary>
<returns>
A byte array containing the buffer data.
</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.WICBitmapLock.Size" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicbitmaplock.h" line="42">
<summary>
Retrieves the pixel width and height of the locked rectangle.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.WICBitmapLock.PixelFormat" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicbitmaplock.h" line="50">
<summary>
Gets the pixel format of for the locked area of pixels. This can be used to compute the number of bytes-per-pixel in the locked area.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent.WICBitmapLock.Stride" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\wic\wicbitmaplock.h" line="58">
<summary>
Provides access to the stride value for the memory. 
</summary>
<remarks>
Note the stride value is specific to the IWICBitmapLock, not the bitmap. 
For example, two consecutive locks on the same rectangle of a bitmap may 
return different pointers and stride values, depending on internal implementation. 
</remarks>
</member>
</members>
</doc>