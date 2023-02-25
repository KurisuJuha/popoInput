using System.Linq;
using System.Collections.ObjectModel;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.InputSystem
{
    public class Input
    {
        public readonly ReadOnlyCollection<byte> bytes;

        public Input(byte[] bytes)
        {
            this.bytes = new(bytes);
        }

        public byte[] Serialize()
        {
            DataWriter writer = new();

            writer.AppendWithLength(bytes.ToArray());

            return writer.bytes.ToArray();
        }
    }
}