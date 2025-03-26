using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("makepetname")]
public class MyControllerForPets : ControllerBase
{
    private static Dictionary<string, List<string>> NamesOfPets = new Dictionary<string, List<string>>()
    {
        { "dog", new List<string>() { "God of Dogs", "Great Wolf Dogie", "Maxie", "Rover", "Pupp", "Rawr", "Courage", "Awoooooo" } },
        { "cat", new List<string>() { "Galaxus", "King Kitty", "Paws", "Fuzzy", "Mr. Fluff", "Mew", "Princess Kitty Car", "Beluga", "Ouu Ie Ie Ah Ie" } },
        { "bird", new List<string>() { "Thender Juicy", "Death Bird", "King Bald Eagle", "Shit eater", "Flappy", "Flapie", "Blacky", "Chirpy" } }
    };

    [HttpPost]
    public IActionResult DoPetName([FromBody] PetName data)
    {
        if (data == null || data.AnimalKind == "")
        {
            return BadRequest(new { error = "ERROR!, you forgot to say what kind of pet!" });
        }

        string lowered = data.AnimalKind.ToLower();
        if (!NamesOfPets.ContainsKey(lowered))
        {
            return BadRequest(new { error = "ERROR!, only dog/cat/bird in the Animal Kind String: <----" });
        }

        if (data.TwoPart.HasValue && !(data.TwoPart is bool))
        {
            return BadRequest(new { error = "'TwoPart' needs to be true or false..." });
        }

        List<string> chosenList = NamesOfPets[lowered];
        Random rand = new Random();
        string finalName;

        if (data.TwoPart == true)
        {
            finalName = chosenList[rand.Next(chosenList.Count)] + " " + chosenList[rand.Next(chosenList.Count)];
        }
        else
        {
            finalName = chosenList[rand.Next(chosenList.Count)];
        }

        return Ok(new { petname = finalName });
    }
}

public class PetName
{
    public string AnimalKind { get; set; }
    public bool? TwoPart { get; set; }
}


// GET https://localhost:7107/makepetname

// POST
// {
// "AnimalKind": "cat",
//   "TwoPart": true
// }
