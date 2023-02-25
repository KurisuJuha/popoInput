using System;

namespace JuhaKurisu.PopoTools.InputSystem
{
    public class Client
    {
        public readonly Guid clientID;
        public readonly Input input;

        public Client(Guid clientID, byte[] inputBytes)
        {
            this.clientID = clientID;
            this.input = new Input(inputBytes);
        }
    }
}