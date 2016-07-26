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
    public class Pokemon
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

            // Query for pokemon w/ name

            // If status code successful, store result and pass to individual parsing methods
            //String result = response.Content.ReadAsStringAsync().Result;

            // Else display error message
        }

        /// <summary>
        /// Connects to the pokeapi REST service and retrieves the evolution chain
        /// of this Pokemon. Fills in the evoChain property.
        /// </summary>
        public void FillEvoStats()
        {

        }

        private void fillID(string result)
        {
            // parse string result using json dll
        }

        private void fillHeight(string result)
        {

        }

        private void fillSpecies(string result)
        {

        }

        private void fillCharacteristic(string result)
        {

        }

        private void fillTypes(string result)
        {

        }

        private void fillMoves(string result)
        {

        }

        private void fillSprites(string result)
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
