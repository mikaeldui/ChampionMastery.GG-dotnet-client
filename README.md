# ChampionMastery.GG .NET Client
A .NET client for ChampionMastery.GG. Created for use by [ChampionMastery.GG-uwp](https://github.com/mikaeldui/ChampionMastery.GG-uwp) and [ChampionMastery.GG-winui](https://github.com/mikaeldui/ChampionMastery.GG-winui).

You can install it using the following package manager command:

    Install-Package MikaelDui.ChampionMasteryGg.Client
    
Example usage:

    using (var client = new ChampionMasteryClient())
    {
        var mastery = await client.GetChampionMasteryAsync("19MILITANT93", "EUW");
        foreach (var champion in mastery)
            console.WriteLine(champion.Name);
    }
