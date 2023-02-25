using System;
using System.Linq;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.InputSystem
{
    public class Client
    {
        public readonly Guid id;
        public readonly Input input;

        public Client(byte[] bytes)
        {
            DataReader reader = new(bytes);
            id = reader.ReadGuid();
            input = new Input(reader.ReadBytes());
        }

        public byte[] Serialize()
        {
            DataWriter writer = new DataWriter();

            writer.Append(id);
            writer.Append(input.Serialize());

            return writer.bytes.ToArray();
        }
    }
}