// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("BRGqw65HNDM1syYKWW+PxMN09FSnYVWFvr/ob+a7FQIqYvSQORGhqAuD1NL0s7SMDW00e31l63q0UwRUTetsUSy5oYRTDC2IvQbL6QwqoGwUc3RU0Ep6HzAAawM9aSNb9g3Jsit6OQ1ctmXcYB7OHvmN6WI6p/pCQpa8WjzAsS3eE8DfkFlCqI4gztVPKuDroghQY5cm86B9yL+tBZ0ykPAzkjyRcc6P8eh+hS8DMjOnBwfHVM7BbmMvaTrGy0Nk4XMReSXUsTgS/+D2NZ7LXV0g4I0Zaax3f3UUd88sJmdLeuXPGLVM+4wYFTM17eQzK6imqZkrqKOrK6ioqSWa+kJ6LhmZK6iLmaSvoIMv4S9epKioqKypqnl9ivXlqS0vlKuqqKmo");
        private static int[] order = new int[] { 10,2,13,4,6,7,9,8,9,12,12,11,13,13,14 };
        private static int key = 169;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
