// Copyright SJG 26Jul2016
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        public string[] types { get; private set; }             // The types of damage this pokemon's attacks cause
        public string[] moves { get; private set; }             // The attacks this pokemon can perform
        public List<dynamic> sprites { get; private set; }      // Visual representation files for this pokemon
        public string[] evoChain { get; private set; }          // The forms this pokemon may evolve into, or evolve from
        
        /// <summary>
        /// Creates a new empty pokemon object.
        /// </summary>
        public Pokemon(string name)
        {
            this.name = name;
            id = Int32.MinValue;
            height = Int32.MinValue;
            types = null;
            moves = null;
            sprites = null;
            evoChain = null;
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
        /// ID, height, species, characteristic, types, moves, and sprites. Fills in 
        /// the properties of this Pokemon.
        /// </summary>
        public void FillPokeStats()
        {
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
                    fillType(pokeStats);

                }
                // Otherwise display failed request message
                else
                {
                    // throw exception to controller?
                }
            }            
        }

        /// <summary>
        /// Connects to the pokeapi REST service and retrieves the evolution chain
        /// of this Pokemon. Fills in the evoChain property.
        /// </summary>
        public void FillEvoStats()
        {
            
        }

        //private void fillMoves(dynamic pokeStats)
        //{
        //    // Open connection
        //    using (HttpClient client = CreateClient())
        //    {
        //        // Send GET request with name                
        //        HttpResponseMessage response = client.GetAsync(URL + "/move/" + id.ToString()).Result;

        //        // Store result if successful and call parsing sub-methods
        //        if (response.IsSuccessStatusCode)
        //        {
        //            String result = response.Content.ReadAsStringAsync().Result;
        //            dynamic pokeStats = JsonConvert.DeserializeObject(result);

        //            // Find appropriate property of deserialized JSON object
        //            this.characteristic = pokeStats.descriptions.description;
        //        }
        //        // Otherwise display failed request message
        //        else
        //        {
        //            // throw exception to controller?
        //        }
        //    }
        //}

        private void fillSprites(dynamic pokeStats)
        {

        }

        /// <summary>
        /// Clears out this Pokemon's properties.
        /// </summary>
        public void clearPokemon()
        {
            name = null;
            id = Int32.MinValue;
            height = Int32.MinValue;
            species = null;
            characteristic = null;
            types = null;
            moves = null;
            sprites = null;
            evoChain = null;
        }
    }
}
