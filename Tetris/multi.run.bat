cd A:\Projects\Tetris\Tetris 
dotnet build Tetris.Desktop
start "" dotnet run --project Tetris.Game.Tests
start "" dotnet run --project Tetris.Game.Tests
cd A:\Projects\TetrisServer\
dotnet build RPI Test 
start "" dotnet run --project Server


