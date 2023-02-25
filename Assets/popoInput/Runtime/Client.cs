using System;

namespace JuhaKurisu.PopoTools.InputSystem
{
    public class Client
    {
        public readonly Guid clientID;
        public readonly byte[] input;

        public Client(Guid clientID, byte[] input)
        {
            this.clientID = clientID;
            this.input = input;
        }
    }
}