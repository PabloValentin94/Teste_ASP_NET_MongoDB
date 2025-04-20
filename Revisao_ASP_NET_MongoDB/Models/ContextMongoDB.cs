using MongoDB.Driver;

namespace Revisao_ASP_NET_MongoDB.Models
{
    public class ContextMongoDB
    {
        // Variáveis de conexão.

        public static string? Connection_String { get; set; }

        public static string? Database_Name { get; set; }

        public static bool Is_Ssl { get; set; }

        // Variável de acesso ao banco de dados.

        private IMongoDatabase? Database { get; set; }

        // Estabelecendo uma conexão com o banco de dados especificado.

        public ContextMongoDB()
        {
            try
            {
                MongoClientSettings configuracoes_conexao = MongoClientSettings.FromUrl(new MongoUrl(Connection_String));

                if (Is_Ssl)
                {
                    configuracoes_conexao.SslSettings = new SslSettings()
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }

                MongoClient utilizador_conexao = new MongoClient(configuracoes_conexao);

                this.Database = utilizador_conexao.GetDatabase(Database_Name);
            }

            catch (Exception)
            {
                throw new Exception("Não foi possível se conectar ao MongoDB.");
            }
        }

        // Variáveis de acesso às coleções do banco de dados MongoDB.

        public IMongoCollection<Carro>? Carros { get { return this.Database.GetCollection<Carro>("Carro"); } }
        
        public IMongoCollection<Avaliacao>? Avaliacoes { get { return this.Database.GetCollection<Avaliacao>("Avaliacao"); } }
    }
}
