using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LabMPPREST
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            Console.WriteLine("Search artist with ID 1");
            Artist artist1 = await FindOne("http://localhost:8080/artists/1");
            Console.WriteLine(artist1.ToString());
            Console.WriteLine();

            Console.WriteLine("Display all artists");
            foreach (Artist artist in await FindAll("http://localhost:8080/artists/"))
            {
                Console.WriteLine(artist.ToString());
            }
            Console.WriteLine();

            Console.WriteLine("Add an artist");
            Artist artist2 = new Artist();
            artist2.id = 100; artist2.name = "Klaus Meine"; artist2.genre = "Rock";
            Artist artist3 = await Save("http://localhost:8080/artists/", artist2);
            Console.WriteLine(artist3.ToString());
            Console.WriteLine();

            Console.WriteLine("Update an artist");
            Artist artist4 = new Artist();
            artist4.id = 31; artist4.name = "Klaus Meine"; artist4.genre = "Metal";
            Artist artist5 = await Update("http://localhost:8080/artists/", artist4);
            Console.WriteLine("Artist updated ");
            Console.WriteLine();

            Console.WriteLine("Delete one entry");
            await client.DeleteAsync("http://localhost:8080/artists/32");
            Console.WriteLine("Deleted!");
            Console.WriteLine();

            Console.WriteLine("Display all artists");
            foreach (Artist artist in await FindAll("http://localhost:8080/artists/"))
            {
                Console.WriteLine(artist.ToString());
            }

            Console.Read();
        }

        static async Task<Artist> FindOne(string path)
        {
            Artist artist = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                artist = await response.Content.ReadAsAsync<Artist>();
            }
            return artist;
        }

        static async Task<Artist[]> FindAll(string path)
        {
            Artist[] probe = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                probe = await response.Content.ReadAsAsync<Artist[]>();
            }
            return probe;
        }

        static async Task<Artist> Save(string path, Artist artist)
        {
            Artist result = null;
            HttpResponseMessage response = await client.PostAsJsonAsync<Artist>(path, artist);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Artist>();
            }
            return result;
        }

        static async Task<Artist> Update(string path, Artist artist)
        {
            Artist result = null;
            HttpResponseMessage response = await client.PutAsJsonAsync<Artist>(path, artist);
            if (response.IsSuccessStatusCode)
            {
                result = await FindOne("http://localhost:8080/probe/31");
            }
            return result;
        }
    }
}
