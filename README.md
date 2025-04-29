## **RabbitMQ Demo com .NET**

Bem-vindo(a) ao RabbitMQDemo, um projeto didático em .NET que demonstra o uso do RabbitMQ para comunicação assíncrona entre aplicações. O projeto inclui um Producer que envia mensagens e dois Receivers que consomem essas mensagens, utilizando um direct exchange para roteamento.
Este repositório foi criado como parte do meu estudo para me aprofundar em sistemas distribuídos e boas práticas em C#. O objetivo é fornecer um exemplo simples, mas funcional, para desenvolvedores que estão começando a explorar o RabbitMQ.

### 📋 **Visão Geral**

**Producer**: Envia 60 mensagens para a fila message_queue via exchange message_exchange.

**Receiver1**: Consome mensagens da fila message_queue.

**Receiver2**: Consome mensagens da mesma fila, demonstrando balanceamento de carga entre consumidores.

**Tecnologias**: .NET 8.0, RabbitMQ.Client, Microsoft.Extensions.Configuration.

### 🚀 **Pré-requisitos**

.NET SDK 8.0 ou superior.
**RabbitMQ instalado localmente ou via Docker**: docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

Acesse o painel de gerenciamento em **http://localhost:15672** (usuário: guest, senha: guest).

### 🛠️ **Estrutura do Projeto**

**ProducerConsole**: Projeto console que envia mensagens.

**Receiver1Console**: Projeto console que consome mensagens.

**Receiver2Console**: Outro consumidor, mostrando balanceamento de carga.

**Configuração**: Uso de appsettings.json para centralizar as configurações do RabbitMQ.

### 📦 **Como Configurar e Executar**

**Clone o repositório**: git clone https://github.com/gusbezerra/RabbitMQDemo.git
cd RabbitMQDemo

Instale as dependências:Execute os seguintes comandos em cada projeto (ProducerConsole, Receiver1Console, Receiver2Console):

cd ProducerConsole && dotnet add package RabbitMQ.Client && dotnet add package Microsoft.Extensions.Configuration && dotnet add package Microsoft.Extensions.Configuration.Json && dotnet add package Microsoft.Extensions.Configuration.Binder

cd ../Receiver1Console && dotnet add package RabbitMQ.Client && dotnet add package Microsoft.Extensions.Configuration && dotnet add package Microsoft.Extensions.Configuration.Json && dotnet add package Microsoft.Extensions.Configuration.Binder

cd ../Receiver2Console && dotnet add package RabbitMQ.Client && dotnet add package Microsoft.Extensions.Configuration && dotnet add package Microsoft.Extensions.Configuration.Json && dotnet add package Microsoft.Extensions.Configuration.Binder

Inicie o RabbitMQ:

Certifique-se de que o RabbitMQ está rodando (veja o comando Docker acima).

Execute os projetos:

Abra três terminais:

**Terminal 1**: cd Receiver1 && dotnet run

**Terminal 2**: cd Receiver2 && dotnet run

**Terminal 3**: cd Producer && dotnet run

Observe os resultados:
O Producer enviará mensagens para a fila.
Receiver1 e Receiver2 consumirão as mensagens, alternando entre si (balanceamento de carga).

### ✅ **Boas Práticas Aplicadas**

**Configuração Centralizada**: Uso de appsettings.json para configurações do RabbitMQ.

**Direct Exchange**: Utilização do tipo de exchange mais comum para roteamento de mensagens.

**Confirmação Manual**: Uso de BasicAck para garantir que mensagens sejam processadas corretamente.

**Balanceamento de Carga**: Dois consumidores processam mensagens da mesma fila.

**Qualidade de Serviço (QoS)**: Configuração de prefetchCount para limitar o número de mensagens por consumidor.

**Gerenciamento de Arquivos**: Configuração no .csproj para copiar o appsettings.json para o diretório de saída.

### 🐞 **Resolução de Problemas Comuns**

Erro FileNotFoundException para appsettings.json: Certifique-se de que o arquivo está na raiz do projeto e que o .csproj contém:<ItemGroup>
  <None Update="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>

Erro SetBasePath não encontrado: Verifique se os pacotes Microsoft.Extensions.Configuration e Microsoft.Extensions.Configuration.Json estão instalados.

### 📈 **Possíveis Melhorias**

Adicionar outros tipos de exchanges (fanout, topic) para cenários mais complexos.
Implementar retry policies para mensagens que falham no processamento.
Integrar com uma API REST para simular um cenário real de microserviços.

### 🤝 **Contribuições**
Sinta-se à vontade para abrir issues ou enviar pull requests com melhorias! 🚀
### 📧 **Contato**
Se tiver dúvidas ou sugestões, entre em contato comigo pelo LinkedIn: [Acessar perfil](https://www.linkedin.com/in/dev-gustavo-bezerra/).
