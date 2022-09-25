

using Newtonsoft.Json;
using System.Net.Http.Json;

var rr = getCountries("un", 100090).Result;

string fileName = System.Environment.GetEnvironmentVariable("OUTPUT_PATH");

TextWriter tw = new StreamWriter(@fileName, true);
int res;
string _s;
_s = Console.ReadLine();

int _p;
_p = Convert.ToInt32(Console.ReadLine());

res = getCountries(_s, _p).Result;
tw.WriteLine(res);

tw.Flush();
tw.Close();






static async Task<int> getCountries(string s, int p)
{

    HttpClient cliente = new HttpClient();
    var result = await cliente.GetAsync($"https://jsonmock.hackerrank.com/api/countries/search?name={s}");

    Rootobject paises = JsonConvert.DeserializeObject<Rootobject>(result.Content.ReadAsStringAsync().Result);
    return paises.data.Where(d => d.population >= p).Count();    //int pp = cliente.GetAsync(https://jsonmock.hackerrank.com/api/countries/search?name=s);


    //https://jsonmock.hackerrank.com/api/countries/search?name=

}




public class Rootobject
{
    public int page { get; set; }
    public int per_page { get; set; }
    public int total { get; set; }
    public int total_pages { get; set; }
    public Datum[]? data { get; set; }
}

public class Datum
{
    public string? name { get; set; }
    public string nativeName { get; set; }
    public string[]? topLevelDomain { get; set; }
    public string? alpha2Code { get; set; }
    public string? numericCode { get; set; }
    public string? alpha3Code { get; set; }
    public string[]? currencies { get; set; }
    public string[]? callingCodes { get; set; }
    public string? capital { get; set; }
    public string[]? altSpellings { get; set; }
    public string? relevance { get; set; }
    public string?    region { get; set; }
    public string? subregion { get; set; }
    public string[]? language { get; set; }
    public string[]? languages { get; set; }
    public Translations? translations { get; set; }
    public int? population { get; set; }
    public float[]?     latlng { get; set; }
    public string?    demonym { get; set; }
    public string[]? borders { get; set; }
    public int? area { get; set; }
    public float? gini { get; set; }
    public string[]? timezones { get; set; }
}

public class Translations
{
    public string de { get; set; }
    public string es { get; set; }
    public string fr { get; set; }
    public string it { get; set; }
    public string ja { get; set; }
    public string nl { get; set; }
    public string hr { get; set; }
}