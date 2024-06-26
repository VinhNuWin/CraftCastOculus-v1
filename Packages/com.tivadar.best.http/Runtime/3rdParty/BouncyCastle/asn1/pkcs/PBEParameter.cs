#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Math;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	public class PbeParameter
		: Asn1Encodable
	{
		private readonly Asn1OctetString	salt;
		private readonly DerInteger			iterationCount;

		public static PbeParameter GetInstance(object obj)
		{
			if (obj is PbeParameter || obj == null)
			{
				return (PbeParameter) obj;
			}

			if (obj is Asn1Sequence)
			{
				return new PbeParameter((Asn1Sequence) obj);
			}

			throw new ArgumentException("Unknown object in factory: " + Org.BouncyCastle.Utilities.Platform.GetTypeName(obj), "obj");
		}

		private PbeParameter(Asn1Sequence seq)
		{
			if (seq.Count != 2)
				throw new ArgumentException("Wrong number of elements in sequence", "seq");

			salt = Asn1OctetString.GetInstance(seq[0]);
			iterationCount = DerInteger.GetInstance(seq[1]);
		}

		public PbeParameter(byte[] salt, int iterationCount)
		{
			this.salt = new DerOctetString(salt);
			this.iterationCount = new DerInteger(iterationCount);
		}

		public byte[] GetSalt()
		{
			return salt.GetOctets();
		}

		public BigInteger IterationCount
		{
			get { return iterationCount.Value; }
		}

		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(salt, iterationCount);
		}
	}
}
#pragma warning restore
#endif
