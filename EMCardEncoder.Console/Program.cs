using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCardEncoder.Console
{
	class Program
	{
		static void Main(string[] args)
		{
		}

		public static string ShowBytes(byte[] payload)
		{
			if (payload == null) return String.Empty;
			if (payload.Length == 0) return String.Empty;

			StringBuilder hexBuilder = new StringBuilder(payload.Length);
			for (int i = 0; i < payload.Length; i++)
			{
				hexBuilder.AppendFormat("{0:x2}", payload[i]);
				if (i + 1 < payload.Length)
					hexBuilder.Append(' ');
			}
			return hexBuilder.ToString();
		}
	}
}
