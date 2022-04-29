# DESAFIO_SPARTA_LABS

ESTA API FOI UM DESAFIO PROPOSTO PELA SPARTA LABS NA SEGUNDA ETAPA DO PROCESSO SELETIVO PARA UMA VAGA DE DESENVOLVEDOR BACK-END EM .NET .


# DETALHES DA API

API FEITA PARA GERENCIAMENTO DE OFICINAS.

PADRÃO ARQUITETURAL USADO: MVC + TDD

TODA A API FOI ARQUITETADA EM CIMA DE TRÊS ENTIDADES:
-OFICINA : É UMA ENTIDADE UTILIZADA TANTO PARA REALIZAÇÃO DE LOGIN + AUTENTICAÇÕES, QUANTO PARA FAZER REFERÊNCIA AO CLIENTE.
           NELA POSSUEM 5 PROPRIEDADES (ID, NOME, CNPJ, PASSWORD E UNIDADE DE TEMPO DIARIA(QUE É A CARGA HORÁRIA DIÁRIA MAXIMA DA OFICINA))
           
-SERVICO : É A ENTIDADE QUE POSSUI AS INFORMAÇÕES SOBRE OS SERVICOS A SEREM REALIZADOS PELA OFICINA.
           NELA POSSUEM 4 PROPRIEDADES (ID, NOME, UNIDADE TRABALHO REQUERIDA(QUE É O TEMPO NECESSÁRIO PARA EXECUTAR O SERVICO) E ID_OFICINA(FOI IMPLEMENTADA PARA CADA OFICINA TER SEU ROL DE SERVIÇOS PRÓPRIOS)

-OFICINASERVICO: É A ENTIDADE RESPONSÁVEL POR VALIDAR TODAS AS REGRAS DE NEGÓCIO. 
                 É A ENTIDADE QUE ASSOCIA O SERVICO, A OFICINA E A DATA DE REALIZAÇÃO DO SERVICO.
                 NELA POSSUEM 4 PROPRIEDADES(ID, OFICINAID, SERVICOID, DATASERVICO)
                 
# DETALHES SOBRE AS TECNOLOGIAS/FERRAMENTAS USADAS

LINGUAGEM:        C#
FRAMEWORK:        .NET CORE 6
ORM:              ENTITY FRAMEWORK
AUTENTICAÇÃO:     JWT SECURITY TOKEN
BANCO DE DADOS:   SQL SERVER
DOCUMENTAÇÃO:     SWAGGER
TESTES UNITÁRIOS: XUNIT
IDE:              VISUAL STUDIO
