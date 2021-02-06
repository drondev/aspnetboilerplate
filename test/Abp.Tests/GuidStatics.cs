using System;

namespace Abp.Tests
{
    public static class GuidStatics
    {
        public static readonly Guid ID_1 = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static readonly Guid ID_2 = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public static readonly Guid ID_3 = Guid.Parse("00000000-0000-0000-0000-000000000003");
        public static readonly Guid ID_4 = Guid.Parse("00000000-0000-0000-0000-000000000004");

        public static Guid ToGuid(this int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}