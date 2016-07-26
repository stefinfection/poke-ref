// Copyright SJG 26Jul2016
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
    class Pokemon
    {        
        private const string URL = "http://pokeapi.co/api/v2";  // The REST service base address

        // Poke-properties
        public string name { get; private set; }                // The name of the pokemon
        public int id { get; private set; }                     // The numeric identifier of this pokemon in the database
        public int height { get; private set; }                 // The height of the pokemon
        public string species { get; private set; }             // The species to which this pokemon belongs
        public string characteristic { get; private set; }      // An outstanding feature of this pokemon
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
            species = null;
            characteristic = null;
            types = null;
            moves = null;
            sprites = null;
            evoChain = null;
        }

        // Creates a connection to the REST service
        public HttpClient CreateClient()
        {
            // Create a client with the base address of the Boggle server
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            return client;
        }

        // Get pokemon

        // Get evolution

    }
}
