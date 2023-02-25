using System;
using System.Linq;
using JuhaKurisu.PopoTools.Utility;
using JuhaKurisu.PopoTools.Extentions;
using JuhaKurisu.PopoTools.InputSystem;
using JuhaKurisu.PopoTools.ByteSerializer;

public class Test : PopoBehaviour
{
    protected override void Start()
    {
        Guid client1ID = Guid.NewGuid();
        Guid secret1ID = Guid.NewGuid();

        Guid client2ID = Guid.NewGuid();
        Guid secret2ID = Guid.NewGuid();

        InputManager manager = new InputManager(client1ID, secret1ID, 2, () =>
        {
            return new byte[] { 0, 1, 2, 3 };
        });

        manager.CreateInputBytes().Join(",").Inspect();

        DataWriter writer = new DataWriter();
        writer.Append(client1ID);
        writer.AppendWithLength(new byte[] { 0, 1, 2, 3 });
        writer.Append(client2ID);
        writer.AppendWithLength(new byte[] { 0, 1, 2, 3 });
        manager.UpdateInputs(writer.bytes.ToArray());

        foreach (var client in manager.clients)
        {
            client.input.Join(",").Inspect();
        }
    }
}
