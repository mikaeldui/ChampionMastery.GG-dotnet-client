# ChampionMastery.GG .NET Client (unofficial)
An **unofficial** .NET client for [ChampionMastery.GG](https://github.com/Derpthemeus/ChampionMastery.GG). 

Lets you get mastery highscores from [ChampionMastery.GG](https://championmastery.gg).

## Installation
You can install it using the following **package manager** command:

    Install-Package MikaelDui.ChampionMasteryGg.Client

Or use the **.NET CLI** and add a reference with a floating version:

    dotnet add package MikaelDui.ChampionMasteryGg.Client --version *

Or add it as a **PackageReference**:

    <PackageReference Include="MikaelDui.ChampionMasteryGg.Client" Version="*" />
  
## Example
    
Get the highscores for a Annie:

    using ChampionMasteryGgClient client = new())

    var highscores = await client.GetHighscoresAsync(1); // The ID of Annie is 1.
    for (int i = 1; i <= highscores.Length; i++)
    {
        var highscore = highscores[i];
        console.WriteLine("#{i}: ${highscore.Summoner}: {highscore.Points}.");

        // Prints a list like "#1: Annie Bot (NA): 10132446"...
    }
