using System;

namespace testXaDroid
{

	public class Shape
	{
		public float X { get;  set; }
		public float Y { get;  set; }


	}

	class Circle : Shape
	{
		public float Radius { get; set; }
		//Circle 
	}
	class Rectangle : Shape
	{
		public float Height { get; set; }
		public float Width { get; set; }
		//Rectangle 
	}
}

