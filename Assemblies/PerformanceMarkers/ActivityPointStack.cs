using System;
using System.Collections.Generic;

namespace PerformanceMarkers
{
	public class ActivityPointStack : Stack<ActivityPoint>
	{
		/// <summary>
		/// Returns true if the stack is not empty, false otherwise.
		/// </summary>
		public bool IsNotEmpty
		{
			get
			{
				return Count > 0;
			}
		}


		/// <summary>
		/// Returns true if the stack is empty, false otherwise.
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return Count == 0;
			}
		}


		/// <summary>
		/// Flips the stack.
		/// </summary>
		public ActivityPointStack Flip ()
		{
			ActivityPointStack FlippedStack = new ActivityPointStack();
			
			while (Count > 0)
				FlippedStack.Push(Pop());
				
			return FlippedStack;
		}
		
		
		public void EmptyIntoStack (ActivityPointStack TargetStack)
		{
			while (IsNotEmpty)
				TargetStack.Push(Pop());
		}
	}
}
