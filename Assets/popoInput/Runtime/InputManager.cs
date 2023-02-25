using System;
using System.Linq;
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
                // 全体で共有しているclientIDを読み込む
                Guid clientID = reader.ReadGuid();
                // input内容を読み込む
                byte[] inputBytes = reader.ReadBytes();

                // クライアントを作成
                Client client = new Client(clientID, inputBytes);
                // clientsにセット
                clients[i] = client;
            }

            // 公開用の変数にセット
            this.clients = new(clients);
        }

        public byte[] CreateInputBytes()
        {
            DataWriter writer = new DataWriter();

            // サーバーとのみ共有しているsecretIDを送る
            writer.Append(secretID);
            // input内容を送る
            writer.AppendWithLength(GetInput());

            return writer.bytes.ToArray();
        }
    }
}