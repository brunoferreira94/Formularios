# Formularios
Um criador de formulários web feito em C# utilizando a arquitetura MVC e Razor pages

**Como começar?**
- Clone o projeto;
- No SQL Server Management Studio utilize o comando `USE master; CREATE DATABASE [Formularios];` para criar o banco de dados;
- Restaure o banco de dados "formularios_um_usuario.bak" que está na raiz do projeto;
- Utilize os comandos abaixo para criar o usuário e dar as suas devidas permissões:
```
USE Formularios
CREATE LOGIN formularios WITH PASSWORD = '123'
CREATE USER formularios FOR LOGIN formularios
GRANT SELECT, DELETE, UPDATE, INSERT ON Formulario to formularios
GRANT SELECT, DELETE, UPDATE, INSERT ON Pergunta to formularios
GRANT SELECT, DELETE, UPDATE, INSERT ON Resposta to formularios
GRANT SELECT, DELETE, UPDATE, INSERT ON Usuario to formularios
```
- Agora é só abrir o projeto no Visual Studio 2019 e rodar;
