using System;
using System.Collections.ObjectModel;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.InputSystem
{
    public class InputManager
    {
        public ReadOnlyCollection<Client> clients { get; private set; }
        public readonly int playerCount;

        private readonly Func<byte[]> GetInput;
        private readonly Guid clientID;
        private readonly Guid secretID;

        public InputManager(Guid clientID, Guid secretID, int playerCount, Func<byte[]> GetInput)
        {
            this.clientID = clientID;
            this.secretID = secretID;
            this.playerCount = playerCount;
            this.GetInput = GetInput;
            clients = new(new Client[0]);
        }

        public void UpdateInputs(byte[] bytes)
        {
            // reader作成
            DataReader reader = new(bytes);

            // クライアントの配列作成
            Client[] clients = new Client[playerCount];

            // クライアントたちのデシリアライズ
            for (int i = 0; i < playerCount; i++)
            {
                Client client = new Client(reader.ReadBytes());
                clients[i] = client;
            }

            // 公開用の変数にセット
            this.clients = new(clients);
        }
    }
}