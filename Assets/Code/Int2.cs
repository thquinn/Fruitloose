// from https://gist.github.com/FreyaHolmer/5743d0ad1c09b64cb548
/////////////////////////////////////////////////////////////////////////////
// int2 is similar to Vector2, but with integers instead of floats
// Useful for various grid related things.
//
// - Swizzle operators xx/xy/yx/yy
// - Extended arithmetic operators similar to shader data types
//		A few examples:
//		int2(8,4) / int2(2,4) -> int2(4,1)
//			   16 / int2(2,4) -> int2(8,4)
//		int2(2,3) * int2(4,5) -> int2(8,15)
//
// - Any operator with an integer will result in an int2
// - Any operator with a float will result in a Vector2
// 

using UnityEngine;

namespace Assets.Code {
	[System.Serializable]
	public struct Int2 {

		//-----------------------------------------------------------------------
		// Data

		[SerializeField] public int x;
		[SerializeField] public int y;

		//-----------------------------------------------------------------------
		// Constructors

		public Int2(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public Int2(int size) {
			this.x = size;
			this.y = size;
		}

		//-----------------------------------------------------------------------
		// Swizzling

		public Int2 xx {
			get { return new Int2(x, x); }
		}

		public Int2 xy {
			get { return new Int2(x, y); }
		}

		public Int2 yx {
			get { return new Int2(y, x); }
		}

		public Int2 yy {
			get { return new Int2(y, y); }
		}

		//-----------------------------------------------------------------------
		// Derived Data

		public int area {
			get { return Mathf.Abs(x * y); }
		}

		public int signedArea {
			get { return x * y; }
		}

		public bool isSquare {
			get { return x == y; }
		}

		public float min {
			get { return Mathf.Min(x, y); }
		}

		public float max {
			get { return Mathf.Max(x, y); }
		}

		public float euclideanLength {
			get { return Mathf.Sqrt(x * x + y * y); }
		}

		public float rectilinearLength {
			get { return Mathf.Abs(x) + Mathf.Abs(y); }
		}

		public float chebyshevLength {
			get { return Mathf.Max(Mathf.Abs(x), Mathf.Abs(y)); }
		}

		//-----------------------------------------------------------------------
		// Operators

		// Equals
		public static bool operator ==(Int2 a, Int2 b) {
			return a.x == b.x && a.y == b.y;
        }
		public static bool operator !=(Int2 a, Int2 b) {
			return a.x != b.x || a.y != b.y;
		}
        public override bool Equals(object obj) {
			return (Int2)obj == this;
        }
        public override int GetHashCode() {
			return x * 31 + y;
		}

        // Add

        public static Int2 operator +(Int2 a, Int2 b) {
			return new Int2(a.x + b.x, a.y + b.y);
		}

		public static Int2 operator +(Int2 a, int v) {
			return new Int2(a.x + v, a.y + v);
		}

		public static Int2 operator +(int v, Int2 a) {
			return new Int2(a.x + v, a.y + v);
		}

		public static Vector2 operator +(Int2 a, float v) {
			return new Vector2(a.x + v, a.y + v);
		}

		public static Vector2 operator +(float v, Int2 a) {
			return new Vector2(a.x + v, a.y + v);
		}

		// Subtract

		public static Int2 operator -(Int2 a, Int2 b) {
			return new Int2(a.x - b.x, a.y - b.y);
		}

		public static Int2 operator -(Int2 a, int v) {
			return new Int2(a.x - v, a.y - v);
		}

		public static Int2 operator -(int v, Int2 a) {
			return new Int2(v - a.x, v - a.y);
		}

		public static Vector2 operator -(Int2 a, float v) {
			return new Vector2(a.x - v, a.y - v);
		}

		public static Vector2 operator -(float v, Int2 a) {
			return new Vector2(v - a.x, v - a.y);
		}

		// Multiply

		public static Int2 operator *(Int2 a, Int2 b) {
			return new Int2(a.x * b.x, a.y * b.y);
		}

		public static Int2 operator *(Int2 a, int v) {
			return new Int2(a.x * v, a.y * v);
		}

		public static Int2 operator *(int v, Int2 a) {
			return new Int2(a.x * v, a.y * v);
		}

		public static Vector2 operator *(Int2 a, float v) {
			return new Vector2(a.x * v, a.y * v);
		}

		public static Vector2 operator *(float v, Int2 a) {
			return new Vector2(a.x * v, a.y * v);
		}

		// Divide

		public static Int2 operator /(Int2 a, Int2 b) {
			return new Int2(a.x / b.x, a.y / b.y);
		}

		public static Int2 operator /(Int2 a, int v) {
			return new Int2(a.x / v, a.y / v);
		}

		public static Int2 operator /(int v, Int2 a) {
			return new Int2(v / a.x, v / a.y);
		}

		public static Vector2 operator /(Int2 a, float v) {
			return new Vector2(a.x / v, a.y / v);
		}

		public static Vector2 operator /(float v, Int2 a) {
			return new Vector2(v / a.x, v / a.y);
		}

		// Component index access

		public int this[int i] {
			get {
				if (i == 0)
					return x;
				else if (i == 1)
					return y;
				else
					throw new System.IndexOutOfRangeException("Expected an index of 0 or 1. " + i + " is out of range");
			}
			set {
				if (i == 0)
					x = value;
				else if (i == 1)
					y = value;
				else
					throw new System.IndexOutOfRangeException("Expected an index of 0 or 1. " + i + " is out of range");
			}
		}

		//-----------------------------------------------------------------------
		// Static members

		public static Int2 zero = new Int2(0, 0);
		public static Int2 one = new Int2(1, 1);
		public static Int2 right = new Int2(1, 0);
		public static Int2 left = new Int2(-1, 0);
		public static Int2 up = new Int2(0, 1);
		public static Int2 down = new Int2(0, -1);

		//-----------------------------------------------------------------------
		// Typecasting

		public static implicit operator Vector2(Int2 i) { // int2 to Vector2
			return new Vector2(i.x, i.y);
		}

		public static explicit operator Int2(Vector2 v) { // Vector2 to int2. Explicit due to precision loss
			return v.FloorToInt2(); // Floor replicates the behavior when typecasting float to int
		}

		//-----------------------------------------------------------------------
		// Static functions

		public static Vector2 Lerp(Int2 a, Int2 b, float t, bool extrapolate = false) {
			t = extrapolate ? t : Mathf.Clamp01(t);
			return a * (1f - t) + b * t;
		}

		public static float EuclideanDistance(Int2 a, Int2 b) {
			return (a - b).euclideanLength;
		}

		public static float RectilinearDistance(Int2 a, Int2 b) {
			return (a - b).rectilinearLength;
		}

		public static float ChebyshevDistance(Int2 a, Int2 b) {
			return (a - b).chebyshevLength;
		}

		public override string ToString() {
			return "[ " + x + " , " + y + " ]";
		}

	}



	// So that it integrates a bit more neatly into Unity's other classes
	public static class int2extensions {

		//-----------------------------------------------------------------------
		// Rect

		// May be slightly immoral to have hidden floors in these

		public static Int2 GetSize(this Rect r) {
			return new Int2(Mathf.FloorToInt(r.width), Mathf.FloorToInt(r.height));
		}

		public static Int2 GetPosition(this Rect r) {
			return new Int2(Mathf.FloorToInt(r.width), Mathf.FloorToInt(r.height));
		}

		//-----------------------------------------------------------------------
		// Texture

		public static void SetResolution(this Texture t, Int2 resolution) {
			t.width = resolution.x;
			t.height = resolution.y;
		}

		public static Int2 GetResolution(this Texture t) {
			return new Int2(t.width, t.height);
		}

		//-----------------------------------------------------------------------
		// Vector2

		public static Int2 RoundToInt2(this Vector2 v) {
			return new Int2(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		}

		public static Int2 FloorToInt2(this Vector2 v) {
			return new Int2(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		public static Int2 CeilToInt2(this Vector2 v) {
			return new Int2(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
		}

	}
}