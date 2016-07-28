// Copyright SJG 26Jul2016
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PokeRef_v1
{
    /// <summary>
    /// Represents a pokemon. Uses HTTP protocols to retrieve information
    /// relative to the pokemon, from the REST service created by ...
    /// </summary>
    public class Pokemon
    {        
        private const string URL = "http://pokeapi.co/api/v2";  // The REST service base address

        // Poke-properties
        public string name { get; private set; }                // The name of the pokemon
        public int id { get; private set; }                     // The numeric identifier of this pokemon in the database
        public int height { get; private set; }                 // The height of the pokemon
        public List<string> types { get; private set; }         // The types of damage this pokemon's attacks cause
        public List<dynamic> sprites { get; private set; }      // Visual representation files for this pokemon
        public List<string> evoChain { get; private set; }      // The forms this pokemon may evolve into, or evolve from
        
        /// <summary>
        /// Creates a new empty pokemon object.
        /// </summary>
        public Pokemon(string name)
        {
            this.name = name;
            id = Int32.MinValue;
            height = Int32.MinValue;
            types = new List<string>();
            sprites = new List<dynamic>();
            evoChain = new List<string>();
        }

        /// <summary>
        /// Creates a HTTPClient used to connect to the pokeapi REST service.
        /// </summary>
        public HttpClient CreateClient()
        {
            // Create a client with the base address of the Boggle server
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            return client;
        }

        /// <summary>
        /// Connects to the pokeapi REST service and retrieves the following criteria: 
        /// ID, height, types, and sprites. Fills in the corresponding properties of this Pokemon.
        /// </summary>
        public void FillPokeStats()
        {
            // Check that name is not an empty string
            if (name.Equals(""))
            {
                throw new ApplicationException("You did not enter a pokemon, please try again.");
            }

            // Ensure token is all lower case prior to sending GET request
            name = name.ToLower();

            // Open connection
            using (HttpClient client = CreateClient())
            {
                // Send GET request with name                
                HttpResponseMessage response = client.GetAsync(URL + "/pokemon/" + name).Result;

                // Store result if successful and call parsing sub-methods
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic pokeStats = JsonConvert.DeserializeObject(result);

                    // Find appropriate properties of deserialized JSON object
                    fillID(pokeStats);
                    fillHeight(pokeStats);
                    fillTypes(pokeStats);
                }

                // Otherwise display failed request message
                else
                {
                    throw new ApplicationException("Retrieving poke-stats failed.");
                }
            }            
        }

        /// <summary>
        /// Connects to the pokeapi REST service and retrieves the evolution chain
        /// of this Pokemon. Fills in the evoChain property.
        /// </summary>
        public void FillEvoStats()
        {
            // Open connection
            using (HttpClient client = CreateClient())
            {
                // Send GET request to pokemon-species path               
                HttpResponseMessage response = client.GetAsync(URL + "/pokemon-species/" + name).Result;

                // Store result if successful
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic speciesStats = JsonConvert.DeserializeObject(result);

                    // Extract the evolution-chain ID and use for next request
                    string evoline = speciesStats.evolution_chain.url;
                    Regex reg = new Regex("\\/(\\d+)");
                    Match regMatch = reg.Match(evoline);
                    int evoID;
                    Int32.TryParse(regMatch.ToString().Substring(1), out evoID);

                    // Send GET request to evolution-chain path
                    response = client.GetAsync(URL + "/evolution-chain/" + evoID.ToString()).Result;

                    // Store result if successful
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                        dynamic evoStats = JsonConvert.DeserializeObject(result);

                        // Retrieve chain level from return object
                        dynamic level = evoStats.chain;
                        // Loop through each possible independent form and add to list
                        foreach (dynamic entry in level)
                        {
                            this.evoChain.Add(level.species.name.ToString());

                            //Try to retrieve subforms until there aren't any more (assume no more than five per chain)
                            bool moreForms = true;
                            while (moreForms)
                            {
                                try
                                {
                                    level = level.evolves_to;
                                    evoChain.Add(level.species.name.ToString());
                                }
                                catch
                                {
                                    moreForms = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Could not extract evolution chain.");
                    }

                }
                else
                {
                    throw new ApplicationException("Could not extract evolution ID from pokemon-species request.");
                }
            }
        }

        /// <summary>
        /// Parses the deserialized JSON object for an id, and assigns the corresponding property.
        /// </summary>
        private void fillID(dynamic pokeStats)
        {
            this.id = pokeStats.id;
        }

        /// <summary>
        /// Parses the deserialized JSON object for a height, and assigns the corresponding property.
        /// </summary>
        private void fillHeight(dynamic pokeStats)
        {
            this.height = pokeStats.height;
        }

        /// <summary>
        /// Parses the deserialized JSON object for types, and adds them to the type list of this pokemon.
        /// </summary>
        private void fillTypes(dynamic pokeStats)
        {
            // Extract the types dataset from the pokemon dataset
            dynamic types = pokeStats.types;
            // Loop through the types dataset, and append the name of each type to this type list
            foreach (dynamic entry in types)
            {
                this.types.Add(entry.type.name.ToString());
            }
        }

        /// <summary>
        /// Extracts PNG files themselves? Or just link to download?
        /// </summary>
        private void fillSprites(dynamic pokeStats)
        {
            //TODO: implement
        }

        /// <summary>
        /// Clears out this Pokemon's properties.
        /// </summary>
        public void clearPokemon()
        {
            name = null;
            id = Int32.MinValue;
            height = Int32.MinValue;
            types = null;
            sprites = null;
            evoChain = null;
        }
    }
}
