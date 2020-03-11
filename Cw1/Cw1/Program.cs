using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ArgumentNullException();
            }

            if (!Uri.IsWellFormedUriString(args[0], UriKind.Absolute))
            {
                throw new ArgumentException();
            }

            var httpClient = new HttpClient();

            try
            {
                HttpResponseMessage res = await httpClient.GetAsync(args[0]);
                httpClient.Dispose();

                if (res.IsSuccessStatusCode)
                {
                    string html = await res.Content.ReadAsStringAsync();
                    var emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                    var matches = emailRegex.Matches(html).OfType<Match>().Select(val => val.Value).Distinct();
                    if (matches.Any())
                    {
                        foreach (var m in matches)
                        {
                            Console.WriteLine(m);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Nie znaleziono adresów email");
                    }

                }
                else
                {
                    Console.WriteLine("Kod odpowiedzi nie pozytywny " + res.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }

            Console.WriteLine("Koniec!");
        }
    }
}