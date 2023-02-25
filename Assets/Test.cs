using System;
using System.Linq;
using UnityEngine;
using JuhaKurisu.PopoTools.ByteSerializer;
using JuhaKurisu.PopoTools.Extentions;
using JuhaKurisu.PopoTools.InputSystem;

public class Test : MonoBehaviour
{
    void Start()
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
            client.input.bytes.Join(",").Inspect();
        }
    }

    void Update()
    {

    }
}
