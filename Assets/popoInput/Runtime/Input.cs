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
            DataReader reader = new DataReader(bytes);
            this.bytes = new(reader.ReadBytes());
        }

        public byte[] Serialize()
        {
            DataWriter writer = new();

            writer.AppendWithLength(bytes.ToArray());

            return writer.bytes.ToArray();
        }
    }
}