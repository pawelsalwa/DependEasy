using System;

namespace DependEasy.Attributes
{
	/// <summary> Mark a MonoBehaviour with it to make it injectable. </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ServiceAttribute : Attribute
	{
		/// <summary> if specified, this will be registered as service in ServiceLocator </summary>
		public readonly Type serviceType;
		
		public ServiceAttribute(Type serviceType)
		{
			this.serviceType = serviceType;
		}
		public ServiceAttribute()
		{
			serviceType = null;
		}
	}
}