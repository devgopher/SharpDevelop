﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using ICSharpCode.SharpDevelop.Gui;

namespace ICSharpCode.WixBinding
{
	/// <summary>
	/// Hides the diff control from the Setup Files window.
	/// </summary>
	public class HideDiffCommand : AbstractActivePackageFilesViewCommand
	{
		public HideDiffCommand()
		{
		}
		
		public HideDiffCommand(IWorkbench workbench)
			: base(workbench)
		{
		}
		
		protected override void Run(PackageFilesView view)
		{
			view.HideDiff();
		}
	}
}
