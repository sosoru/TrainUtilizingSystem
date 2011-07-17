<?xml version="1.0"?><doc>
<members>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="17">
<summary>
The format of text used for text layout purpose.
<para>(Also see DirectX SDK: IDWriteTextFormat)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.TextAlignment" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="25">
<summary>
Get or Set alignment option of text relative to layout box's leading and trailing edge.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.ParagraphAlignment" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="34">
<summary>
Get or Set alignment option of paragraph relative to layout box's top and bottom edge.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.WordWrapping" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="42">
<summary>
Get or Set word wrapping option.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.ReadingDirection" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="50">
<summary>
Get or Set paragraph reading direction.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.FlowDirection" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="58">
<summary>
Get or Set paragraph flow direction.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.IncrementalTabStop" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="66">
<summary>
Set incremental tab stop position.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.LineSpacing" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="74">
<summary>
Get or Set line spacing.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.Trimming" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="82">
<summary>
Get or Set trimming options.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.SetTrimming(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Trimming,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="90">
<summary>
Set trimming options for any trailing text exceeding the layout width
or for any far text exceeding the layout height.
</summary>
<param name="trimmingOptions">Text trimming options.</param>
<param name="trimmingSign">Application-defined omission sign.</param>
<remarks>
Any inline object can be used for the trimming sign, but DWriteFactory.CreateEllipsisTrimmingSign
provides a typical ellipsis symbol. Trimming is also useful vertically for hiding
partial lines.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.SetTrimming(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Trimming,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.InlineObject)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="106">
<summary>
Set trimming options for any trailing text exceeding the layout width
or for any far text exceeding the layout height.
</summary>
<param name="trimmingOptions">Text trimming options.</param>
<param name="trimmingSign">Application-defined omission sign.</param>
<remarks>
Any inline object can be used for the trimming sign, but DWriteFactory.CreateEllipsisTrimmingSign
provides a typical ellipsis symbol. Trimming is also useful vertically for hiding
partial lines.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.GetTrimming(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.InlineObject@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="122">
<summary>
Get trimming options for text overflowing the layout width.
</summary>
<param name="trimmingSign">Trimming omission sign.</param>
<returns>
Text trimming options.
</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.FontWeight" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="138">
<summary>
Get the font weight.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.FontStyle" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="146">
<summary>
Get the font style.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.FontStretch" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="154">
<summary>
Get the font stretch.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.FontSize" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="162">
<summary>
Get the font em height.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextFormat.CultureInfo" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextformat.h" line="170">
<summary>
Get the CultureInfo for this object.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="17">
<summary>
Represents a block of text after it has been fully analyzed and formatted.
<para>All coordinates are in device independent pixels (DIPs).</para>
<para>(Also see DirectX SDK: IDWriteTextLayout)</para>
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.Text" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="25">
<summary>
The text that was used to create this TextLayout.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.MaxWidth" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="33">
<summary>
Get or set layout maximum width
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.MaxHeight" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="42">
<summary>
Get or set layout maximum height
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetFontFamilyName(System.String,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="51">
<summary>
Set font family name.
</summary>
<param name="fontFamilyName">Font family name</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetFontWeight(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.FontWeight,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="61">
<summary>
Set font weight.
</summary>
<param name="fontWeight">Font weight</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetFontStyle(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.FontStyle,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="71">
<summary>
Set font style.
</summary>
<param name="fontStyle">Font style</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetFontStretch(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.FontStretch,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="81">
<summary>
Set font stretch.
</summary>
<param name="fontStretch">font stretch</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetFontSize(System.Single,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="91">
<summary>
Set font em height.
</summary>
<param name="fontSize">Font em height</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetUnderline(System.Boolean,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="101">
<summary>
Set underline.
</summary>
<param name="hasUnderline">The Boolean flag indicates whether underline takes place</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetStrikethrough(System.Boolean,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="111">
<summary>
Set strikethrough.
</summary>
<param name="hasStrikethrough">The Boolean flag indicates whether strikethrough takes place</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetInlineObject(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="122">
<summary>
Set a custom inline object.
</summary>
<param name="inlineObject">An application-implemented inline object.</param>
<param name="textRange">Text range to which this change applies.</param>
<remarks>
This inline object applies to the specified range and will be passed back
to the application via the DrawInlineObject callback when the range is drawn.
Any text in that range will be suppressed.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetInlineObject(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.InlineObject,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="137">
<summary>
Set inline object.
</summary>
<param name="inlineObject">An application-implemented inline object.</param>
<param name="textRange">Text range to which this change applies.</param>
<remarks>
This inline object applies to the specified range and will be passed back
to the application via the DrawInlineObject callback when the range is drawn.
Any text in that range will be suppressed.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetCultureInfo(System.Globalization.CultureInfo,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="152">
<summary>
Set CultureInfo.
</summary>
<param name="cultureInfo">Culture Info</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.SetTypography(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Typography,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="162">
<summary>
Sets font typography features for text within a specified text range.
</summary>
<param name="typography">Font typography settings collection.</param>
<param name="textRange">Text range to which this change applies.</param>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontFamilyName(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="172">
<summary>
Get the font family name of the text where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>The current font family name</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontFamilyName(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="183">
<summary>
Get the font family name of the text where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>The current font family name</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontWeight(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="192">
<summary>
Get the font weight where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>The current font weight</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontWeight(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="203">
<summary>
Get the font weight where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>The current font weight</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontStyle(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="212">
<summary>
Get the font style where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>
The current font style.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontStyle(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="225">
<summary>
Get the font style where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>
The current font style.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontStretch(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="236">
<summary>
Get the font stretch where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>
The current font stretch.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontStretch(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="250">
<summary>
Get the font stretch where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>
The current font stretch.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontSize(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="261">
<summary>
Get the font em height where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>
The current font em height.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetFontSize(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="274">
<summary>
Get the font em height where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>
The current font em height.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetUnderline(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="285">
<summary>
Get the underline presence where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>
The Boolean flag indicates whether text is underlined.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetUnderline(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="299">
<summary>
Get the underline presence where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>
The Boolean flag indicates whether text is underlined.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetStrikethrough(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="310">
<summary>
Get the strikethrough presence where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>
The Boolean flag indicates whether text has strikethrough.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetStrikethrough(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="323">
<summary>
Get the strikethrough presence where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>
The Boolean flag indicates whether text has strikethrough.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetCultureInfo(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="334">
<summary>
Get the culture info where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>
The current culture info.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetCultureInfo(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="347">
<summary>
Get the culture info where the current position is at.
</summary>
<param name="currentPosition">The current text position.</param>
<returns>
The current culture info.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetTypography(System.UInt32,Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextRange@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="358">
<summary>
Gets the typography setting of the text at the specified position. 
</summary>
<param name="currentPosition">The current text position.</param>
<param name="textRange">The position range of the current format.</param>
<returns>
The current typography collection, or null if no typography has been set.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetTypography(System.UInt32)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="371">
<summary>
Gets the typography setting of the text at the specified position. 
</summary>
<param name="currentPosition">The current text position.</param>
<returns>
The current typography collection, or null if no typography has been set.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetLineMetrics" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="382">
<summary>
Retrieves the information about each individual text line of the text string. 
</summary>
<returns>Properties of each line.</returns>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.Metrics" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="388">
<summary>
Retrieves overall metrics for the formatted string.
</summary>
<remarks>
Drawing effects like underline and strikethrough do not contribute
to the text size, which is essentially the sum of advance widths and
line heights. Additionally, visible swashes and other graphic
adornments may extend outside the returned width and height.
</remarks>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.OverhangMetrics" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="402">
<summary>
GetOverhangMetrics returns the overhangs (in DIPs) of the layout and all
objects contained in it, including text glyphs and inline objects.
</summary>
<remarks>
Any underline and strikethrough do not contribute to the black box
determination, since these are actually drawn by the renderer, which
is allowed to draw them in any variety of styles.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.GetClusterMetrics" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="417">
<summary>
Retrieves logical properties and measurements of each glyph cluster. 
</summary>
<returns>
Array of Cluster Metrics such as line-break or total advance width, for each glyph cluster.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.DetermineMinWidth" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="425">
<summary>
Determines the minimum possible width the layout can be set to without
emergency breaking between the characters of whole words.
</summary>
<returns>
Minimum width.
</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.HitTestPoint(System.Single,System.Single,System.Boolean@,System.Boolean@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="435">
<summary>
Given a coordinate (in DIPs) relative to the top-left of the layout box,
this returns the corresponding hit-test metrics of the text string where
the hit-test has occurred. This is useful for mapping mouse clicks to caret
positions. When the given coordinate is outside the text string, the function
sets the output value *isInside to false but returns the nearest character
position.
</summary>
<param name="pointX">X coordinate to hit-test, relative to the top-left location of the layout box.</param>
<param name="pointY">Y coordinate to hit-test, relative to the top-left location of the layout box.</param>
<param name="isInside">Output flag indicating whether the hit-test location is inside the text string.
    When false, the position nearest the text's edge is returned.</param>
<param name="isTrailingHit">Output flag indicating whether the hit-test location is at the leading or the trailing
    side of the character. When the output *isInside value is set to false, this value is set according to the output
    *position value to represent the edge closest to the hit-test location. </param>
<returns>Output geometry fully enclosing the hit-test location. When the output isInside value
    is set to false, this structure represents the geometry enclosing the edge closest to the hit-test location.</returns>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.HitTestTextPosition(System.UInt32,System.Boolean,System.Single@,System.Single@)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="459">
<summary>
Given a text position and whether the caret is on the leading or trailing
edge of that position, this returns the corresponding coordinate (in DIPs)
relative to the top-left of the layout box. This is most useful for drawing
the caret's current position, but it could also be used to anchor an IME to the
typed text or attach a floating menu near the point of interest. It may also be
used to programmatically obtain the geometry of a particular text position
for UI automation.
</summary>
<param name="textPosition">Text position to get the coordinate of.</param>
<param name="isTrailingHit">Flag indicating whether the location is of the leading or the trailing side of the specified text position. </param>
<param name="pointX">Output caret X, relative to the top-left of the layout box.</param>
<param name="pointY">Output caret Y, relative to the top-left of the layout box.</param>
<returns>
Output geometry fully enclosing the specified text position.
</returns>
<remarks>
When drawing a caret at the returned X,Y, it should should be centered on X
and drawn from the Y coordinate down. The height will be the size of the
hit-tested text (which can vary in size within a line).
Reading direction also affects which side of the character the caret is drawn.
However, the returned X coordinate will be correct for either case.
You can get a text length back that is larger than a single character.
This happens for complex scripts when multiple characters form a single cluster,
when diacritics join their base character, or when you test a surrogate pair.
</remarks>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TextLayout.HitTestTextRange(System.UInt32,System.UInt32,System.Single,System.Single)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetextlayout.h" line="492">
<summary>
The application calls this function to get a set of hit-test metrics
corresponding to a range of text positions. The main usage for this
is to draw highlighted selection of the text string.
</summary>
<param name="textPosition">First text position of the specified range.</param>
<param name="textLength">Number of positions of the specified range.</param>
<param name="originX">Offset of the X origin (left of the layout box) which is added to each of the hit-test metrics returned.</param>
<param name="originY">Offset of the Y origin (top of the layout box) which is added to each of the hit-test metrics returned.</param>
<returns>
Aarray of the output geometry fully enclosing the specified position range.
</returns>
<remarks>
There are no gaps in the returned metrics. While there could be visual gaps,
depending on bidi ordering, each range is contiguous and reports all the text,
including any hidden characters and trimmed text.
The height of each returned range will be the same within each line, regardless
of how the font sizes vary.
</remarks>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\icustominlineobject.h" line="21">
<summary>
Wraps an application defined inline graphic, allowing DWrite to query 
metrics as if it was a glyph inline with the text.
<para>(Also see DirectX SDK: IDWriteInlineObject)</para>
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject.Draw(System.Single,System.Single,System.Boolean,System.Boolean,Microsoft.WindowsAPICodePack.DirectX.Direct2D1.Brush)" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\icustominlineobject.h" line="28">
<summary>
The application implemented rendering callback (IDWriteTextRenderer::DrawInlineObject)
can use this to draw the inline object without needing to cast or query the object
type. The text layout does not call this method directly.
</summary>
<param name="originX">X-coordinate at the top-left corner of the inline object.</param>
<param name="originY">Y-coordinate at the top-left corner of the inline object.</param>
<param name="isSideways">The object should be drawn on its side.</param>
<param name="isRightToLeft">The object is in an right-to-left context and should be drawn flipped.</param>
<param name="clientDrawingEffect">The drawing effect. This is usually a foreground brush.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject.Metrics" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\icustominlineobject.h" line="46">
<summary>
TextLayout calls this callback function to get the measurement of the inline object.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject.OverhangMetrics" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\icustominlineobject.h" line="54">
<summary>
TextLayout calls this callback function to get the visible extents (in DIPs) of the inline object.
In the case of a simple bitmap, with no padding and no overhang, all the overhangs will
simply be zeroes.
</summary>
<remarks>
The overhangs should be returned relative to the reported size of the object
(InlineObjectMetrics width/height), and should not be baseline
adjusted. If you have an image that is actually 100x100 DIPs, but you want it
slightly inset (perhaps it has a glow) by 20 DIPs on each side, you would
return a width/height of 60x60 and four overhangs of 20 DIPs.
</remarks>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject.BreakConditionBefore" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\icustominlineobject.h" line="71">
<summary>
Layout uses this to determine the line breaking behavior of the inline object
amidst the text.
Should return the line-breaking condition between the object and the content immediately preceding it.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.ICustomInlineObject.BreakConditionAfter" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\icustominlineobject.h" line="81">
<summary>
Layout uses this to determine the line breaking behavior of the inline object
amidst the text.
Should return the line-breaking condition between the object and the content immediately following it.
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.InlineObject" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwriteinlineobject.h" line="16">
<summary>
Wraps an inline graphic.
<para>(Also see DirectX SDK: IDWriteInlineObject)</para>
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.TypographyList" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetypography.h" line="15">
<summary>
Implements an enumerable list of FontFeature
</summary>
</member>
<member name="T:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Typography" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetypography.h" line="62">
<summary>
Represents a collection of OpenType font typography settings (<see cref="T:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.FontFeature"/>).
<para>(Also see DirectX SDK: IDWriteTypography)</para>
</summary>
<remarks>Once an OpenType font feature setting has been added, it cannot be removed from the collection.</remarks>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Typography.Item(System.Int32)" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetypography.h" line="72">
<summary>
Gets the font feature at the specified index.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Typography.Add(Microsoft.WindowsAPICodePack.DirectX.DirectWrite.FontFeature)" decl="true" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetypography.h" line="82">
<summary>
Adds an OpenType font feature.
</summary>
<param name="feature">Font feature to add to the collection.</param>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Typography.Count" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetypography.h" line="88">
<summary>
Get the number of font features in the collection.
</summary>
</member>
<member name="P:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Typography.IsReadOnly" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetypography.h" line="96">
<summary>
Determines if  the collection is readonly.
</summary>
</member>
<member name="M:Microsoft.WindowsAPICodePack.DirectX.DirectWrite.Typography.GetEnumerator" decl="false" source="c:\users\root\documents\trainutilizingsystem\serverutility\codepack_directx\directwrite\dwritetypography.h" line="104">
<summary>
Gets the enumerator for this collection.
</summary>
</member>
</members>
</doc>