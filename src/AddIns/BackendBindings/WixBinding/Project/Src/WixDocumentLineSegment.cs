﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Editor;

namespace ICSharpCode.WixBinding
{
	public class WixDocumentLineSegment
	{
		int offset;
		int length;
		
		public WixDocumentLineSegment(int offset, int length)
		{
			this.offset = offset;
			this.length = length;
		}
		
		public int Offset {
			get { return offset; }
			set { offset = value; }
		}
		
		public int Length {
			get { return length; }
			set { length = value; }
		}
		
		public override string ToString()
		{
			return String.Format("[LineSegment: Offset {0}, Length {1}]", offset, length);
		}
	
		public override int GetHashCode()
		{
			return offset.GetHashCode() ^ length.GetHashCode();
		}
		
		public override bool Equals(object obj)
		{
			WixDocumentLineSegment rhs = obj as WixDocumentLineSegment;
			if (rhs != null) {
				return (offset == rhs.offset) && (length == rhs.length);
			}
			return false;
		}
		
		public static WixDocumentLineSegment ConvertRegionToSegment(IDocument document, DomRegion region)
		{
			// Single line region
			if (IsSingleLineRegion(region)) {
				return ConvertRegionToSingleLineSegment(document, region);
			}
			return ConvertRegionToMultiLineSegment(document, region);
		}
		
		static bool IsSingleLineRegion(DomRegion region)
		{
			return region.BeginLine == region.EndLine;
		}
		
		static WixDocumentLineSegment ConvertRegionToSingleLineSegment(IDocument document, DomRegion region)
		{
			IDocumentLine documentLine = document.GetLine(region.BeginLine + 1);
			return new WixDocumentLineSegment(documentLine.Offset + region.BeginColumn, 
					region.EndColumn + 1 - region.BeginColumn);
		}
		
		static WixDocumentLineSegment ConvertRegionToMultiLineSegment(IDocument document, DomRegion region)
		{
			int length = 0;
			int startOffset = 0;
			for (int line = region.BeginLine; line <= region.EndLine; ++line) {
				IDocumentLine currentDocumentLine = document.GetLine(line + 1);
				if (line == region.BeginLine) {
					length += currentDocumentLine.TotalLength - region.BeginColumn;
					startOffset = currentDocumentLine.Offset + region.BeginColumn;
				} else if (line < region.EndLine) {
					length += currentDocumentLine.TotalLength;
				} else {
					length += region.EndColumn + 1;
				}
			}
			return new WixDocumentLineSegment(startOffset, length);
		}
	}
}
