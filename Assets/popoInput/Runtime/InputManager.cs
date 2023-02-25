using System;
using System.Collections.ObjectModel;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.InputSystem
{
    public class InputManager
    {
        public ReadOnlyCollection<Client> clients { get; private set; }
        public readonly int playerCount;
        public readonly Func<byte[]> GetInput;

        public InputManager(int playerCount, Func<byte[]> GetInput)
        {
            this.playerCount = playerCount;
            this.GetInput = GetInput;
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