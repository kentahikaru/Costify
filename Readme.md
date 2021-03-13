~/.dotnet/tools/dotnet-ef -v migrations add InitialCreate -s ../../Presentation/Costify -p . 
~/.dotnet/tools/dotnet-ef database update -s ../../Presentation/Costify -p . -v

https://stackoverflow.com/questions/43501023/entity-framework-core-migration-no-parameterless-constructor-defined-for-thi
