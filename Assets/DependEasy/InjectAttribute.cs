using System;
using JetBrains.Annotations;

namespace DependEasy
{
	[MeansImplicitUse(ImplicitUseKindFlags.Assign)]
	[AttributeUsage(AttributeTargets.Field)]
	public class InjectAttribute : Attribute
	{
		
	}
}